﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Windows.Attributes;

namespace StackExchange.Windows.Services.Settings
{
    /// <summary>
    /// Defines a list of possible ways to open post links.
    /// </summary>
    public enum OpenPostLinksBrowserType
    {
        [Resource("OpenPostEmbeddedBrowserType")]
        EmbeddedBrowser,

        [Resource("OpenPostExternalBrowserType")]
        ExternalBrowser
    }
}
