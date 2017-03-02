using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using StackExchange.Windows.Api.Models;
using StackExchange.Windows.User.UserCard;

namespace StackExchange.Windows.Questions
{
    /// <summary>
    /// Defines a view model for <see cref="Question"/> models.
    /// </summary>
    public class QuestionViewModel
    {
        public string Title { get; }

        public string[] Tags { get; }

        public UserCardViewModel User { get; }

        public QuestionViewModel(Question question)
        {
            Title = WebUtility.HtmlDecode(question.Title);
            Tags = question.Tags;
            User = new UserCardViewModel(question);
        }
    }
}
