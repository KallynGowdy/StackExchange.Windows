using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace StackExchange.Windows.Api.Models
{
    /// <summary>
    /// Defines a post. That is, a piece of text that was written by a user.
    /// </summary>
    public abstract class Post
    {
        public string Body { get; set; }
        public ShallowUser Owner { get; set; }
        public DateTime CreationDate { get; set; }

        public abstract string FormattedDate { get; }
    }
}
