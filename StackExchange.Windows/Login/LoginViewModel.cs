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

namespace StackExchange.Windows.Login
{
    /// <summary>
    /// Defines a view model that represents the logic for the login page.
    /// </summary>
    public class LoginViewModel : ReactiveObject
    {
        private ApplicationViewModel Application { get; }

        public readonly string OAuthUrl = "https://stackexchange.com/oauth/dialog?client_id=9093&scope=&redirect_uri=https://stackexchange.com/oauth/login_success";

        public ReactiveCommand<WebViewNavigationStartingEventArgs, string> NavigationStarted { get; }

        public LoginViewModel()
        {
            Application = Locator.Current.GetService<ApplicationViewModel>();

            NavigationStarted = ReactiveCommand.Create<WebViewNavigationStartingEventArgs, string>(args =>
                args.Uri.AbsolutePath == "/oauth/login_success" ?
                    PullAccessTokenFromUri(args) :
                    null);

            NavigationStarted.Where(token => token != null)
                .InvokeCommand(Application, vm => vm.Authentication.RegisterAccessToken);
        }

        private static string PullAccessTokenFromUri(WebViewNavigationStartingEventArgs args)
        {
            // Skip the '#' character at the beginning
            var fragment = args.Uri.Fragment.Substring(1);
            var parameters = fragment.Split('&');
            foreach (var p in parameters)
            {
                var keyValue = p.Split('=');
                var key = keyValue[0];
                var value = keyValue[1];

                if (key == "access_token")
                {
                    return value;
                }
            }
            return null;
        }
    }
}
