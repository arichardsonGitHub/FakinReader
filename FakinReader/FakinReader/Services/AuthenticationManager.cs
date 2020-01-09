using RedditSharp;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FakinReader.Services
{
    public class AuthenticationManager : IAuthenticationManager
    {
        #region Fields
        public const string CLIENT_ID = "aoJ6tnu56BmRDQ";
        public const string REDIRECT_URL = "red://FakinReader.companyname.com/oauth2redirect";
        public const string USER_AGENT = "FakinReader";
        private static Reddit _reddit;
        #endregion Fields

        #region Properties
        public IAccountManager AccountManager => DependencyService.Get<IAccountManager>();
        public AuthProvider AuthProvider => new AuthProvider(CLIENT_ID, null, REDIRECT_URL);

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

        public Reddit GetRedditObject()
        {
            var rwa = new RefreshTokenWebAgent(GetSetting(AccountManager.RefreshTokenKey), CLIENT_ID, null, REDIRECT_URL)
            {
                UserAgent = USER_AGENT
            };

            return new Reddit(rwa, true);
        }

        public string GetSetting(string key)
        {
            return Preferences.Get(key, null);
        }

        public void RemoveSetting(string key)
        {
            Preferences.Remove(key);
        }

        public void SaveSetting(string key, string value)
        {
            if (string.IsNullOrEmpty(value) == false)
            {
                Preferences.Set(key, value);
            }
            else
            {
                Preferences.Set(key, null);
            }
        }
        #endregion Methods
    }
}