using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace StackExchange.Windows.Services
{
    public class UwpClipboard : IClipboard
    {
        public void SetContent(DataPackage data)
        {
            Clipboard.SetContent(data);
        }
    }
}
