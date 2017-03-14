using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;
using StackExchange.Windows.Api.Models;

namespace StackExchange.Windows.Api
{
    /// <summary>
    /// Defines an API for search related methods.
    /// </summary>
    public interface ISearchApi
    {
        /// <summary>
        /// Searches the given site for questions.
        /// </summary>
        /// <param name="q"></param>
        /// <param name="site"></param>
        /// <returns></returns>
        [Get("/search/advanced")]
        Task<Response<Question>> SearchAdvanced(string q, string site);
    }
}
