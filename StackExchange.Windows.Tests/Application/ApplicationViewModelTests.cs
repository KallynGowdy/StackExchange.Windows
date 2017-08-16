using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Splat;
using StackExchange.Windows.Api;
using StackExchange.Windows.Application;
using StackExchange.Windows.Authentication;
using StackExchange.Windows.Common.SearchBox;
using StackExchange.Windows.Services.Settings;
using Xunit;

namespace StackExchange.Windows.Tests.Application
{
    public class ApplicationViewModelTests
    {
        public ApplicationViewModel Subject { get; set; }
        public StubISettingsStore Settings { get; set; }

        public ApplicationViewModelTests()
        {
            Settings = new StubISettingsStore();
            Subject = new ApplicationViewModel(Settings);
        }

        [Fact]
        public void Test_Start_Registers_AuthenticationViewModel()
        {
            Subject.Start();

            Assert.NotNull(Locator.Current.GetService<IAuthenticationViewModel>());
        }

        [Fact]
        public void Test_Start_Registers_Self()
        {
            Subject.Start();

            Assert.Same(Subject, Locator.Current.GetService<IApplicationViewModel>());
        }

        [Fact]
        public void Test_OnLogin_Registers_SearchViewModel()
        {
            Locator.CurrentMutable.Register(() => new StubINetworkApi(), typeof(INetworkApi));
            Subject.OnLogin();
            Assert.NotNull(Locator.Current.GetService<ISearchViewModel>());
        }

        [Fact]
        public async Task Test_OpenUri_Passes_Uri_To_Interaction_Handler()
        {
            Uri uri = new Uri("https://www.example.com");
            SavedSetting setting = SettingsStore.OpenPostLinksBrowserTypeDefinition
                .WithValue(OpenPostLinksBrowserType.EmbeddedBrowser);

            Settings.GetSetting(def => Observable.Return(setting));

            using (Subject.UriOpened.RegisterHandler(ctx =>
            {
                Assert.Same(uri, ctx.Input.Uri);
                Assert.Equal(OpenPostLinksBrowserType.EmbeddedBrowser, ctx.Input.BrowserType);
                ctx.SetOutput(Unit.Default);
            }))
            {
                await Subject.OpenUri.Execute(uri);
            }
        }

    }
}
