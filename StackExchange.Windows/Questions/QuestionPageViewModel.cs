using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Windows.Api;
using StackExchange.Windows.Api.Models;
using StackExchange.Windows.Application;
using StackExchange.Windows.Html;
using StackExchange.Windows.User.UserCard;

namespace StackExchange.Windows.Questions
{
    /// <summary>
    /// Defines a view model that is able to present a question with it's answers in a full page.
    /// </summary>
    public class QuestionPageViewModel : BaseViewModel
    {
        public IQuestionsApi QuestionsApi { get; }

        public string Title { get; }
        public string[] Tags { get; }
        public string Body { get; }
        public UserCardViewModel Asker { get; }

        public QuestionPageViewModel(Question question, ApplicationViewModel application = null, IQuestionsApi questionsApi = null)
            : base(application)
        {
            var htmlHelper = new HtmlHelper();

            QuestionsApi = questionsApi ?? Api<IQuestionsApi>();
            Title = question.DecodedTitle;
            Tags = question.Tags;
            Body = htmlHelper.WrapPostBody(question.Body);
            Asker = new UserCardViewModel(question);
        }
    }
}
