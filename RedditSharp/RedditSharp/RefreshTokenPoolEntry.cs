using System;

namespace RedditSharp
{
    /// <summary>
    /// Information required to locate and generate a <see cref="RefreshTokenWebAgent"/>
    /// </summary>
    public class RefreshTokenPoolEntry
    {
        #region Constructors

        /// <summary>
        /// Create a new entry with given refresh token and settings.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="refreshToken"></param>
        /// <param name="rateLimiterMode">Defaults to RateLimitMode.Burst</param>
        /// <param name="userAgentString">If none is provided, will use default UserAgentString set on RefreshTokenWebAgentPool</param>
        public RefreshTokenPoolEntry(string username, string refreshToken, RateLimitMode rateLimiterMode = RateLimitMode.Burst, string userAgentString = "")
        {
            Username = username;
            RefreshToken = refreshToken;
            RateLimiterMode = rateLimiterMode;
            UserAgentString = userAgentString;
            WebAgentID = Guid.NewGuid();
        }
        #endregion Constructors

        #region Properties

        /// <summary>
        /// Rate Limit mode that the WebAgent will use
        /// </summary>
        public RateLimitMode RateLimiterMode { get; set; }

        /// <summary>
        /// Time that the <see cref="AccessToken"/> expires
        /// </summary>
        public DateTime TokenExpires { get; set; }

        /// <summary>
        /// User Agent to pass to Reddit's API
        /// </summary>
        public string UserAgentString { get; set; }

        /// <summary>
        /// Username of the <see cref="RefreshToken"/> owner
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Unique cache identifier for WebAgent
        /// </summary>
        public Guid WebAgentID { get; set; }

        internal string AccessToken { get; set; }
        internal string RefreshToken { get; set; }
        #endregion Properties
    }
}