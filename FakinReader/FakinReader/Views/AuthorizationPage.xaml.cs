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

        public ISettingsManager SettingsManager => DependencyService.Get<ISettingsManager>();
        #endregion Properties

        #region Methods
        protected override void OnAppearing()
        {
            base.OnAppearing();

            webView.Source = AccountManager.GetAuthorizationUrl();

            webView.Navigated += WebView_Navigated;

            webView.Navigating += WebView_NavigatingAsync;
        }

        private async Task Authenticate(string urlWithAuthenticationCode)
        {
            await AccountManager.LogOutAllAccounts();

            var parsed = await Helpers.Helpers.ParseQueryString(urlWithAuthenticationCode);

            var tokens = await AuthenticationManager.AuthProvider.GetOAuthRefreshTokenFromCodeAsync(parsed.Get("code"));

            await AccountManager.SecureSave(parsed.Get("code"));

            SettingsManager.SaveSetting(AccountManager.AuthorizationCodeKey, parsed.Get("code"));

            SettingsManager.SaveSetting(AccountManager.ActiveRefreshTokenKey, tokens.RefreshToken);

            SettingsManager.SaveSetting(AccountManager.ActiveAccessTokenKey, tokens.AccessToken);

            var user = new Account(AuthenticationManager.Reddit.User.Name, tokens.AccessToken, tokens.RefreshToken, parsed.Get("code"));

            AccountManager.SaveAccount(user);

            SettingsManager.SaveSetting(AccountManager.ActiveUserNameKey, user.Username);
        }

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (e.Url.ToUpper().StartsWith(AuthenticationManager.RedirectUrl.ToUpper()))
            {
                Navigation.PopAsync();
                
                Application.Current.MainPage = new MainPage();
            }
        }

        private async void WebView_NavigatingAsync(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.ToUpper().Contains("&CODE"))
            {
                await Authenticate(e.Url);
            }

            if(e.Url.ToUpper() == "HTTPS://WWW.REDDIT.COM/")
            {
                webView.Source = AccountManager.GetAuthorizationUrl();
            }
        }
        #endregion Methods
    }
}