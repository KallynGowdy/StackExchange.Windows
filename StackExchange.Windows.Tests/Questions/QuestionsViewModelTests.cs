using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reactive.Testing;
using ReactiveUI;
using ReactiveUI.Testing;
using StackExchange.Windows.Api;
using StackExchange.Windows.Api.Models;
using StackExchange.Windows.Application;
using StackExchange.Windows.Common.SearchBox;
using StackExchange.Windows.Questions;
using Xunit;

namespace StackExchange.Windows.Tests.Questions
{
    public class QuestionsViewModelTests
    {
        public QuestionsViewModel Subject { get; set; }
        public StubISearchViewModel Search { get; set; }
        public ReactiveCommand<Unit, Unit> LoadSites { get; set; } = ReactiveCommand.Create(() => { });
        public StubINetworkApi NetworkApi { get; set; }
        public SiteViewModel Site { get; set; }

        public QuestionsViewModelTests()
        {
            NetworkApi = new StubINetworkApi();
            Search = new StubISearchViewModel();
            Subject = new QuestionsViewModel(search: Search, networkApi: NetworkApi);

            Search.SelectedSite_Set(site => Site = site);
            Search.SelectedSite_Get(() => Site);
            Search.LoadSites_Get(() => LoadSites);
        }

        [Fact]
        public async Task Test_Clear_Removes_Questions_From_The_Questions_List()
        {
            Subject.Questions.Add(new QuestionItemViewModel());

            await Subject.Clear.Execute();

            Assert.Empty(Subject.Questions);
        }

        [Fact]
        public async Task Test_Refresh_Clears_And_Loads_Questions()
        {
            Site = new SiteViewModel(new Site());
            Subject.Questions.Add(new QuestionItemViewModel());
            NetworkApi.Questions(
                (site, order, sort, page, pagesize, filter) => Task.FromResult(new Response<Question>()
                {
                    Items = new[]
                    {
                        new Question()
                        {
                            Title = "Test",
                            Owner = new ShallowUser()
                            {
                                ProfileImage = "https://example.com"
                            }
                        },
                    }
                }));

            await Subject.Refresh.Execute();

            Assert.Collection(Subject.Questions,
                q => Assert.Equal("Test", q.Title));
        }

        [Fact]
        public async Task Test_LoadQuestions_Executes_LoadSites_If_There_Is_No_Currently_Selected_Site()
        {
            Site = null;
            NetworkApi.Questions((site, order, sort, page, pagesize, filter) => Task.FromResult(new Response<Question>()));

            LoadSites = ReactiveCommand.Create(() =>
            {
                Site = new SiteViewModel();
            });

            await Subject.LoadQuestions.Execute();

            Assert.NotNull(Site);
        }

        [Fact]
        public Task Test_LoadQuestions_Waits_For_LoadSites_To_Finish_Executing_If_No_Site_Is_Selected_But_Is_Already_Executing()
        {
            // TODO: Clean up test implementation?
            var loadSitesTask = new TaskCompletionSource<Unit>();
            var testTask = new TaskCompletionSource<int>();
            Site = null;
            LoadSites = ReactiveCommand.CreateFromTask(() => loadSitesTask.Task);
            NetworkApi.Questions(
                (site, order, sort, page, pagesize, filter) => Task.FromResult(new Response<Question>()
                {
                    Items = new[]
                    {
                        new Question()
                        {
                            Owner = new ShallowUser()
                            {
                                DisplayName = "Test",
                                ProfileImage= "http://example.com"
                            }
                        },
                    }
                }));

            // Act
            LoadSites.Execute().Subscribe();

            var loadingObservable = Subject.LoadQuestions.Execute();
            loadingObservable.Subscribe(u =>
            {
                try
                {
                    // Assert
                    Assert.NotEmpty(Subject.Questions);
                    testTask.SetResult(0);
                }
                catch (Exception ex)
                {
                    testTask.SetException(ex);
                }
            }, ex => testTask.SetException(ex));

            // Precondition
            Assert.Empty(Subject.Questions);

            Site = new SiteViewModel();
            loadSitesTask.SetResult(Unit.Default);

            return testTask.Task;
        }

        [Fact]
        public async Task Test_LoadQuestions_Loads_From_Currently_Selected_Site()
        {
            var called = false;
            Site = new SiteViewModel(new Site()
            {
                ApiSiteParameter = "stackoverflow"
            });

            NetworkApi.Questions((site, order, sort, page, pagesize, filter) =>
            {
                Assert.Equal("stackoverflow", site);
                called = true;
                return Task.FromResult(new Response<Question>());
            });

            await Subject.LoadQuestions.Execute();

            Assert.True(called);
        }

        [Fact]
        public void Test_Refreshes_Questions_When_Site_Is_Changed_After_Activation()
        {
            NetworkApi.Questions((site, order, sort, page, pagesize, filter) =>
            {
                Assert.Equal("othersite", site);
                return Task.FromResult(new Response<Question>());
            });
            var api = new StubINetworkApi();
            var search = new SearchViewModel(networkApi: api);
            Subject = new QuestionsViewModel(search: search, networkApi: NetworkApi);
            Subject.Questions.Add(new QuestionItemViewModel());

            using (Subject.Activator.Activate())
            {
                search.SelectedSite = new SiteViewModel(new Site()
                {
                    ApiSiteParameter = "othersite"
                });

                Assert.Empty(Subject.Questions);
            }
        }

        [Fact]
        public async Task Test_DisplayQuestion_Calls_Navigate_With_The_Given_QuestionViewModel()
        {
            var application = new ApplicationViewModel();
            var question = new Question();
            var questionViewModel = new QuestionItemViewModel(question);
            Subject = new QuestionsViewModel(application, Search, NetworkApi);

            using (application.Navigate.RegisterHandler(ctx =>
            {
                Assert.Same(question, ctx.Input.Parameter);
                ctx.SetOutput(Unit.Default);
            }))
            {
                await Subject.DisplayQuestion.Execute(questionViewModel);
            }
        }

        [Fact]
        public async Task Test_LoadQuestions_Does_Not_Reload_When_Questions_Already_Exist()
        {
            Site = new SiteViewModel(new Site()
            {
                ApiSiteParameter = "stackoverflow"
            });

            Subject.LoadedSite = "stackoverflow";

            var question = new QuestionItemViewModel();
            Subject.Questions.Add(question);

            await Subject.LoadQuestions.Execute();

            Assert.Collection(Subject.Questions,
                q => Assert.Same(question, q));
        }

        [Fact]
        public async Task Test_LoadQuestions_Does_Reload_If_The_Site_Is_Different_Than_The_Current_Questions()
        {
            Site = new SiteViewModel(new Site()
            {
                ApiSiteParameter = "stackoverflow"
            });

            NetworkApi.Questions((site, order, sort, page, pagesize, filter) =>
            {
                if (site == "stackoverflow")
                {
                    return Task.FromResult(new Response<Question>()
                    {
                        Items = new[]
                        {
                            new Question()
                            {
                                Title = "First"
                            },
                        }
                    });
                }
                else
                {
                    return Task.FromResult(new Response<Question>()
                    {
                        Items = new[]
                        {
                            new Question()
                            {
                                Title = "Second"
                            },
                        }
                    });
                }
            });

            await Subject.LoadQuestions.Execute();

            Assert.Collection(Subject.Questions,
                q => Assert.Equal("First", q.Title));

            Site = new SiteViewModel(new Site()
            {
                ApiSiteParameter = "other"
            });

            await Subject.LoadQuestions.Execute();

            Assert.Collection(Subject.Questions,
                q => Assert.Equal("Second", q.Title));
        }
    }
}
