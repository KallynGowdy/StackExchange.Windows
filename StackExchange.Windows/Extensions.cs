using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

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
            webView.Height = Convert.ToDouble(result);
        }
    }
}
