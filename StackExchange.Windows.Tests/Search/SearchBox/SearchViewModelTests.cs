using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reactive.Testing;
using ReactiveUI.Testing;
using StackExchange.Windows.Api;
using StackExchange.Windows.Api.Models;
using StackExchange.Windows.Application;
using StackExchange.Windows.Common.SearchBox;
using StackExchange.Windows.Questions;
using Xunit;

namespace StackExchange.Windows.Tests.Search.SearchBox
{
    public class SearchViewModelTests
    {
        public SearchViewModel Subject { get; set; }
        public StubINetworkApi NetworkApi { get; set; }
        public StubISearchApi SearchApi { get; set; }

        public SearchViewModelTests()
        {
            NetworkApi = new StubINetworkApi();
            SearchApi = new StubISearchApi();
            Subject = new SearchViewModel(null, NetworkApi, SearchApi);
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
                        Name = "Stack Overflow",
                        Audience = "programming",
                        LogoUrl = "https://stackoverflow.com",
                        IconUrl = "https://stackoverflow.com/icon",
                        HighResolutionIconUrl = "https://stackoverflow.com/hi-res"
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
                    Assert.Equal("For programming", s.Audience);
                    Assert.Equal(new Uri("https://stackoverflow.com"), s.LogoUrl);
                    Assert.Equal(new Uri("https://stackoverflow.com/icon"), s.IconUrl);
                    Assert.Equal(new Uri("https://stackoverflow.com/hi-res"), s.HighResolutionIconUrl);
                },
                s =>
                {
                    Assert.Equal("Super User", s.Name);
                    Assert.Equal("superuser", s.ApiSiteParameter);
                    Assert.Equal("", s.Audience);
                    Assert.Null(s.LogoUrl);
                    Assert.Null(s.IconUrl);
                    Assert.Null(s.HighResolutionIconUrl);
                });
        }

        [Fact]
        public async Task Test_LoadSites_Selects_The_First_Returned_Site()
        {
            NetworkApi.Sites(() => Task.FromResult(new Response<Site>()
            {
                Items = new[] {
                    new Site()
                    {
                        ApiSiteParameter = "superuser",
                        Name = "Super User"
                    },
                    new Site()
                    {
                        ApiSiteParameter = "stackoverflow",
                        Name = "Stack Overflow"
                    }
                },
                HasMore = false
            }));

            await Subject.LoadSites.Execute();

            Assert.Equal("superuser", Subject.SelectedSite.ApiSiteParameter);
        }

        [Fact]
        public async Task Test_LoadAssociatedAccounts_Loads_A_List_Of_Sites_That_The_User_Has_Accounts_For()
        {
            NetworkApi.UserAssociatedAccounts(ids => Task.FromResult(new Response<NetworkUser>()
            {
                Items = new[]
                {
                    new NetworkUser()
                    {
                        AccountId = 1,
                        AnswerCount = 2,
                        Reputation = 100,
                        SiteName = "Stack Overflow",
                        SiteUrl = "http://stackoverflow.com",
                        UserId = 3
                    },
                    new NetworkUser()
                    {
                        AccountId = 1,
                        AnswerCount = 2,
                        Reputation = 200,
                        SiteName = "Super User",
                        SiteUrl = "http://superuser.com",
                        UserId = 3
                    },
                }
            }));

            await Subject.LoadAssociatedAccounts.Execute();

            Assert.Collection(Subject.AssociatedAccounts,
                account =>
                {
                    Assert.Equal("Super User", account.SiteName);
                    Assert.Equal(200, account.Reputation);
                },
                account =>
                {
                    Assert.Equal("Stack Overflow", account.SiteName);
                    Assert.Equal(100, account.Reputation);
                });
        }

        [Fact]
        public async Task Test_Search_Passes_The_Current_Query_To_The_Api()
        {
            SearchApi.SearchAdvanced((q, site, filter) =>
            {
                Assert.Equal("query", q);
                return Task.FromResult(new Response<Question>());
            });
            Subject.SelectedSite = new SiteViewModel(new Site()
            {
                ApiSiteParameter = "stackoverflow"
            });

            Subject.Query = "query";

            await Subject.Search.Execute();
        }

        [Fact]
        public async Task Test_Search_Fills_SuggestedQuestions_From_The_Returned_Search_Questions()
        {
            SearchApi.SearchAdvanced((q, site, filter) => Task.FromResult(new Response<Question>()
            {
                Items = new[]
                {
                    new Question()
                    {
                        QuestionId = 10,
                        Title = "Test",
                        Body = "Test",
                        Owner = new ShallowUser()
                        {
                            DisplayName = "user",
                            ProfileImage = "https://image.example.com"
                        }
                    },
                }
            }));
            Subject.SelectedSite = new SiteViewModel(new Site()
            {
                ApiSiteParameter = "stackoverflow"
            });

            Subject.Query = "query";

            await Subject.Search.Execute();

            Assert.Collection(Subject.SuggestedQuestions,
                q => Assert.Equal("Test", q.Title));
        }

        [Fact]
        public void Test_Automatically_Searches_When_Activated_After_Throttling_For_Half_A_Second()
        {
            SearchApi.SearchAdvanced((q, site, filter) => Task.FromResult(new Response<Question>()
            {
                Items = new[]
                {
                    new Question()
                    {
                        QuestionId = 10,
                        Title = "Test",
                        Body = "Test",
                        Owner = new ShallowUser()
                        {
                            DisplayName = "user",
                            ProfileImage = "https://image.example.com"
                        }
                    },
                }
            }));
            Subject.SelectedSite = new SiteViewModel(new Site()
            {
                ApiSiteParameter = "stackoverflow"
            });

            new TestScheduler().With(scheduler =>
            {
                using (Subject.Activator.Activate())
                {
                    scheduler.AdvanceToMs(1);

                    Subject.Query = "test";

                    Assert.Empty(Subject.SuggestedQuestions);

                    scheduler.AdvanceToMs(501);

                    Assert.Collection(Subject.SuggestedQuestions,
                        q => Assert.Equal("Test", q.Title));
                }
            });
        }

        [Theory]
        [InlineData("")]
        [InlineData("\n")]
        [InlineData(null)]
        public async Task Test_Search_Cannot_Execute_When_Query_Is_Null_Or_Whitespace(string query)
        {
            SearchApi.SearchAdvanced((q, site, filter) => Task.FromResult(new Response<Question>()));
            Subject.SelectedSite = new SiteViewModel(new Site()
            {
                ApiSiteParameter = "stackoverflow"
            });

            Subject.Query = query;

            Assert.False(await Subject.Search.CanExecute.FirstAsync());
        }

        [Theory]
        [InlineData("search")]
        public async Task Test_Search_Can_Execute_When_Query_Contains_Letters(string query)
        {
            SearchApi.SearchAdvanced((q, site, filter) => Task.FromResult(new Response<Question>()));
            Subject.SelectedSite = new SiteViewModel(new Site()
            {
                ApiSiteParameter = "stackoverflow"
            });

            Subject.Query = query;

            Assert.True(await Subject.Search.CanExecute.FirstAsync());
        }

        [Fact]
        public async Task Test_DisplayQuestion_Navigates_With_The_Given_QuestionViewModel()
        {
            var application = new ApplicationViewModel();
            var question = new Question();
            var questionViewModel = new QuestionItemViewModel(question);
            Subject = new SearchViewModel(application, NetworkApi, SearchApi);

            using (application.Navigate.RegisterHandler(ctx =>
            {
                Assert.Same(question, ctx.Input.Parameter);
                ctx.SetOutput(Unit.Default);
            }))
            {
                await Subject.DisplayQuestion.Execute(questionViewModel);
            }
        }
    }
}
