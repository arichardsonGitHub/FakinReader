using FakinReader.Models;
using FakinReader.Models.Enums;
using FakinReader.Services;
using FakinReader.ViewModels;
using System.ComponentModel;
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

            BindingContext = _menuViewModel = new MenuViewModel();
        }

        public MenuPage(MenuViewModel menuViewModel)
        {
            InitializeComponent();

            _menuViewModel = menuViewModel;

            BindingContext = _menuViewModel = new MenuViewModel();
        }
        #endregion Constructors

        #region Fields
        private MenuViewModel _menuViewModel;
        #endregion Fields

        #region Properties
        public IAccountManager AccountManager => DependencyService.Get<IAccountManager>();
        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        #endregion Properties

        #region Methods

        private async void AccountManagementListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            if (e.SelectedItem is Account)
            {
                var menuItemType = (int)((Account)e.SelectedItem).MenuItemType;

                switch (menuItemType)
                {
                    case (int)MenuItemType.MakeAccountActive:
                        _menuViewModel.MakeAccountActive(((Account)e.SelectedItem).Username);
                        break;
                }
            }
            else if (e.SelectedItem is HomeMenuItem)
            {
                var id = (int)((HomeMenuItem)e.SelectedItem).Id;

                switch (id)
                {
                    case (int)MenuItemType.LogAllAccountsOut:
                        await _menuViewModel.LogOutAllAccounts();
                        break;

                    case (int)MenuItemType.AddAccount:
                        await RootPage.NavigateFromMenu(id);
                        break;
                }
            }

            _menuViewModel.ResetAllMenuItems();
        }

        private async void MainListViewMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var id = (int)((HomeMenuItem)e.SelectedItem).Id;

            await RootPage.NavigateFromMenu(id);

            _menuViewModel.ResetAllMenuItems();

        }
        #endregion Methods
    }
}