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
        public ApplicationViewModel Application { get; }
        public AuthenticationViewModel Authentication => Application.Authentication;

        public BaseViewModel(ApplicationViewModel application = null)
        {
            Application = application ?? Locator.Current.GetService<ApplicationViewModel>();
        }

        protected TService Api<TService>()
            where TService : class
        {
            return RestService.For<TService>(Application.HttpClient);
        }

        protected TService Service<TService>(TService service = null)
            where TService : class
        {
            return service ?? Locator.Current.GetService<TService>();
        }
    }
}
