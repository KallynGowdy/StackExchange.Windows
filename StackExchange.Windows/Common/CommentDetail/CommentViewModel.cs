using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using StackExchange.Windows.Api.Models;
using StackExchange.Windows.Html;
using StackExchange.Windows.User.UserCard;

namespace StackExchange.Windows.Common.CommentDetail
{
    public class CommentViewModel : ReactiveObject
    {
        public CommentViewModel(Comment comment)
        {
            Score = comment.Score.ToString();
            Body = comment.Body;
            Poster = new UserCardViewModel(comment);
        }

        public UserCardViewModel Poster { get; }
        public string Body { get; }
        public string Score { get; }
    }
}
