using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace StackExchange.Windows.Search.SearchBox
{
    /// <summary>
    /// Defines a view model that represents the logic for a search box.
    /// </summary>
    public class SearchBoxViewModel : ReactiveObject
    {
        private string query = "";
        private ObservableAsPropertyHelper<string[]> tags;
        private ObservableAsPropertyHelper<string> sort;

        /// <summary>
        /// Gets or sets the query that is currently contained in the search box.
        /// </summary>
        public string Query
        {
            get { return query; }
            set { this.RaiseAndSetIfChanged(ref query, value); }
        }

        /// <summary>
        /// Gets the array of tags that have been parsed from the query.
        /// </summary>
        public string[] Tags => tags.Value;

        /// <summary>
        /// Gets the sort order that should be used.
        /// </summary>
        public string Sort => sort.Value;

        public SearchBoxViewModel()
        {
            
        }
    }
}
