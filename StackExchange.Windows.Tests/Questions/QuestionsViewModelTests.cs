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
        public ReactiveCommand<Unit, Unit> LoadSites { get; set; }
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
        public void Test_LoadQuestions_Waits_For_LoadSites_To_Finish_Executing_If_No_Site_Is_Selected_But_Is_Already_Executing()
        {
            new TestScheduler().With(scheduler =>
            {
                Site = null;
                QuestionsApi.Questions(
                    (site, order, sort, page, pagesize, filter) => Task.FromResult(new Response<Question>()
                    {
                        Items = new[]
                        {
                            new Question(),
                        }
                    }));
                LoadSites = ReactiveCommand.CreateFromObservable(() =>
                    Observable.Return(Unit.Default, RxApp.MainThreadScheduler)
                        .Delay(TimeSpan.FromMilliseconds(1000), RxApp.MainThreadScheduler)
                        .Do(u => Site = new SiteViewModel()), outputScheduler: RxApp.MainThreadScheduler);

                LoadSites.Execute().Subscribe();

                scheduler.AdvanceToMs(1);

                var loadingObservable = Subject.LoadQuestions.Execute();
                loadingObservable.Subscribe();

                Assert.Null(Site);
                Assert.Empty(Subject.Questions);

                scheduler.AdvanceToMs(1001);

                Assert.NotNull(Site);

                scheduler.AdvanceToMs(2000);

                Assert.NotEmpty(Subject.Questions);
            });
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
