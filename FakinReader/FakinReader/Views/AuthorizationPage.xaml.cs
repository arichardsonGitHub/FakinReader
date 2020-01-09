using FakinReader.Services;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FakinReader.Views
{
    [DesignTimeVisible(false)]
    public partial class AuthorizationPage : ContentPage
    {
        #region Constructors

        public AuthorizationPage()
        {
            InitializeComponent();
        }
        #endregion Constructors

        #region Properties
        public IAccountManager AccountManager => DependencyService.Get<IAccountManager>();

        public IAuthenticationManager AuthenticationManager => DependencyService.Get<IAuthenticationManager>();
        #endregion Properties

        #region Methods

        protected override void OnAppearing()
        {
            base.OnAppearing();

            webView.Source = AccountManager.GetAuthorizationUrl();

            webView.Navigated += WebView_Navigated;

            webView.Navigating += WebView_NavigatingAsync;
        }

        private async Task LogUserInFromAuthenticationCode(string urlWithAuthenticationCode)
        {
            var parsed = await Helpers.Helpers.ParseQueryString(urlWithAuthenticationCode);

            var tokens = await AuthenticationManager.AuthProvider.GetOAuthRefreshTokenFromCodeAsync(parsed.Get("code"));

            await AccountManager.SecureSave(tokens.AccessToken, tokens.RefreshToken, parsed.Get("code"));

            var reddit = AuthenticationManager.GetRedditObject();

            var user = new FakinReader.User(reddit.User.Name, tokens.AccessToken, tokens.RefreshToken, parsed.Get("code"));

            AccountManager.ApplicationUser = user;
        }

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (e.Url.ToUpper().StartsWith(AuthenticationManager.RedirectUrl.ToUpper()))
            {
                Navigation.PopAsync();
            }
        }

        private async void WebView_NavigatingAsync(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.ToUpper().Contains("&CODE"))
            {
                await LogUserInFromAuthenticationCode(e.Url);
            }
        }
        #endregion Methods
    }
}