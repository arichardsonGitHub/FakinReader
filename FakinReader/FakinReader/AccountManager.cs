using Newtonsoft.Json;
using RedditSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FakinReader.Helpers
{
    public class AccountManager
    {
        #region Fields
        public const string ACCESS_TOKEN_KEY = "FakinReader.AccessToken";
        public const string AUTHORIZATION_CODE_KEY = "FakinReader.AuthorizationCode";
        public const string CURRENT_SESSION_USERNAME = "FakinReader.CurrentSessionUsername";
        public const string PREVIOUS_SESSION_USERS = "FakinReader.PreviousSessionUsers";
        public const string REFRESH_TOKEN_KEY = "FakinReader.RefreshToken";
        private static User _applicationUser;
        #endregion Fields

        #region Properties

        public static User ApplicationUser
        {
            get
            {
                return _applicationUser;
            }
            set
            {
                _applicationUser = value;

                if (_applicationUser != null)
                {
                    SettingsManager.SaveSetting(CURRENT_SESSION_USERNAME, _applicationUser.Username);
                }
                else
                {
                    SettingsManager.SaveSetting(CURRENT_SESSION_USERNAME, null);
                }
            }
        }

        #endregion Properties

        #region Methods

        public static string GetAuthorizationUrl()
        {
            var authProvider = new AuthProvider(AuthenticationHelper.CLIENT_ID, null, AuthenticationHelper.REDIRECT_URL);

            var scopes = AuthProvider.Scope.edit | AuthProvider.Scope.flair | AuthProvider.Scope.history | AuthProvider.Scope.identity | AuthProvider.Scope.modconfig | AuthProvider.Scope.modflair | AuthProvider.Scope.modlog | AuthProvider.Scope.modposts | AuthProvider.Scope.modwiki | AuthProvider.Scope.mysubreddits | AuthProvider.Scope.privatemessages | AuthProvider.Scope.read | AuthProvider.Scope.report | AuthProvider.Scope.save | AuthProvider.Scope.submit | AuthProvider.Scope.subscribe | AuthProvider.Scope.vote | AuthProvider.Scope.wikiedit | AuthProvider.Scope.wikiread;

            return authProvider.GetAuthUrl("step1", scopes, true);
        }


        public static List<User> PreviousSessionUsers
        {
            get => GetPreviousSessionUsers().Result;
        }

        private static Task<List<User>> GetPreviousSessionUsers()
        {
            List<User> listOfPreviousSessionUsers = new List<User>();

            var knownUsers = Preferences.Get(PREVIOUS_SESSION_USERS, null);

            if (knownUsers != null)
            {
                listOfPreviousSessionUsers = JsonConvert.DeserializeObject<List<User>>(knownUsers);
            }

            return Task.FromResult(listOfPreviousSessionUsers);
        }

        public static async Task<User> GetLoggedInUser()
        {
            var currentUserLoggedIn = SettingsManager.GetSetting(CURRENT_SESSION_USERNAME);

            if (string.IsNullOrEmpty(currentUserLoggedIn) == false)
            {
                return new User(SettingsManager.GetSetting(CURRENT_SESSION_USERNAME), SettingsManager.GetSetting(ACCESS_TOKEN_KEY), SettingsManager.GetSetting(REFRESH_TOKEN_KEY));
            }
            else
            {
                return null;
            }
        }

        public static Task LoadLoggedInUser()
        {
            try
            {
                var currentUserLoggedIn = GetLoggedInUser();

                ApplicationUser = GetLoggedInUser().Result;

                return Task.FromResult(true);
            }
            catch (Exception exception)
            {
                return Task.FromResult(exception);
            }
        }

        public static Task<bool> LogCurrentUserOut()
        {
            try
            {
                SettingsManager.SaveSetting(ACCESS_TOKEN_KEY, null);

                SettingsManager.SaveSetting(REFRESH_TOKEN_KEY, null);

                return Task.FromResult(true);
            }
            catch (Exception exception)
            {
                return Task.FromResult(false);
            }
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
                SettingsManager.SaveSetting(ACCESS_TOKEN_KEY, user.AccessToken);

                SettingsManager.SaveSetting(REFRESH_TOKEN_KEY, user.RefreshToken);

                return Task.FromResult(true);
            }
            catch (Exception exception)
            {
                return Task.FromResult(false);
            }
        }

        public static async Task<bool> SecureSave(string accessToken, string refreshToken, string authorizationCode, string userName = null)
        {
            try
            {
                SettingsManager.SaveSetting(ACCESS_TOKEN_KEY, accessToken);

                SettingsManager.SaveSetting(REFRESH_TOKEN_KEY, refreshToken);

                SettingsManager.SaveSetting(AUTHORIZATION_CODE_KEY, authorizationCode);

                SettingsManager.SaveSetting(CURRENT_SESSION_USERNAME, userName);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

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