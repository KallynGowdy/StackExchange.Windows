using System;
using System.ComponentModel;
using System.Reactive;
using ReactiveUI;
using StackExchange.Windows.Authentication;
using StackExchange.Windows.Services.Settings;

namespace StackExchange.Windows.Application
{
    /// <summary>
    /// Defines an interface for a view model that contains the shared application state.
    /// </summary>
    public interface IApplicationViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the current color mode that the view model has requested.
        /// </summary>
        ColorMode CurrentColorMode { get; }

        /// <summary>
        /// Gets a view model that represents the current authentication state.
        /// </summary>
        IAuthenticationViewModel Authentication { get; }

        /// <summary>
        /// Gets the interaction that requests navigation to other pages.
        /// </summary>
        Interaction<NavigationParams, Unit> Navigate { get; }

        /// <summary>
        /// Gets the interaction that requests navigation back in the navigation stack.
        /// </summary>
        Interaction<Unit, Unit> NavigateBack { get; }

        /// <summary>
        /// Gets the interaction that requests navigation to the given page type and clears the page stack at the same time.
        /// </summary>
        Interaction<NavigationParams, Unit> NavigateAndClearStack { get; }

        /// <summary>
        /// Gets the interaction that requests a URI to be opened.
        /// </summary>
        Interaction<Uri, Unit> OpenUri { get; }

        /// <summary>
        /// Gets the API parameter of the site that the user is currently viewing.
        /// </summary>
        string CurrentSite { get; }

        /// <summary>
        /// Gets a service for the api defined by the given type.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        TService Api<TService>();
    }
}