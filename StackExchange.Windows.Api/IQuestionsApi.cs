using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;
using StackExchange.Windows.Api.Models;

namespace StackExchange.Windows.Api
{
    public interface IQuestionsApi
    {
        [Get("/questions")]
        Task<Response<Question>> Questions(
            string site,
            string order = "desc",
            string sort = "activity",
            int page = 1,
            int pagesize = 10,
            string filter = "withbody");

        [Get("/questions/{questionIds}/answers")]
        Task<Response<Answer>> QuestionAnswers(
            string questionIds,
            string site,
            string order = "desc",
            string sort = "votes",
            int page = 1,
            int pagesize = 10,
            string filter = "withbody");
    }
}
