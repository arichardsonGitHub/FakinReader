using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakinReader.Services
{
    public interface IAccountManager
    {
        #region Properties
        string ActiveAccessTokenKey { get; }
        Account ActiveAccount { get; set; }
        string ActiveRefreshTokenKey { get; }
        string ActiveUserNameKey { get; }
        string AuthorizationCodeKey { get; }
        List<Account> SavedAccounts { get; }
        ISettingsManager SettingsManager { get; }
        Account LoggedOutAccount { get; set; }
        #endregion Properties

        #region Methods
        Task<string> GetAuthorizationUrl();
        Task<bool> LogOutAllAccounts();
        Task<bool> MakeAccountActive(string username);
        Task<bool> RemoveSavedAccount(string accountUserName);
        Task<bool> SaveAccount(Account account, bool setAsActive = true);
        Task<bool> SecureSave(string authorizationCode, string userName = null);
        #endregion Methods
    }
}