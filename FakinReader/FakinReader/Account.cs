using FakinReader.Models.Enums;
using System.ComponentModel;

namespace FakinReader
{
    public class Account : INotifyPropertyChanged
    {
        public Account(string userName, string accessToken, string refreshToken, string authorizationCodeForSession=null)
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
        public MenuItemType MenuItemType { get; set; }
        #endregion Properties



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}


