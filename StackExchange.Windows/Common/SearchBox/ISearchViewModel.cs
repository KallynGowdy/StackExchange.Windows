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
        ReactiveList<QuestionViewModel> SuggestedQuestions { get; }

        /// <summary>
        /// Gets the command that can display the given question.
        /// </summary>
        ReactiveCommand<QuestionViewModel, Unit> DisplayQuestion { get; }

        /// <summary>
        /// Gets or sets the current search query.
        /// </summary>
        string Query { get; set; }
    }
}