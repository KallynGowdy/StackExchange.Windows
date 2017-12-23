using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Windows.Storage;

namespace StackExchange.Windows.Services.Settings
{
    /// <summary>
    /// Defines a concrete implementation of <see cref="ISettingsStore"/>.
    /// </summary>
    public class SettingsStore : ISettingsStore
    {
        public const string AppearanceSettingGroup = "AppearanceSettingGroup";
        public const string FunctionalitySettingGroup = "FunctionalitySettingGroup";

        /// <summary>
        /// Gets the <see cref="SettingDefinition"/> for the current color mode.
        /// </summary>
        public static readonly SettingDefinition ColorModeDefinition = new SettingDefinition()
        {
            NameResource = "ColorModeSettingName",
            DescriptionResource = "ColorModeSettingDescription",
            Key = "ColorMode",
            GroupResource = AppearanceSettingGroup,
            Type = typeof(ColorMode),
            DefaultValue = ColorMode.Light
        };

        /// <summary>
        /// Gets the <see cref="SettingDefinition"/> for the current way to open post links.
        /// </summary>
        public static readonly SettingDefinition OpenPostLinksBrowserTypeDefinition = new SettingDefinition()
        {
            NameResource = "OpenPostLinksBrowserTypeSettingName",
            DescriptionResource = "OpenPostLinksBrowserTypeSettingDescription",
            Key = "OpenPostLinksBrowserType",
            GroupResource = FunctionalitySettingGroup,
            Type = typeof(OpenPostLinksBrowserType),
            DefaultValue = OpenPostLinksBrowserType.EmbeddedBrowser
        };

        public static readonly SettingDefinition SyntaxStyleDefinition = new SettingDefinition()
        {
            NameResource = "SyntaxStyleSettingName",
            DescriptionResource = "SyntaxStyleSettingDescription",
            Key = "SyntaxStyle",
            GroupResource = AppearanceSettingGroup,
            Type = typeof(SyntaxStyle),
            DefaultValue = SyntaxStyle.AtelierHeathLight
        };

        private static readonly SettingDefinition[] Definitions = {
            ColorModeDefinition,
            SyntaxStyleDefinition,
            OpenPostLinksBrowserTypeDefinition
        };

        private readonly ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private readonly Subject<SavedSetting> settingUpdated = new Subject<SavedSetting>();

        public IObservable<SavedSetting> SettingUpdated => settingUpdated;

        public Task SaveSettingAsync(SavedSetting setting)
        {
            if (setting == null) throw new ArgumentNullException(nameof(setting));
            if (setting.SavedValue == null)
            {
                localSettings.Values.Remove(setting.Definition.Key);
            }
            else
            {
                localSettings.Values[setting.Definition.Key] = Serialize(setting);
            }
            settingUpdated.OnNext(setting);
            return Task.FromResult(0);
        }

        public Task<IEnumerable<SavedSetting>> GetSettingsAsync()
        {
            return Task.FromResult(Definitions.Select(GetSettingImpl));
        }

        public IObservable<SavedSetting> GetSetting(SettingDefinition definition)
        {
            if (definition == null) throw new ArgumentNullException(nameof(definition));
            return SettingUpdated
                .Where(s => s.Definition == definition)
                .StartWith(GetSettingImpl(definition));
        }

        private SavedSetting GetSettingImpl(SettingDefinition definition)
        {
            return localSettings.Values.ContainsKey(definition.Key)
                ? new SavedSetting(Deserialize(definition, localSettings.Values[definition.Key]), definition)
                : definition.Default();
        }

        private static object Serialize(SavedSetting setting)
        {
            if (setting.Definition.StoresEnum)
            {
                return setting.SavedValue.ToString();
            }

            return setting.SavedValue;
        }

        private object Deserialize(SettingDefinition definition, object value)
        {
            if (definition.StoresEnum)
            {
                return Enum.Parse(definition.Type, value.ToString());
            }
            return value;
        }
    }
}