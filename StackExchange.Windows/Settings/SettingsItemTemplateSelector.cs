using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace StackExchange.Windows.Settings
{
    public class SettingsItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EnumTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is EnumSettingsItemViewModel)
            {
                return EnumTemplate;
            }

            return null;
        }
    }
}
