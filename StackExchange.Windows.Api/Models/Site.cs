﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchange.Windows.Api.Models
{
    /// <summary>
    /// Represents a model that contains information about a stack exchange site.
    /// </summary>
    public class Site
    {
        public string ApiSiteParameter { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string IconUrl { get; set; }
        public string SiteUrl { get; set; }
        public string Audience { get; set; }
        public string HighResolutionIconUrl { get; set; }
    }
}
