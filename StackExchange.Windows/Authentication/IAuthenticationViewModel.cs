using System;
using System.Reactive;
using ReactiveUI;

namespace StackExchange.Windows.Authentication
{
    /// <summary>
    /// Defines an interface for view models that can represent the application authentication state.
    /// </summary>
    public interface IAuthenticationViewModel
    {
        /// <summary>
        /// Gets a command that can attempt login for the user.
        /// </summary>
        ReactiveCommand<Unit, Unit> Login { get; }

        /// <summary>
        /// Gets the interaction that handles redirecting the user to the OAuth Login page.
        /// </summary>
        Interaction<Uri, Uri> RedirectToLogin { get; }

        /// <summary>
        /// Gets a string that represents the authentication token the application
        /// currently possesses for the user.
        /// </summary>
        string Token { get; }

        /// <summary>
        /// Determines if the given URL acts as a successful redirect after OAuth Login.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        bool IsSuccessUrl(Uri url);
    }
}
