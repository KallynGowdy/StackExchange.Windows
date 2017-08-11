using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Windows.Services.Settings;

namespace StackExchange.Windows.Settings
{
    /// <summary>
    /// Defines a basic factory that creates <see cref="SettingsItemViewModel"/> objects to represent
    /// <see cref="SavedSetting"/> objects.
    /// </summary>
    public class SettingsItemViewModelFactory : ISettingsItemViewModelFactory
    {
        public SettingsItemViewModel CreateViewModel(SavedSetting setting)
        {
            if (setting == null) throw new ArgumentNullException(nameof(setting));

            if (setting.Definition.StoresEnum)
            {
                return new EnumSettingsItemViewModel(setting);
            }
            if (setting.Definition.Type == typeof(bool))
            {
                return new BoolSettingsItemViewModel(setting);
            }

            throw new ArgumentException("Could not create a view model for the given setting. Unrecognized type.", nameof(setting));
        }
    }
}
