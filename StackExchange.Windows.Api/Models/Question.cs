using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace StackExchange.Windows.Api.Models
{
    public class Question : Post
    {
        public int QuestionId { get; set; }
        public string Link { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public string[] Tags { get; set; }

        public override string FormattedDate => $"asked {CreationDate:MMM dd \"'\"yy} at {CreationDate:HH:mm}";
    }
}
