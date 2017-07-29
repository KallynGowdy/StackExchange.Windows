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
        public string BronzeBadges { get; } = "";
        public string SilverBadges { get; } = "";
        public string GoldBadges { get; } = "";
        public bool HasBadges => !string.IsNullOrEmpty(BronzeBadges) && !string.IsNullOrEmpty(SilverBadges) && !string.IsNullOrEmpty(GoldBadges);

        public UserCardViewModel(Content content)
        {
            if (content.Owner != null)
            {
                Owner = content.Owner.DecodedDisplayName;
                ImageUrl = string.IsNullOrEmpty(content.Owner.ProfileImage) ? null : new Uri(content.Owner.ProfileImage);
                Reputation = FormatReputation(content.Owner.Reputation);

                if (content.Owner.BadgeCounts != null)
                {
                    BronzeBadges = content.Owner.BadgeCounts.Bronze.ToString();
                    SilverBadges = content.Owner.BadgeCounts.Silver.ToString();
                    GoldBadges = content.Owner.BadgeCounts.Gold.ToString();
                }
            }
            PostedOn = content.FormattedDate;
        }

        private string FormatReputation(int? ownerReputation)
        {
            if (ownerReputation != null)
            {
                int rep = ownerReputation.Value;
                if (rep < 1000)
                {
                    return rep.ToString();
                }
                else if (rep < 100_000)
                {
                    var roundedRep = rep / 1000d;
                    roundedRep = Truncate(roundedRep, 2);
                    return $"{roundedRep:0.##}k";
                }
                else
                {
                    var roundedRep = rep / 1000d;
                    roundedRep = Truncate(roundedRep, 0);
                    return $"{roundedRep:0}k";
                }
            }
            else
            {
                return "0";
            }
        }

        private static double Truncate(double roundedRep, double place)
        {
            place = Math.Pow(10, place);
            return Math.Truncate(roundedRep * place) / place;
        }

        public UserCardViewModel()
        {
        }
    }
}
