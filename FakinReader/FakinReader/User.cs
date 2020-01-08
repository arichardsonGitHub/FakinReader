namespace FakinReader
{
    public class User
    {
        public User(string userName, string accessToken, string refreshToken)
        {
            Username = userName;

            AccessToken = accessToken;

            RefreshToken = refreshToken;
        }

        #region Properties
        public string AccessToken { get; set; }
        public bool HasAuthorizedThisApp => string.IsNullOrEmpty(RefreshToken) == false;
        public string RefreshToken { get; set; }
        public string Username { get; set; }
        #endregion Properties
    }
}