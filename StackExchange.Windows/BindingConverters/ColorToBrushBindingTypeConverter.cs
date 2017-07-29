using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using ReactiveUI;

namespace StackExchange.Windows.BindingConverters
{
    public class ColorToBrushBindingTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            if (fromType.IsAssignableFrom(typeof(Color)) && toType.IsAssignableFrom(typeof(Brush)))
            {
                return 3;
            }
            return -1;
        }

        public bool TryConvert(object @from, Type toType, object conversionHint, out object result)
        {
            if (from is Color color)
            {
                result = new SolidColorBrush(color);
                return true;
            }
            result = new SolidColorBrush();
            return false;
        }
    }
}
