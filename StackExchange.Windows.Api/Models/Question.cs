using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace StackExchange.Windows.Api.Models
{
    public class Question : Post
    {
        public int QuestionId { get; set; }
        public string Title { get; set; } = "";
        public string[] Tags { get; set; } = new string[0];
        public int AnswerCount { get; set; }
        public int ViewCount { get; set; }

        /// <summary>
        /// The ID of the accepted answer for this question.
        /// Null if no answer has been accepted.
        /// </summary>
        public int? AcceptedAnswerId { get; set; }

        /// <summary>
        /// Whether the question has at least one answer that has been upvoted or accepted.
        /// </summary>
        public bool IsAnswered { get; set; }

        [JsonIgnore]
        public string DecodedTitle => Title == null ? "" : WebUtility.HtmlDecode(Title);

        [JsonIgnore]
        public override string FormattedDate => $"asked {CreationDate:MMM dd \"'\"yy} at {CreationDate:HH:mm}";
    }
}
