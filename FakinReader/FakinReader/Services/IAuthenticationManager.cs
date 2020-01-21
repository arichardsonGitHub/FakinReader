using RedditSharp;
using Reddit;

namespace FakinReader.Services
{
    public interface IAuthenticationManager
    {
        #region Properties
        IAccountManager AccountManager { get; }
        AuthProvider AuthProvider { get; }
        RedditSharp.Reddit Reddit { get; }
        Reddit.RedditClient RedditClient { get; }

        string RedirectUrl { get; }
        #endregion Properties
    }
}