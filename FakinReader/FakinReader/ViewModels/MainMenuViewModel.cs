using FakinReader.Models;
using FakinReader.Models.Enums;
using FakinReader.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FakinReader.ViewModels
{
    public class MainMenuViewModel : BaseViewModel, IMenuControl<HomeMenuItem>
    {
        #region Constructors
        public MainMenuViewModel()
        {
            Title = "";

            ResetMenuItems();
        }
        #endregion Constructors

        #region Fields
        public string _expanderButtonText = ">";
        public int _expandingHeight = 0;
        private List<HomeMenuItem> _menuItems;
        #endregion Fields

        #region Properties
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

        public List<HomeMenuItem> MenuItems
        {
            get { return _menuItems; }
            private set { SetProperty(ref _menuItems, value, "MenuItems"); }
        }
        #endregion Properties

        #region Methods
        public async Task<List<HomeMenuItem>> GetMenuItems()
        {
            var menuItems = new List<HomeMenuItem>();

            await Task.Run(() =>
            {
                if (AccountManager.ActiveAccount == AccountManager.LoggedOutAccount)
                {
                    menuItems = new List<HomeMenuItem>
                    {
                        new HomeMenuItem {MenuItemType = MenuItemType.FindUser, Title="User", IconSource="img_87237.png"},
                        new HomeMenuItem {MenuItemType = MenuItemType.Subreddit, Title="Subreddit", IconSource="img_75613.png"},
                        new HomeMenuItem {MenuItemType = MenuItemType.Settings, Title="Settings", IconSource="img_242.png"},
                        new HomeMenuItem {MenuItemType = MenuItemType.HelpAndSupport, Title="Help And Support", IconSource="img_1313.png"},
                        new HomeMenuItem {MenuItemType = MenuItemType.Testing, Title="test", IconSource="img_513433.png" }
                    };
                }
                else
                {
                    menuItems = new List<HomeMenuItem>
                    {
                        new HomeMenuItem {MenuItemType = MenuItemType.Home, Title="Home" },
                        new HomeMenuItem {MenuItemType = MenuItemType.Profile, Title="Profile", IconSource = "img_87237.png"},
                        new HomeMenuItem {MenuItemType = MenuItemType.Inbox, Title="Inbox", IconSource="img_452847"},
                        new HomeMenuItem {MenuItemType = MenuItemType.ManageSubreddits, Title="Manage Subreddits", IconSource="img_51.png"},
                        new HomeMenuItem {MenuItemType = MenuItemType.SubmitPost, Title="Submit Post", IconSource="img_1825.png"},
                        new HomeMenuItem {MenuItemType = MenuItemType.Subreddit, Title="Subreddit", IconSource="img_75613.png"},
                        new HomeMenuItem {MenuItemType = MenuItemType.Settings, Title="Settings", IconSource="img_242.png"},
                        new HomeMenuItem {MenuItemType = MenuItemType.HelpAndSupport, Title="Help And Support", IconSource="img_1313.png"},
                        new HomeMenuItem {MenuItemType = MenuItemType.Testing, Title="test", IconSource="img_513433.png" }
                    };
                }
            });

            return menuItems;
        }

        public async void ResetMenuItems()
        {
            var getMenuItemsTask = GetMenuItems();

            MenuItems = await getMenuItemsTask;
        }
        #endregion Methods
    }
}