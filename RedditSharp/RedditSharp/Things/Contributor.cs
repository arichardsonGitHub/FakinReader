using Newtonsoft.Json.Linq;
using RedditSharp.Things.User;
using System;

namespace RedditSharp.Things
{
    /// <summary>
    /// A contributor to a subreddit.
    /// </summary>
    public class Contributor : RelatedUser
    {
        #region Constructors

        /// <inheritdoc />
        public Contributor(IWebAgent agent, JToken json) : base(agent, json)
        {
        }
        #endregion Constructors

        #region Properties

        /// <summary>
        /// Date contributor was added.
        /// </summary>
        [Obsolete("User RelUser.Date")]
        public DateTime? DateAdded { get => DateUTC; private set => DateUTC = value; }

        #endregion Properties
    }
}