using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Windows.Api.Models;
using Xunit;

namespace StackExchange.Windows.Tests.Models
{
    public class QuestionTests
    {
        public Question Subject { get; set; } = new Question();

        [Theory]
        [InlineData("2017/1/1 11:00 AM", "asked Jan 01 '17 at 11:00")]
        [InlineData("2017/2/1 11:00 AM", "asked Feb 01 '17 at 11:00")]
        [InlineData("2017/3/1 11:32 PM", "asked Mar 01 '17 at 23:32")]
        [InlineData("2017/4/1 11:00 AM", "asked Apr 01 '17 at 11:00")]
        [InlineData("2017/5/1 11:00 AM", "asked May 01 '17 at 11:00")]
        [InlineData("2017/6/1 11:00 AM", "asked Jun 01 '17 at 11:00")]
        [InlineData("2017/7/1 11:00 AM", "asked Jul 01 '17 at 11:00")]
        [InlineData("2017/8/1 11:00 AM", "asked Aug 01 '17 at 11:00")]
        [InlineData("2017/9/1 11:00 AM", "asked Sep 01 '17 at 11:00")]
        [InlineData("2017/10/1 11:00 AM", "asked Oct 01 '17 at 11:00")]
        [InlineData("2017/11/1 11:00 AM", "asked Nov 01 '17 at 11:00")]
        [InlineData("2017/12/1 11:00 AM", "asked Dec 01 '17 at 11:00")]
        [InlineData("2014/3/1 9:05 AM", "asked Mar 01 '14 at 09:05")]
        public void Test_FormattedDate_Produces_Correct_String(string date, string expected)
        {
            Subject.CreationDate = DateTime.Parse(date);
            var result = Subject.FormattedDate;
            Assert.Equal(expected, result);
        }

    }
}
