using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StackExchange.Windows.Api
{
    /// <summary>
    /// Defines a model that represents a generic response from the stackexchange API.
    /// </summary>
    public class Response<T>
    {
        /// <summary>
        /// Whether there are more items that can be retrieved from another request for another page.
        /// </summary>
        public bool? HasMore { get; set; } = false;

        /// <summary>
        /// The array of items returned by the request.
        /// </summary>
        public T[] Items { get; set; } = new T[0];
    }
}
