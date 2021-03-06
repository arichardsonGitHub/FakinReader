﻿using FakinReader.Models;
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

            BindingContext = _mainMenuViewModel = new MainMenuViewModel();
        }

        public MenuPage(MainMenuViewModel menuViewModel)
        {
            InitializeComponent();

            _mainMenuViewModel = menuViewModel;

            BindingContext = _mainMenuViewModel = new MainMenuViewModel();
        }
        #endregion Constructors

        #region Fields
        private readonly MainMenuViewModel _mainMenuViewModel;
        #endregion Fields

        #region Properties
        public IAccountManager AccountManager => DependencyService.Get<IAccountManager>();
        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        #endregion Properties

        #region Methods
        private async void MainListViewMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var id = (int)((HomeMenuItem)e.SelectedItem).MenuItemType;

            await RootPage.NavigateFromMenu(id);

            _mainMenuViewModel.ResetMenuItems();
        }
        #endregion Methods
    }
}