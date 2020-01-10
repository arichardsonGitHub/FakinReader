using Newtonsoft.Json;
using RedditSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FakinReader.Services
{
    public class AccountManager : IAccountManager
    {
        #region Fields
        private const string ACTIVE_ACCESS_TOKEN_KEY = "FakinReader.ActiveAccessToken";
        private const string ACTIVE_REFRESH_TOKEN_KEY = "FakinReader.ActiveRefreshToken";
        private const string ACTIVE_USER_NAME_KEY = "FakinReader.ActiveUserName";
        private const string AUTHORIZATION_CODE_KEY = "FakinReader.AuthorizationCode";
        private const string SAVED_USERS_KEY = "FakinReader.SavedUsers";
        private User _activeUser;
        private List<User> _savedUsers;
        #endregion Fields

        #region Properties
        public string ActiveAccessTokenKey => ACTIVE_ACCESS_TOKEN_KEY;

        public string ActiveRefreshTokenKey => ACTIVE_REFRESH_TOKEN_KEY;

        public User ActiveUser
        {
            get
            {
                if (_activeUser == null)
                {
                    var currentActiveUser = SettingsManager.GetSetting(ACTIVE_USER_NAME_KEY);

                    if (string.IsNullOrEmpty(currentActiveUser) == false)
                    {
                        _activeUser = SavedUsers.Where(x => x.Username.ToUpper() == currentActiveUser.ToUpper()).Single();
                    }
                }

                return _activeUser;
            }
        }

        public string ActiveUserNameKey => ACTIVE_USER_NAME_KEY;

        public IAuthenticationManager AuthenticationManager => DependencyService.Get<IAuthenticationManager>();

        public string AuthorizationCodeKey => AUTHORIZATION_CODE_KEY;

        public List<User> SavedUsers
        {
            get
            {
                if (_savedUsers == null)
                {
                    _savedUsers = GetSavedUsers().Result;
                }

                return _savedUsers;
            }
        }

        public ISettingsManager SettingsManager => DependencyService.Get<ISettingsManager>();
        #endregion Properties

        #region Methods

        public string GetAuthorizationUrl()
        {
            var scopes = AuthProvider.Scope.edit | AuthProvider.Scope.flair | AuthProvider.Scope.history | AuthProvider.Scope.identity | AuthProvider.Scope.modconfig | AuthProvider.Scope.modflair | AuthProvider.Scope.modlog | AuthProvider.Scope.modposts | AuthProvider.Scope.modwiki | AuthProvider.Scope.mysubreddits | AuthProvider.Scope.privatemessages | AuthProvider.Scope.read | AuthProvider.Scope.report | AuthProvider.Scope.save | AuthProvider.Scope.submit | AuthProvider.Scope.subscribe | AuthProvider.Scope.vote | AuthProvider.Scope.wikiedit | AuthProvider.Scope.wikiread;

            return AuthenticationManager.AuthProvider.GetAuthUrl("step1", scopes, true);
        }

        public Task<bool> LogOut()
        {
            try
            {
                SettingsManager.SaveSetting(ACTIVE_USER_NAME_KEY, null);

                SettingsManager.SaveSetting(ACTIVE_ACCESS_TOKEN_KEY, null);

                SettingsManager.SaveSetting(ACTIVE_REFRESH_TOKEN_KEY, null);

                _activeUser = null;

                var savedUsers = GetSavedUsers().Result;

                foreach (var user in savedUsers)
                {
                    user.AccessToken = null;

                    user.AuthorizationCodeForSession = null;

                    user.RefreshToken = null;
                }

                return Task.FromResult(true);
            }
            catch (Exception exception)
            {
                return Task.FromResult(false);
            }
        }

        public void MakeUserActive(string username)
        {
            var existingUser = SavedUsers.Where(x => x.Username.ToUpper() == username.ToUpper()).Single();

            if (existingUser.HasAuthorizedThisApp == false)
            {
                SendToActivate();
            }
            else
            {

                SettingsManager.SaveSetting(ACTIVE_USER_NAME_KEY, existingUser.Username);

                SettingsManager.SaveSetting(ACTIVE_ACCESS_TOKEN_KEY, existingUser.AccessToken);

                SettingsManager.SaveSetting(ACTIVE_REFRESH_TOKEN_KEY, existingUser.RefreshToken);
            }
        }

        public async void SaveUser(User user)
        {
            var listOfSavedUsers = await GetSavedUsers();

            var existingUser = listOfSavedUsers.Where(x => x.Username.ToUpper() == user.Username.ToUpper()).FirstOrDefault();

            if (existingUser != null)
            {
                listOfSavedUsers.Remove(existingUser);
            }

            listOfSavedUsers.Add(user);

            var toSave = JsonConvert.SerializeObject(listOfSavedUsers);

            SettingsManager.SaveSetting(SAVED_USERS_KEY, toSave);
        }

        public async Task<bool> SecureSave(string authorizationCode, string userName = null)
        {
            try
            {
                SettingsManager.SaveSetting(AUTHORIZATION_CODE_KEY, authorizationCode);

                SettingsManager.SaveSetting(ACTIVE_USER_NAME_KEY, userName);

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

        private Task<List<User>> GetSavedUsers()
        {
            var savedUsers = new List<User>();

            var savedUsersSerialized = Preferences.Get(SAVED_USERS_KEY, null);

            try
            {
                if (savedUsersSerialized != null)
                {
                    savedUsers = JsonConvert.DeserializeObject<List<User>>(savedUsersSerialized);
                }
            }
            catch (Exception e)
            {
            }

            return Task.FromResult(savedUsers);
        }
        #endregion Methods
    }
}