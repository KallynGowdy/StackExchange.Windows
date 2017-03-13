using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchange.Windows.Api.Models
{
    public class NetworkUser
    {
        public int AccountId { get; set; }
        public int AnswerCount { get; set; }
        public int Reputation { get; set; }
        public string SiteName { get; set; }
        public string SiteUrl { get; set; }
        public int UserId { get; set; }
    }
}
