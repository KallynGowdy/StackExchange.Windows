using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace StackExchange.Windows.Api.Converters
{
    /// <summary>
    /// Defines a <see cref="JsonConverter"/> that can read unix formatted dates.
    /// </summary>
    public class UnixDateConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType == typeof(DateTime?))
            {
                if (reader.Value == null)
                {
                    return null;
                }
            }
            var t = Convert.ToInt64(reader.Value);
            return new DateTime(1970, 1, 1).AddSeconds(t);
        }

        public override bool CanConvert(Type objectType) => typeof(DateTime) == objectType || typeof(DateTime?) == objectType;
    }
}
