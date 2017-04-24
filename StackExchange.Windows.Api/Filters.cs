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
        public const string QuestionDetail = "!b0OfNFIBFo03eJ";
        
        // <summary>
        // Defines a filter that returns question info that contains only the information needed for a list.
        // </summary>
        //public const string QuestionWithListInfo = "";

        /// <summary>
        /// Defines a filter that returns answers with detail information such as
        /// the body and comments.
        /// </summary>
        public const string AnswerDetail = "!)rCcH8tl27TkOv40lXxi";
    }
}
