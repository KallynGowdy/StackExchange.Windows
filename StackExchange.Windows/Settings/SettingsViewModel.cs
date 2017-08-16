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
        private readonly ObservableAsPropertyHelper<SettingsItemViewModel[]> loadedSettings;
        private readonly ObservableAsPropertyHelper<IGrouping<string, SettingsItemViewModel>[]> groupedSettings;
        private string savingNotice;

        /// <summary>
        /// Gets the list of settings that have been loaded into this view model.
        /// </summary>
        public SettingsItemViewModel[] LoadedSettings => loadedSettings.Value;

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
        public ReactiveCommand<IList<SettingsItemViewModel>, Unit> SaveSettings { get; }

        public SettingsViewModel(ISettingsItemViewModelFactory factory = null, ISettingsStore store = null, IApplicationViewModel application = null) : base(application)
        {
            this.factory = factory ?? Locator.Current.GetService<ISettingsItemViewModelFactory>();
            settingsStore = store ?? Locator.Current.GetService<ISettingsStore>();
            LoadSettings = ReactiveCommand.CreateFromTask(LoadSettingsImpl, outputScheduler: RxApp.MainThreadScheduler);
            SaveSettings = ReactiveCommand.CreateFromTask<IList<SettingsItemViewModel>, Unit>(SaveSettingsImpl, outputScheduler: RxApp.MainThreadScheduler);

            loadedSettings = LoadSettings.ToProperty(this, vm => vm.LoadedSettings);
            groupedSettings = LoadSettings.Select(settings => settings.GroupBy(s => s.GroupResource).ToArray())
                .ToProperty(this, vm => vm.GroupedSettings);

            this.WhenActivated(d =>
            {
                LoadSettings
                    .Select(ChangedSettings)
                    .Switch()
                    .Buffer(TimeSpan.FromSeconds(0.3), RxApp.TaskpoolScheduler)
                    .InvokeCommand(this, vm => vm.SaveSettings)
                    .DisposeWith(d);

                LoadSettings.Execute().Subscribe().DisposeWith(d);
            });
        }

        private async Task<Unit> SaveSettingsImpl(IList<SettingsItemViewModel> settings)
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

        public ViewModelActivator Activator { get; } = new ViewModelActivator();
    }
}
