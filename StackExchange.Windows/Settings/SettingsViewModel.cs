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

        public SettingsViewModel(ISettingsItemViewModelFactory factory = null, ISettingsStore store = null, IApplicationViewModel application = null) : base(application)
        {
            this.factory = factory ?? Locator.Current.GetService<ISettingsItemViewModelFactory>();
            settingsStore = store ?? Locator.Current.GetService<ISettingsStore>();
            LoadSettings = ReactiveCommand.CreateFromTask(LoadSettingsImpl, outputScheduler: RxApp.MainThreadScheduler);

            loadedSettings = LoadSettings.ToProperty(this, vm => vm.LoadedSettings);
            groupedSettings = LoadSettings.Select(settings => settings.GroupBy(s => s.GroupResource).ToArray())
                .ToProperty(this, vm => vm.GroupedSettings);

            this.WhenActivated(d =>
            {
                LoadSettings.Execute().Subscribe().DisposeWith(d);
            });
        }

        private async Task<SettingsItemViewModel[]> LoadSettingsImpl()
        {
            var settings = await settingsStore.GetSettingsAsync();

            return settings.Select(s => factory.CreateViewModel(s)).ToArray();
        }

        public ViewModelActivator Activator { get; } = new ViewModelActivator();
    }
}
