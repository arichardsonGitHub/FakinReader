using System;
using System.Net;

namespace RedditSharp
{
    /// <summary>
    /// HTTP exception from Reddit
    /// </summary>
    public class RedditHttpException : Exception
    {
        #region Constructors

        /// <summary>
        /// Create new exception with response code
        /// </summary>
        /// <param name="statusCode"></param>
        public RedditHttpException(HttpStatusCode statusCode)
          : base($"The server responded with error {(int)statusCode} ({statusCode})")
        {
            StatusCode = statusCode;
        }
        #endregion Constructors

        #region Properties

        /// <summary>
        /// Reddit status resposne code
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        #endregion Properties
    }
}