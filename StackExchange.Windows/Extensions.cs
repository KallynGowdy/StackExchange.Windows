using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackExchange.Windows
{

    public static class Extensions
    {

        /// <summary>
        /// Adds the given disposable to the list of disposables via the given action.
        /// </summary>
        /// <param name="disposable"></param>
        /// <param name="disposables"></param>
        public static void DisposeWith(this IDisposable disposable, Action<IDisposable> disposables)
        {
            disposables(disposable);
        }

    }
}
