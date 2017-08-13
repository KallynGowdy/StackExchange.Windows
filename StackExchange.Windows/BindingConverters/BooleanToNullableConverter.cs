using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackExchange.Windows.BindingConverters
{
    public class BooleanToNullableConverter : ConverterBase
    {
        public override int GetAffinityForObjects(Type fromType, Type toType)
        {
            if (fromType == typeof(bool) && toType == typeof(bool?))
            {
                return 100;
            }
            return 0;
        }

        protected override object ConvertCore(object @from, Type toType, object conversionHint)
        {
            return (bool?)from ?? false;
        }
    }
}
