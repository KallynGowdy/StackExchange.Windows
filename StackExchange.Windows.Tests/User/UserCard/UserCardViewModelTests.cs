using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Windows.Api.Models;
using StackExchange.Windows.User.UserCard;
using Xunit;

namespace StackExchange.Windows.Tests.User.UserCard
{
    public class UserCardViewModelTests
    {
        private UserCardViewModel Subject { get; set; }

        [Fact]
        public void Test_Handles_Users_With_Empty_Image_URLs()
        {
            Subject = new UserCardViewModel(new Question()
            {
                Owner = new ShallowUser()
                {
                    DisplayName = "Name",
                    Link = "https://www.example.com",
                    ProfileImage = "",
                    Reputation = 0,
                    UserId = 1
                }
            });

            Assert.Null(Subject.ImageUrl);
        }

        [Theory]
        [InlineData(10, "10")]
        [InlineData(100, "100")]
        [InlineData(1000, "1k")]
        [InlineData(1100, "1.1k")]
        [InlineData(9999, "9.99k")]
        [InlineData(10000, "10k")]
        [InlineData(10500, "10.5k")]
        [InlineData(15900, "15.9k")]
        [InlineData(30000, "30k")]
        [InlineData(100000, "100k")]
        [InlineData(100500, "100k")]
        [InlineData(109999, "109k")]
        public void Test_Formats_Reputation(int reputation, string expected)
        {
            Subject = new UserCardViewModel(new Question()
            {
                Owner = new ShallowUser()
                {
                    DisplayName = "Name",
                    Reputation = reputation
                }
            });

            Assert.Equal(expected, Subject.Reputation);
        }
    }
}
