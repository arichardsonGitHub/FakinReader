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
        Account LoggedOutAccount { get; set; }
        List<Account> SavedAccounts { get; }
        ISettingsManager SettingsManager { get; }
        #endregion Properties

        #region Methods
        Task<string> GetAuthorizationUrl();
        Task<bool> LogOutAllAccounts();
        Task<bool> MakeAccountActive(string username);
        Task<bool> RemoveSavedAccount(string accountUserName);
        Task<bool> SaveAccount(Account account, bool setAsActive = true);
        #endregion Methods
    }
}