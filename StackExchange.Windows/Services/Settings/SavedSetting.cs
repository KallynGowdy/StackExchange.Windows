using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackExchange.Windows.Services.Settings
{
    /// <summary>
    /// Defines a setting that can be saved and retrieved from the store.
    /// </summary>
    public class SavedSetting
    {
        public SavedSetting(object value, SettingDefinition definition)
        {
            SavedValue = value;
            Definition = definition;
        }

        /// <summary>
        /// Gets or sets the definition of this setting.
        /// </summary>
        public SettingDefinition Definition { get; }

        /// <summary>
        /// Gets or sets the settings saved value.
        /// </summary>
        public object SavedValue { get; }

        /// <summary>
        /// Creates a new <see cref="SavedSetting"/> with the given <paramref name="newValue"/>.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        /// <returns></returns>
        public SavedSetting SetValue(object newValue)
        {
            return new SavedSetting(newValue, Definition);
        }
    }
}
