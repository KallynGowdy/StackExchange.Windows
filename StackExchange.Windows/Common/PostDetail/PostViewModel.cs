using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using ReactiveUI;
using Splat;
using StackExchange.Windows.Html;
using StackExchange.Windows.User.UserCard;
using StackExchange.Windows.Api.Models;
using StackExchange.Windows.Common.CommentDetail;
using StackExchange.Windows.Services;

namespace StackExchange.Windows.Common.PostDetail
{
    /// <summary>
    /// Defines a view model that represents a <see cref="Post"/> object.
    /// </summary>
    public class PostViewModel : ReactiveObject
    {
        public IClipboard Clipboard { get; }

        public UserCardViewModel Poster { get; } = new UserCardViewModel();
        public CommentViewModel[] Comments { get; } = new CommentViewModel[0];
        public string Body { get; } = "";
        public string Score { get; } = "";
        public string Link { get; }
        public ReactiveCommand<Unit, Unit> CopyLink { get; }

        public PostViewModel() : this((IClipboard)null)
        {
        }

        public PostViewModel(Post post) : this(post, null)
        {
        }

        public PostViewModel(Post post, IClipboard clipboard) : this(clipboard)
        {
            var htmlHelper = new HtmlHelper();

            Score = post.Score.ToString();
            Body = htmlHelper.WrapPostBody(post.Body);
            Link = post.Link;
            Poster = new UserCardViewModel(post);
            Comments = post.Comments.Select(comment => new CommentViewModel(comment)).ToArray();
        }

        public PostViewModel(IClipboard clipboard)
        {
            this.Clipboard = clipboard ?? Locator.Current.GetService<IClipboard>();
            CopyLink = ReactiveCommand.Create(CopyLinkImpl);
        }

        private void CopyLinkImpl()
        {
            var copy = new DataPackage()
            {
                RequestedOperation = DataPackageOperation.Copy
            };
            copy.SetText(Link);
            Clipboard.SetContent(copy);
        }
    }
}
