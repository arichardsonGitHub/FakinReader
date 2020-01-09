using Newtonsoft.Json;
using RedditSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;

namespace FakinReader.Services
{
    public class AccountManager : IAccountManager
    {
        #region Fields
        public const string AUTHORIZATION_CODE_KEY = "FakinReader.AuthorizationCode";
        public const string CURRENT_SESSION_USERNAME = "FakinReader.CurrentSessionUsername";
        public const string PREVIOUS_SESSION_USERS_KEY = "FakinReader.PreviousSessionUsers";
        private const string ACCESS_TOKEN_KEY = "FakinReader.AccessToken";
        private const string REFRESH_TOKEN_KEY = "FakinReader.RefreshToken";
        private User _applicationUser;
        #endregion Fields

        #region Properties
        public string AccessTokenKey => ACCESS_TOKEN_KEY;

        public User ApplicationUser
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

                    AddToPreviousSessionUsers(value.Username);
                }
                else
                {
                    SettingsManager.SaveSetting(CURRENT_SESSION_USERNAME, null);
                }
            }
        }

        public List<string> PreviousSessionUsers
        {
            get => GetPreviousSessionUsers().Result;
        }

        public string RefreshTokenKey => REFRESH_TOKEN_KEY;
        public ISettingsManager SettingsManager => DependencyService.Get<ISettingsManager>();
        #endregion Properties

        #region Methods

        public string GetAuthorizationUrl()
        {
            var authProvider = new AuthProvider(AuthenticationManager.CLIENT_ID, null, AuthenticationManager.REDIRECT_URL);

            var scopes = AuthProvider.Scope.edit | AuthProvider.Scope.flair | AuthProvider.Scope.history | AuthProvider.Scope.identity | AuthProvider.Scope.modconfig | AuthProvider.Scope.modflair | AuthProvider.Scope.modlog | AuthProvider.Scope.modposts | AuthProvider.Scope.modwiki | AuthProvider.Scope.mysubreddits | AuthProvider.Scope.privatemessages | AuthProvider.Scope.read | AuthProvider.Scope.report | AuthProvider.Scope.save | AuthProvider.Scope.submit | AuthProvider.Scope.subscribe | AuthProvider.Scope.vote | AuthProvider.Scope.wikiedit | AuthProvider.Scope.wikiread;

            return authProvider.GetAuthUrl("step1", scopes, true);
        }

        public async Task<User> GetLoggedInUser()
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

        public Task LoadLoggedInUser()
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

        public Task<bool> LogCurrentUserOut()
        {
            try
            {
                SettingsManager.SaveSetting(ACCESS_TOKEN_KEY, null);

                SettingsManager.SaveSetting(REFRESH_TOKEN_KEY, null);

                SettingsManager.SaveSetting(CURRENT_SESSION_USERNAME, null);

                ApplicationUser = null;

                return Task.FromResult(true);
            }
            catch (Exception exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> LogUserIn(string userName)
        {
            var user = new User(userName, null, null);

            return LogUserIn(user);
        }

        public Task<bool> LogUserIn(User user)
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

        public async Task<bool> SecureSave(string accessToken, string refreshToken, string authorizationCode, string userName = null)
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

        public void SendToActivate()
        {
            WebView webView = new WebView
            {
                Source = GetAuthorizationUrl()
            };

            webView.IsVisible = true;
        }

        private Task<List<string>> GetPreviousSessionUsers()
        {
            var listOfPreviousSessionUsers = new List<string>();

            var knownUsers = Preferences.Get(PREVIOUS_SESSION_USERS_KEY, null);

            if (knownUsers != null)
            {
                listOfPreviousSessionUsers = JsonConvert.DeserializeObject<List<string>>(knownUsers);
            }

            return Task.FromResult(listOfPreviousSessionUsers);
        }

        public async void AddToPreviousSessionUsers(string username)
        {
            var listOfPreviousSessionUsers = await GetPreviousSessionUsers();

            if (listOfPreviousSessionUsers.Contains(username) == false)
            {
                listOfPreviousSessionUsers.Add(username);
            }

            var toSave = JsonConvert.SerializeObject(listOfPreviousSessionUsers);

            SettingsManager.SaveSetting(PREVIOUS_SESSION_USERS_KEY, toSave);
        }
        #endregion Methods
    }
}