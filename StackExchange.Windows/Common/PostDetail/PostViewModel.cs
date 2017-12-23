using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
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
using StackExchange.Windows.Common.TagsList;
using StackExchange.Windows.Services;
using StackExchange.Windows.Services.Settings;

namespace StackExchange.Windows.Common.PostDetail
{
    /// <summary>
    /// Defines a view model that represents a <see cref="Post"/> object.
    /// </summary>
    public class PostViewModel : ReactiveObject, ISupportsActivation
    {
        private ObservableAsPropertyHelper<string> body = ObservableAsPropertyHelper<string>.Default("");
        private readonly string originalBody = "";
        public IClipboard Clipboard { get; }
        public ISettingsStore Settings { get; }

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
        public TagsListViewModel Tags { get; } = new TagsListViewModel();

        /// <summary>
        /// Gets the raw body of the post.
        /// </summary>
        public string Body => body.Value;

        /// <summary>
        /// Gets the total score of the post.
        /// </summary>
        public string Score { get; } = "";

        /// <summary>
        /// Gets the link to the post.
        /// </summary>
        public string Link { get; }

        /// <summary>
        /// Gets whether the post has been edited.
        /// </summary>
        public bool Edited => LastEditDate.HasValue;

        /// <summary>
        /// Gets the date that the post was last edited on.
        /// </summary>
        public DateTimeOffset? LastEditDate { get; }

        /// <summary>
        /// Gets a string that describes when the post was edited.
        /// </summary>
        public string EditDescription => $"edited {LastEditDate:MMM dd \"'\"yy} at {LastEditDate:HH:mm}";

        /// <summary>
        /// The command that copies the link to the clipboard.
        /// </summary>
        public ReactiveCommand<Unit, Unit> CopyLink { get; }

        /// <summary>
        /// The command that opens the post in a web browser.
        /// </summary>
        public ReactiveCommand<Unit, Unit> OpenPostInBrowser { get; }

        public bool Accepted { get; }

        public PostViewModel() : this(null, null)
        {
        }

        public PostViewModel(Post post) : this(post, null, null)
        {
        }

        public PostViewModel(Post post, IClipboard clipboard, ISettingsStore settings) : this(clipboard, settings)
        {
            originalBody = post.Body;
            Score = post.Score.ToString();
            Link = post.Link;
            LastEditDate = post.LastEditDate;
            Poster = new UserCardViewModel(post);
            Comments = post.Comments.Select(comment => new CommentViewModel(comment)).ToArray();

            if (post is Question q)
            {
                Tags = q.Tags.ToListViewModel();
            }
            else if (post is Answer a)
            {
                Accepted = a.IsAccepted;
            }

            this.WhenActivated(d =>
            {
                body = Settings.GetSettingValue<SyntaxStyle>(SettingsStore.SyntaxStyleDefinition)
                    .Select(style => HtmlHelper.WrapPostBody(originalBody, style))
                    .ToProperty(this, vm => vm.Body)
                    .DisposeWith(d);
            });
        }

        public PostViewModel(IClipboard clipboard, ISettingsStore settings)
        {
            this.Clipboard = clipboard ?? Locator.Current.GetService<IClipboard>();
            this.Settings = settings ?? Locator.Current.GetService<ISettingsStore>();
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

        public ViewModelActivator Activator { get; } = new ViewModelActivator();
    }
}
