using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using StackExchange.Windows.Api.Models;
using StackExchange.Windows.Common.PostDetail;

namespace StackExchange.Windows.Questions
{
    public class QuestionDetailViewModel : PostViewModel
    {
        public QuestionDetailViewModel(Question question) : base(question)
        {
            Id = question.QuestionId.ToString();
            Title = question.DecodedTitle;
            Tags = question.Tags;
        }

        public QuestionDetailViewModel(int questionId)
        {
            this.Id = questionId.ToString();
        }

        public string Title { get; } = "";
        public string[] Tags { get; } = new string[0];
        public string Id { get; }
    }
}
