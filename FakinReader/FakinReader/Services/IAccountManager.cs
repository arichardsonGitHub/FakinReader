using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakinReader.Services
{
    public interface IAccountManager
    {
        #region Properties
        string AccessTokenKey { get; }
        User ApplicationUser { get; set; }
        List<string> PreviousSessionUsers { get; }
        string RefreshTokenKey { get; }
        ISettingsManager SettingsManager { get; }
        #endregion Properties

        #region Methods

        string GetAuthorizationUrl();

        Task<User> GetLoggedInUser();

        Task LoadLoggedInUser();

        Task<bool> LogCurrentUserOut();

        Task<bool> LogUserIn(string userName);

        Task<bool> LogUserIn(User user);

        Task<bool> SecureSave(string accessToken, string refreshToken, string authorizationCode, string userName = null);

        void SendToActivate();
        #endregion Methods
    }
}