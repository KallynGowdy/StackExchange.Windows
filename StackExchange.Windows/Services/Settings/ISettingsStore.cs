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
        /// Saves the given setting.
        /// </summary>
        /// <param name="setting"></param>
        Task SaveSettingAsync(SavedSetting setting);

        /// <summary>
        /// Gets the list of saved settings.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SavedSetting>> GetSettingsAsync();
    }
}
