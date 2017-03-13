using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackExchange.Windows.Api
{
    /// <summary>
    /// Extension methods for the URI class.
    /// </summary>
    public static class UriExtensions
    {

        public static Dictionary<string, string> ToDictionary(this Uri uri)
        {
            return uri.Query
                .TrimStart('?')
                .Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(q => q.Split('='))
                .ToDictionary(split => split[0], split => Uri.UnescapeDataString(split.ElementAtOrDefault(1) ?? ""));
        }

        public static Uri ToUri(this Dictionary<string, string> query, Uri originalUri)
        {
            var uri = new UriBuilder(originalUri)
            {
                Query = string.Join("&", query.Select(q => $"{q.Key}={Uri.EscapeDataString(q.Value)}"))
            };

            return uri.Uri;
        }
    }
}
