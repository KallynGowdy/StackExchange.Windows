using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Windows.Api.Models;

namespace StackExchange.Windows.Api
{
    public static class NetworkApiExtensions
    {
        public static Task<Response<Question>> QuestionWithDetail(
            this INetworkApi api,
            string ids,
            string site) =>
            api.Question(ids, site, filter: Filters.QuestionDetail);

        public static Task<Response<Answer>> QuestionAnswersWithDetail(
            this INetworkApi api,
            string questionIds,
            string site,
            string order = "desc",
            string sort = "votes",
            int page = 1,
            int pagesize = 10) =>
            api.QuestionAnswers(questionIds, site, order, sort, page, pagesize, filter: Filters.AnswerDetail);

    }
}
