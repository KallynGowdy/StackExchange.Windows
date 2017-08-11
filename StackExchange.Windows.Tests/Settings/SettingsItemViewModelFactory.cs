using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Windows.Services.Settings;
using StackExchange.Windows.Settings;
using StackExchange.Windows.Tests.DataStructures;
using Xunit;

namespace StackExchange.Windows.Tests.Settings
{
    public class SettingsItemViewModelFactoryTests
    {
        public SettingsItemViewModelFactory Subject { get; set; }

        public SettingsItemViewModelFactoryTests()
        {
            Subject = new SettingsItemViewModelFactory();
        }

        [Theory]
        [InlineData(false, typeof(BoolSettingsItemViewModel))]
        [InlineData(TestEnum.A, typeof(EnumSettingsItemViewModel))]
        public void Test_Setting_With_Type_Returns_Type(object value, Type expected)
        {
            var setting = new SavedSetting(value, new SettingDefinition()
            {
                Key = "test",
                GroupResource = "test",
                Type = value.GetType(),
                DescriptionResource = "description",
                NameResource = "name",
                DefaultValue = value
            });

            var viewModel = Subject.CreateViewModel(setting);

            Assert.NotNull(viewModel);
            Assert.IsAssignableFrom(expected, viewModel);
        }
    }
}
