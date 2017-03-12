using System;
using System.Runtime.CompilerServices;
using Etg.SimpleStubs;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;
using StackExchange.Windows.Api.Models;
using System.Reactive;
using ReactiveUI;

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

namespace StackExchange.Windows.Search.SearchBox
{
    [CompilerGenerated]
    public class StubISearchViewModel : ISearchViewModel
    {
        private readonly StubContainer<StubISearchViewModel> _stubs = new StubContainer<StubISearchViewModel>();

        global::StackExchange.Windows.Search.SearchBox.SiteViewModel global::StackExchange.Windows.Search.SearchBox.ISearchViewModel.SelectedSite
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

        global::ReactiveUI.ReactiveList<global::StackExchange.Windows.Search.SearchBox.SiteViewModel> global::StackExchange.Windows.Search.SearchBox.ISearchViewModel.AvailableSites
        {
            get
            {
                return _stubs.GetMethodStub<AvailableSites_Get_Delegate>("get_AvailableSites").Invoke();
            }
        }

        global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> global::StackExchange.Windows.Search.SearchBox.ISearchViewModel.LoadSites
        {
            get
            {
                return _stubs.GetMethodStub<LoadSites_Get_Delegate>("get_LoadSites").Invoke();
            }
        }

        public delegate global::StackExchange.Windows.Search.SearchBox.SiteViewModel SelectedSite_Get_Delegate();

        public StubISearchViewModel SelectedSite_Get(SelectedSite_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate void SelectedSite_Set_Delegate(global::StackExchange.Windows.Search.SearchBox.SiteViewModel value);

        public StubISearchViewModel SelectedSite_Set(SelectedSite_Set_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::ReactiveUI.ReactiveList<global::StackExchange.Windows.Search.SearchBox.SiteViewModel> AvailableSites_Get_Delegate();

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
    }
}