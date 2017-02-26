using System;
using System.Net.Http;
using System.Reactive;
using System.Reactive.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using StackExchange.Windows.Application;

namespace StackExchange.Windows.Authentication
{
    /// <summary>
    /// Defines a view model that represents the current authentication state for the application.
    /// </summary>
    public class AuthenticationViewModel : ReactiveObject
    {
        private readonly ApplicationViewModel application;
        private string token;

        /// <summary>
        /// The settings that are used for authentication.
        /// </summary>
        public AuthenticationSettings Settings { get; set; } = new AuthenticationSettings()
        {
            LoginUri = "https://stackexchange.com/oauth/dialog",
            ClientId = "9093",
            RedirectUri = "https://stackexchange.com/oauth/login_success",
            Scope = ""
        };

        /// <summary>
        /// Attempts to log the user in.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Login { get; }

        /// <summary>
        /// Gets the interaction that handles redirecting the user to the OAuth Login page.
        /// </summary>
        public Interaction<Uri, Uri> RedirectToLogin { get; } = new Interaction<Uri, Uri>();

        /// <summary>
        /// Gets the current access token.
        /// </summary>
        public string Token
        {
            get { return token; }
            private set { this.RaiseAndSetIfChanged(ref token, value); }
        }

        public AuthenticationViewModel(ApplicationViewModel application)
        {
            this.application = application;
            Login = ReactiveCommand.CreateFromTask(LoginImpl);
        }

        private async Task LoginImpl()
        {
            var state = GetRandomState();
            var url = BuildOAuth2Url(state);
            var tokenUrl = await RedirectToLogin.Handle(url);
            var response = PullAccessTokenFromUri(tokenUrl);
            if (string.Equals(response.State, state, StringComparison.Ordinal))
            {
                Token = response.AccessToken;
            }
            else
            {
                throw new InvalidOperationException("The returned state must match the given scope.");
            }
        }

        /// <summary>
        /// Creates the URL that the user should be directed to.
        /// </summary>
        /// <returns></returns>
        private Uri BuildOAuth2Url(string state)
        {
            var uri = new UriBuilder(Settings.LoginUri)
            {
                Query = string.Format(
                    "client_id={0}&redirect_uri={1}&scope={2}&state={3}",
                    Uri.EscapeDataString(Settings.ClientId),
                    Uri.EscapeDataString(Settings.RedirectUri),
                    Uri.EscapeDataString(Settings.Scope),
                    Uri.EscapeDataString(state)
                )
            };

            return uri.Uri;
        }

        private string GetRandomState()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var bytes = new byte[16];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }

        private static TokenResponse PullAccessTokenFromUri(Uri uri)
        {
            var response = new TokenResponse();
            // Skip the '#' character at the beginning
            var fragment = uri.Fragment.Substring(1);
            var parameters = fragment.Split('&');
            foreach (var p in parameters)
            {
                var keyValue = p.Split('=');
                var key = keyValue[0];
                var value = Uri.UnescapeDataString(keyValue[1]);

                if (key == "access_token")
                {
                    response.AccessToken = value;
                }
                else if (key == "expires")
                {
                    response.ExpiresIn = Convert.ToInt32(value);
                }
                else if (key == "state")
                {
                    response.State = value;
                }
            }
            return response;
        }

        public bool IsSuccessUrl(Uri arg) => Settings.RedirectUri.EndsWith(arg.AbsolutePath);

        private class TokenResponse
        {
            public string AccessToken { get; set; }
            public int ExpiresIn { get; set; }
            public string State { get; set; }
        }
    }
}