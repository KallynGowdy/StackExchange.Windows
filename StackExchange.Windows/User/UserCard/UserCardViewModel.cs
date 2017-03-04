using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using ReactiveUI;
using StackExchange.Windows.Api.Models;

namespace StackExchange.Windows.User.UserCard
{
    public class UserCardViewModel : ReactiveObject
    {
        public string Owner { get; }

        public string PostedOn { get; }

        public ImageSource Image { get; }

        public UserCardViewModel(Post post)
        {
            Owner = WebUtility.HtmlDecode(post.Owner.DisplayName);
            PostedOn = post.FormattedDate;
            Image = new BitmapImage(new Uri(post.Owner.ProfileImage));
        }

    }
}
