using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
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
using StackExchange.Windows.Interactions;
using StackExchange.Windows.Questions;
using StackExchange.Windows.Services;
using StackExchange.Windows.Services.Settings;
using StackExchange.Windows.Settings;

namespace StackExchange.Windows.Application
{
    /// <summary>
    /// Defines a view model that represents the entire application.
    /// </summary>
    public class ApplicationViewModel : ReactiveObject, IApplicationViewModel
    {
        private readonly ObservableAsPropertyHelper<ColorMode> currentColorMode;

        /// <summary>
        /// Gets the view model in charge of authentication.
        /// </summary>
        public IAuthenticationViewModel Authentication { get; }

        /// <summary>
        /// Gets the view model in charge of search for the site.
        /// </summary>
        public ISearchViewModel Search { get; private set; }

        /// <summary>
        /// Gets the store in charge of storing settings for the application.
        /// </summary>
        public ISettingsStore Settings { get; private set; }

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

        public Interaction<OpenUriOptions, Unit> UriOpened { get; } = new Interaction<OpenUriOptions, Unit>();

        public ReactiveCommand<Uri, Unit> OpenUri { get; }

        /// <summary>
        /// Gets the site that the user is currently viewing.
        /// </summary>
        public string CurrentSite => Search.SelectedSite.ApiSiteParameter;

        /// <summary>
        /// Gets the current HTTP client for the application.
        /// </summary>
        public HttpClient HttpClient { get; private set; }

        /// <summary>
        /// Gets the current color mode that the view model has requested.
        /// </summary>
        public ColorMode CurrentColorMode => currentColorMode.Value;

        public ApplicationViewModel(ISettingsStore settings = null, ISearchViewModel search = null)
        {
            Search = search;
            Settings = settings ?? new SettingsStore();
            OpenUri = ReactiveCommand.CreateFromTask<Uri, Unit>(OpenUriImpl);

            // ApplicationViewModel is expected to live while the application is running.
            Authentication = new AuthenticationViewModel();
            Authentication.Login.Do(u => OnLogin()).Subscribe();

            currentColorMode = Settings.GetSetting(SettingsStore.ColorModeDefinition)
                .Select(setting => (ColorMode)setting.SavedValue)
                .ToProperty(this, vm => vm.CurrentColorMode);
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
            Locator.CurrentMutable.RegisterConstant(Settings, typeof(ISettingsStore));

            Locator.CurrentMutable.RegisterLazySingleton(() => new UwpClipboard(), typeof(IClipboard));
            Locator.CurrentMutable.RegisterLazySingleton(() => new QuestionsViewModel(), typeof(QuestionsViewModel));
            Locator.CurrentMutable.RegisterLazySingleton(() => new SettingsItemViewModelFactory(), typeof(ISettingsItemViewModelFactory));
            Locator.CurrentMutable.Register(UriToImageSourceBindingTypeConverter.Create, typeof(IBindingTypeConverter));
            Locator.CurrentMutable.Register(() => new ColorToBrushBindingTypeConverter(), typeof(IBindingTypeConverter));
            Locator.CurrentMutable.Register(() => StringResourceConverter.App, typeof(IResourceStore));

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

            UriOpened.RegisterHandler(async ctx =>
            {
                await Launcher.LaunchUriAsync(ctx.Input.Uri);
                ctx.SetOutput(Unit.Default);
            });
        }

        public TService Api<TService>()
        {
            return RestService.For<TService>(HttpClient);
        }

        private async Task<Unit> OpenUriImpl(Uri uri)
        {
            OpenUriOptions options = new OpenUriOptions()
            {
                Uri = uri,
                BrowserType = await Settings.GetSettingValue<OpenPostLinksBrowserType>(SettingsStore.OpenPostLinksBrowserTypeDefinition)
            };

            await UriOpened.Handle(options);

            return Unit.Default;
        }
    }
}
