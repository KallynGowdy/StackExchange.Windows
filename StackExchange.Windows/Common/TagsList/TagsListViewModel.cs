using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace StackExchange.Windows.Common.TagsList
{
    public class TagsListViewModel : ReactiveObject
    {
        public TagViewModel[] Tags { get; }

        public TagsListViewModel() : this(new TagViewModel[0])
        {
        }

        public TagsListViewModel(TagViewModel[] tags)
        {
            Tags = tags;
        }
    }
}
