using System;

namespace RedditSharp
{
    public class RateLimitException : Exception
    {
        #region Constructors

        public RateLimitException(TimeSpan timeToReset)
        {
            TimeToReset = timeToReset;
        }
        #endregion Constructors

        #region Properties
        public TimeSpan TimeToReset { get; set; }
        #endregion Properties
    }
}