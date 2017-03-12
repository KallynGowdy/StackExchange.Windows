using System.Collections.Generic;
using StackExchange.Windows.Api.Models;

namespace StackExchange.Windows.Search.SearchBox
{
    public class SiteViewModel
    {
        public SiteViewModel(Site site)
        {
            Name = site.Name;
            ApiSiteParameter = site.ApiSiteParameter;
        }

        public string Name { get; }
        public string ApiSiteParameter { get; }
    }
}