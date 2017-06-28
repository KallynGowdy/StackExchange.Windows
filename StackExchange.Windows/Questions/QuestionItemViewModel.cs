using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using StackExchange.Windows.Api.Models;
using StackExchange.Windows.Common.TagsList;
using StackExchange.Windows.User.UserCard;

namespace StackExchange.Windows.Questions
{
    /// <summary>
    /// Defines a view model for <see cref="Question"/> models.
    /// </summary>
    public class QuestionItemViewModel
    {
        private readonly int score;
        private readonly int answers;
        private readonly int views;

        public string Title { get; }
        public TagsListViewModel Tags { get; }
        public string Score => score.ToString();
        public string Answers => answers.ToString();
        public string Views => views.ToString();
        public UserCardViewModel User { get; }
        public bool IsAnswered { get; }
        public Question Question { get; }

        /// <summary>
        /// Returns this object.
        /// Workaround for XAML ListView item bindings to the entire object.
        /// </summary>
        public QuestionItemViewModel Self => this;

        public QuestionItemViewModel(Question question)
        {
            Question = question;
            Title = question.DecodedTitle;
            Tags = question.Tags.ToListViewModel();
            User = new UserCardViewModel(question);
            score = question.Score;
            answers = question.AnswerCount;
            views = question.ViewCount;
            IsAnswered = question.IsAnswered;
        }

        public QuestionItemViewModel()
        {
        }
    }
}
