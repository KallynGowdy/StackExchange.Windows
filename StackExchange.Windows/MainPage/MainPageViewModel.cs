using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using StackExchange.Windows.Application;
using StackExchange.Windows.Questions;
using StackExchange.Windows.Settings;

namespace StackExchange.Windows
{
    public class MainPageViewModel : BaseViewModel
    {
        private bool navigationMenuOpen;

        /// <summary>
        /// Gets or sets whether the navigation menu is open.
        /// </summary>
        public bool NavigationMenuOpen
        {
            get => navigationMenuOpen;
            set => this.RaiseAndSetIfChanged(ref navigationMenuOpen, value);
        }

        /// <summary>
        /// Gets a command that toggles the state of the navigation menu.
        /// </summary>
        public ReactiveCommand<Unit, Unit> ToggleNavigationMenu { get; }

        /// <summary>
        /// Gets a command that navigates to the home page.
        /// </summary>
        public ReactiveCommand<Unit, Unit> NavigateHome { get; }

        /// <summary>
        /// Gets a command that navigates to the settings page.
        /// </summary>
        public ReactiveCommand<Unit, Unit> NavigateToSettings { get; }

        public MainPageViewModel(IApplicationViewModel application = null) : base(application)
        {
            ToggleNavigationMenu = ReactiveCommand.Create(ToggleNavigationMenuImpl);
            NavigateHome = ReactiveCommand.CreateFromTask(NavigateHomeImpl);
            NavigateToSettings = ReactiveCommand.CreateFromTask(NavigateToSettingsImpl);
        }

        private async Task NavigateToSettingsImpl()
        {
            await Application.Navigate.Handle(new NavigationParams(typeof(SettingsPage)));
        }

        private async Task NavigateHomeImpl()
        {
            await Application.NavigateAndClearStack.Handle(new NavigationParams(typeof(QuestionsPage)));
        }

        private void ToggleNavigationMenuImpl()
        {
            NavigationMenuOpen = !NavigationMenuOpen;
        }
    }
}
