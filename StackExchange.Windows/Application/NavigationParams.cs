using System;

namespace StackExchange.Windows.Application
{
    /// <summary>
    /// Defines a class that represents parameters for navigating through the application.
    /// </summary>
    public class NavigationParams
    {
        public NavigationParams()
        {
        }

        public NavigationParams(Type pageType)
        {
            this.PageType = pageType;
        }

        public NavigationParams(Type pageType, object parameter) : this(pageType)
        {
            this.Parameter = parameter;
        }

        /// <summary>
        /// Gets or sets the type of the page that should be navigated to.
        /// </summary>
        public Type PageType { get; set; }

        /// <summary>
        /// Gets or sets the parameter that should be passed to the page.
        /// </summary>
        public object Parameter { get; set; }
    }
}