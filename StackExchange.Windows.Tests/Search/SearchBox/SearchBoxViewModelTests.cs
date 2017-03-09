using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Windows.Search.SearchBox;
using Xunit;

namespace StackExchange.Windows.Tests.Search.SearchBox
{
    public class SearchBoxViewModelTests
    {
        public SearchBoxViewModel Subject { get; set; } = new SearchBoxViewModel();

        [Fact]
        public void Test_Starts_With_Empty_Query()
        {
            Assert.Equal("", Subject.Query);
        }
    }
}
