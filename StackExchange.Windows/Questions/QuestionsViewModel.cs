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
        public ReactiveCommand<Unit, Question[]> LoadQuestions { get; }

        /// <summary>
        /// Gets the list of questions that have been loaded.
        /// </summary>
        public ReactiveList<Question> Questions { get; } = new ReactiveList<Question>();

        public QuestionsViewModel()
        {
            QuestionsApi = RestService.For<IQuestionsApi>(Application.HttpClient);
            LoadQuestions = ReactiveCommand.CreateFromTask(async () => await QuestionsApi.Questions(Application.CurrentSite));

            LoadQuestions.Subscribe(questions =>
            {
                Questions.AddRange(questions);
            });
        }

    }
}
