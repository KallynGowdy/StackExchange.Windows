using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using StackExchange.Windows.BindingConverters;
using StackExchange.Windows.Common.TagsList;
using StackExchange.Windows.Services;
using StackExchange.Windows.Services.Settings;

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

        /// <summary>
        /// Maps the given list of resource names to their actual representations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resourceEnumerable"></param>
        /// <returns></returns>
        public static IEnumerable<T> MapResources<T>(this IEnumerable<string> resourceEnumerable)
        {
            return resourceEnumerable.Select(MapResource<T>);
        }

        /// <summary>
        /// Maps the given stream of resource names to their actual resource representations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resourceObservable"></param>
        /// <returns></returns>
        public static IObservable<T> MapResources<T>(this IObservable<string> resourceObservable)
        {
            return resourceObservable.Select(MapResource<T>);
        }

        /// <summary>
        /// Maps the given resource to the actual entity it represents.
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static T MapResource<T>(string resource)
        {
            return (T)StringResourceConverter.App.Convert(resource, typeof(T), null,
                CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
        }

        /// <summary>
        /// Gets the current value stored in the given settings store for the given definition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="store"></param>
        /// <param name="definition"></param>
        /// <returns></returns>
        public static IObservable<T> GetSettingValue<T>(this ISettingsStore store, SettingDefinition definition)
        {
            return store.GetSetting(definition)
                .Select(s => (T)s.SavedValue)
                .FirstAsync();
        }

        /// <summary>
        /// Gets a string resource with the given key.
        /// </summary>
        /// <param name="resources"></param>
        /// <param name="key">The key of the resource to retrieve.</param>
        /// <returns>The resource cast into a string or null if it doesn't exist.</returns>
        public static string GetString(this IResourceStore resources, string key)
        {
            return (string)resources.GetResource(key);
        }
    }
}
