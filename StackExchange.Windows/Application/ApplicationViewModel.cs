using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ReactiveUI;
using Refit;
using Splat;
using StackExchange.Windows.Api;
using StackExchange.Windows.Api.Converters;
using StackExchange.Windows.Authentication;
using StackExchange.Windows.BindingConverters;
using StackExchange.Windows.Common.SearchBox;
using StackExchange.Windows.Questions;
using StackExchange.Windows.Services;

namespace StackExchange.Windows.Application
{
    /// <summary>
    /// Defines a view model that represents the entire application.
    /// </summary>
    public class ApplicationViewModel : ReactiveObject, IApplicationViewModel
    {
        /// <summary>
        /// Gets the view model in charge of authentication.
        /// </summary>
        public IAuthenticationViewModel Authentication { get; }

        /// <summary>
        /// Gets the view model in charge of search for the site.
        /// </summary>
        public ISearchViewModel Search { get; private set; }

        /// <summary>
        /// Gets the interaction that requests navigation to other pages.
        /// </summary>
        public Interaction<NavigationParams, Unit> Navigate { get; } = new Interaction<NavigationParams, Unit>();

        /// <summary>
        /// Gets the interaction that requests navigation back in the navigation stack.
        /// </summary>
        public Interaction<Unit, Unit> NavigateBack { get; } = new Interaction<Unit, Unit>();

        /// <summary>
        /// Gets the interaction that requests navigation to the given page type and clears the page stack at the same time.
        /// </summary>
        public Interaction<NavigationParams, Unit> NavigateAndClearStack { get; } = new Interaction<NavigationParams, Unit>();

        /// <summary>
        /// Gets the site that the user is currently viewing.
        /// </summary>
        public string CurrentSite => Search.SelectedSite.ApiSiteParameter;

        /// <summary>
        /// Gets the current HTTP client for the application.
        /// </summary>
        public HttpClient HttpClient { get; private set; }

        public ApplicationViewModel(ISearchViewModel search = null)
        {
            Search = search;
            Authentication = new AuthenticationViewModel();
            Authentication.Login.Do(u => OnLogin()).Subscribe();
        }

        public void OnLogin()
        {
            var handler = new AuthenticatedHttpClientHandler("eZclcV**uSVviAazkVJ6ug((", () => Authentication.Token)
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
            };

            // TODO: Add message handler to add access token
            HttpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://api.stackexchange.com/2.2"),
            };

            Search = new SearchViewModel(this);
            Locator.CurrentMutable.RegisterConstant(Search, typeof(ISearchViewModel));
        }

        public void Start()
        {
            Locator.CurrentMutable.RegisterConstant(this, typeof(IApplicationViewModel));
            Locator.CurrentMutable.RegisterConstant(Authentication, typeof(IAuthenticationViewModel));

            Locator.CurrentMutable.RegisterLazySingleton(() => new UwpClipboard(), typeof(IClipboard));
            Locator.CurrentMutable.RegisterLazySingleton(() => new QuestionsViewModel(), typeof(QuestionsViewModel));
            Locator.CurrentMutable.Register(UriToImageSourceBindingTypeConverter.Create, typeof(IBindingTypeConverter));

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Converters = new List<JsonConverter>()
                {
                    new UnixDateConverter()
                }
            };
        }

        public TService Api<TService>()
        {
            return RestService.For<TService>(HttpClient);
        }
    }
}
