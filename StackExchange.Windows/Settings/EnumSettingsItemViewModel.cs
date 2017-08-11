using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Windows.Services.Settings;

namespace StackExchange.Windows.Settings
{
    /// <summary>
    /// Defines a <see cref="SettingsItemViewModel"/> that represents enum values.
    /// </summary>
    public class EnumSettingsItemViewModel : SettingsItemViewModel
    {
        public EnumSettingsItemViewModel(SavedSetting setting) : base(setting)
        {
            Values = Enum.GetValues(setting.Definition.Type)
                .Cast<int>()
                .OrderBy(val => val)
                .Select(val => new EnumValue(val, GetName(setting, val), this))
                .ToArray();
        }

        /// <summary>
        /// Gets the array of values and names stored in the enum.
        /// </summary>
        public EnumValue[] Values { get; }

        private static string GetName(SavedSetting setting, int val)
        {
            return Enum.GetName(setting.Definition.Type, val);
        }
    }
}
