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
    public abstract class SettingsItemViewModel : ReactiveObject
    {
        private SavedSetting setting;

        public SettingsItemViewModel(SavedSetting setting)
        {
            this.setting = setting;
        }

        public string GroupResource => setting.Definition.GroupResource;
        public string NameResource => setting.Definition.NameResource;
        public string DescriptionResource => setting.Definition.DescriptionResource;
        public bool HasDescription => !string.IsNullOrEmpty(DescriptionResource);

        public object Value
        {
            get => setting.SavedValue;
            set
            {
                if (setting.SavedValue != value)
                {
                    setting = setting.SetValue(value);
                    this.RaisePropertyChanged();
                }
            }
        }
    }
}
