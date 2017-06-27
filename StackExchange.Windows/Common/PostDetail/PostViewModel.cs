using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;
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

        /// <summary>
        /// Gets the view model that represents the user that authored the post.
        /// </summary>
        public UserCardViewModel Poster { get; } = new UserCardViewModel();

        /// <summary>
        /// Gets the array of view models that represent the comments on the post.
        /// </summary>
        public CommentViewModel[] Comments { get; } = new CommentViewModel[0];

        /// <summary>
        /// Gets the immutable array of view models that represent the tags on the post.
        /// </summary>
        public ImmutableArray<string> Tags { get; } = ImmutableArray<string>.Empty;

        /// <summary>
        /// Gets the raw body of the post.
        /// </summary>
        public string Body { get; } = "";
        
        /// <summary>
        /// Gets the total score of the post.
        /// </summary>
        public string Score { get; } = "";

        /// <summary>
        /// Gets the link to the post.
        /// </summary>
        public string Link { get; }
        
        /// <summary>
        /// The command that copies the link to the clipboard.
        /// </summary>
        public ReactiveCommand<Unit, Unit> CopyLink { get; }

        /// <summary>
        /// The command that opens the post in a web browser.
        /// </summary>
        public ReactiveCommand<Unit, Unit> OpenPostInBrowser { get; }

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

            if (post is Question q)
            {
                Tags = q.Tags.ToImmutableArray();
            }
        }

        public PostViewModel(IClipboard clipboard)
        {
            this.Clipboard = clipboard ?? Locator.Current.GetService<IClipboard>();
            CopyLink = ReactiveCommand.Create(CopyLinkImpl);
            OpenPostInBrowser = ReactiveCommand.CreateFromTask(OpenPostImpl);
        }

        private async Task OpenPostImpl()
        {
            // TODO: Move to ILauncher service or similar abstraction
            await Launcher.LaunchUriAsync(new Uri(Link));
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
