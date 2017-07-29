using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace StackExchange.Windows.Api.Models
{
    public class ShallowUser
    {
        public string DisplayName { get; set; } = "";
        public string Link { get; set; } = "";
        public int UserId { get; set; }
        public int? Reputation { get; set; }
        public string ProfileImage { get; set; } = "";
        public BadgeCount BadgeCounts { get; set; }

        public string DecodedDisplayName => WebUtility.HtmlDecode(DisplayName);
    }
}
