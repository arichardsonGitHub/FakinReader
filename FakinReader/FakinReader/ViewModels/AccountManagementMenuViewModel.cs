using FakinReader.Models;
using FakinReader.Models.Enums;
using FakinReader.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FakinReader.ViewModels
{
    public class AccountManagementMenuViewModel : BaseViewModel, IMenuControl<AccountManagementMenuItem>
    {
        #region Constructors
        public AccountManagementMenuViewModel()
        {
            Title = "";

            ResetMenuItems();
        }
        #endregion Constructors

        #region Fields
        public int _expandedHeight = 0;
        private string _expanderButtonText = ">";
        private List<AccountManagementMenuItem> _menuItems;
        #endregion Fields

        #region Properties
        public IAccountManager AccountManager => DependencyService.Get<IAccountManager>();
        public int ExpandedHeight
        {
            get { return _expandedHeight; }
            private set { SetProperty(ref _expandedHeight, value, "ExpandedHeight"); }
        }
        public string ExpanderButtonText
        {
            get { return _expanderButtonText; }
            private set { SetProperty(ref _expanderButtonText, value, "ExpanderButtonText"); }
        }
        public ICommand ExpandHideData
        {
            get
            {
                return new Command(() =>
                {
                    if (ExpanderButtonText == ">")
                    {
                        ExpandedHeight = 200;

                        ExpanderButtonText = "<";
                    }
                    else
                    {
                        ExpandedHeight = 0;

                        ExpanderButtonText = ">";
                    }
                });
            }
        }
        public List<AccountManagementMenuItem> MenuItems
        {
            get { return _menuItems; }
            private set { SetProperty(ref _menuItems, value, "MenuItems"); }
        }
        #endregion Properties

        #region Methods
        public async Task<List<AccountManagementMenuItem>> GetMenuItems()
        {
            var menuItems = new List<AccountManagementMenuItem>();

            await Task.Run(() =>
            {
                if (AccountManager.ActiveAccount != AccountManager.LoggedOutAccount)
                {
                    menuItems.Add(new AccountManagementMenuItem { MenuItemType = MenuItemType.LogAllAccountsOut, Title = "Log out", IconSource = "img_87237.png" });
                }

                menuItems.Add(new AccountManagementMenuItem { MenuItemType = MenuItemType.AddAccount, Title = "Add account" });
            });

            return menuItems;
        }
        public async Task<bool> LogOutAllAccounts()
        {
            return await AccountManager.LogOutAllAccounts();
        }
        public async Task<bool> ActivateAccount(string userName)
        {
            await AccountManager.ActivateAccount(userName);

            ResetMenuItems();

            return true;
        }
        public async void ResetMenuItems()
        {
            var getMenuItemsTask = GetMenuItems();

            MenuItems = await getMenuItemsTask;
        }
        #endregion Methods
    }
}