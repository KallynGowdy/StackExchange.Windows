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
    /// <summary>
    /// Defines a view model that represents a comment.
    /// </summary>
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

        /// <summary>
        /// Gets a view model that represents the user that authored the comment.
        /// </summary>
        public UserCardViewModel Poster { get; }
        
        /// <summary>
        /// Gets a <see cref="Block"/> that represents the formatted content of the comment.
        /// </summary>
        public Block Body { get; }
        
        /// <summary>
        /// Gets the score of the comment.
        /// </summary>
        public string Score { get; }
    }
}
