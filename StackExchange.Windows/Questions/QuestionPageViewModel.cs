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
        private QuestionDetailViewModel question;
        public INetworkApi NetworkApi { get; }

        public QuestionDetailViewModel Question
        {
            get { return question; }
            private set { this.RaiseAndSetIfChanged(ref question, value); }
        }

        public ReactiveCommand<Unit, Unit> LoadAnswers { get; }
        public ReactiveCommand<Unit, Unit> LoadQuestions { get; }
        public CombinedReactiveCommand<Unit, Unit> Load { get; }

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

        public QuestionPageViewModel(int questionId, IApplicationViewModel application = null, INetworkApi networkApi = null)
            : base(application)
        {
            NetworkApi = networkApi ?? Api<INetworkApi>();
            LoadAnswers = ReactiveCommand.CreateFromTask(LoadAnswersImpl, outputScheduler: RxApp.MainThreadScheduler);
            LoadQuestions = ReactiveCommand.CreateFromTask(LoadQuestionsImpl, outputScheduler: RxApp.MainThreadScheduler);
            Load = ReactiveCommand.CreateCombined(new[] { LoadQuestions, LoadAnswers });

            Question = new QuestionDetailViewModel(questionId);
        }

        private async Task LoadQuestionsImpl()
        {
            var result = await NetworkApi.QuestionWithDetail(Question.Id, Application.CurrentSite);
            Question = new QuestionDetailViewModel(result.Items.First());
        }

        private async Task LoadAnswersImpl()
        {
            var result = await NetworkApi.QuestionAnswersWithDetail(Question.Id, Application.CurrentSite);
            Answers = result.Items.Select(answer => new PostViewModel(answer)).ToArray();
        }
    }
}
