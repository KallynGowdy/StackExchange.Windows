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
        public Uri ImageUrl { get; }
        public string Reputation { get; }

        public UserCardViewModel(Post post)
        {
            if (post.Owner != null)
            {
                Owner = post.Owner.DecodedDisplayName;
                ImageUrl = new Uri(post.Owner.ProfileImage);
                Reputation = post.Owner.Reputation.ToString();
            }
            PostedOn = post.FormattedDate;
        }
    }
}
