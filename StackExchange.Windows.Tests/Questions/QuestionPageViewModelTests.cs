using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Windows.Api;
using StackExchange.Windows.Api.Models;
using StackExchange.Windows.Application;
using StackExchange.Windows.Common.SearchBox;
using StackExchange.Windows.Questions;
using Xunit;

namespace StackExchange.Windows.Tests.Questions
{
    public class QuestionPageViewModelTests
    {

        public QuestionPageViewModel Subject { get; set; }

        [Fact]
        public async Task Test_LoadAnswers_Makes_A_Request_To_The_API()
        {
            // TODO: Revisit dependency structure so that 
            //       test setup doesn't require so many extra stubs.
            var answers = new[]
            {
                new Answer()
                {
                    Score = 5
                },
                new Answer()
                {
                    Score = -1
                }
            };
            var search = new StubISearchViewModel();
            var application = new ApplicationViewModel(search);
            var question = new Question();
            var questionsApi = new StubIQuestionsApi();
            Subject = new QuestionPageViewModel(question, application, questionsApi);

            search.SelectedSite_Get(() => new SiteViewModel(new Site()
            {
                ApiSiteParameter = "stackoverflow"
            }));
            questionsApi.QuestionAnswers((ids, site, order, sort, page, pagesize, filter) => Task.FromResult(new Response<Answer>()
            {
                Items = answers
            }));

            await Subject.LoadAnswers.Execute();

            Assert.Collection(Subject.Answers,
                a => Assert.Equal("5", a.Score),
                a => Assert.Equal("-1", a.Score));
        }
    }
}
