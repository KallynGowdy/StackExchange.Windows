using Windows.ApplicationModel.DataTransfer;

namespace StackExchange.Windows.Services
{
    /// <summary>
    /// Defines a service that represents a clipboard.
    /// </summary>
    public interface IClipboard
    {
        void SetContent(DataPackage data);
    }
}