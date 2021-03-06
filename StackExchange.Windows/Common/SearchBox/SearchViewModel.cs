﻿using System;
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
        private ObservableAsPropertyHelper<ReactiveList<QuestionItemViewModel>> suggestedQuestions;
        private INetworkApi NetworkApi { get; }

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
        public ReactiveCommand<Unit, ReactiveList<QuestionItemViewModel>> Search { get; }

        /// <summary>
        /// Displays the given question.
        /// </summary>
        public ReactiveCommand<QuestionItemViewModel, Unit> DisplayQuestion { get; }

        /// <summary>
        /// Sets the query to the given value and focuses the search box.
        /// </summary>
        public ReactiveCommand<string, Unit> SetQueryAndFocus { get; }

        /// <summary>
        /// The list of available sites.
        /// </summary>
        public ReactiveList<SiteViewModel> AvailableSites { get; } = new ReactiveList<SiteViewModel>();

        /// <summary>
        /// The list of associated user accounts for the current user.
        /// </summary>
        public ReactiveList<NetworkUserViewModel> AssociatedAccounts { get; } = new ReactiveList<NetworkUserViewModel>();

        /// <summary>
        /// The list of questions that are suggested based on the current query.
        /// </summary>
        public ReactiveList<QuestionItemViewModel> SuggestedQuestions => suggestedQuestions.Value;

        /// <summary>
        /// The interaction that focuses the search box.
        /// </summary>
        public Interaction<Unit, Unit> FocusSearchBox { get; } = new Interaction<Unit, Unit>();

        /// <summary>
        /// Gets or sets the currently selected site.
        /// </summary>
        public SiteViewModel SelectedSite
        {
            get { return selectedSite; }
            set { this.RaiseAndSetIfChanged(ref selectedSite, value); }
        }

        public SearchViewModel(IApplicationViewModel application = null, INetworkApi networkApi = null) : base(application)
        {
            this.NetworkApi = networkApi ?? Service<INetworkApi>() ?? Api<INetworkApi>();
            LoadSites = ReactiveCommand.CreateFromTask(LoadSitesImpl);
            LoadAssociatedAccounts = ReactiveCommand.CreateFromTask(LoadAssociatedAccountsImpl);
            DisplayQuestion = ReactiveCommand.CreateFromTask<QuestionItemViewModel>(DisplayQuestionImpl);
            var canSearch = this.WhenAnyValue(vm => vm.Query).Select(q => !string.IsNullOrWhiteSpace(q));
            Search = ReactiveCommand.CreateFromTask(SearchImpl, canSearch, outputScheduler: RxApp.MainThreadScheduler);
            suggestedQuestions = Search.ToProperty(this, vm => vm.SuggestedQuestions, initialValue: new ReactiveList<QuestionItemViewModel>());
            SetQueryAndFocus = ReactiveCommand.CreateFromTask<string>(SetQueryAndFocusImpl);

            this.WhenActivated(d =>
            {
                d(this.WhenAnyValue(vm => vm.Query)
                    .Throttle(TimeSpan.FromMilliseconds(500), RxApp.TaskpoolScheduler)
                    .Select(q => Unit.Default)
                    .InvokeCommand(this, vm => vm.Search));
            });
        }

        private async Task SetQueryAndFocusImpl(string query)
        {
            await FocusSearchBox.Handle(Unit.Default).FirstAsync();
            Query = query;
        }

        private async Task DisplayQuestionImpl(QuestionItemViewModel question)
        {
            await Application.Navigate.Handle(new NavigationParams(typeof(QuestionPage), question.Question));
            Query = "";
        }

        private async Task<ReactiveList<QuestionItemViewModel>> SearchImpl()
        {
            var result = await NetworkApi.SearchAdvanced(Query, SelectedSite.ApiSiteParameter);
            return new ReactiveList<QuestionItemViewModel>(result.Items.Select(q => new QuestionItemViewModel(q)));
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
            if (!AvailableSites.Any())
            {
                var sites = await NetworkApi.Sites();
                AvailableSites.Clear();
                AvailableSites.AddRange(sites.Items.Select(site => new SiteViewModel(site)));
                SelectedSite = AvailableSites.FirstOrDefault();
            }
        }

        public ViewModelActivator Activator { get; } = new ViewModelActivator();

    }
}
