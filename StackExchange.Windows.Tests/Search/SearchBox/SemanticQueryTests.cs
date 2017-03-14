using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Windows.Common.SearchBox;
using StackExchange.Windows.Search.SearchBox;
using Xunit;

namespace StackExchange.Windows.Tests.Search.SearchBox
{
    public class SemanticQueryTests
    {
        private SemanticQuery Subject { get; set; }

        [Fact]
        public void Test_Default_Constructor_Sorts_By_Activity()
        {
            Subject = new SemanticQuery();

            Assert.Equal("activity", Subject.Sort);
        }

        [Fact]
        public void Test_Default_Constructor_Does_Not_Filter_Tags()
        {
            Subject = new SemanticQuery();

            Assert.Empty(Subject.Tags);
        }

        [Fact]
        public void Test_ParseTags_Splits_Tags_By_Comma()
        {
            var result = SemanticQuery.ParseTags("tag1,tag-2,TAG-3,c#");
            Assert.Collection(result,
                tag => Assert.Equal("tag1", tag),
                tag => Assert.Equal("tag-2", tag),
                tag => Assert.Equal("tag-3", tag),
                tag => Assert.Equal("c#", tag));
        }

        [Fact]
        public void Test_FormatTags_Joins_Tags_By_Comma()
        {
            var result = SemanticQuery.FormatTags(new[]
            {
                "tag1",
                "tag-2",
                "TAG-3",
                "c#"
            });

            Assert.Equal("tag1,tag-2,tag-3,c#", result);
        }

        [Theory]
        [InlineData("hot", "hot")]
        [InlineData("activity", "activity")]
        [InlineData("votes", "votes")]
        [InlineData("creation", "creation")]
        [InlineData("week", "week")]
        [InlineData("month", "month")]
        [InlineData("HOT", "hot")]
        [InlineData("notAValue", "notavalue")]
        public void Test_Constructor_From_Query_Maps_Sort_Parameter(string given, string expected)
        {
            Subject = new SemanticQuery("sort:" + given);

            Assert.Equal(expected, Subject.Sort);
        }

        [Fact]
        public void Test_Constructor_From_Query_Maps_Tags_Parameter()
        {
            Subject = new SemanticQuery("tags:tag1,tag-2");

            Assert.Collection(Subject.Tags,
                tag => Assert.Equal("tag1", tag),
                tag => Assert.Equal("tag-2", tag));
        }

        [Theory]
        [InlineData("key:value", "key", "value")]
        [InlineData("key:value ", "key", "value")]
        [InlineData(" key:value", "key", "value")]
        [InlineData(" key:value ", "key", "value")]
        [InlineData("key", "key", null)]
        [InlineData("KEY:VALUE", "key", "VALUE")]
        public void Test_ParseKeyPairs_Returns_A_KeyValuePair_For_The_Given_Query(string query, string expectedKey, string expectedValue)
        {
            var pairs = SemanticQuery.ParseKeyPairs(query);

            Assert.Collection(pairs,
                p =>
                {
                    Assert.Equal(expectedKey, p.Key);
                    Assert.Equal(expectedValue, p.Value);
                });
        }

        [Theory]
        [InlineData("first:value second:other", "first", "value", "second", "other")]
        [InlineData("first:value second:other ", "first", "value", "second", "other")]
        [InlineData(" first:value second:other", "first", "value", "second", "other")]
        [InlineData("first:value  second:other", "first", "value", "second", "other")]
        [InlineData("first second:other", "first", null, "second", "other")]
        [InlineData("first second", "first", null, "second", null)]
        public void Test_ParseKeyPairs_Returns_KeyValuePairs_For_The_Given_Query(string query, string expectedKey1, string expectedValue1, string expectedKey2, string expectedValue2)
        {
            var pairs = SemanticQuery.ParseKeyPairs(query);

            Assert.Collection(pairs,
                p =>
                {
                    Assert.Equal(expectedKey1, p.Key);
                    Assert.Equal(expectedValue1, p.Value);
                },
                p =>
                {
                    Assert.Equal(expectedKey2, p.Key);
                    Assert.Equal(expectedValue2, p.Value);
                });
        }
    }
}
