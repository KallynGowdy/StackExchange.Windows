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
    public class BooleanToBrushBindingTypeConverter : IBindingTypeConverter
    {
        private readonly Brush @true;
        private readonly Brush @false;

        private BooleanToBrushBindingTypeConverter(Brush @true, Brush @false)
        {
            if (@true == null) throw new ArgumentNullException(nameof(@true));
            if (@false == null) throw new ArgumentNullException(nameof(@false));
            this.@true = @true;
            this.@false = @false;
        }

        public static IBindingTypeConverter Create(Color @true, Color @false)
        {
            return new BooleanToBrushBindingTypeConverter(new SolidColorBrush(@true), new SolidColorBrush(@false));
        }

        public static IBindingTypeConverter Create(Brush @true, Brush @false)
        {
            return new BooleanToBrushBindingTypeConverter(@true, @false);
        }

        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            if (fromType == typeof(bool) && toType.IsAssignableFrom(typeof(Brush)))
            {
                return 2;
            }
            return -1;
        }

        public bool TryConvert(object @from, Type toType, object conversionHint, out object result)
        {
            var value = from as bool?;

            if (value.HasValue)
            {
                result = value.Value ? @true : @false;
                return true;
            }
            result = @false;
            return false;
        }
    }
}
