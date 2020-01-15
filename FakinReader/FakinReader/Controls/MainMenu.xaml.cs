using FakinReader.Models;
using FakinReader.Services;
using FakinReader.ViewModels;
using FakinReader.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FakinReader.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : StackLayout
    {
        #region Constructors
        public MainMenu()
        {
            InitializeComponent();

            BindingContext = _mainMenuViewModel = new MainMenuViewModel();
        }
        public MainMenu(MainMenuViewModel mainMenuViewModel)
        {
            _mainMenuViewModel = mainMenuViewModel;

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
        private async void MainMenuListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var menuItemType = (int)((HomeMenuItem)e.SelectedItem).MenuItemType;

            await RootPage.NavigateFromMenu(menuItemType);
        }
        #endregion Methods
    }
}