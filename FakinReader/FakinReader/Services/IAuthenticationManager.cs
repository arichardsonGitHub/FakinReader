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
    }
}




