using System.Threading.Tasks;

namespace RedditSharp
{
    partial class Helpers
    {
        #region Methods

        protected internal static async Task<T> GetThingAsync<T>(IWebAgent agent, string url) where T : Things.Thing
        {
            var json = await agent.Get(url).ConfigureAwait(false);

            return Things.Thing.Parse<T>(agent, json);
        }
        #endregion Methods
    }
}