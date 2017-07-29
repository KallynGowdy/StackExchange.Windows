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

        /// <summary>
        /// Retrieves a list of questsion from the specified site.
        /// </summary>
        /// <param name="site">The site that the questions should be retrieved from. Possible values can be retrieved from Sites.</param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [Get("/questions")]
        Task<Response<Question>> Questions(
            string site,
            string order = "desc",
            string sort = "activity",
            int page = 1,
            int pagesize = 10,
            string filter = "default");

        /// <summary>
        /// Retrieves the questions with the given semi-colon IDs.
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="site"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [Get("/questions/{ids}")]
        Task<Response<Question>> Question(string ids, string site, string filter = "withbody");

        /// <summary>
        /// Retrieves a list of answers for the given semi-colon separated question IDs.
        /// </summary>
        /// <param name="questionIds">A list of question IDs separated by semi-colons.</param>
        /// <param name="site"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [Get("/questions/{questionIds}/answers")]
        Task<Response<Answer>> QuestionAnswers(
            string questionIds,
            string site,
            string order = "desc",
            string sort = "votes",
            int page = 1,
            int pagesize = 10,
            string filter = "withbody");

        /// <summary>
        /// Searches the given site for questions.
        /// </summary>
        /// <param name="q"></param>
        /// <param name="site"></param>
        /// <returns></returns>
        [Get("/search/advanced")]
        Task<Response<Question>> SearchAdvanced(string q, string site, string filter = "default");
    }
}
