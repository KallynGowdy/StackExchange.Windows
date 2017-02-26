using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;

namespace StackExchange.Windows.Application
{
    /// <summary>
    /// Defines a view model that represents the current authentication state for the application.
    /// </summary>
    public class AuthenticationViewModel : ReactiveObject
    {
        private string token;
        public ReactiveCommand<string, Unit> RegisterAccessToken { get; }

        public string Token
        {
            get { return token; }
            set { this.RaiseAndSetIfChanged(ref token, value); }
        }

        public AuthenticationViewModel(ApplicationViewModel application)
        {
            RegisterAccessToken = ReactiveCommand.Create<string>(async token =>
            {
                Token = token;
                await application.Navigate.Handle(typeof(MainPage));
            });
        }

    }
}