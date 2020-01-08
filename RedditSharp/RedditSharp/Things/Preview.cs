using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace RedditSharp.Things
{
    /// <summary>
    /// Post preview images.
    /// </summary>
    public class Preview
    {
        #region Constructors

        public Preview(IWebAgent agent, JToken json)
        {
        }
        #endregion Constructors

        #region Properties

        /// <summary>
        /// Is there a preview enabled for this post.
        /// </summary>
        [JsonProperty("enabled")]
        public bool Enabled { get; private set; }

        /// <summary>
        /// List of post image in various resolutions.
        /// </summary>
        [JsonProperty("images")]
        public List<Images> Images { get; private set; }

        #endregion Properties
    }
}