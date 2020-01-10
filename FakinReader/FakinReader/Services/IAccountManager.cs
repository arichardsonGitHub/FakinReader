using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakinReader.Services
{
    public interface IAccountManager
    {
        #region Properties
        string ActiveAccessTokenKey { get; }
        string ActiveRefreshTokenKey { get; }
        User ActiveUser { get; }
        string ActiveUserNameKey { get; }
        string AuthorizationCodeKey { get; }
        List<User> SavedUsers { get; }
        ISettingsManager SettingsManager { get; }
        #endregion Properties

        #region Methods

        string GetAuthorizationUrl();

        Task<bool> LogOut();

        void MakeUserActive(string username);

        void SaveUser(User user);

        Task<bool> SecureSave(string authorizationCode, string userName = null);

        void SendToActivate();
        #endregion Methods
    }
}