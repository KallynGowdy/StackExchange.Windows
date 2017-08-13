using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackExchange.Windows.Attributes
{
    /// <summary>
    /// Defines an attribute that can be used to specify a resource that should be used
    /// when describing the annotated member.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class ResourceAttribute : Attribute
    {
        public readonly string ResourceName;

        public ResourceAttribute(string resourceName)
        {
            this.ResourceName = resourceName ?? throw new ArgumentNullException(nameof(resourceName));
        }
    }
}
