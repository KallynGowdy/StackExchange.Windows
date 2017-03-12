using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Windows.Api;
using StackExchange.Windows.Api.Models;
using StackExchange.Windows.Search.SearchBox;
using Xunit;

namespace StackExchange.Windows.Tests.Search.SearchBox
{
    public class SearchViewModelTests
    {
        public SearchViewModel Subject { get; set; }
        public StubINetworkApi NetworkApi { get; set; }

        public SearchViewModelTests()
        {
            NetworkApi = new StubINetworkApi();
            Subject = new SearchViewModel(NetworkApi);
        }

        [Fact]
        public void Test_Starts_With_Empty_Query()
        {
            Assert.Equal("", Subject.Query);
        }

        [Fact]
        public async Task Test_LoadSites_Loads_A_List_Of_Sites_From_The_Api()
        {
            NetworkApi.Sites(() => Task.FromResult(new Response<Site>()
            {
                Items = new[] {
                    new Site()
                    {
                        ApiSiteParameter = "stackoverflow",
                        Name = "Stack Overflow"
                    },
                    new Site()
                    {
                        ApiSiteParameter = "superuser",
                        Name = "Super User"
                    }
                },
                HasMore = false
            }));

            await Subject.LoadSites.Execute();

            Assert.Collection(Subject.AvailableSites,
                s =>
                {
                    Assert.Equal("Stack Overflow", s.Name);
                    Assert.Equal("stackoverflow", s.ApiSiteParameter);
                },
                s =>
                {
                    Assert.Equal("Super User", s.Name);
                    Assert.Equal("superuser", s.ApiSiteParameter);
                });
        }
    }
}
