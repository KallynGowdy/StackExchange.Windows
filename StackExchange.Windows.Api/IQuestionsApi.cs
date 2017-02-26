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
        Task<Question[]> Questions(
            string site,
            string sort = "",
            int page = 0,
            int pagesize = 10);
    }
}
