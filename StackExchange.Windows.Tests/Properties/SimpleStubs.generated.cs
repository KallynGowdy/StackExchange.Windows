using System;
using System.Runtime.CompilerServices;
using Etg.SimpleStubs;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;
using StackExchange.Windows.Api.Models;
using System.Reactive;
using System.Windows.Input;
using ReactiveUI;
using StackExchange.Windows.Questions;

namespace StackExchange.Windows.Api
{
    [CompilerGenerated]
    public class StubINetworkApi : INetworkApi
    {
        private readonly StubContainer<StubINetworkApi> _stubs = new StubContainer<StubINetworkApi>();

        global::System.Threading.Tasks.Task<global::StackExchange.Windows.Api.Response<global::StackExchange.Windows.Api.Models.Site>> global::StackExchange.Windows.Api.INetworkApi.Sites()
        {
            return _stubs.GetMethodStub<Sites_Delegate>("Sites").Invoke();
        }

        public delegate global::System.Threading.Tasks.Task<global::StackExchange.Windows.Api.Response<global::StackExchange.Windows.Api.Models.Site>> Sites_Delegate();

        public StubINetworkApi Sites(Sites_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.Threading.Tasks.Task<global::StackExchange.Windows.Api.Response<global::StackExchange.Windows.Api.Models.NetworkUser>> global::StackExchange.Windows.Api.INetworkApi.UserAssociatedAccounts(string ids)
        {
            return _stubs.GetMethodStub<UserAssociatedAccounts_String_Delegate>("UserAssociatedAccounts").Invoke(ids);
        }

        public delegate global::System.Threading.Tasks.Task<global::StackExchange.Windows.Api.Response<global::StackExchange.Windows.Api.Models.NetworkUser>> UserAssociatedAccounts_String_Delegate(string ids);

        public StubINetworkApi UserAssociatedAccounts(UserAssociatedAccounts_String_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}

namespace StackExchange.Windows.Api
{
    [CompilerGenerated]
    public class StubIQuestionsApi : IQuestionsApi
    {
        private readonly StubContainer<StubIQuestionsApi> _stubs = new StubContainer<StubIQuestionsApi>();

        global::System.Threading.Tasks.Task<global::StackExchange.Windows.Api.Response<global::StackExchange.Windows.Api.Models.Question>> global::StackExchange.Windows.Api.IQuestionsApi.Questions(string site, string order, string sort, int page, int pagesize, string filter)
        {
            return _stubs.GetMethodStub<Questions_String_String_String_Int32_Int32_String_Delegate>("Questions").Invoke(site, order, sort, page, pagesize, filter);
        }

        public delegate global::System.Threading.Tasks.Task<global::StackExchange.Windows.Api.Response<global::StackExchange.Windows.Api.Models.Question>> Questions_String_String_String_Int32_Int32_String_Delegate(string site, string order, string sort, int page, int pagesize, string filter);

        public StubIQuestionsApi Questions(Questions_String_String_String_Int32_Int32_String_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}

namespace StackExchange.Windows.Api
{
    [CompilerGenerated]
    public class StubISearchApi : ISearchApi
    {
        private readonly StubContainer<StubISearchApi> _stubs = new StubContainer<StubISearchApi>();

        global::System.Threading.Tasks.Task<global::StackExchange.Windows.Api.Response<global::StackExchange.Windows.Api.Models.Question>> global::StackExchange.Windows.Api.ISearchApi.SearchAdvanced(string q, string site)
        {
            return _stubs.GetMethodStub<SearchAdvanced_String_String_Delegate>("SearchAdvanced").Invoke(q, site);
        }

        public delegate global::System.Threading.Tasks.Task<global::StackExchange.Windows.Api.Response<global::StackExchange.Windows.Api.Models.Question>> SearchAdvanced_String_String_Delegate(string q, string site);

        public StubISearchApi SearchAdvanced(SearchAdvanced_String_String_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}

namespace StackExchange.Windows.Common.SearchBox
{
    [CompilerGenerated]
    public class StubISearchViewModel : ISearchViewModel
    {
        private readonly StubContainer<StubISearchViewModel> _stubs = new StubContainer<StubISearchViewModel>();

        global::StackExchange.Windows.Common.SearchBox.SiteViewModel global::StackExchange.Windows.Common.SearchBox.ISearchViewModel.SelectedSite
        {
            get
            {
                return _stubs.GetMethodStub<SelectedSite_Get_Delegate>("get_SelectedSite").Invoke();
            }

            set
            {
                _stubs.GetMethodStub<SelectedSite_Set_Delegate>("set_SelectedSite").Invoke(value);
            }
        }

        global::ReactiveUI.ReactiveList<global::StackExchange.Windows.Common.SearchBox.SiteViewModel> global::StackExchange.Windows.Common.SearchBox.ISearchViewModel.AvailableSites
        {
            get
            {
                return _stubs.GetMethodStub<AvailableSites_Get_Delegate>("get_AvailableSites").Invoke();
            }
        }

        global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> global::StackExchange.Windows.Common.SearchBox.ISearchViewModel.LoadSites
        {
            get
            {
                return _stubs.GetMethodStub<LoadSites_Get_Delegate>("get_LoadSites").Invoke();
            }
        }

        global::ReactiveUI.ReactiveList<global::StackExchange.Windows.Questions.QuestionItemViewModel> global::StackExchange.Windows.Common.SearchBox.ISearchViewModel.SuggestedQuestions
        {
            get
            {
                return _stubs.GetMethodStub<SuggestedQuestions_Get_Delegate>("get_SuggestedQuestions").Invoke();
            }
        }

        global::ReactiveUI.ReactiveCommand<global::StackExchange.Windows.Questions.QuestionItemViewModel, global::System.Reactive.Unit> global::StackExchange.Windows.Common.SearchBox.ISearchViewModel.DisplayQuestion
        {
            get
            {
                return _stubs.GetMethodStub<DisplayQuestion_Get_Delegate>("get_DisplayQuestion").Invoke();
            }
        }

        string global::StackExchange.Windows.Common.SearchBox.ISearchViewModel.Query
        {
            get
            {
                return _stubs.GetMethodStub<Query_Get_Delegate>("get_Query").Invoke();
            }

            set
            {
                _stubs.GetMethodStub<Query_Set_Delegate>("set_Query").Invoke(value);
            }
        }

        public delegate global::StackExchange.Windows.Common.SearchBox.SiteViewModel SelectedSite_Get_Delegate();

        public StubISearchViewModel SelectedSite_Get(SelectedSite_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate void SelectedSite_Set_Delegate(global::StackExchange.Windows.Common.SearchBox.SiteViewModel value);

        public StubISearchViewModel SelectedSite_Set(SelectedSite_Set_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::ReactiveUI.ReactiveList<global::StackExchange.Windows.Common.SearchBox.SiteViewModel> AvailableSites_Get_Delegate();

        public StubISearchViewModel AvailableSites_Get(AvailableSites_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> LoadSites_Get_Delegate();

        public StubISearchViewModel LoadSites_Get(LoadSites_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::ReactiveUI.ReactiveList<global::StackExchange.Windows.Questions.QuestionItemViewModel> SuggestedQuestions_Get_Delegate();

        public StubISearchViewModel SuggestedQuestions_Get(SuggestedQuestions_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::ReactiveUI.ReactiveCommand<global::StackExchange.Windows.Questions.QuestionItemViewModel, global::System.Reactive.Unit> DisplayQuestion_Get_Delegate();

        public StubISearchViewModel DisplayQuestion_Get(DisplayQuestion_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate string Query_Get_Delegate();

        public StubISearchViewModel Query_Get(Query_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate void Query_Set_Delegate(string value);

        public StubISearchViewModel Query_Set(Query_Set_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}