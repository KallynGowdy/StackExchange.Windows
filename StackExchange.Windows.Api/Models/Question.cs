using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace StackExchange.Windows.Api.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Link { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public ShallowUser Owner { get; set; }
        public string[] Tags { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
