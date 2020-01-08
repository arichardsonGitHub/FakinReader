using FakinReader.Helpers;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FakinReader.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AuthorizationPage : ContentPage
    {
        #region Constructors

        public AuthorizationPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            webView.Source = AuthenticationHelper.GetAuthorizationUrl();


            webView.Navigated += WebView_Navigated;

            webView.Navigating += WebView_NavigatingAsync;

        }

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

        private async Task SaveAuthorizationFromUrlParameter(string url)
        {
            var parsedQuery = await ParseQueryString(url);

            AuthenticationHelper.AuthorizationCode = parsedQuery.Get("code");

            var tokens = await AuthenticationHelper.AuthProvider.GetOAuthRefreshTokenFromCodeAsync(AuthenticationHelper.AuthorizationCode);

            AuthenticationHelper.TokenFromAuthorization = tokens;

            await AuthenticationHelper.SecureSave();

        }
        private async void WebView_NavigatingAsync(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.ToUpper().Contains("&CODE"))
            {
                await SaveAuthorizationFromUrlParameter(e.Url);

                var reddit = AuthenticationHelper.GetRedditObject();

                var user = new FakinReader.User(reddit.User.Name, AuthenticationHelper.AccessToken, AuthenticationHelper.RefreshToken);

                AuthenticationHelper.SaveSetting(AuthenticationHelper.LAST_ACTIVE_USER_KEY, user.Username);

                await AuthenticationHelper.SaveUserAsync(user);
            }
        }

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (e.Url.ToUpper().StartsWith(AuthenticationHelper.REDIRECT_URL.ToUpper()))
            {                
                Navigation.PopAsync();
            }
        }
        #endregion Constructors
    }
}