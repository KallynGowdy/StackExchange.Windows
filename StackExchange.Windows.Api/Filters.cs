using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchange.Windows.Api
{
    /// <summary>
    /// Defines a set of filters that can be used with the StackExchange API for returning the expected information.
    /// </summary>
    public static class Filters
    {
        /// <summary>
        /// Defines a filter that returns questions with detail information such as
        /// the body, and comments.
        /// </summary>
        public const string QuestionDetail = "!g9XCYK.yjpHYJ7cr0kEdMUO)HSi-RxJp.8N";
        
        // <summary>
        // Defines a filter that returns question info that contains only the information needed for a list.
        // </summary>
        //public const string QuestionWithListInfo = "";

        /// <summary>
        /// Defines a filter that returns answers with detail information such as
        /// the body and comments.
        /// </summary>
        public const string AnswerDetail = "!3yXvh5nX2Ow3EnKVm";
    }
}
