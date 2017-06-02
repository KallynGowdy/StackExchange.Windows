using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using StackExchange.Windows.Api.Models;
using StackExchange.Windows.Common.PostDetail;
using StackExchange.Windows.Services;
using Xunit;

namespace StackExchange.Windows.Tests.Common.PostDetail
{
    public class PostViewModelTests
    {
        private PostViewModel Subject { get; set; }
        private StubIClipboard Clipboard { get; set; }

        public PostViewModelTests()
        {
            Clipboard = new StubIClipboard();
        }

        [Fact]
        public async Task Test_CopyLink_Copies_Post_Link_To_Clipboard()
        {
            DataPackage setData = null;
            Clipboard.SetContent(data => setData = data);

            Subject = new PostViewModel(new Question()
            {
                Link = "link"
            }, Clipboard);

            await Subject.CopyLink.Execute();

            Assert.NotNull(setData);
            Assert.Equal(DataPackageOperation.Copy, setData.RequestedOperation);

            var view = setData.GetView();
            var text = await view.GetTextAsync();
            Assert.Equal("link", text);

        }

    }
}
