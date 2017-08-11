using StackExchange.Windows.Services.Settings;
using StackExchange.Windows.Settings;

namespace StackExchange.Windows.Tests.Settings
{
    public class DummySettingsItemViewModel : SettingsItemViewModel
    {
        public DummySettingsItemViewModel(SavedSetting setting) : base(setting)
        {
        }
    }
}