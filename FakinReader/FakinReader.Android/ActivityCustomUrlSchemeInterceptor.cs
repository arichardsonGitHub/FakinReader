using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using FakinReader.Helpers;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FakinReader.Droid
{
    [Activity(Label = "ActivityCustomUrlSchemeInterceptor", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    DataScheme = "red",
    DataHost = "FakinReader.companyname.com",
    DataPath = "/oauth2redirect")]
    public class ActivityCustomUrlSchemeInterceptor : Activity
    {
        #region Methods

        private async Task<NameValueCollection> ParseQueryString(string s)
        {
            var nameValueCollection = new NameValueCollection();

            // remove anything other than query string from url
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
                    // only one key with no value specified in query string
                    nameValueCollection.Add(singlePair[0], string.Empty);
                }
            }

            return nameValueCollection;
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var parsedQuery = await ParseQueryString(Intent.Data.Query);

            AuthenticationHelper.AuthorizationCode = parsedQuery.Get("code");// Intent.Data.Query.Substring(Intent.Data.Query.IndexOf("code") + 5); //todo: this is a little janky

            var tokens = await AuthenticationHelper.AuthProvider.GetOAuthRefreshTokenFromCodeAsync(AuthenticationHelper.AuthorizationCode);

            AuthenticationHelper.TokenFromAuthorization = tokens;

            await AuthenticationHelper.SecureSave();

            Finish();

            return;
        }
        #endregion Methods
    }
}