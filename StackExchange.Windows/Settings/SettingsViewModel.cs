using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using StackExchange.Windows.Application;
using StackExchange.Windows.Services;
using StackExchange.Windows.Services.Settings;

namespace StackExchange.Windows.Settings
{
    /// <summary>
    /// Defines a view model that manages settings for the application.
    /// </summary>
    public class SettingsViewModel : BaseViewModel, ISupportsActivation
    {
        private readonly ISettingsStore settingsStore;
        private readonly ISettingsItemViewModelFactory factory;
        private readonly IResourceStore resources;
        private readonly ObservableAsPropertyHelper<SettingsItemViewModel[]> loadedSettings;
        private readonly ObservableAsPropertyHelper<SettingsItemViewModel[]> filteredSettings;
        private readonly ObservableAsPropertyHelper<IGrouping<string, SettingsItemViewModel>[]> groupedSettings;
        private string savingNotice;
        private string searchTerm = "";

        /// <summary>
        /// Gets or sets the search term that should be used to filter settings.
        /// </summary>
        public string SearchTerm
        {
            get => searchTerm;
            set => this.RaiseAndSetIfChanged(ref searchTerm, value);
        }

        /// <summary>
        /// Gets the list of settings that have been loaded into this view model.
        /// </summary>
        public SettingsItemViewModel[] LoadedSettings => loadedSettings.Value;

        public SettingsItemViewModel[] FilteredSettings => filteredSettings.Value;

        /// <summary>
        /// Gets the list of settings grouped by category.
        /// </summary>
        public IGrouping<string, SettingsItemViewModel>[] GroupedSettings => groupedSettings.Value;

        /// <summary>
        /// Gets the command that loads the settings from the store.
        /// </summary>
        public ReactiveCommand<Unit, SettingsItemViewModel[]> LoadSettings { get; }

        /// <summary>
        /// Saves the given list of settings.
        /// </summary>
        public ReactiveCommand<IEnumerable<SettingsItemViewModel>, Unit> SaveSettings { get; }

        /// <summary>
        /// Filters the given list of settings.
        /// </summary>
        public ReactiveCommand<FilterOptions, SettingsItemViewModel[]> FilterSettings { get; }

        public ViewModelActivator Activator { get; } = new ViewModelActivator();

        public SettingsViewModel(ISettingsItemViewModelFactory factory = null, ISettingsStore store = null, IResourceStore resources = null, IApplicationViewModel application = null) : base(application)
        {
            this.factory = factory ?? Locator.Current.GetService<ISettingsItemViewModelFactory>();
            settingsStore = store ?? Locator.Current.GetService<ISettingsStore>();
            this.resources = resources ?? Locator.Current.GetService<IResourceStore>();
            LoadSettings = ReactiveCommand.CreateFromTask(LoadSettingsImpl, outputScheduler: RxApp.MainThreadScheduler);
            SaveSettings = ReactiveCommand.CreateFromTask<IEnumerable<SettingsItemViewModel>, Unit>(SaveSettingsImpl, outputScheduler: RxApp.MainThreadScheduler);
            FilterSettings = ReactiveCommand.CreateFromTask<FilterOptions, SettingsItemViewModel[]>(FilterSettingsImpl, outputScheduler: RxApp.MainThreadScheduler);

            loadedSettings = LoadSettings.ToProperty(this, vm => vm.LoadedSettings);
            filteredSettings = FilterSettings.ToProperty(this, vm => vm.FilteredSettings);
            groupedSettings = FilterSettings.Select(settings => settings.GroupBy(s => s.GroupResource).ToArray())
                .ToProperty(this, vm => vm.GroupedSettings);

            this.WhenActivated(d =>
            {
                this.LoadSettings
                    .Select(ChangedSettings)
                    .Switch()
                    .Buffer(TimeSpan.FromSeconds(0.3), RxApp.TaskpoolScheduler)
                    .OfType<IEnumerable<SettingsItemViewModel>>()
                    .InvokeCommand(this, vm => vm.SaveSettings)
                    .DisposeWith(d);

                this.WhenAnyValue(vm => vm.LoadedSettings, vm => vm.SearchTerm, (settings, search) => new FilterOptions(settings, search))
                    .Throttle(TimeSpan.FromSeconds(0.4), RxApp.TaskpoolScheduler)
                    .InvokeCommand(this, vm => vm.FilterSettings)
                    .DisposeWith(d);

                this.LoadSettings.Execute().Subscribe().DisposeWith(d);
            });
        }

        private Task<SettingsItemViewModel[]> FilterSettingsImpl(FilterOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            return Task.Run(() =>
            {
                if (string.IsNullOrEmpty(options.SearchTerm))
                {
                    return options.Settings;
                }
                else
                {
                    return options.Settings
                        .Select(setting => (Setting: setting, Score: ScoreSetting(options.SearchTerm, setting)))
                        .OrderByDescending(pair => pair.Score)
                        .Where(pair => pair.Score > 0.8)
                        .Select(pair => pair.Setting)
                        .ToArray();
                }
            });
        }

        private async Task<Unit> SaveSettingsImpl(IEnumerable<SettingsItemViewModel> settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            foreach (var setting in settings)
            {
                await settingsStore.SaveSettingAsync(setting.Setting);
            }

            return Unit.Default;
        }

        private async Task<SettingsItemViewModel[]> LoadSettingsImpl()
        {
            var settings = await settingsStore.GetSettingsAsync();

            return settings.Select(s => factory.CreateViewModel(s)).ToArray();
        }

        private static IObservable<SettingsItemViewModel> ChangedSettings(SettingsItemViewModel[] settings)
        {
            return settings.Select(setting => setting.WhenAny(s => s.Value, ctx => ctx.Sender)).Merge();
        }

        private double ScoreSetting(string term, SettingsItemViewModel setting)
        {
            var termTokens = TokenizeTerm(term);
            var definition = setting.Setting.Definition;
            var name = resources.GetString(definition.NameResource);
            var description = resources.GetString(definition.DescriptionResource);
            var group = resources.GetString(definition.GroupResource);

            var nameScore = ScoreString(termTokens, name);
            var descriptionScore = ScoreString(termTokens, description);
            var groupScore = ScoreString(termTokens, group);

            return new[] { nameScore, descriptionScore, groupScore }.Max();
        }

        private double ScoreString(string[] termTokens, string str)
        {
            var strTokens = TokenizeTerm(str);

            var intersection = termTokens.Intersect(strTokens).ToArray();

            return (double)intersection.Length / (double)termTokens.Length;
        }

        private string[] TokenizeTerm(string term)
        {
            term = term.ToLowerInvariant();

            return Tokenize(term).ToArray();

            IEnumerable<string> Tokenize(string t)
            {
                // Add more word splits here
                string[] split = t.Split(new[] { ' ', '-', '.' }, StringSplitOptions.RemoveEmptyEntries);

                if (split.Length == 1)
                {
                    yield return split[0];
                }
                else
                {
                    foreach (var str in split)
                    {
                        foreach (var token in Tokenize(str))
                        {
                            yield return token;
                        }
                    }
                }
            }
        }

        public class FilterOptions
        {
            public SettingsItemViewModel[] Settings { get; }
            public string SearchTerm { get; }

            public FilterOptions(SettingsItemViewModel[] settings, string searchTerm)
            {
                Settings = settings;
                SearchTerm = searchTerm;
            }
        }
    }
}
