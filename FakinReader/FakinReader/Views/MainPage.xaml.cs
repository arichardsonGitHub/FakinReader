using FakinReader.Models.Enums;
using FakinReader.Views.Submit;
using FakinReader.Views.User;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FakinReader.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        #region Constructors
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Home, (NavigationPage)Detail);
        }
        #endregion Constructors

        #region Fields
        private Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        #endregion Fields

        #region Methods
        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.AddAccount:
                    case (int)MenuItemType.MakeAccountActive:
                        MenuPages.Add(id, new NavigationPage(new AuthorizationPage()));
                        break;

                    case (int)MenuItemType.FindUser:
                        MenuPages.Add(id, new NavigationPage(new FindUserPage()));
                        break;

                    case (int)MenuItemType.HelpAndSupport:
                        MenuPages.Add(id, new NavigationPage(new HelpAndSupportPage()));
                        break;

                    case (int)MenuItemType.Inbox:
                        MenuPages.Add(id, new NavigationPage(new InboxPage()));
                        break;

                    case (int)MenuItemType.LogAllAccountsOut:
                        MenuPages.Add(id, new NavigationPage(new TestingPage()));
                        break;

                    case (int)MenuItemType.ManageSubreddits:
                        MenuPages.Add(id, new NavigationPage(new ManageSubredditsPage()));
                        break;

                    case (int)MenuItemType.Profile:
                        MenuPages.Add(id, new NavigationPage(new RedditProfilePage()));
                        break;

                    case (int)MenuItemType.Settings:
                        MenuPages.Add(id, new NavigationPage(new SettingsPage()));
                        break;

                    case (int)MenuItemType.SubmitPost:
                        MenuPages.Add(id, new NavigationPage(new SubmitPostPage()));
                        break;

                    case (int)MenuItemType.Subreddit:
                        MenuPages.Add(id, new NavigationPage(new NavigateToSubredditPage()));
                        break;

                    case (int)MenuItemType.Testing:
                        MenuPages.Add(id, new NavigationPage(new TestingPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                {
                    await Task.Delay(100);
                }

                IsPresented = false;
            }
        }
        #endregion Methods
    }
}