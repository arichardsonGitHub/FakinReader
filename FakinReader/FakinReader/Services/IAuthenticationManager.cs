using RedditSharp;

namespace FakinReader.Services
{
    public interface IAuthenticationManager
    {
        #region Properties
        IAccountManager AccountManager { get; }
        AuthProvider AuthProvider { get; }
        Reddit Reddit { get; }

        string RedirectUrl { get; }
        #endregion Properties

        #region Methods

        Reddit GetRedditObject();

        string GetSetting(string key);

        void RemoveSetting(string key);

        void SaveSetting(string key, string value);
        #endregion Methods
    }
}