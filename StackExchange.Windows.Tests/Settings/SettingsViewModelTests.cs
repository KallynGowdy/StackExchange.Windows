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
using StackExchange.Windows.Services.Settings;
using StackExchange.Windows.Settings;
using Xunit;

namespace StackExchange.Windows.Tests.Settings
{
    public class SettingsViewModelTests
    {
        public SettingsViewModel Subject { get; set; }
        public StubIApplicationViewModel Application { get; set; }
        public StubISettingsStore SettingsStore { get; set; }
        public StubISettingsItemViewModelFactory Factory { get; set; }

        public SettingsViewModelTests()
        {
            Application = new StubIApplicationViewModel();
            SettingsStore = new StubISettingsStore();
            Subject = new SettingsViewModel(Factory, SettingsStore, Application);

            Factory.CreateViewModel(s => new DummySettingsItemViewModel(s));
        }

        [Fact]
        public void Test_Loads_Settings_When_Activated()
        {
            bool hit = false;
            SettingsStore.GetSettingsAsync(() =>
            {
                hit = true;
                return Task.FromResult(Enumerable.Empty<SavedSetting>());
            });

            using (Subject.Activator.Activate())
            {
                Assert.True(hit);
            }
        }

        [Fact]
        public async Task Test_Loads_Settings_From_Store()
        {
            IEnumerable<SavedSetting> settings = new[]
            {
                new SavedSetting(1, new SettingDefinition()
                {
                    Key = "Key1",
                    GroupResource = "Group1"
                }),
                new SavedSetting(2, new SettingDefinition()
                {
                    Key = "Key2",
                    GroupResource = "Group2"
                }),
            };
            SettingsStore.GetSettingsAsync(() => Task.FromResult(settings));

            await Subject.LoadSettings.Execute();

            Assert.Collection(Subject.GroupedSettings,
                group => Assert.Collection(group,
                    setting => Assert.Equal("Group1", setting.GroupResource)),
                group => Assert.Collection(group,
                    setting => Assert.Equal("Group2", setting.GroupResource)));
        }
    }
}
