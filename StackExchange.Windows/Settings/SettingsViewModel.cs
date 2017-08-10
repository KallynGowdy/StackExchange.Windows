using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
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
        private IEnumerable<IGrouping<string, SettingsItemViewModel>> groupedSettings;

        /// <summary>
        /// Gets the list of settings grouped by category.
        /// </summary>
        public IEnumerable<IGrouping<string, SettingsItemViewModel>> GroupedSettings
        {
            get => groupedSettings;
            private set => this.RaiseAndSetIfChanged(ref groupedSettings, value);
        }

        /// <summary>
        /// Gets the command that loads the settings from the store.
        /// </summary>
        public ReactiveCommand<Unit, Unit> LoadSettings { get; }

        public SettingsViewModel(ISettingsStore store = null, IApplicationViewModel application = null) : base(application)
        {
            settingsStore = store ?? Locator.Current.GetService<ISettingsStore>();

            LoadSettings = ReactiveCommand.CreateFromTask(LoadSettingsImpl);

            this.WhenActivated(d =>
            {
                LoadSettings.Execute().Subscribe().DisposeWith(d);
            });
        }

        private async Task LoadSettingsImpl()
        {
            var settings = await settingsStore.GetSettingsAsync();

            GroupedSettings = settings.Select(s => new SettingsItemViewModel(s))
                .GroupBy(s => s.GroupResource);
        }

        public ViewModelActivator Activator { get; } = new ViewModelActivator();
    }
}
