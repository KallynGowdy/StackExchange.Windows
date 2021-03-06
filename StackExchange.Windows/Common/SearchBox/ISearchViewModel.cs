using System.Reactive;
using System.Windows.Input;
using ReactiveUI;
using StackExchange.Windows.Questions;

namespace StackExchange.Windows.Common.SearchBox
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

        /// <summary>
        /// Gets the suggested list of questions found from the <see cref="Query"/>.
        /// </summary>
        ReactiveList<QuestionItemViewModel> SuggestedQuestions { get; }

        /// <summary>
        /// Gets the command that can display the given question.
        /// </summary>
        ReactiveCommand<QuestionItemViewModel, Unit> DisplayQuestion { get; }

        /// <summary>
        /// Sets the query to the given value and focuses the search box.
        /// </summary>
        ReactiveCommand<string, Unit> SetQueryAndFocus { get; }

        /// <summary>
        /// Gets or sets the current search query.
        /// </summary>
        string Query { get; set; }

        /// <summary>
        /// The interaction that focuses the search box.
        /// </summary>
        Interaction<Unit, Unit> FocusSearchBox { get; }
    }
}