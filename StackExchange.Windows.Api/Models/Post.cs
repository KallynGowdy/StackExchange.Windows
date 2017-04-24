using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace StackExchange.Windows.Api.Models
{
    /// <summary>
    /// Defines a post. That is, a piece of text that was written by a user.
    /// </summary>
    public abstract class Post : Content
    {
        public Comment[] Comments { get; set; } = new Comment[0];
        public string Link { get; set; }
    }
}
