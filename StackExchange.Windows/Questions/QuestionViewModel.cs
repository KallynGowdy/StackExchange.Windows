using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Windows.Api.Models;

namespace StackExchange.Windows.Questions
{
    /// <summary>
    /// Defines a view model for <see cref="Question"/> models.
    /// </summary>
    public class QuestionViewModel
    {
        public string Title { get; }

        public string[] Tags { get; }

        public string Activity { get; }

        public QuestionViewModel(Question question)
        {
            Title = WebUtility.HtmlDecode(question.Title);
            Tags = question.Tags;
            Activity = $"{question.Owner.DisplayName} asked on {question.CreationDate:f}";
        }
    }
}
