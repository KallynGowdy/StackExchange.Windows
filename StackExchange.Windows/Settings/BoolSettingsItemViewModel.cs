using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Windows.Services.Settings;

namespace StackExchange.Windows.Settings
{
    /// <summary>
    /// Defines a view model for saved settings that store booleans.
    /// </summary>
    public class BoolSettingsItemViewModel : SettingsItemViewModel
    {
        public BoolSettingsItemViewModel(SavedSetting setting) : base(setting)
        {
        }
    }
}
