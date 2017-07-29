using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchange.Windows.Api.Models
{
    public class Answer : Post
    {
        public int AnswerId { get; set; }
        public bool IsAccepted { get; set; }

        public override string FormattedDate => $"answered {CreationDate:MMM dd \"'\"yy} at {CreationDate:HH:mm}";
    }
}
