using RedditSharp.Things;
using FakinReader.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using FakinReader.Services;

namespace FakinReader.Views.User
{
    [DesignTimeVisible(false)]
    public partial class MySubredditsPage : ContentPage
    {
        public IAuthenticationManager AuthenticationManager => DependencyService.Get<IAuthenticationManager>();

        #region Constructors

        public MySubredditsPage()
        {
            InitializeComponent();
        }
        #endregion Constructors

        #region Fields
        private bool _isRefreshing;

        private ObservableCollection<Subreddit> _mySubreddits;
        #endregion Fields

        #region Properties

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ObservableCollection<Subreddit> ListOfMySubreddits
        {
            get { return _mySubreddits; }
            set { _mySubreddits = value; OnPropertyChanged(nameof(ListOfMySubreddits)); }
        }

        #endregion Properties

        #region Methods

        protected override async void OnAppearing()
        {
            await RefreshList();
        }

        private void ListingListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Subreddit selectedItem)
            {
                MessagingCenter.Send(this, "subredditHasBeenSelected", selectedItem);
            }

            Navigation.PopAsync(true);
        }

        private async Task RefreshList()
        {
            try
            {
                IsRefreshing = true;

                var mySubreddits = AuthenticationManager.Reddit.User.GetSubscribedSubreddits();

                ListOfMySubreddits = new ObservableCollection<Subreddit>();

                await mySubreddits.ForEachAsync(subreddit => { ListOfMySubreddits.Add(subreddit); });

                MySubreddits.ItemsSource = ListOfMySubreddits.OrderBy(x => x.DisplayName);

                IsRefreshing = false;
            }
            catch (Exception exception)
            {
            }
        }
        #endregion Methods
    }
}