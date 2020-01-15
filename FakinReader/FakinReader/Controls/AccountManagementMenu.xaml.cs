using FakinReader.Models;
using FakinReader.Models.Enums;
using FakinReader.Services;
using FakinReader.ViewModels;
using FakinReader.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FakinReader.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountManagementMenu : StackLayout
    {
        #region Constructors
        public AccountManagementMenu()
        {
            InitializeComponent();

            BindingContext = _accountManagementMenuViewModel = new AccountManagementMenuViewModel();
        }
        public AccountManagementMenu(AccountManagementMenuViewModel accountManagementMenuViewModel)
        {
            _accountManagementMenuViewModel = accountManagementMenuViewModel;

            BindingContext = _accountManagementMenuViewModel = new AccountManagementMenuViewModel();
        }
        #endregion Constructors

        #region Fields
        private readonly AccountManagementMenuViewModel _accountManagementMenuViewModel;
        #endregion Fields

        #region Properties
        public IAccountManager AccountManager => DependencyService.Get<IAccountManager>();

        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        #endregion Properties

        #region Methods
        private async void AccountManagementMainListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var menuItemType = (int)((AccountManagementMenuItem)e.SelectedItem).MenuItemType;

            switch (menuItemType)
            {
                case (int)MenuItemType.AddAccount:
                    await RootPage.NavigateFromMenu(menuItemType);
                    break;

                case (int)MenuItemType.LogAllAccountsOut:
                    await _accountManagementMenuViewModel.LogOutAllAccounts();
                    break;

                case (int)MenuItemType.MakeAccountActive:
                    await _accountManagementMenuViewModel.MakeAccountActive(((Account)e.SelectedItem).Username);
                    break;
            }

            AccountManagementMainListView.SelectedItem = null;
        }

        private async void AccountManagementSavedAccountsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var menuItemType = (int)((Account)e.SelectedItem).MenuItemType;

            switch (menuItemType)
            {
                case (int)MenuItemType.MakeAccountActive:
                    await RootPage.NavigateFromMenu(menuItemType);
                    break;
            }

            AccountManagementSavedAccountsListView.SelectedItem = null;
        }
        #endregion Methods
    }
}