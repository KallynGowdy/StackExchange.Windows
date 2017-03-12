using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;
using StackExchange.Windows.Api.Models;

namespace StackExchange.Windows.Api
{
    /// <summary>
    /// Defines a set of methods that map to stack exchange network related APIs.
    /// </summary>
    public interface INetworkApi
    {
        /// <summary>
        /// Retrives a list of available sites.
        /// </summary>
        /// <returns></returns>
        [Get("/sites")]
        Task<Response<Site>> Sites();
    }
}
