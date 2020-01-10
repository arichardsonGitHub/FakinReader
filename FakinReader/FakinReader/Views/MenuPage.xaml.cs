using FakinReader.Models;
using FakinReader.Models.Enums;
using FakinReader.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FakinReader.Views
{
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        #region Constructors

        public MenuPage()
        {
            InitializeComponent();

            SetMenuItems();

            BindingContext = this;
        }
        #endregion Constructors

        #region Fields
        protected string _ExpanderButtonText = "+";
        private List<HomeMenuItem> _accountManagementItems;
        private int _ExpandingHeight = 0;
        private List<HomeMenuItem> _menuItems;
        #endregion Fields

        #region Properties
        public IAccountManager AccountManager => DependencyService.Get<IAccountManager>();

        public int ExpandedHeight
        {
            get
            {
                return _ExpandingHeight;
            }
            private set
            {
                _ExpandingHeight = value;

                OnPropertyChanged();
            }
        }

        public string ExpanderButtonText
        {
            get
            {
                return _ExpanderButtonText;
            }
            private set
            {
                _ExpanderButtonText = value;

                OnPropertyChanged();
            }
        }

        public ICommand ExpandHideData
        {
            get
            {
                return new Command(() =>
                {
                    if (ExpanderButtonText == "+")
                    {
                        ExpandedHeight = 200;

                        ExpanderButtonText = "-";
                    }
                    else
                    {
                        ExpandedHeight = 0;

                        ExpanderButtonText = "+";
                    }
                });
            }
        }

        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        #endregion Properties

        #region Methods

        private void SetAccountManagementMenuItems()
        {
            _accountManagementItems = new List<HomeMenuItem>();

            if (AccountManager.ActiveUser == null)
            {
            }
            else
            {
                _accountManagementItems.Add(new HomeMenuItem { Id = MenuItemType.LogOut, Title = "Log out", IconSource = "img_87237.png" });
            }

            AccountManager.SavedUsers.ForEach(user =>
            {
                _accountManagementItems.Add(new HomeMenuItem { Id = MenuItemType.LogIn, Title = user.Username });
            });

            AccountManagermentListView.ItemsSource = _accountManagementItems;


            AccountManagermentListView.ItemSelected += async (sender, e) =>
            {

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;

                switch (id)
                {
                    case (int)MenuItemType.LogOut:
                        await AccountManager.LogOut();
                        break;

                    case (int)MenuItemType.LogIn:
                        AccountManager.MakeUserActive(((HomeMenuItem)e.SelectedItem).Title);
                        break;
                }


            };
        }

        private void SetMenuItems()
        {
            if (AccountManager.ActiveUser == null)
            {
                _menuItems = new List<HomeMenuItem>
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
                _menuItems = new List<HomeMenuItem>
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

            ListViewMenu.ItemsSource = _menuItems;

            ListViewMenu.SelectedItem = _menuItems[0];

            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;

                await RootPage.NavigateFromMenu(id);
            };

            SetAccountManagementMenuItems();
        }
        #endregion Methods
    }
}