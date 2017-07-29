using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using ReactiveUI;
using StackExchange.Windows.Application;
using StackExchange.Windows.Authentication;
using Xunit;

namespace StackExchange.Windows.Tests.Authentication
{
    public class AuthenticationViewModelTests
    {
        private AuthenticationSettings Settings { get; } = new AuthenticationSettings()
        {
            ClientId = "id",
            LoginUri = "https://example.com/login",
            RedirectUri = "https://example.com/redirect",
            Scope = "test,other"
        };
        private AuthenticationViewModel Subject { get; }

        public AuthenticationViewModelTests()
        {
            Subject = new AuthenticationViewModel()
            {
                Settings = Settings
            };
        }

        [Fact]
        public async Task Test_Login_Triggers_Redirection_To_The_Base_URL()
        {
            using (Subject.RedirectToLogin.RegisterHandler(c =>
            {
                Assert.True(new Uri("https://example.com/login").IsBaseOf(c.Input));
                var state = c.Input.Query.Split('&').First(kv => kv.StartsWith("state")).Split('=').Last();
                c.SetOutput(new Uri($"https://example.com/redirect#access_token=test&expires=10&state={state}"));
            }))
            {
                await Subject.Login.Execute();

                Assert.Equal("test", Subject.Token);
            }
        }
    }
}
