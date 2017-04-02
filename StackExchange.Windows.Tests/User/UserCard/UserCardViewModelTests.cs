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
    }
}
