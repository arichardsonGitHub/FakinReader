using FakinReader.Models;
using FakinReader.Models.Enums;
using FakinReader.Services;
using FakinReader.ViewModels;
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
        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        #endregion Properties

        #region Methods
        public IAccountManager AccountManager => DependencyService.Get<IAccountManager>();

        private async void AccountManagementListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var id = (int)((HomeMenuItem)e.SelectedItem).Id;

            switch (id)
            {
                case (int)MenuItemType.LogAllAccountsOut:
                    await _menuViewModel.LogOutAllAccounts();
                    break;

                case (int)MenuItemType.MakeAccountActive:
                    _menuViewModel.MakeAccountActive(((HomeMenuItem)e.SelectedItem).Title);
                    break;

                case (int)MenuItemType.AddAccount:
                    await RootPage.NavigateFromMenu(id);
                    break;
            }
        }

        private async void MainListViewMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var id = (int)((HomeMenuItem)e.SelectedItem).Id;

            await RootPage.NavigateFromMenu(id);
        }
        #endregion Methods
    }
}