using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using StackExchange.Windows.Authentication;

namespace StackExchange.Windows.Application
{
    /// <summary>
    /// Defines a view model that represents the entire application.
    /// </summary>
    public class ApplicationViewModel : ReactiveObject
    {
        public AuthenticationViewModel Authentication { get; }

        /// <summary>
        /// Gets the interaction that requests navigation to other pages.
        /// </summary>
        public Interaction<Type, Unit> Navigate { get; } = new Interaction<Type, Unit>();

        /// <summary>
        /// Gets the interaction that requests navigation back in the navigation stack.
        /// </summary>
        public Interaction<Unit, Unit> NavigateBack { get; } = new Interaction<Unit, Unit>();

        /// <summary>
        /// Gets the interaction that requests navigation to the given page type and clears the page stack at the same time.
        /// </summary>
        public Interaction<Type, Unit> NavigateAndClearStack { get; } = new Interaction<Type, Unit>();

        public ApplicationViewModel()
        {
            Authentication = new AuthenticationViewModel(this);
        }

        public void Start()
        {
            Locator.CurrentMutable.RegisterConstant(this, typeof(ApplicationViewModel));
            Locator.CurrentMutable.RegisterConstant(Authentication, typeof(AuthenticationViewModel));
        }
    }
}
