using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RedditSharp.Things.User
{
    public class NotedUser : RelatedUser
    {
        #region Constructors

        public NotedUser(IWebAgent agent, JToken json) : base(agent, json)
        {
        }
        #endregion Constructors

        #region Properties

        [JsonProperty("note")]
        public string Note { get; internal set; }

        #endregion Properties
    }
}