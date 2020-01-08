using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace RedditSharp.Things
{
    /// <summary>
    /// Post images provided for the post.
    /// </summary>
    public class Images
    {
        #region Constructors

        public Images(IWebAgent agent, JToken json)
        {
        }
        #endregion Constructors

        #region Properties

        /// <summary>
        /// Post preview id.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; private set; }

        /// <summary>
        /// Different resolutions of the source image for the post.
        /// </summary>
        [JsonProperty("resolutions")]
        public List<Image> Resolutions { get; private set; }

        /// <summary>
        /// Source image for the post.
        /// </summary>
        [JsonProperty("source")]
        public Image Source { get; private set; }

        #endregion Properties
    }
}