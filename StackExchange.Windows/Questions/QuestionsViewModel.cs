using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Refit;
using StackExchange.Windows.Api;
using StackExchange.Windows.Api.Models;
using StackExchange.Windows.Application;
using StackExchange.Windows.Common.SearchBox;

namespace StackExchange.Windows.Questions
{
    public class QuestionsViewModel : BaseViewModel, ISupportsActivation
    {
        private ReactiveList<QuestionViewModel> questions = new ReactiveList<QuestionViewModel>();
        private IQuestionsApi QuestionsApi { get; }
        private ISearchViewModel Search { get; }

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
        public ReactiveCommand<Unit, Unit> Refresh { get; }

        /// <summary>
        /// Gets the list of questions that have been loaded.
        /// </summary>
        public ReactiveList<QuestionViewModel> Questions
        {
            get { return questions; }
            private set { this.RaiseAndSetIfChanged(ref questions, value); }
        }

        public QuestionsViewModel(ApplicationViewModel application = null, ISearchViewModel search = null, IQuestionsApi questionsApi = null)
            : base(application)
        {
            QuestionsApi = questionsApi ?? Api<IQuestionsApi>();
            Search = Service(search);
            LoadQuestions = ReactiveCommand.CreateFromTask(LoadQuestionsImpl);
            Clear = ReactiveCommand.Create(() =>
            {
                Questions.Clear();
            });
            Refresh = ReactiveCommand.CreateFromTask(async () =>
            {
                await Clear.Execute();
                await LoadQuestions.Execute();
            }, canExecute: Clear.CanExecute.CombineLatest(LoadQuestions.CanExecute, (canClearExecute, canLoadExecute) => canClearExecute && canLoadExecute));

            this.WhenActivated(d =>
            {
                d(Search.WhenAnyValue(s => s.SelectedSite)
                    .Skip(1)
                    .Select(svm => Unit.Default)
                    .InvokeCommand(this, vm => vm.Refresh));
            });
        }

        private async Task LoadQuestionsImpl()
        {
            if (Search.SelectedSite == null)
            {
                if (!await Search.LoadSites.IsExecuting.FirstAsync())
                {
                    await Search.LoadSites.Execute();
                }
                else
                {
                    await Search.LoadSites.FirstAsync();
                }
            }
            var response = await QuestionsApi.Questions(Search.SelectedSite.ApiSiteParameter);
            var questions = response.Items.Select(q => new QuestionViewModel(q)).ToArray();
            Questions = new ReactiveList<QuestionViewModel>(questions);
        }

        public ViewModelActivator Activator { get; } = new ViewModelActivator();
    }
}
