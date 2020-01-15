using FakinReader.Models;
using FakinReader.Models.Enums;
using FakinReader.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FakinReader.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        #region Constructors

        public MenuViewModel()
        {
            Title = "";

            ResetAllMenuItems();
        }
        #endregion Constructors

        #region Fields
        public string _expanderButtonText = ">";
        public int _expandingHeight = 0;

        private List<HomeMenuItem> _accountManagementMenuItems;
        private List<HomeMenuItem> _mainMenuItems;
        #endregion Fields

        #region Properties

        public List<HomeMenuItem> AccountManagementMenuItems
        {
            get { return _accountManagementMenuItems; }
            set { SetProperty(ref _accountManagementMenuItems, value, "AccountManagementMenuItems"); }
        }

        public IAccountManager AccountManager => DependencyService.Get<IAccountManager>();

        public int ExpandedHeight
        {
            get { return _expandingHeight; }
            private set { SetProperty(ref _expandingHeight, value, "ExpandedHeight"); }
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

        public List<HomeMenuItem> MainMenuItems
        {
            get { return _mainMenuItems; }
            private set { SetProperty(ref _mainMenuItems, value, "MainMenuItems"); }
        }

        #endregion Properties

        #region Methods
        public async Task<List<HomeMenuItem>> GetAccountManagementMenuItems()
        {
            var menuItems = new List<HomeMenuItem>();

            await Task.Run(() =>
            {
                if (AccountManager.ActiveAccount != AccountManager.LoggedOutAccount)
                {
                    menuItems.Add(new HomeMenuItem { Id = MenuItemType.LogAllAccountsOut, Title = "Log out", IconSource = "img_87237.png" });
                }

                menuItems.Add(new HomeMenuItem { Id = MenuItemType.AddAccount, Title = "Add account" });
            });

            return menuItems;
        }

        public async Task<bool> LogOutAllAccounts()
        {
            return await AccountManager.LogOutAllAccounts();
        }

        public void MakeAccountActive(string userName)
        {
            AccountManager.MakeAccountActive(userName);
        }

        public async void ResetAllMenuItems()
        {
            var setAccountManagementMenuItems = GetAccountManagementMenuItems();
            var setMainMenuItems = SetMainMenuItems();

            AccountManagementMenuItems = await setAccountManagementMenuItems;
            MainMenuItems = await setMainMenuItems;
        }

        public async Task<List<HomeMenuItem>> SetMainMenuItems()
        {
            var menuItems = new List<HomeMenuItem>();

            await Task.Run(() =>
            {
                if (AccountManager.ActiveAccount == AccountManager.LoggedOutAccount)
                {
                    menuItems = new List<HomeMenuItem>
                    {
                        new HomeMenuItem {Id = MenuItemType.FindUser, Title="User", IconSource="img_87237.png"},
                        new HomeMenuItem {Id = MenuItemType.Subreddit, Title="Subreddit", IconSource="img_75613.png"},
                        new HomeMenuItem {Id = MenuItemType.Settings, Title="Settings", IconSource="img_242.png"},
                        new HomeMenuItem {Id = MenuItemType.HelpAndSupport, Title="Help And Support", IconSource="img_1313.png"},
                        new HomeMenuItem {Id = MenuItemType.Testing, Title="test", IconSource="img_513433.png" }
                    };
                }
                else
                {
                    menuItems = new List<HomeMenuItem>
                    {
                        new HomeMenuItem {Id = MenuItemType.Home, Title="Home" },
                        new HomeMenuItem {Id = MenuItemType.Profile, Title="Profile", IconSource = "img_87237.png"},
                        new HomeMenuItem {Id = MenuItemType.Inbox, Title="Inbox", IconSource="img_452847"},
                        new HomeMenuItem {Id = MenuItemType.ManageSubreddits, Title="Manage Subreddits", IconSource="img_51.png"},
                        new HomeMenuItem {Id = MenuItemType.SubmitPost, Title="Submit Post", IconSource="img_1825.png"},
                        new HomeMenuItem {Id = MenuItemType.Subreddit, Title="Subreddit", IconSource="img_75613.png"},
                        new HomeMenuItem {Id = MenuItemType.Settings, Title="Settings", IconSource="img_242.png"},
                        new HomeMenuItem {Id = MenuItemType.HelpAndSupport, Title="Help And Support", IconSource="img_1313.png"},
                        new HomeMenuItem {Id = MenuItemType.Testing, Title="test", IconSource="img_513433.png" }
                    };
                }

                MainMenuItems = menuItems;
            });

            return menuItems;
        }
        #endregion Methods
    }
}