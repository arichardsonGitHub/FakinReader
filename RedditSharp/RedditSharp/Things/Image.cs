using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace RedditSharp.Things
{
    /// <summary>
    /// Single image for a post.
    /// </summary>
    public class Image
    {
        #region Constructors

        public Image(IWebAgent agent, JToken json)
        {
        }
        #endregion Constructors

        #region Properties

        /// <summary>
        /// Height of the image.
        /// </summary>
        [JsonProperty("height")]
        public int? Height { get; private set; }

        /// <summary>
        /// Url for the image.
        /// </summary>
        [JsonProperty("url")]
        public Uri Url { get; private set; }

        /// <summary>
        /// Width of the image.
        /// </summary>
        [JsonProperty("width")]
        public int? Width { get; private set; }

        #endregion Properties
    }
}