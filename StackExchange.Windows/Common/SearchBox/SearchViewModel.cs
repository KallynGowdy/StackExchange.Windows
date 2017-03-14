using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using StackExchange.Windows.Api;
using StackExchange.Windows.Application;
using StackExchange.Windows.Questions;
using StackExchange.Windows.User;

namespace StackExchange.Windows.Common.SearchBox
{
    /// <summary>
    /// Defines a view model that represents the logic for the current search state.
    /// </summary>
    public class SearchViewModel : BaseViewModel, ISearchViewModel, ISupportsActivation
    {
        private string query = "";
        private SiteViewModel selectedSite;
        private ObservableAsPropertyHelper<ReactiveList<QuestionViewModel>> suggestedQuestions;
        private INetworkApi NetworkApi { get; }
        private ISearchApi SearchApi { get; }

        /// <summary>
        /// Gets or sets the query that is currently contained in the search box.
        /// </summary>
        public string Query
        {
            get { return query; }
            set { this.RaiseAndSetIfChanged(ref query, value); }
        }

        /// <summary>
        /// Loads the list of available sites from the network api.
        /// </summary>
        public ReactiveCommand<Unit, Unit> LoadSites { get; }

        /// <summary>
        /// Loads the list of accounts associated with the current user.
        /// </summary>
        public ReactiveCommand<Unit, Unit> LoadAssociatedAccounts { get; }

        /// <summary>
        /// Triggers a search using the current query.
        /// </summary>
        public ReactiveCommand<Unit, ReactiveList<QuestionViewModel>> Search { get; }

        public ReactiveCommand<QuestionViewModel, Unit> DisplayQuestion { get; }

        /// <summary>
        /// The list of available sites.
        /// </summary>
        public ReactiveList<SiteViewModel> AvailableSites { get; } = new ReactiveList<SiteViewModel>();

        /// <summary>
        /// The list of associated user accounts for the current user.
        /// </summary>
        public ReactiveList<NetworkUserViewModel> AssociatedAccounts { get; } = new ReactiveList<NetworkUserViewModel>();

        public ReactiveList<QuestionViewModel> SuggestedQuestions => suggestedQuestions.Value;

        /// <summary>
        /// Gets or sets the currently selected site.
        /// </summary>
        public SiteViewModel SelectedSite
        {
            get { return selectedSite; }
            set { this.RaiseAndSetIfChanged(ref selectedSite, value); }
        }

        public SearchViewModel(ApplicationViewModel application = null, INetworkApi networkApi = null, ISearchApi searchApi = null) : base(application)
        {
            this.NetworkApi = networkApi ?? Service<INetworkApi>() ?? Api<INetworkApi>();
            this.SearchApi = searchApi ?? Service<ISearchApi>() ?? Api<ISearchApi>();
            LoadSites = ReactiveCommand.CreateFromTask(LoadSitesImpl);
            LoadAssociatedAccounts = ReactiveCommand.CreateFromTask(LoadAssociatedAccountsImpl);
            DisplayQuestion = ReactiveCommand.CreateFromTask<QuestionViewModel>(DisplayQuestionImpl);
            var canSearch = this.WhenAnyValue(vm => vm.Query).Select(q => !string.IsNullOrWhiteSpace(q));
            Search = ReactiveCommand.CreateFromTask(SearchImpl, canSearch, outputScheduler: RxApp.MainThreadScheduler);
            suggestedQuestions = Search.ToProperty(this, vm => vm.SuggestedQuestions, initialValue: new ReactiveList<QuestionViewModel>());

            this.WhenActivated(d =>
            {
                d(this.WhenAnyValue(vm => vm.Query)
                    .Throttle(TimeSpan.FromMilliseconds(500), RxApp.TaskpoolScheduler)
                    .Select(q => Unit.Default)
                    .InvokeCommand(this, vm => vm.Search));
            });
        }

        private async Task DisplayQuestionImpl(QuestionViewModel question)
        {
            await Application.Navigate.Handle(new NavigationParams(typeof(QuestionPage), question));
            Query = "";
        }

        private async Task<ReactiveList<QuestionViewModel>> SearchImpl()
        {
            var result = await SearchApi.SearchAdvanced(Query, SelectedSite.ApiSiteParameter);
            return new ReactiveList<QuestionViewModel>(result.Items.Select(q => new QuestionViewModel(q)));
        }

        private async Task LoadAssociatedAccountsImpl()
        {
            var accounts = await NetworkApi.UserAssociatedAccounts(Ids.Me);
            AssociatedAccounts.Clear();
            AssociatedAccounts.AddRange(accounts.Items
                .Select(account => new NetworkUserViewModel(account))
                .OrderByDescending(account => account.Reputation));
        }

        private async Task LoadSitesImpl()
        {
            var sites = await NetworkApi.Sites();
            AvailableSites.Clear();
            AvailableSites.AddRange(sites.Items.Select(site => new SiteViewModel(site)));
            SelectedSite = AvailableSites.FirstOrDefault();
        }

        public ViewModelActivator Activator { get; } = new ViewModelActivator();
    }
}
