using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Refit;
using StackExchange.Windows.Api;
using StackExchange.Windows.Api.Models;

namespace StackExchange.Windows.Questions
{
    public class QuestionsViewModel : BaseViewModel
    {
        private IQuestionsApi QuestionsApi { get; }

        /// <summary>
        /// Gets the command that can load questions from the site.
        /// </summary>
        public ReactiveCommand<Unit, Unit> LoadQuestions { get; }

        /// <summary>
        /// Gets the command that can clear the loaded questions.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Clear { get; }

        /// <summary>
        /// Gets the command that can refresh the loaded questions on demand.
        /// </summary>
        public CombinedReactiveCommand<Unit, Unit> Refresh { get; }

        /// <summary>
        /// Gets the list of questions that have been loaded.
        /// </summary>
        public ReactiveList<QuestionViewModel> Questions { get; } = new ReactiveList<QuestionViewModel>();

        public QuestionsViewModel()
        {
            QuestionsApi = RestService.For<IQuestionsApi>(Application.HttpClient);
            LoadQuestions = ReactiveCommand.CreateFromTask(LoadQuestionsImpl);
            Clear = ReactiveCommand.Create(() =>
            {
                Questions.Clear();
            });
            Refresh = ReactiveCommand.CreateCombined(new[]
            {
                Clear,
                LoadQuestions
            });
        }

        private async Task LoadQuestionsImpl()
        {
            var response = await QuestionsApi.Questions(Application.CurrentSite);
            var questions = response.Items.Select(q => new QuestionViewModel(q)).ToArray();
            Questions.AddRange(questions);
        }
    }
}
