using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Windows.Attributes;

namespace StackExchange.Windows.Tests.DataStructures
{
    public enum TestEnum
    {
        [Resource("TheResource")]
        A = 3,
        B = 2,
        C = 1
    }
}
