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
using StackExchange.Windows.Questions;
using StackExchange.Windows.Search.SearchBox;
using Xunit;

namespace StackExchange.Windows.Tests.Questions
{
    public class QuestionsViewModelTests
    {
        public QuestionsViewModel Subject { get; set; }
        public StubISearchViewModel Search { get; set; }
        public ReactiveCommand<Unit, Unit> LoadSites { get; set; } = ReactiveCommand.Create(() => { });
        public StubIQuestionsApi QuestionsApi { get; set; }
        public SiteViewModel Site { get; set; }

        public QuestionsViewModelTests()
        {
            QuestionsApi = new StubIQuestionsApi();
            Search = new StubISearchViewModel();
            Subject = new QuestionsViewModel(search: Search, questionsApi: QuestionsApi);

            Search.SelectedSite_Set(site => Site = site);
            Search.SelectedSite_Get(() => Site);
            Search.LoadSites_Get(() => LoadSites);
        }

        [Fact]
        public async Task Test_Clear_Removes_Questions_From_The_Questions_List()
        {
            Subject.Questions.Add(new QuestionViewModel());

            await Subject.Clear.Execute();

            Assert.Empty(Subject.Questions);
        }

        [Fact]
        public async Task Test_Refresh_Clears_And_Loads_Questions()
        {
            Site = new SiteViewModel(new Site());
            Subject.Questions.Add(new QuestionViewModel());
            QuestionsApi.Questions(
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
            QuestionsApi.Questions((site, order, sort, page, pagesize, filter) => Task.FromResult(new Response<Question>()));

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
            QuestionsApi.Questions(
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
            Site = new SiteViewModel(new Site()
            {
                ApiSiteParameter = "stackoverflow"
            });

            QuestionsApi.Questions((site, order, sort, page, pagesize, filter) =>
            {
                Assert.Equal("stackoverflow", site);
                return Task.FromResult(new Response<Question>());
            });

            await Subject.LoadQuestions.Execute();
        }

    }
}
