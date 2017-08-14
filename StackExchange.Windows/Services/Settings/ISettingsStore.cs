using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackExchange.Windows.Services.Settings
{
    /// <summary>
    /// Defines a service that helps store settings.
    /// </summary>
    public interface ISettingsStore
    {
        /// <summary>
        /// Gets an observable that resolves whenever a setting is updated.
        /// </summary>
        IObservable<SavedSetting> SettingUpdated { get; }

        /// <summary>
        /// Saves the given setting.
        /// </summary>
        /// <param name="setting"></param>
        Task SaveSettingAsync(SavedSetting setting);

        /// <summary>
        /// Gets the list of saved settings.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SavedSetting>> GetSettingsAsync();

        /// <summary>
        /// Gets the setting that represents the current value for the given definition.
        /// The returned observable resolves whenever the setting is updated.
        /// </summary>
        /// <param name="definition"></param>
        /// <returns></returns>
        IObservable<SavedSetting> GetSetting(SettingDefinition definition);
    }
}
