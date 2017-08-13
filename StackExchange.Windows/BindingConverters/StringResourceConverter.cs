using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using ReactiveUI;
using Splat;

namespace StackExchange.Windows.BindingConverters
{
    /// <summary>
    /// Defines a <see cref="IBindingTypeConverter"/> that looks up strings in the application resource dictionary.
    /// </summary>
    public class StringResourceConverter : IBindingTypeConverter, IValueConverter
    {
        private static readonly Lazy<StringResourceConverter> app = new Lazy<StringResourceConverter>(() => new StringResourceConverter());
        public static StringResourceConverter App => app.Value;

        private readonly ResourceDictionary dictionary;

        public StringResourceConverter() : this(global::Windows.UI.Xaml.Application.Current.Resources)
        {
        }

        public StringResourceConverter(ResourceDictionary resourceDictionary)
        {
            dictionary = resourceDictionary ?? throw new ArgumentNullException(nameof(resourceDictionary));
        }

        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            if (fromType == typeof(string) && toType == typeof(string))
            {
                return 100;
            }
            return 0;
        }

        public bool TryConvert(object @from, Type toType, object conversionHint, out object result)
        {
            string fromStr = (string) @from;
            try
            {
                result = dictionary[fromStr];
            }
            catch (Exception ex)
            {
                this.Log().WarnException($"Couldnt convert object to type {toType}", ex);
                result = fromStr;
                return false;
            }

            return true;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            object result;
            TryConvert(value, targetType, parameter, out result);
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
