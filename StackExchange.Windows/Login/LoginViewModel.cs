using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using ReactiveUI;
using Splat;
using StackExchange.Windows.Application;
using StackExchange.Windows.Authentication;

namespace StackExchange.Windows.Login
{
    /// <summary>
    /// Defines a view model that represents the logic for the login page.
    /// </summary>
    public class LoginViewModel : ReactiveObject
    {
        private ApplicationViewModel Application { get; }
        public AuthenticationViewModel Authentication => Application.Authentication;

        public ReactiveCommand<Unit, Unit> GoToMainPage { get; }

        public LoginViewModel()
        {
            Application = Locator.Current.GetService<ApplicationViewModel>();
            GoToMainPage = ReactiveCommand.CreateFromTask(async () =>
            {
                await Application.NavigateAndClearStack.Handle(typeof(MainPage));
            });

            Authentication.Login.InvokeCommand(this, vm => vm.GoToMainPage);
        }
    }
}
