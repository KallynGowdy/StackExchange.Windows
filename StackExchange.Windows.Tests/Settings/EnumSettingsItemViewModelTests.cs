using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Windows.Services.Settings;
using StackExchange.Windows.Settings;
using StackExchange.Windows.Tests.DataStructures;
using Xunit;

namespace StackExchange.Windows.Tests.Settings
{
    public class EnumSettingsItemViewModelTests
    {
        public EnumSettingsItemViewModel Subject { get; set; }
        public SavedSetting Setting { get; set; }

        public EnumSettingsItemViewModelTests()
        {
            Setting = new SavedSetting(TestEnum.B, new SettingDefinition()
            {
                Key = "key",
                Type = typeof(TestEnum),
                GroupResource = "group",
                NameResource = "name",
                DescriptionResource = "description",
                DefaultValue = TestEnum.A
            });
            Subject = new EnumSettingsItemViewModel(Setting);
        }

        [Fact]
        public void Test_Lists_Out_Enum_Values_In_Order_Of_Value()
        {
            Assert.Collection(Subject.Values,
                val => Assert.Equal(TestEnum.C, val.Value),
                val => Assert.Equal(TestEnum.B, val.Value),
                val => Assert.Equal(TestEnum.A, val.Value));
        }

        [Fact]
        public async Task Test_Changes_Value_When_Enum_Is_Selected()
        {
            var first = Subject.Values.First();
            await first.Select.Execute();

            Assert.Equal(first.Value, Subject.Value);
        }

        [Fact]
        public void Test_Pulls_Name_From_ResourceAttribute_If_Available()
        {
            Assert.Collection(Subject.Values,
                val => Assert.Equal("C", val.Name),
                val => Assert.Equal("B", val.Name),
                val => Assert.Equal("TheResource", val.Name));
        }
    }
}
