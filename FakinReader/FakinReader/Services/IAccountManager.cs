using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakinReader.Services
{
    public interface IAccountManager
    {
        #region Properties
        string ActiveAccessTokenKey { get; }
        string ActiveRefreshTokenKey { get; }
        Account ActiveAccount { get; set; }
        string ActiveUserNameKey { get; }
        string AuthorizationCodeKey { get; }
        List<Account> SavedAccounts { get; }
        ISettingsManager SettingsManager { get; }
        #endregion Properties

        #region Methods
        string GetAuthorizationUrl();

        Task<bool> LogOutAllAccounts();

        void MakeAccountActive(string username);

        void SaveAccount(Account account);

        Task<bool> SecureSave(string authorizationCode, string userName = null);

        void SendToActivate();
        #endregion Methods
    }
}