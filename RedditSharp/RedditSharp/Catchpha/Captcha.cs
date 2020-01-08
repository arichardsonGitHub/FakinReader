using System;

namespace RedditSharp
{
    /// <summary>
    /// A captcha challenge.
    /// </summary>
    public struct Captcha
    {
        #region Constructors

        internal Captcha(string id)
        {
            Id = id;
            Url = new Uri(string.Format(UrlFormat, Id), UriKind.Absolute);
        }
        #endregion Constructors

        #region Fields

        /// <summary>
        /// Captcha Id.
        /// </summary>
        public readonly string Id;

        /// <summary>
        /// Captcha url.
        /// </summary>
        public readonly Uri Url;

        private const string UrlFormat = "http://www.reddit.com/captcha/{0}";
        #endregion Fields
    }
}