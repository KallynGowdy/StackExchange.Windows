using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using StackExchange.Windows.Api;
using StackExchange.Windows.Api.Models;
using StackExchange.Windows.Application;
using StackExchange.Windows.Common.PostDetail;
using StackExchange.Windows.Html;
using StackExchange.Windows.User.UserCard;

namespace StackExchange.Windows.Questions
{
    /// <summary>
    /// Defines a view model that is able to present a question with it's answers in a full page.
    /// </summary>
    public class QuestionPageViewModel : BaseViewModel
    {
        private readonly ObservableAsPropertyHelper<PostViewModel[]> answers;
        public string Id { get; }
        public IQuestionsApi QuestionsApi { get; }

        public string Title { get; }
        public string[] Tags { get; }
        public PostViewModel Question { get; }
        public ReactiveCommand<Unit, PostViewModel[]> LoadAnswers { get; }

        public PostViewModel[] Answers => answers.Value;

        public QuestionPageViewModel(Question question, ApplicationViewModel application = null, IQuestionsApi questionsApi = null)
            : base(application)
        {
            QuestionsApi = questionsApi ?? Api<IQuestionsApi>();
            LoadAnswers = ReactiveCommand.CreateFromTask(LoadAnswersImpl, outputScheduler: RxApp.MainThreadScheduler);

            answers = LoadAnswers.ToProperty(this, vm => vm.Answers, initialValue: new PostViewModel[0]);
            Title = question.DecodedTitle;
            Tags = question.Tags;
            Id = question.QuestionId.ToString();
            Question = new PostViewModel(question);
        }

        private async Task<PostViewModel[]> LoadAnswersImpl()
        {
            var result = await QuestionsApi.QuestionAnswers(Id, Application.CurrentSite);
            return result.Items.Select(answer => new PostViewModel(answer)).ToArray();
        }
    }
}
