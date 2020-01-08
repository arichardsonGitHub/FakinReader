using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace RedditSharp.Things
{
    /// <summary>
    /// Represents more comments omitted from the base comment tree.
    ///
    /// https://github.com/reddit/reddit/wiki/JSON#more
    /// </summary>
    public class More : Thing
    {
        #region Constructors

        /// <inheritdoc />
        public More(IWebAgent agent, JToken json) : base(agent, json)
        {
        }
        #endregion Constructors

        #region Fields
        private const string MoreUrl = "/api/morechildren.json?link_id={0}&children={1}&api_type=json";
        #endregion Fields

        #region Properties

        /// <summary>
        /// Bae36 Ids of children.
        /// </summary>
        [JsonProperty("children")]
        public string[] Children { get; private set; }

        /// <summary>
        /// Parent base36 id.
        /// </summary>
        [JsonProperty("parent_id")]
        public string ParentId { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<List<Thing>> GetThingsAsync()
        {
            var url = string.Format(MoreUrl, ParentId, string.Join(",", Children));
            var json = await WebAgent.Get(url).ConfigureAwait(false);
            if (json["errors"].Count() != 0)
                throw new AuthenticationException("Incorrect login.");
            var moreJson = json["data"]["things"];
            return moreJson.Select(t => Thing.Parse(WebAgent, t)).ToList();
        }

        /// <inheritdoc />
        internal override JToken GetJsonData(JToken json) => json["data"];
        #endregion Methods
    }
}