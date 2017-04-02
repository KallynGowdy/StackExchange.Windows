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
        public string Owner { get; } = "";
        public string PostedOn { get; } = "";
        public Uri ImageUrl { get; }
        public string Reputation { get; } = "";

        public UserCardViewModel(Content content)
        {
            if (content.Owner != null)
            {
                Owner = content.Owner.DecodedDisplayName;
                ImageUrl = string.IsNullOrEmpty(content.Owner.ProfileImage) ? null : new Uri(content.Owner.ProfileImage);
                Reputation = content.Owner.Reputation.ToString();
            }
            PostedOn = content.FormattedDate;
        }

        public UserCardViewModel()
        {
        }
    }
}
