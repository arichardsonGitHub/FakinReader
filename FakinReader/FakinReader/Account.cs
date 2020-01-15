using FakinReader.Models.Enums;
using System.ComponentModel;

namespace FakinReader
{
    public class Account : INotifyPropertyChanged
    {
        #region Constructors
        public Account(string userName, string accessToken, string refreshToken, string authorizationCodeForSession = null)
        {
            Username = userName;

            AccessToken = accessToken;

            RefreshToken = refreshToken;

            AuthorizationCodeForSession = authorizationCodeForSession;
        }
        #endregion Constructors

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties
        public string AccessToken { get; set; }
        public string AuthorizationCodeForSession { get; set; }
        public bool HasAuthorizedThisApp => string.IsNullOrEmpty(RefreshToken) == false;
        public MenuItemType MenuItemType { get; set; }
        public string RefreshToken { get; set; }
        public string Username { get; set; }
        #endregion Properties

        #region Methods
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion Methods
    }
}