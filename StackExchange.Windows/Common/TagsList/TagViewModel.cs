using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using StackExchange.Windows.Application;
using StackExchange.Windows.Common.SearchBox;

namespace StackExchange.Windows.Common.TagsList
{
    /// <summary>
    /// Defines a view model that represents a single tag.
    /// </summary>
    public class TagViewModel : ReactiveObject
    {
        /// <summary>
        /// Gets the tag that this view model wraps.
        /// </summary>
        public string Tag { get; }

        /// <summary>
        /// Gets the command that searches for this tag.
        /// </summary>
        public ReactiveCommand<Unit, Unit> SearchTag { get; }

        private ISearchViewModel search;

        public TagViewModel(string tag, ISearchViewModel search = null)
        {
            this.search = search ?? Locator.Current.GetService<ISearchViewModel>();
            Tag = tag;

            SearchTag = ReactiveCommand.CreateFromTask(SearchTagImpl);
        }

        private async Task SearchTagImpl()
        {
            await search.SetQueryAndFocus.Execute($"[{Tag}]").FirstAsync();
        }
    }
}