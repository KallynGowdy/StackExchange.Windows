using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using StackExchange.Windows.Common.SearchBox;
using StackExchange.Windows.Common.TagsList;
using Xunit;

namespace StackExchange.Windows.Tests.Common.TagsList
{
    public class TagViewModelTests
    {
        public TagViewModel Subject { get; set; }
        public StubISearchViewModel SearchViewModel { get; set; }
        public string Tag { get; set; }
        public string ExpectedTag { get; set; }

        public TagViewModelTests()
        {
            Tag = "test";
            ExpectedTag = "[test]";
            SearchViewModel = new StubISearchViewModel();
            Subject = new TagViewModel(Tag, SearchViewModel);
        }

        [Fact]
        public async Task Test_Calls_SetQueryAndFocus_When_SearchTag_Is_Executed()
        {
            var set = "";
            var searchQueryAndFocus = ReactiveCommand.Create<string>(s =>
            {
                set = s;
            });
            SearchViewModel.SetQueryAndFocus_Get(() => searchQueryAndFocus);
            await Subject.SearchTag.Execute().FirstAsync();

            Assert.Equal(ExpectedTag, set);
        }
    }
}
