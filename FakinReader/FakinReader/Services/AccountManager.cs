using FakinReader.ViewModels;
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
    public class AccountManager : BaseViewModel, IAccountManager
    {
        public AccountManager()
        {
            _savedAccounts = GetSavedAccounts().Result;

            _activeAccount = new Account(LOGGED_OUT, null, null);
        }

        #region Fields
        private const string ACTIVE_ACCESS_TOKEN_KEY = "FakinReader.ActiveAccessToken";
        private const string ACTIVE_REFRESH_TOKEN_KEY = "FakinReader.ActiveRefreshToken";
        private const string ACTIVE_ACCOUNT_USERNAME_KEY = "FakinReader.ActiveAccountUserName";
        private const string AUTHORIZATION_CODE_KEY = "FakinReader.AuthorizationCode";
        private const string SAVED_ACCOUNTS_KEY = "FakinReader.SavedAccounts";
        private const string LOGGED_OUT = "Logged out";
        private Account _activeAccount;
        private List<Account> _savedAccounts;
        #endregion Fields

        #region Properties
        public string ActiveAccessTokenKey => ACTIVE_ACCESS_TOKEN_KEY;
        public Account ActiveAccount
        {
            get
            {
                if (_activeAccount == null || _activeAccount.Username.ToUpper() == LOGGED_OUT.ToUpper() )
                {
                    var currentActiveUser = SettingsManager.GetSetting(ACTIVE_ACCOUNT_USERNAME_KEY);

                    if (string.IsNullOrEmpty(currentActiveUser) == false)
                    {
                        _activeAccount = SavedAccounts.Where(x => x.Username.ToUpper() == currentActiveUser.ToUpper()).Single();
                    }
                }

                return _activeAccount;
            }
            set
            {
                SetProperty(ref _activeAccount, value, "ActiveAccount");
            }
        }
        public string ActiveRefreshTokenKey => ACTIVE_REFRESH_TOKEN_KEY;
        public string ActiveUserNameKey => ACTIVE_ACCOUNT_USERNAME_KEY;
        public IAuthenticationManager AuthenticationManager => DependencyService.Get<IAuthenticationManager>();
        public string AuthorizationCodeKey => AUTHORIZATION_CODE_KEY;
        public List<Account> SavedAccounts
        {
            get 
            { 
                return _savedAccounts; 
            }
            private set 
            { 
                SetProperty(ref _savedAccounts, value, "SavedAccounts");
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

        public Task<bool> LogOutAllAccounts()
        {
            try
            {
                SettingsManager.SaveSetting(ACTIVE_ACCOUNT_USERNAME_KEY, null);

                SettingsManager.SaveSetting(ACTIVE_ACCESS_TOKEN_KEY, null);

                SettingsManager.SaveSetting(ACTIVE_REFRESH_TOKEN_KEY, null);

                var savedUsers = GetSavedAccounts().Result;

                foreach (var user in savedUsers)
                {
                    user.AccessToken = null;

                    user.AuthorizationCodeForSession = null;

                    user.RefreshToken = null;
                }

                var toSave = JsonConvert.SerializeObject(savedUsers);

                SettingsManager.SaveSetting(SAVED_ACCOUNTS_KEY, toSave);

                ActiveAccount = new Account("Logged out", null, null);

                return Task.FromResult(true);
            }
            catch (Exception exception)
            {
                return Task.FromResult(false);
            }
        }

        public void MakeAccountActive(string username)
        {
            var existingUser = SavedAccounts.Where(x => x.Username.ToUpper() == username.ToUpper()).Single();

            if (existingUser.HasAuthorizedThisApp == false || existingUser == null)
            {
                SendToActivate();
            }
            else
            {
                SettingsManager.SaveSetting(ACTIVE_ACCOUNT_USERNAME_KEY, existingUser.Username);

                SettingsManager.SaveSetting(ACTIVE_ACCESS_TOKEN_KEY, existingUser.AccessToken);

                SettingsManager.SaveSetting(ACTIVE_REFRESH_TOKEN_KEY, existingUser.RefreshToken);

                ActiveAccount = existingUser;
            }
        }

        public async void SaveAccount(Account account)
        {
            var listOfSavedAccounts = await GetSavedAccounts();

            var existingAccount = listOfSavedAccounts.Where(x => x.Username.ToUpper() == account.Username.ToUpper()).FirstOrDefault();

            if (existingAccount != null)
            {
                listOfSavedAccounts.Remove(existingAccount);
            }

            listOfSavedAccounts.Add(account);

            var toSave = JsonConvert.SerializeObject(listOfSavedAccounts);

            SettingsManager.SaveSetting(SAVED_ACCOUNTS_KEY, toSave);

            SavedAccounts = listOfSavedAccounts;
        }

        public async Task<bool> SecureSave(string authorizationCode, string userName = null)
        {
            try
            {
                SettingsManager.SaveSetting(AUTHORIZATION_CODE_KEY, authorizationCode);

                SettingsManager.SaveSetting(ACTIVE_ACCOUNT_USERNAME_KEY, userName);

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

        private Task<List<Account>> GetSavedAccounts()
        {
            var savedUsers = new List<Account>();

            var savedUsersSerialized = Preferences.Get(SAVED_ACCOUNTS_KEY, null);

            if (savedUsersSerialized != null)
            {
                savedUsers = JsonConvert.DeserializeObject<List<Account>>(savedUsersSerialized);
            }

            return Task.FromResult(savedUsers);
        }
        #endregion Methods
    }
}