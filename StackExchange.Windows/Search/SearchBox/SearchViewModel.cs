using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using ReactiveUI;
using Refit;
using Splat;
using StackExchange.Windows.Api;
using StackExchange.Windows.Application;

namespace StackExchange.Windows.Search.SearchBox
{
    /// <summary>
    /// Defines a view model that represents the logic for the current search state.
    /// </summary>
    public class SearchViewModel : BaseViewModel
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

        public INetworkApi NetworkApi { get; }

        /// <summary>
        /// Loads the list of available sites from the network api.
        /// </summary>
        public ReactiveCommand<Unit, Unit> LoadSites { get; }

        /// <summary>
        /// The list of available sites.
        /// </summary>
        public ReactiveList<SiteViewModel> AvailableSites { get; } = new ReactiveList<SiteViewModel>();

        public SearchViewModel(ApplicationViewModel application = null, INetworkApi networkApi = null) : base(application)
        {
            this.NetworkApi = networkApi ?? Api<INetworkApi>();

            LoadSites = ReactiveCommand.CreateFromTask(LoadSitesImpl);
        }

        private async Task LoadSitesImpl()
        {
            var sites = await NetworkApi.Sites();
            AvailableSites.Clear();
            AvailableSites.AddRange(sites.Items.Select(site => new SiteViewModel(site)));
        }
    }
}
