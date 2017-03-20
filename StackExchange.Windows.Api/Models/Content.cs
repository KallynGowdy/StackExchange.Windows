using System;

namespace StackExchange.Windows.Api.Models
{
    public abstract class Content
    {
        public string Body { get; set; }
        public ShallowUser Owner { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }

        public abstract string FormattedDate { get; }
    }
}