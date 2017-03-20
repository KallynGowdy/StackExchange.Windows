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
        public StubINetworkApi NetworkApi { get; set; }
        public Question Question { get; set; }
        public StubIApplicationViewModel Application { get; set; }

        public QuestionPageViewModelTests()
        {
            Question = new Question();
            NetworkApi = new StubINetworkApi();
            Application = new StubIApplicationViewModel();
            Subject = new QuestionPageViewModel(Question.QuestionId, Application, NetworkApi);

            Application.CurrentSite_Get(() => "test_site");
        }

        [Fact]
        public async Task Test_LoadAnswers_Makes_A_Request_To_The_Api()
        {
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

            NetworkApi.QuestionAnswers((ids, site, order, sort, page, pagesize, filter) => Task.FromResult(new Response<Answer>()
            {
                Items = answers
            }));

            await Subject.LoadAnswers.Execute();

            Assert.Collection(Subject.Answers,
                a => Assert.Equal("5", a.Score),
                a => Assert.Equal("-1", a.Score));
        }

        [Theory]
        [InlineData(0, "0 Answers")]
        [InlineData(1, "1 Answer")]
        [InlineData(2, "2 Answers")]
        public async Task Test_AnswersTitle_Pluralizes_The_Label_With_The_Number_Of_Answers_That_Are_Present(int numAnswers, string expected)
        {
            var answers = Enumerable.Range(0, numAnswers)
                .Select(i => new Answer()
                {
                    Score = i
                })
                .ToArray();

            NetworkApi.QuestionAnswers((ids, site, order, sort, page, pagesize, filter) => Task.FromResult(new Response<Answer>()
            {
                Items = answers
            }));

            await Subject.LoadAnswers.Execute();

            Assert.Equal(expected, Subject.AnswersTitle);
        }

        [Fact]
        public async Task Test_LoadQuestions_Makes_A_Request_To_The_Api()
        {
            var questions = new[]
            {
                new Question()
                {
                    Score = 5
                },
                new Question()
                {
                    Score = -1
                }
            };

            NetworkApi.Question((ids, site, filter) => Task.FromResult(new Response<Question>()
            {
                Items = questions
            }));

            await Subject.LoadQuestions.Execute();

            Assert.Equal("5", Subject.Question.Score);
        }

        [Fact]
        public void Test_Question_Is_Not_Null_By_Default()
        {
            Assert.NotNull(Subject.Question);
        }
    }
}
