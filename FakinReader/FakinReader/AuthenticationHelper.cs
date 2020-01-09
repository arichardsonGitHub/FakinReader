using RedditSharp;
using Xamarin.Essentials;

namespace FakinReader.Helpers
{
    public class AuthenticationHelper
    {
        #region Fields
        public const string CLIENT_ID = "aoJ6tnu56BmRDQ";
        public const string REDIRECT_URL = "red://FakinReader.companyname.com/oauth2redirect";
        public const string USER_AGENT = "FakinReader";
        private static Reddit _reddit;
        #endregion Fields

        #region Properties
        public static AuthProvider AuthProvider => new AuthProvider(CLIENT_ID, null, REDIRECT_URL);

        public static Reddit Reddit
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

        #endregion Properties

        #region Methods

        public static Reddit GetRedditObject()
        {
            var rwa = new RefreshTokenWebAgent(GetSetting(AccountManager.REFRESH_TOKEN_KEY), CLIENT_ID, null, REDIRECT_URL)
            {
                UserAgent = USER_AGENT
            };

            return new Reddit(rwa, true);
        }

        public static string GetSetting(string key)
        {
            return Preferences.Get(key, null);
        }

        public static void RemoveSetting(string key)
        {
            Preferences.Remove(key);
        }

        public static void SaveSetting(string key, string value)
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