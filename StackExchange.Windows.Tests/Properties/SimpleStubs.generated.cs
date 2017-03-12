using System;
using System.Runtime.CompilerServices;
using Etg.SimpleStubs;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;
using StackExchange.Windows.Api.Models;

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