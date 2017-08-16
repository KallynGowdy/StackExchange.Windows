using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackExchange.Windows.Services
{
    /// <summary>
    /// Defines an interface for objects that are able to retrieve resources.
    /// </summary>
    public interface IResourceStore
    {
        /// <summary>
        /// Gets the resource with the given key.
        /// </summary>
        /// <param name="key">The key of the resource to retrieve.</param>
        /// <returns>The resource or null if it doesn't exist.</returns>
        object GetResource(string key);
    }
}
