using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using StackExchange.Windows.Services.Settings;

namespace StackExchange.Windows.Settings
{
    /// <summary>
    /// Defines a view model that represents a settings item.
    /// </summary>
    public class SettingsItemViewModel : ReactiveObject
    {
        private SavedSetting s;

        public SettingsItemViewModel(SavedSetting s)
        {
            this.s = s;
        }

        public string GroupResource => s.Definition.GroupResource;
        public string NameResource => s.Definition.NameResource;
    }
}
