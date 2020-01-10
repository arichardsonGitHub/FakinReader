using RedditSharp;
using Xamarin.Forms;

namespace FakinReader.Services
{
    public class AuthenticationManager : IAuthenticationManager
    {
        #region Fields
        private const string CLIENT_ID = "aoJ6tnu56BmRDQ";
        private const string REDIRECT_URL = "red://FakinReader.companyname.com/oauth2redirect";
        private const string USER_AGENT = "FakinReader";
        private static Reddit _reddit;
        #endregion Fields

        #region Properties
        public IAccountManager AccountManager => DependencyService.Get<IAccountManager>();
        public AuthProvider AuthProvider => new AuthProvider(CLIENT_ID, null, REDIRECT_URL);
        public ISettingsManager SettingsManager => DependencyService.Get<ISettingsManager>();

        public Reddit Reddit
        {
            get
            {
                if (_reddit == null)
                {
                    _reddit = GetRedditObject();
                }

                return _reddit;
            }
        }

        public string RedirectUrl => REDIRECT_URL;
        #endregion Properties

        #region Methods

        private Reddit GetRedditObject()
        {
            RefreshTokenWebAgent refreshTokenWebAgent;

            if (AccountManager.ActiveUser != null)
            {
                refreshTokenWebAgent = new RefreshTokenWebAgent(AccountManager.ActiveUser.RefreshToken, CLIENT_ID, null, REDIRECT_URL)
                {
                    UserAgent = USER_AGENT
                };

                return new Reddit(refreshTokenWebAgent, true);

            }
            else if (SettingsManager.GetSetting(AccountManager.ActiveRefreshTokenKey) != null)
            {
                refreshTokenWebAgent = new RefreshTokenWebAgent(SettingsManager.GetSetting(AccountManager.ActiveRefreshTokenKey), CLIENT_ID, null, REDIRECT_URL)
                {
                    UserAgent = USER_AGENT
                };

                return new Reddit(refreshTokenWebAgent, true);

            }
            else
            {
                return null;
            }
        }
        #endregion Methods
    }
}