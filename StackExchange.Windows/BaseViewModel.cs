using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splat;
using StackExchange.Windows.Application;
using StackExchange.Windows.Authentication;

namespace StackExchange.Windows
{
    public class BaseViewModel
    {
        public ApplicationViewModel Application { get; }
        public AuthenticationViewModel Authentication => Application.Authentication;

        public BaseViewModel()
        {
            Application = Locator.Current.GetService<ApplicationViewModel>();
        }
    }
}
