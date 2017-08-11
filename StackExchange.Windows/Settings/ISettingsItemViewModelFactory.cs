using StackExchange.Windows.Services.Settings;

namespace StackExchange.Windows.Settings
{
    /// <summary>
    /// Defines a factory that is able to produce <see cref="SettingsItemViewModel"/> objects from <see cref="SavedSetting"/> objects.
    /// </summary>
    public interface ISettingsItemViewModelFactory
    {
        /// <summary>
        /// Creates a new <see cref="SettingsItemViewModel"/> from the given <see cref="SavedSetting"/>.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        SettingsItemViewModel CreateViewModel(SavedSetting setting);
    }
}