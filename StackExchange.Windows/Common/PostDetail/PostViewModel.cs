using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using StackExchange.Windows.Html;
using StackExchange.Windows.User.UserCard;
using StackExchange.Windows.Api.Models;

namespace StackExchange.Windows.Common.PostDetail
{
    /// <summary>
    /// Defines a view model that represents a <see cref="Post"/> object.
    /// </summary>
    public class PostViewModel : ReactiveObject
    {
        public string Body { get; }
        public UserCardViewModel Poster { get; }
        public string Score { get; }

        public PostViewModel(Post post)
        {
            var htmlHelper = new HtmlHelper();

            Score = post.Score.ToString();
            Body = htmlHelper.WrapPostBody(post.Body);
            Poster = new UserCardViewModel(post);
        }
    }
}
