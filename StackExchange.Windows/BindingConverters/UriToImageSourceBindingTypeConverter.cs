using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using ReactiveUI;

namespace StackExchange.Windows.BindingConverters
{
    /// <summary>
    /// Defines a <see cref="IBindingTypeConverter"/> between <see cref="Uri"/> and <see cref="ImageSource"/> entities.
    /// </summary>
    public class UriToImageSourceBindingTypeConverter : IBindingTypeConverter
    {
        private UriToImageSourceBindingTypeConverter()
        {
        }

        public static IBindingTypeConverter Create()
        {
            return new UriToImageSourceBindingTypeConverter();
        }

        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            if (fromType == typeof(Uri) && toType.IsAssignableFrom(typeof(ImageSource)))
            {
                return 2;
            }
            return -1;
        }

        public bool TryConvert(object @from, Type toType, object conversionHint, out object result)
        {
            var value = from as Uri;

            if (value != null)
            {
                result = new BitmapImage(value);
                return true;
            }
            result = null;
            return false;
        }
    }
}
