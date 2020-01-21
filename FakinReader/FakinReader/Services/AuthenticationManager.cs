using Reddit;
using RedditSharp;
using System;
using Xamarin.Forms;

namespace FakinReader.Services
{
    public class AuthenticationManager : IAuthenticationManager
    {
        #region Fields
        private const string CLIENT_ID = "aoJ6tnu56BmRDQ";
        private const string REDIRECT_URL = "red://FakinReader.companyname.com/oauth2redirect";
        private const string USER_AGENT = "FakinReader";
        private RedditSharp.Reddit _reddit;
        private Reddit.RedditClient _redditClient;
        #endregion Fields

        #region Properties
        public IAccountManager AccountManager => DependencyService.Get<IAccountManager>();
        public AuthProvider AuthProvider => new AuthProvider(CLIENT_ID, null, REDIRECT_URL);
        public RedditSharp.Reddit Reddit
        {
            get
            {
                if (_reddit == null)
                {
                    _reddit = GetRedditSharpRedditObject();
                }

                return _reddit;
            }
        }
        public Reddit.RedditClient RedditClient
        {
            get
            {
                if (_redditClient == null)
                {
                    _redditClient = GetRedditClient();
                }

                return _redditClient;
            }
        }

        public string RedirectUrl => REDIRECT_URL;
        public ISettingsManager SettingsManager => DependencyService.Get<ISettingsManager>();
        #endregion Properties

        #region Methods
        private Reddit.RedditClient GetRedditClient()
        {
            if (AccountManager.ActiveAccount != null && AccountManager.ActiveAccount.Username.ToUpper() != "LOGGED OUT")
            {
                return new RedditClient(appId: CLIENT_ID, refreshToken: AccountManager.ActiveAccount.RefreshToken, accessToken: AccountManager.ActiveAccount.AccessToken, userAgent: USER_AGENT); ;
            }
            else if (SettingsManager.GetSetting(AccountManager.ActiveRefreshTokenKey) != null && SettingsManager.GetSetting(AccountManager.ActiveAccessTokenKey) != null)
            {
                return new RedditClient(appId: CLIENT_ID, refreshToken: SettingsManager.GetSetting(AccountManager.ActiveRefreshTokenKey), accessToken: SettingsManager.GetSetting(AccountManager.ActiveAccessTokenKey), userAgent: USER_AGENT);
            }
            else
            {
                return new RedditClient(appId: CLIENT_ID, userAgent: USER_AGENT);
            }
        }
        private RedditSharp.Reddit GetRedditSharpRedditObject()
        {
            RefreshTokenWebAgent refreshTokenWebAgent;

            if (AccountManager.ActiveAccount != null && AccountManager.ActiveAccount.Username.ToUpper() != "LOGGED OUT")
            {
                refreshTokenWebAgent = new RefreshTokenWebAgent(AccountManager.ActiveAccount.RefreshToken, CLIENT_ID, null, REDIRECT_URL)
                {
                    UserAgent = USER_AGENT
                };

                return new RedditSharp.Reddit(refreshTokenWebAgent, true);
            }
            else if (SettingsManager.GetSetting(AccountManager.ActiveRefreshTokenKey) != null)
            {
                refreshTokenWebAgent = new RefreshTokenWebAgent(SettingsManager.GetSetting(AccountManager.ActiveRefreshTokenKey), CLIENT_ID, null, REDIRECT_URL)
                {
                    UserAgent = USER_AGENT
                };

                return new RedditSharp.Reddit(refreshTokenWebAgent, true);
            }
            else
            {
                return new RedditSharp.Reddit();
                //return null;
            }
        }
        #endregion Methods
    }
}