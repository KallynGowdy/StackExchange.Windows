using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using ReactiveUI;
using Splat;
using StackExchange.Windows.Services;

namespace StackExchange.Windows.BindingConverters
{
    /// <summary>
    /// Defines a <see cref="IBindingTypeConverter"/> that looks up strings in the application resource dictionary.
    /// </summary>
    public class StringResourceConverter : ConverterBase, IResourceStore
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

        public override int GetAffinityForObjects(Type fromType, Type toType)
        {
            if (fromType == typeof(string) && toType == typeof(string))
            {
                return 100;
            }
            else if (fromType == typeof(string) && toType == typeof(Color))
            {
                return 100;
            }
            return 0;
        }

        protected override object ConvertCore(object @from, Type toType, object conversionHint)
        {
            var fromStr = (string)@from;
            return dictionary.ContainsKey(fromStr) ? dictionary[fromStr] : fromStr;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        public object GetResource(string key)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            else
            {
                return null;
            }
        }
    }
}
