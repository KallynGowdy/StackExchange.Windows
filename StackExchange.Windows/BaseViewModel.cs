using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Refit;
using Splat;
using StackExchange.Windows.Application;
using StackExchange.Windows.Authentication;

namespace StackExchange.Windows
{
    public class BaseViewModel : ReactiveObject
    {
        public IApplicationViewModel Application { get; }
        public IAuthenticationViewModel Authentication => Application.Authentication;

        public BaseViewModel(IApplicationViewModel application = null)
        {
            Application = application ?? Locator.Current.GetService<IApplicationViewModel>();
        }

        protected TService Api<TService>()
            where TService : class
        {
            return Application.Api<TService>();
        }

        protected TService Service<TService>(TService service = null)
            where TService : class
        {
            return service ?? Locator.Current.GetService<TService>();
        }
    }
}
