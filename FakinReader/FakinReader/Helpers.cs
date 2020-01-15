using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FakinReader.Helpers
{
    public class Helpers
    {
        #region Methods

        public Task<NameValueCollection> ParseQueryString(string s)
        {
            var nameValueCollection = new NameValueCollection();

            if (s.Contains("?"))
            {
                s = s.Substring(s.IndexOf('?') + 1);
            }

            foreach (string vp in Regex.Split(s, "&"))
            {
                var singlePair = Regex.Split(vp, "=");

                if (singlePair.Length == 2)
                {
                    nameValueCollection.Add(singlePair[0], singlePair[1]);
                }
                else
                {
                    nameValueCollection.Add(singlePair[0], string.Empty);
                }
            }

            return Task.FromResult(nameValueCollection);
        }
        #endregion Methods
    }
}