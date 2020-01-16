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
        #region Constructors
        public AccountManager()
        {
            LoggedOutAccount = new Account("Logged out", null, null);

            SavedAccounts = GetSavedAccounts().Result;

            ActiveAccount = LoggedOutAccount;
        }
        #endregion Constructors

        #region Fields
        private const string ACTIVE_ACCESS_TOKEN_KEY = "FakinReader.ActiveAccessToken";
        private const string ACTIVE_ACCOUNT_USERNAME_KEY = "FakinReader.ActiveAccountUserName";
        private const string ACTIVE_REFRESH_TOKEN_KEY = "FakinReader.ActiveRefreshToken";
        private const string AUTHORIZATION_CODE_KEY = "FakinReader.AuthorizationCode";
        private const string SAVED_ACCOUNTS_KEY = "FakinReader.SavedAccounts";
        private Account _activeAccount;
        private List<Account> _savedAccounts;
        #endregion Fields

        #region Properties
        public string ActiveAccessTokenKey => ACTIVE_ACCESS_TOKEN_KEY;

        public Account ActiveAccount
        {
            get
            {
                var currentActiveUser = SettingsManager.GetSetting(ACTIVE_ACCOUNT_USERNAME_KEY);

                if (string.IsNullOrEmpty(currentActiveUser) == false)
                {
                    _activeAccount = SavedAccounts.Where(x => x.Username.ToUpper() == currentActiveUser.ToUpper()).FirstOrDefault();
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

        public Account LoggedOutAccount { get; set; }

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
        public Task<string> GetAuthorizationUrl()
        {
            var scopes = AuthProvider.Scope.edit | AuthProvider.Scope.flair | AuthProvider.Scope.history | AuthProvider.Scope.identity | AuthProvider.Scope.modconfig | AuthProvider.Scope.modflair | AuthProvider.Scope.modlog | AuthProvider.Scope.modposts | AuthProvider.Scope.modwiki | AuthProvider.Scope.mysubreddits | AuthProvider.Scope.privatemessages | AuthProvider.Scope.read | AuthProvider.Scope.report | AuthProvider.Scope.save | AuthProvider.Scope.submit | AuthProvider.Scope.subscribe | AuthProvider.Scope.vote | AuthProvider.Scope.wikiedit | AuthProvider.Scope.wikiread;

            return Task.FromResult(AuthenticationManager.AuthProvider.GetAuthUrl("step1", scopes, true));
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

                SavedAccounts = savedUsers;

                return Task.FromResult(true);
            }
            catch (Exception exception)
            {
                return Task.FromResult(false);
            }
        }

        public async Task<bool> ActivateAccount(string username)
        {
            Account existingUser;

            await Task.Run(() =>
            {
                existingUser = SavedAccounts.Where(account => account.Username.ToUpper() == username.ToUpper()).Single();

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
            });

            return true;
        }

        public async Task<bool> RemoveSavedAccount(string accountUserName)
        {
            try
            {
                var listOfSavedAccounts = await GetSavedAccounts();

                var existingAccount = listOfSavedAccounts.Where(x => x.Username.ToUpper() == accountUserName.ToUpper()).FirstOrDefault();

                if (existingAccount != null)
                {
                    listOfSavedAccounts.Remove(existingAccount);

                    var toSave = JsonConvert.SerializeObject(listOfSavedAccounts);

                    SettingsManager.SaveSetting(SAVED_ACCOUNTS_KEY, toSave);

                    SavedAccounts = listOfSavedAccounts;

                    ActiveAccount = null;
                }

                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public async Task<bool> SaveAccount(Account accountToSave, bool setAsActive = true)
        {
            var listOfSavedAccounts = await GetSavedAccounts();

            var existingAccount = listOfSavedAccounts.Where(x => x.Username.ToUpper() == accountToSave.Username.ToUpper()).FirstOrDefault();

            if (existingAccount != null)
            {
                listOfSavedAccounts.Remove(existingAccount);
            }

            listOfSavedAccounts.Add(accountToSave);

            var toSave = JsonConvert.SerializeObject(listOfSavedAccounts);

            SettingsManager.SaveSetting(SAVED_ACCOUNTS_KEY, toSave);

            SavedAccounts = listOfSavedAccounts;

            if (setAsActive)
            {
                await ActivateAccount(accountToSave.Username);
            }

            return true;
        }

        public async void SendToActivate()
        {
            var getAuthorizationUrl = await GetAuthorizationUrl();

            WebView webView = new WebView
            {
                Source = getAuthorizationUrl
            };

            webView.IsVisible = true;
        }

        private Task<List<Account>> GetSavedAccounts()
        {
            var savedAccounts = new List<Account>();

            var savedUsersSerialized = Preferences.Get(SAVED_ACCOUNTS_KEY, null);

            if (savedUsersSerialized != null)
            {
                savedAccounts = JsonConvert.DeserializeObject<List<Account>>(savedUsersSerialized);
            }

            savedAccounts.ForEach(x =>
            {
                x.MenuItemType = Models.Enums.MenuItemType.ActivateAccount;
            });

            return Task.FromResult(savedAccounts);
        }
        #endregion Methods
    }
}