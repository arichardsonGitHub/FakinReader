using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace RedditSharp
{
    partial class Helpers
    {
        #region Fields

        static internal JsonSerializer jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
        {
            CheckAdditionalContent = false,
            DefaultValueHandling = DefaultValueHandling.Ignore
        });

        #endregion Fields

        #region Methods

        internal static void PopulateObject(JToken json, object obj)
        {
            if (json == null)
                throw new ArgumentNullException(nameof(json));
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            using (var reader = json.CreateReader())
            {
                jsonSerializer.Populate(reader, obj);
            }
        }
        #endregion Methods
    }
}