﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedditSharp.Things.User;

namespace RedditSharp
{
    /// <summary>
    /// Represents a moderator.
    /// </summary>
    public class ModeratorUser : RelatedUser
    {
        #region Constructors

        public ModeratorUser(IWebAgent agent, JToken json) : base(agent, json)
        {
        }
        #endregion Constructors

        #region Properties

        /// <summary>
        /// Permissions the moderator has in the subreddit.
        /// </summary>
        [JsonProperty("mod_permissions")]
        [JsonConverter(typeof(ModeratorPermissionConverter))]
        public ModeratorPermission Permissions { get; private set; }

        #endregion Properties
    }
}