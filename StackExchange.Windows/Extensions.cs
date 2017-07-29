using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using StackExchange.Windows.Common.TagsList;

namespace StackExchange.Windows
{
    public static class Extensions
    {
        /// <summary>
        /// Adds the given disposable to the list of disposables via the given action.
        /// </summary>
        /// <param name="disposable"></param>
        /// <param name="disposables"></param>
        public static void DisposeWith(this IDisposable disposable, Action<IDisposable> disposables)
        {
            disposables(disposable);
        }

        /// <summary>
        /// Causes the web view to resize it's height to the size of its content.
        /// </summary>
        /// <param name="webView"></param>
        /// <returns></returns>
        public static async Task ResizeHeightToContentAsync(this WebView webView)
        {
            var result = await webView.InvokeScriptAsync("getHeight", null);
            while (result == "-1")
            {
                await Task.Delay(25);
                result = await webView.InvokeScriptAsync("getHeight", null);
            }
            webView.Height = Convert.ToDouble(result);
        }

        /// <summary>
        /// Converts the given list of strings into an array of <see cref="TagViewModel"/> objects.
        /// </summary>
        /// <param name="tags">The list of tags to wrap.</param>
        /// <returns></returns>
        public static TagsListViewModel ToListViewModel(this IEnumerable<string> tags)
        {
            return new TagsListViewModel(tags.Select(t => new TagViewModel(t)).ToArray());
        }

        /// <summary>
        /// Converts the given list of strings into an array of <see cref="TagViewModel"/> objects.
        /// </summary>
        /// <param name="tags">The list of tags to wrap.</param>
        /// <returns></returns>
        public static TagViewModel[] ToViewModels(this IEnumerable<string> tags)
        {
            return tags.Select(t => new TagViewModel(t)).ToArray();
        }
    }
}
