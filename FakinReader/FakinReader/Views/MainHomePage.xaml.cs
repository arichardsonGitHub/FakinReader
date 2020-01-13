using FakinReader.Models;
using FakinReader.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace FakinReader.Views
{
    [DesignTimeVisible(false)]
    public partial class MainHomePage : ContentPage
    {
        #region Constructors

        public MainHomePage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }
        #endregion Constructors

        #region Fields
        private ItemsViewModel viewModel;
        #endregion Fields

        #region Methods

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
            {
                viewModel.LoadItemsCommand.Execute(null);
            }
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Item item))
            {
                return; 
            }

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
        #endregion Methods
    }
}