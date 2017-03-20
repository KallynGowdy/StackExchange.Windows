using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanizer;
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
        private PostViewModel[] answers = new PostViewModel[0];
        private string answersTitle = "";
        public string Id { get; }
        public INetworkApi NetworkApi { get; }

        public string Title { get; }
        public string[] Tags { get; }
        public PostViewModel Question { get; }
        public ReactiveCommand<Unit, Unit> LoadAnswers { get; }

        public string AnswersTitle
        {
            get { return answersTitle; }
            private set { this.RaiseAndSetIfChanged(ref answersTitle, value); }
        }

        public PostViewModel[] Answers
        {
            get { return answers; }
            private set
            {
                this.RaiseAndSetIfChanged(ref answers, value);
                AnswersTitle = "Answers".ToQuantity(Answers.Length);
            }
        }

        public QuestionPageViewModel(Question question, IApplicationViewModel application = null, INetworkApi networkApi = null)
            : base(application)
        {
            NetworkApi = networkApi ?? Api<INetworkApi>();
            LoadAnswers = ReactiveCommand.CreateFromTask(LoadAnswersImpl, outputScheduler: RxApp.MainThreadScheduler);

            Title = question.DecodedTitle;
            Tags = question.Tags;
            Id = question.QuestionId.ToString();
            Question = new PostViewModel(question);
        }

        private async Task LoadAnswersImpl()
        {
            var result = await NetworkApi.QuestionAnswers(Id, Application.CurrentSite);
            Answers = result.Items.Select(answer => new PostViewModel(answer)).ToArray();
        }
    }
}
