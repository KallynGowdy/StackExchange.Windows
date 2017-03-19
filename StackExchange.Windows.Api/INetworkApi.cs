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
        Task<Response<Site>> Sites(int page = 1, int pagesize = 100);

        /// <summary>
        /// Retrieves a list of user's associated accounts.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Get("/users/{ids}/associated")]
        Task<Response<NetworkUser>> UserAssociatedAccounts(string ids);
    }
}
