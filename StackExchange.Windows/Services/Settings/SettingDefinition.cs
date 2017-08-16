using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StackExchange.Windows.Services.Settings
{
    /// <summary>
    /// Defines a definition of a setting.
    /// That is, a description of valid values and what they affect.
    /// </summary>
    public class SettingDefinition
    {
        /// <summary>
        /// Gets or sets the key of the resource for the name of the setting.
        /// </summary>
        public string NameResource { get; set; }

        /// <summary>
        /// Gets or sets the key of the resource for the description of the setting.
        /// </summary>
        public string DescriptionResource { get; set; }

        /// <summary>
        /// Gets or sets the key of the resource for the group of the setting.
        /// </summary>
        public string GroupResource { get; set; }

        /// <summary>
        /// Gets or sets the key that this setting is stored under.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the data type of the setting.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the default value of this setting.
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the constraints on the setting.
        /// </summary>
        public SettingsConstraints Constraints { get; set; }

        /// <summary>
        /// Gets whether this setting stores an enum value.
        /// </summary>
        public bool StoresEnum => Type.GetTypeInfo().IsEnum;

        /// <summary>
        /// Gets the default <see cref="SavedSetting"/> representation of this definition.
        /// </summary>
        /// <returns></returns>
        public SavedSetting Default() => new SavedSetting(DefaultValue, this);

        /// <summary>
        /// Gets a new <see cref="SavedSetting"/> that contains the given value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public SavedSetting WithValue(object value) => new SavedSetting(value, this);
    }
}
