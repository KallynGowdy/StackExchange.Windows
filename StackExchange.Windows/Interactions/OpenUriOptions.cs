using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Windows.Services.Settings;

namespace StackExchange.Windows.Interactions
{
    /// <summary>
    /// Defines a class that represents options for opening a URI.
    /// </summary>
    public class OpenUriOptions
    {
        /// <summary>
        /// Gets or sets the URI that should be opened.
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// The type of browser that should be opened.
        /// </summary>
        public OpenPostLinksBrowserType BrowserType { get; set; }
    }
}
