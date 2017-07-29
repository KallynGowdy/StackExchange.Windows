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
using StackExchange.Windows.Authentication;
using System.Windows.Input;
using StackExchange.Windows.Questions;
using Windows.ApplicationModel.DataTransfer;

namespace StackExchange.Windows.Api
{
    [CompilerGenerated]
    public class StubINetworkApi : INetworkApi
    {
        private readonly StubContainer<StubINetworkApi> _stubs = new StubContainer<StubINetworkApi>();

        global::System.Threading.Tasks.Task<global::StackExchange.Windows.Api.Response<global::StackExchange.Windows.Api.Models.Site>> global::StackExchange.Windows.Api.INetworkApi.Sites(int page, int pagesize)
        {
            return _stubs.GetMethodStub<Sites_Int32_Int32_Delegate>("Sites").Invoke(page, pagesize);
        }

        public delegate global::System.Threading.Tasks.Task<global::StackExchange.Windows.Api.Response<global::StackExchange.Windows.Api.Models.Site>> Sites_Int32_Int32_Delegate(int page, int pagesize);

        public StubINetworkApi Sites(Sites_Int32_Int32_Delegate del, int count = Times.Forever, bool overwrite = false)
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

        global::System.Threading.Tasks.Task<global::StackExchange.Windows.Api.Response<global::StackExchange.Windows.Api.Models.Question>> global::StackExchange.Windows.Api.INetworkApi.Questions(string site, string order, string sort, int page, int pagesize, string filter)
        {
            return _stubs.GetMethodStub<Questions_String_String_String_Int32_Int32_String_Delegate>("Questions").Invoke(site, order, sort, page, pagesize, filter);
        }

        public delegate global::System.Threading.Tasks.Task<global::StackExchange.Windows.Api.Response<global::StackExchange.Windows.Api.Models.Question>> Questions_String_String_String_Int32_Int32_String_Delegate(string site, string order, string sort, int page, int pagesize, string filter);

        public StubINetworkApi Questions(Questions_String_String_String_Int32_Int32_String_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.Threading.Tasks.Task<global::StackExchange.Windows.Api.Response<global::StackExchange.Windows.Api.Models.Question>> global::StackExchange.Windows.Api.INetworkApi.Question(string ids, string site, string filter)
        {
            return _stubs.GetMethodStub<Question_String_String_String_Delegate>("Question").Invoke(ids, site, filter);
        }

        public delegate global::System.Threading.Tasks.Task<global::StackExchange.Windows.Api.Response<global::StackExchange.Windows.Api.Models.Question>> Question_String_String_String_Delegate(string ids, string site, string filter);

        public StubINetworkApi Question(Question_String_String_String_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.Threading.Tasks.Task<global::StackExchange.Windows.Api.Response<global::StackExchange.Windows.Api.Models.Answer>> global::StackExchange.Windows.Api.INetworkApi.QuestionAnswers(string questionIds, string site, string order, string sort, int page, int pagesize, string filter)
        {
            return _stubs.GetMethodStub<QuestionAnswers_String_String_String_String_Int32_Int32_String_Delegate>("QuestionAnswers").Invoke(questionIds, site, order, sort, page, pagesize, filter);
        }

        public delegate global::System.Threading.Tasks.Task<global::StackExchange.Windows.Api.Response<global::StackExchange.Windows.Api.Models.Answer>> QuestionAnswers_String_String_String_String_Int32_Int32_String_Delegate(string questionIds, string site, string order, string sort, int page, int pagesize, string filter);

        public StubINetworkApi QuestionAnswers(QuestionAnswers_String_String_String_String_Int32_Int32_String_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.Threading.Tasks.Task<global::StackExchange.Windows.Api.Response<global::StackExchange.Windows.Api.Models.Question>> global::StackExchange.Windows.Api.INetworkApi.SearchAdvanced(string q, string site, string filter)
        {
            return _stubs.GetMethodStub<SearchAdvanced_String_String_String_Delegate>("SearchAdvanced").Invoke(q, site, filter);
        }

        public delegate global::System.Threading.Tasks.Task<global::StackExchange.Windows.Api.Response<global::StackExchange.Windows.Api.Models.Question>> SearchAdvanced_String_String_String_Delegate(string q, string site, string filter);

        public StubINetworkApi SearchAdvanced(SearchAdvanced_String_String_String_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}

namespace StackExchange.Windows.Application
{
    [CompilerGenerated]
    public class StubIApplicationViewModel : IApplicationViewModel
    {
        private readonly StubContainer<StubIApplicationViewModel> _stubs = new StubContainer<StubIApplicationViewModel>();

        global::StackExchange.Windows.Authentication.IAuthenticationViewModel global::StackExchange.Windows.Application.IApplicationViewModel.Authentication
        {
            get
            {
                return _stubs.GetMethodStub<Authentication_Get_Delegate>("get_Authentication").Invoke();
            }
        }

        global::ReactiveUI.Interaction<global::StackExchange.Windows.Application.NavigationParams, global::System.Reactive.Unit> global::StackExchange.Windows.Application.IApplicationViewModel.Navigate
        {
            get
            {
                return _stubs.GetMethodStub<Navigate_Get_Delegate>("get_Navigate").Invoke();
            }
        }

        global::ReactiveUI.Interaction<global::System.Reactive.Unit, global::System.Reactive.Unit> global::StackExchange.Windows.Application.IApplicationViewModel.NavigateBack
        {
            get
            {
                return _stubs.GetMethodStub<NavigateBack_Get_Delegate>("get_NavigateBack").Invoke();
            }
        }

        global::ReactiveUI.Interaction<global::StackExchange.Windows.Application.NavigationParams, global::System.Reactive.Unit> global::StackExchange.Windows.Application.IApplicationViewModel.NavigateAndClearStack
        {
            get
            {
                return _stubs.GetMethodStub<NavigateAndClearStack_Get_Delegate>("get_NavigateAndClearStack").Invoke();
            }
        }

        global::ReactiveUI.Interaction<global::System.Uri, global::System.Reactive.Unit> global::StackExchange.Windows.Application.IApplicationViewModel.OpenUri
        {
            get
            {
                return _stubs.GetMethodStub<OpenUri_Get_Delegate>("get_OpenUri").Invoke();
            }
        }

        string global::StackExchange.Windows.Application.IApplicationViewModel.CurrentSite
        {
            get
            {
                return _stubs.GetMethodStub<CurrentSite_Get_Delegate>("get_CurrentSite").Invoke();
            }
        }

        public delegate global::StackExchange.Windows.Authentication.IAuthenticationViewModel Authentication_Get_Delegate();

        public StubIApplicationViewModel Authentication_Get(Authentication_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::ReactiveUI.Interaction<global::StackExchange.Windows.Application.NavigationParams, global::System.Reactive.Unit> Navigate_Get_Delegate();

        public StubIApplicationViewModel Navigate_Get(Navigate_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::ReactiveUI.Interaction<global::System.Reactive.Unit, global::System.Reactive.Unit> NavigateBack_Get_Delegate();

        public StubIApplicationViewModel NavigateBack_Get(NavigateBack_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::ReactiveUI.Interaction<global::StackExchange.Windows.Application.NavigationParams, global::System.Reactive.Unit> NavigateAndClearStack_Get_Delegate();

        public StubIApplicationViewModel NavigateAndClearStack_Get(NavigateAndClearStack_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::ReactiveUI.Interaction<global::System.Uri, global::System.Reactive.Unit> OpenUri_Get_Delegate();

        public StubIApplicationViewModel OpenUri_Get(OpenUri_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate string CurrentSite_Get_Delegate();

        public StubIApplicationViewModel CurrentSite_Get(CurrentSite_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        TService global::StackExchange.Windows.Application.IApplicationViewModel.Api<TService>()
        {
            return _stubs.GetMethodStub<Api_Delegate<TService>>("Api<TService>").Invoke();
        }

        public delegate TService Api_Delegate<TService>();

        public StubIApplicationViewModel Api<TService>(Api_Delegate<TService> del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}

namespace StackExchange.Windows.Authentication
{
    [CompilerGenerated]
    public class StubIAuthenticationViewModel : IAuthenticationViewModel
    {
        private readonly StubContainer<StubIAuthenticationViewModel> _stubs = new StubContainer<StubIAuthenticationViewModel>();

        global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> global::StackExchange.Windows.Authentication.IAuthenticationViewModel.Login
        {
            get
            {
                return _stubs.GetMethodStub<Login_Get_Delegate>("get_Login").Invoke();
            }
        }

        global::ReactiveUI.Interaction<global::System.Uri, global::System.Uri> global::StackExchange.Windows.Authentication.IAuthenticationViewModel.RedirectToLogin
        {
            get
            {
                return _stubs.GetMethodStub<RedirectToLogin_Get_Delegate>("get_RedirectToLogin").Invoke();
            }
        }

        string global::StackExchange.Windows.Authentication.IAuthenticationViewModel.Token
        {
            get
            {
                return _stubs.GetMethodStub<Token_Get_Delegate>("get_Token").Invoke();
            }
        }

        public delegate global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> Login_Get_Delegate();

        public StubIAuthenticationViewModel Login_Get(Login_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::ReactiveUI.Interaction<global::System.Uri, global::System.Uri> RedirectToLogin_Get_Delegate();

        public StubIAuthenticationViewModel RedirectToLogin_Get(RedirectToLogin_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate string Token_Get_Delegate();

        public StubIAuthenticationViewModel Token_Get(Token_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        bool global::StackExchange.Windows.Authentication.IAuthenticationViewModel.IsSuccessUrl(global::System.Uri url)
        {
            return _stubs.GetMethodStub<IsSuccessUrl_Uri_Delegate>("IsSuccessUrl").Invoke(url);
        }

        public delegate bool IsSuccessUrl_Uri_Delegate(global::System.Uri url);

        public StubIAuthenticationViewModel IsSuccessUrl(IsSuccessUrl_Uri_Delegate del, int count = Times.Forever, bool overwrite = false)
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

        global::ReactiveUI.ReactiveCommand<string, global::System.Reactive.Unit> global::StackExchange.Windows.Common.SearchBox.ISearchViewModel.SetQueryAndFocus
        {
            get
            {
                return _stubs.GetMethodStub<SetQueryAndFocus_Get_Delegate>("get_SetQueryAndFocus").Invoke();
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

        global::ReactiveUI.Interaction<global::System.Reactive.Unit, global::System.Reactive.Unit> global::StackExchange.Windows.Common.SearchBox.ISearchViewModel.FocusSearchBox
        {
            get
            {
                return _stubs.GetMethodStub<FocusSearchBox_Get_Delegate>("get_FocusSearchBox").Invoke();
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

        public delegate global::ReactiveUI.ReactiveCommand<string, global::System.Reactive.Unit> SetQueryAndFocus_Get_Delegate();

        public StubISearchViewModel SetQueryAndFocus_Get(SetQueryAndFocus_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
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

        public delegate global::ReactiveUI.Interaction<global::System.Reactive.Unit, global::System.Reactive.Unit> FocusSearchBox_Get_Delegate();

        public StubISearchViewModel FocusSearchBox_Get(FocusSearchBox_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}

namespace StackExchange.Windows.Services
{
    [CompilerGenerated]
    public class StubIClipboard : IClipboard
    {
        private readonly StubContainer<StubIClipboard> _stubs = new StubContainer<StubIClipboard>();

        void global::StackExchange.Windows.Services.IClipboard.SetContent(global::Windows.ApplicationModel.DataTransfer.DataPackage data)
        {
            _stubs.GetMethodStub<SetContent_DataPackage_Delegate>("SetContent").Invoke(data);
        }

        public delegate void SetContent_DataPackage_Delegate(global::Windows.ApplicationModel.DataTransfer.DataPackage data);

        public StubIClipboard SetContent(SetContent_DataPackage_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}