using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splat;
using StackExchange.Windows.Api;
using StackExchange.Windows.Application;
using StackExchange.Windows.Authentication;
using StackExchange.Windows.Common.SearchBox;
using Xunit;

namespace StackExchange.Windows.Tests.Application
{
    public class ApplicationViewModelTests
    {
        public ApplicationViewModel Subject { get; set; }

        public ApplicationViewModelTests()
        {
            Subject = new ApplicationViewModel();
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

    }
}
