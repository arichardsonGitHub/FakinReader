namespace FakinReader
{
    public class User
    {
        public User(string userName, string accessToken, string refreshToken, string authorizationCodeForSession=null)
        {
            Username = userName;

            AccessToken = accessToken;

            RefreshToken = refreshToken;

            AuthorizationCodeForSession = authorizationCodeForSession;
        }

        #region Properties
        public string AccessToken { get; set; }
        public bool HasAuthorizedThisApp => string.IsNullOrEmpty(RefreshToken) == false;
        public string RefreshToken { get; set; }
        public string Username { get; set; }
        public string AuthorizationCodeForSession { get; set; }
        #endregion Properties
    }
}