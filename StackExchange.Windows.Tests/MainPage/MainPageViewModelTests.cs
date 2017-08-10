using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using StackExchange.Windows.Application;
using StackExchange.Windows.Questions;
using StackExchange.Windows.Settings;
using Xunit;

namespace StackExchange.Windows.Tests.MainPage
{
    public class MainPageViewModelTests
    {
        public MainPageViewModel Subject { get; set; }
        public StubIApplicationViewModel Application { get; set; }

        public MainPageViewModelTests()
        {
            Application = new StubIApplicationViewModel();
            Subject = new MainPageViewModel(Application);

            var navigate = new Interaction<NavigationParams, Unit>();
            Application.Navigate_Get(() => navigate);

            var navigateAndClear = new Interaction<NavigationParams, Unit>();
            Application.NavigateAndClearStack_Get(() => navigateAndClear);
        }

        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        public async Task Test_ToggleNavigationMenu_Toggles_NavigationMenuOpen(bool current, bool expected)
        {
            Subject.NavigationMenuOpen = current;

            await Subject.ToggleNavigationMenu.Execute();

            Assert.Equal(expected, Subject.NavigationMenuOpen);
        }

        [Fact]
        public async Task Test_NavigateHome_Navigates_To_The_QuestionsPage_And_Resets_The_Stack()
        {
            Type type = null;
            using (Subject.Application.NavigateAndClearStack.RegisterHandler(ctx =>
            {
                type = ctx.Input.PageType;
                ctx.SetOutput(Unit.Default);
            }))
            {
                await Subject.NavigateHome.Execute();

                Assert.Equal(typeof(QuestionsPage), type);
            }
        }

        [Fact]
        public async Task Test_NavigateToSettings_Navigates_To_The_SettingsPage()
        {
            Type type = null;
            using (Subject.Application.Navigate.RegisterHandler(ctx =>
            {
                type = ctx.Input.PageType;
                ctx.SetOutput(Unit.Default);
            }))
            {
                await Subject.NavigateToSettings.Execute();

                Assert.Equal(typeof(SettingsPage), type);
            }
        }
    }
}
