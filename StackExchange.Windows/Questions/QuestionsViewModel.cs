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
        public ReactiveCommand<Unit, QuestionViewModel[]> LoadQuestions { get; }

        /// <summary>
        /// Gets the list of questions that have been loaded.
        /// </summary>
        public ReactiveList<QuestionViewModel> Questions { get; } = new ReactiveList<QuestionViewModel>();

        public QuestionsViewModel()
        {
            QuestionsApi = RestService.For<IQuestionsApi>(Application.HttpClient);
            LoadQuestions = ReactiveCommand.CreateFromTask(LoadQuestionsImpl);

            LoadQuestions.Subscribe(questions =>
            {
                Questions.AddRange(questions);
            });
        }

        private async Task<QuestionViewModel[]> LoadQuestionsImpl()
        {
            var response = await QuestionsApi.Questions(Application.CurrentSite);
            return response.Items.Select(q => new QuestionViewModel(q)).ToArray();
        }
    }
}
