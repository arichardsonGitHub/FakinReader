using FakinReader.Services;
using System.ComponentModel;
using Xamarin.Forms;

namespace FakinReader.Views
{
    [DesignTimeVisible(false)]
    public partial class AccountManagementPage : ContentPage
    {
        #region Constructors

        public AccountManagementPage()
        {
            InitializeComponent();
        }
        #endregion Constructors

        #region Properties
        public IAccountManager AccountManager => DependencyService.Get<IAccountManager>();
        #endregion Properties
    }
}