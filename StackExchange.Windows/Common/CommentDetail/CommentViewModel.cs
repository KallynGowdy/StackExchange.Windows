using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using StackExchange.Windows.Api.Models;
using StackExchange.Windows.Html;
using StackExchange.Windows.User.UserCard;
using Windows.UI.Xaml.Documents;

namespace StackExchange.Windows.Common.CommentDetail
{
    public class CommentViewModel : ReactiveObject
    {
        public CommentViewModel(Comment comment)
        {
            Score = comment.Score.ToString();
            Poster = new UserCardViewModel(comment);
            var paragraph = (Paragraph)HtmlHelper.ConvertHtmlToBlocks(comment.Body);
            paragraph.Inlines.Add(new Run()
            {
                Text = " - "
            });
            paragraph.Inlines.Add(new Bold()
            {
                Inlines =
                {
                    new Run()
                    {
                        Text = Poster.Owner
                    }
                }
            });
            Body = paragraph;
        }

        public UserCardViewModel Poster { get; }
        public Block Body { get; }
        public string Score { get; }
    }
}
