using FakinReader.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace FakinReader.Views
{
    [DesignTimeVisible(false)]
    public partial class FrontPage : ContentPage
    {
        #region Constructors
        public FrontPage()
        {
            InitializeComponent();

            BindingContext = _frontPageViewModel = new FrontPageViewModel();
        }

        public FrontPage(FrontPageViewModel frontPageViewModel)
        {
            InitializeComponent();

            _frontPageViewModel = frontPageViewModel;

            BindingContext = _frontPageViewModel = new FrontPageViewModel();
        }
        #endregion Constructors

        #region Fields
        private readonly FrontPageViewModel _frontPageViewModel;
        #endregion Fields
    }
}