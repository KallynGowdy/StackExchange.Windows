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
        public string Link { get; set; } = "";
        public string Title { get; set; } = "";
        public string[] Tags { get; set; } = new string[0];
        public int AnswerCount { get; set; }
        public int ViewCount { get; set; }
        public bool IsAnswered { get; set; }

        [JsonIgnore]
        public string DecodedTitle => Title == null ? "" : WebUtility.HtmlDecode(Title);

        [JsonIgnore]
        public override string FormattedDate => $"asked {CreationDate:MMM dd \"'\"yy} at {CreationDate:HH:mm}";
    }
}
