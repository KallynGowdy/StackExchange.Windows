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
using StackExchange.Windows.Api;
using StackExchange.Windows.Application;
using StackExchange.Windows.Authentication;
using StackExchange.Windows.Questions;

namespace StackExchange.Windows.Login
{
    /// <summary>
    /// Defines a view model that represents the logic for the login page.
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        public ReactiveCommand<Unit, Unit> GoToMainPage { get; }

        public LoginViewModel()
        {
            GoToMainPage = ReactiveCommand.CreateFromTask(async () =>
            {
                await Application.NavigateAndClearStack.Handle(new NavigationParams(typeof(QuestionsPage)));
            });

            Authentication.Login.InvokeCommand(this, vm => vm.GoToMainPage);
        }
    }
}
