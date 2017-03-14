using System;
using StackExchange.Windows.Api.Models;

namespace StackExchange.Windows.Common.SearchBox
{
    public class SiteViewModel
    {
        public SiteViewModel(Site site)
        {
            Name = site.Name;
            ApiSiteParameter = site.ApiSiteParameter;
            Audience = !string.IsNullOrEmpty(site.Audience) ? $"For {site.Audience}" : "";
            LogoUrl = !string.IsNullOrEmpty(site.LogoUrl) ? new Uri(site.LogoUrl) : null;
            IconUrl = !string.IsNullOrEmpty(site.IconUrl) ? new Uri(site.IconUrl) : null;
            HighResolutionIconUrl = !string.IsNullOrEmpty(site.HighResolutionIconUrl) ? new Uri(site.HighResolutionIconUrl) : null;
        }

        public SiteViewModel()
        {
        }

        public string Name { get; }
        public string ApiSiteParameter { get; }
        public string Audience { get; }
        public Uri LogoUrl { get; }
        public Uri IconUrl { get; }
        public Uri HighResolutionIconUrl { get; }
        public Uri HighResIconUrlOrFallback => HighResolutionIconUrl ?? IconUrl;
    }
}