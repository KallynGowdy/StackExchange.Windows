using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Windows.Api.Models;
using StackExchange.Windows.Common.SearchBox;
using Xunit;

namespace StackExchange.Windows.Tests.Search.SearchBox
{
    public class SiteViewModelTests
    {

        public SiteViewModel Subject { get; set; }

        [Theory]
        [InlineData("https://stackoverflow.com/hi-res", "https://stackoverflow.com/hi-res")]
        [InlineData("", "https://stackoverflow.com/lo-res")]
        [InlineData(null, "https://stackoverflow.com/lo-res")]
        public void Test_HighResIconUrlOrFallback_Uses_The_High_Resolution_Icon_First(string url, string expected)
        {
            Subject = new SiteViewModel(new Site()
            {
                HighResolutionIconUrl = url,
                IconUrl = "https://stackoverflow.com/lo-res"
            });

            Assert.Equal(new Uri(expected), Subject.HighResIconUrlOrFallback);
        }

    }
}
