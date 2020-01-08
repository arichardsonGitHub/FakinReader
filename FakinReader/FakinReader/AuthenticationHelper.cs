using Newtonsoft.Json;
using RedditSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FakinReader.Helpers
{
    public class AuthenticationHelper
    {
        #region Fields
        public const string ACCESS_TOKEN_KEY = "FakinReader.AccessToken";
        public const string CLIENT_ID = "aoJ6tnu56BmRDQ";
        public const string KNOWN_USERS_KEY = "FakinReader.KnownUsers";
        public const string REDIRECT_URL = "red://FakinReader.companyname.com/oauth2redirect";
        public const string REFRESH_TOKEN_KEY = "FakinReader.RefreshToken";
        public const string USER_AGENT = "FakinReader";
        public const string LAST_ACTIVE_USER_KEY = "FakinReader.LastActiveUser";
        public static string AuthorizationCode;
        public static WebRedirectAuthenticator WebRedirectAuthenticator;
        private static Reddit _reddit;
        #endregion Fields

        #region Properties
        public static string AccessToken => GetSetting("FakinReader.AccessToken");

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

        public static string RefreshToken => GetSetting("FakinReader.RefreshToken");

        public static TokensFromAuthorization TokenFromAuthorization { get; set; }
        #endregion Properties

        #region Methods

        public static void DeleteSavedTokens()
        {
            Preferences.Remove(ACCESS_TOKEN_KEY);

            Preferences.Remove(REFRESH_TOKEN_KEY);
        }

        public static string GetAuthorizationUrl()
        {
            var authProvider = new AuthProvider(CLIENT_ID, null, REDIRECT_URL);

            var scopes = AuthProvider.Scope.edit | AuthProvider.Scope.flair | AuthProvider.Scope.history | AuthProvider.Scope.identity | AuthProvider.Scope.modconfig | AuthProvider.Scope.modflair | AuthProvider.Scope.modlog | AuthProvider.Scope.modposts | AuthProvider.Scope.modwiki | AuthProvider.Scope.mysubreddits | AuthProvider.Scope.privatemessages | AuthProvider.Scope.read | AuthProvider.Scope.report | AuthProvider.Scope.save | AuthProvider.Scope.submit | AuthProvider.Scope.subscribe | AuthProvider.Scope.vote | AuthProvider.Scope.wikiedit | AuthProvider.Scope.wikiread;

            return authProvider.GetAuthUrl("step1", scopes, true);
        }

        public static Task<List<User>> GetKnownUsers()
        {
            List<User> listOfKnownUsers = new List<User>();

            var knownUsers = Preferences.Get(KNOWN_USERS_KEY, null);

            if (knownUsers != null)
            {
                listOfKnownUsers = JsonConvert.DeserializeObject<List<User>>(knownUsers);
            }

            return Task.FromResult(listOfKnownUsers);
        }

        //todo: move this into a different class. I'm just being lazy for now
        public static Reddit GetRedditObject()
        {
            var rwa = new RefreshTokenWebAgent(RefreshToken, CLIENT_ID, null, REDIRECT_URL)
            {
                UserAgent = USER_AGENT
            };

            return new Reddit(rwa, true);
        }

        public static string GetSetting(string key)
        {
            return Preferences.Get(key, null);
        }

        public static void SaveSetting(string key, string value)
        {
            Preferences.Set(key, value);
        }

        public static void RemoveSetting(string key)
        {
            Preferences.Remove(key);
        }

        public static async Task SaveUserAsync(User user)
        {
            if (user != null)
            {
                var listOfKnownUsers = await GetKnownUsers();

                var existingUser = listOfKnownUsers.Where(x => x.Username.ToUpper() == user.Username.ToUpper()).FirstOrDefault();

                if (existingUser != null)
                {
                    listOfKnownUsers.Remove(existingUser);

                    listOfKnownUsers.Add(user);
                }
                else
                {
                    listOfKnownUsers.Add(user);
                }

                Preferences.Set(KNOWN_USERS_KEY, JsonConvert.SerializeObject(listOfKnownUsers));
            }
        }

        public static async Task<bool> SecureSave()
        {
            try
            {
                Preferences.Set(ACCESS_TOKEN_KEY, TokenFromAuthorization.AccessToken);

                Preferences.Set(REFRESH_TOKEN_KEY, TokenFromAuthorization.RefreshToken);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool UserHasAuthorizedThisApp()
        {
            try
            {
                return RefreshToken != null;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public static async Task<User> GetLastActiveUser()
        {
            var lastActiveUsername = GetSetting(LAST_ACTIVE_USER_KEY);

            if (string.IsNullOrEmpty(lastActiveUsername) != true)
            {
                var listOfKnownUsers = await GetKnownUsers();

                var lastKnownActiveUser = listOfKnownUsers.Where(x => x.Username.ToUpper() == lastActiveUsername.ToUpper()).FirstOrDefault();

                return lastKnownActiveUser;
            }

            return null;

        }

        public static Task<bool> LogUserIn(string userName)
        {
            var user = new User(userName, null, null);

            return LogUserIn(user);
        }

        public static Task<bool> LogUserIn(User user)
        {
            try
            {
                SaveSetting(ACCESS_TOKEN_KEY, user.AccessToken);

                SaveSetting(REFRESH_TOKEN_KEY, user.RefreshToken);

                SaveSetting(LAST_ACTIVE_USER_KEY, user.Username);

                return Task.FromResult(true);
            }
            catch(Exception exception)
            {
                return Task.FromResult(false);
            }
        }

        public static Task<bool> LogCurrentUserOut()
        {
            try
            {
                SaveSetting(ACCESS_TOKEN_KEY, null);

                SaveSetting(REFRESH_TOKEN_KEY, null);

                SaveSetting(LAST_ACTIVE_USER_KEY, null);

                return Task.FromResult(true);
            }
            catch (Exception exception)
            {
                return Task.FromResult(false);
            }
        }

        //public static void SendToActivate()
        //{
        //    Device.OpenUri(new Uri(GetAuthUrl()));

        //    if(TokenFromAuthorization != null)
        //    {
        //        var reddit = GetRedditObject();

        //        if(reddit != null)
        //        {
        //        }
        //    }
        //}
        public static void SendToActivate()
        {
            WebView webView = new WebView
            {
                Source = GetAuthorizationUrl()
            };


            webView.IsVisible = true;            
        }



        #endregion Methods
    }
}