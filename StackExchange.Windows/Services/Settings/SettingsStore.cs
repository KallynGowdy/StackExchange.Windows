using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace StackExchange.Windows.Services.Settings
{
    /// <summary>
    /// Defines a concrete implementation of <see cref="ISettingsStore"/>.
    /// </summary>
    public class SettingsStore : ISettingsStore
    {
        /// <summary>
        /// Gets the <see cref="SettingDefinition"/> for the current color mode.
        /// </summary>
        public static readonly SettingDefinition ColorModeDefinition = new SettingDefinition()
        {
            NameResource = "ColorModeSettingName",
            DescriptionResource = "ColorModeSettingDescription",
            Key = "ColorMode",
            GroupResource = "AppearanceSettingGroup",
            Type = typeof(ColorMode)
        };

        private static readonly SettingDefinition[] Definitions = {
            ColorModeDefinition
        };

        private readonly ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        public Task SaveSettingAsync(SavedSetting setting)
        {
            if (setting == null) throw new ArgumentNullException(nameof(setting));
            localSettings.Values[setting.Definition.Key] = setting.SavedValue;
            return Task.FromResult(0);
        }

        public Task<IEnumerable<SavedSetting>> GetSettingsAsync()
        {
            return Task.FromResult(Definitions.Select(GetSetting));
        }

        public SavedSetting GetSetting(SettingDefinition definition)
        {
            return localSettings.Values.ContainsKey(definition.Key)
                ? new SavedSetting(localSettings.Values[definition.Key], definition)
                : definition.Default();
        }
    }
}