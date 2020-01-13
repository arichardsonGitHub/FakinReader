using FakinReader.Models;
using FakinReader.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace FakinReader.Views
{
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        #region Constructors

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);

            BindingContext = viewModel;
        }
        #endregion Constructors

        #region Fields
        private ItemDetailViewModel viewModel;
        #endregion Fields
    }
}