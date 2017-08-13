using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using ReactiveUI;
using Splat;

namespace StackExchange.Windows.BindingConverters
{
    public abstract class ConverterBase : IBindingTypeConverter, IValueConverter
    {
        public abstract int GetAffinityForObjects(Type fromType, Type toType);

        public bool TryConvert(object @from, Type toType, object conversionHint, out object result)
        {
            try
            {
                result = ConvertCore(from, toType, conversionHint);
            }
            catch (Exception ex)
            {
                this.Log().WarnException($"Couldnt convert object to type {toType}", ex);
                result = null;
                return false;
            }

            return true;
        }

        public virtual object Convert(object value, Type targetType, object parameter, string language)
        {
            object result;
            TryConvert(value, targetType, parameter, out result);
            return result;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            object result;
            TryConvert(value, targetType, parameter, out result);
            return result;
        }

        protected abstract object ConvertCore(object from, Type toType, object conversionHint);
    }
}
