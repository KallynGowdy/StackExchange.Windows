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
    public abstract class Post : Content
    {
        /// <summary>
        /// The set of comments on the post.
        /// </summary>
        public Comment[] Comments { get; set; } = new Comment[0];

        /// <summary>
        /// A URL to the post.
        /// </summary>
        public string Link { get; set; }
        
        /// <summary>
        /// The nullable date that this post was last edited on.
        /// </summary>
        public DateTime? LastEditDate { get; set; }
    }
}
