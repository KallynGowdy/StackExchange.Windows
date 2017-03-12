using System.Reactive;
using ReactiveUI;

namespace StackExchange.Windows.Search.SearchBox
{
    /// <summary>
    /// Defines an interface for a view model that represents the current search state of the application.
    /// </summary>
    public interface ISearchViewModel
    {
        /// <summary>
        /// Gets or sets the currently selected site.
        /// </summary>
        SiteViewModel SelectedSite { get; set; }

        /// <summary>
        /// Gets the list of currently available sites.
        /// </summary>
        ReactiveList<SiteViewModel> AvailableSites { get; }

        /// <summary>
        /// Gets the command that can load the list of sites from the API.
        /// </summary>
        ReactiveCommand<Unit, Unit> LoadSites { get; }
    }
}