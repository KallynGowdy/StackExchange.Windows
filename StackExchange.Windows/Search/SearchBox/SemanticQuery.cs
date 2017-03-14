using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackExchange.Windows.Search.SearchBox
{
    /// <summary>
    /// Defines a class that represents a query with semantic information.
    /// </summary>
    public class SemanticQuery
    {
        /// <summary>
        /// Gets the sort order of the query.
        /// </summary>
        public string Sort { get; }

        /// <summary>
        /// Gets the tags used by the query.
        /// </summary>
        public string[] Tags { get; }

        public SemanticQuery()
        {
            Sort = "activity";
            Tags = new string[0];
        }

        public SemanticQuery(string query)
        {
            var dictionary = ParseKeyPairs(query);

            if (dictionary.ContainsKey("sort"))
            {
                Sort = dictionary["sort"].ToLowerInvariant();
            }
            if (dictionary.ContainsKey("tags"))
            {
                Tags = ParseTags(dictionary["tags"]);
            }
        }

        /// <summary>
        /// Parses the given query into a set of key/value pairs.
        /// </summary>
        /// <param name="query">The query to parse.</param>
        /// <returns></returns>
        public static Dictionary<string, string> ParseKeyPairs(string query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            return query
                .Trim()
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(split => split.Trim().Split(':'))
                .ToDictionary(keyAndValue => keyAndValue[0].ToLowerInvariant(), keyAndValue => keyAndValue.ElementAtOrDefault(1));
        }

        /// <summary>
        /// Parses the given formatted string into an array.
        /// </summary>
        /// <param name="tags">The list of comma-separated tags.</param>
        /// <returns></returns>
        public static string[] ParseTags(string tags)
        {
            return tags.Trim().Split(',').Select(tag => tag.ToLowerInvariant()).ToArray();
        }

        /// <summary>
        /// Formats the given array of tags.
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static string FormatTags(string[] tags)
        {
            return string.Join(",", tags.Select(t => t.ToLowerInvariant()));
        }
    }
}
