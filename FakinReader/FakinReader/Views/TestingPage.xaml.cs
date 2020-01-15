using FakinReader.Models.Enums;
using FakinReader.Services;
using FakinReader.ViewModels;
using RedditSharp.Things;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FakinReader.Views
{
    [DesignTimeVisible(false)]
    public partial class TestingPage : ContentPage
    {
        #region Constructors

        public TestingPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new TestingPageViewModel();
        }
        #endregion Constructors

        #region Fields
        private bool _isRefreshing;
        private ObservableCollection<Post> _listOfSomething;
        private TestingPageViewModel viewModel;
        #endregion Fields

        #region Properties
        public IAccountManager AccountManager => DependencyService.Get<IAccountManager>();

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ObservableCollection<Post> ListOfSomething
        {
            get { return _listOfSomething; }
            set { _listOfSomething = value; OnPropertyChanged(nameof(ListOfSomething)); }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    //await DoSomethingElseWithAccessToken();

                    IsRefreshing = false;
                });
            }
        }

        #endregion Properties

        #region Methods

        private void ListingListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Post post)
            {
                Launcher.OpenAsync(post.Url);
            }
        }
            MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        private void OnDoSomethingButtonClickedAsync(object sender, EventArgs e)
        {

            AccountManager.RemoveSavedAccount("i_win_because_i_quit");
            AccountManager.RemoveSavedAccount("i_cant_think_of_a_us");

            //Task.WaitAll(RootPage.NavigateFromMenu((int)MenuItemType.AddAccount));

        }

        private void SetLastActiveUser(string userName)
        {
            //AuthenticationHelper.SaveSetting(AuthenticationHelper.LAST_ACTIVE_USER_KEY, userName);
        }

        private async Task SimulateCheckingForAuthenticationAsync()
        {
            //var lastActiveUser = await AuthenticationHelper.GetLastActiveUser();

            //if (lastActiveUser != null)
            //{
            //    await AuthenticationHelper.LogUserIn(lastActiveUser);
            //}
            //else
            //{
            //    await Navigation.PushAsync(new AuthorizationPage());
            //}
        }
        #endregion Methods
    }
}

//private async Task DoSomethingWithAccessTokenAsync(string refreshToken)
//        {
//            try
//            {
//                //WebAgent wa = new WebAgent
//                //{
//                //    AccessToken = accessToken,
//                //    UserAgent = USER_AGENT
//                //};

//                RefreshTokenWebAgent rwa = new RefreshTokenWebAgent(refreshToken, CLIENT_ID, null, REDIRECT_URL)
//                {
//                    UserAgent = USER_AGENT
//                };

//                Reddit reddit = new Reddit(rwa, true);

//                var subredditTask = reddit.GetSubredditAsync("/r/drums");

//                var posts = new List<Post>();

//                var subredditResult = await subredditTask;

//                var thePosts = subredditResult.GetPosts(Subreddit.Sort.Top, 20).Take(20);

//                thePosts.ForEach(post => { posts.Add(post); });

//                ItemsListView.ItemsSource = posts;

//                ItemCount.Text = $"Posts ({posts.Count.ToString()})";
//            }
//            catch (Exception exception)
//            {
//            }
//        }

//private async Task<TokensFromAuthorization> GetTokensFromAuthorizationCodeAsync(string code)
//{
//    AuthProvider authProvider = new AuthProvider(CLIENT_ID, null, REDIRECT_URL);

//    var getRefreshToken = authProvider.GetOAuthRefreshTokenFromCodeAsync(code);

//    return await getRefreshToken;
//}

//private async Task DoSomething(TokensFromAuthorization tokensFromAuthorization)
//{
//    //var account = AccountStore.Create().FindAccountsForService(Constants.AppName).FirstOrDefault();

//    try
//    {
//        //Application.Current.Properties["someBetterNameThanAccessToken"] = tokensFromAuthorization.AccessToken;
//        //Application.Current.Properties["someBetterNameThanRefreshToken"] = tokensFromAuthorization.RefreshToken;

//        var accessToken = Application.Current.Properties["someBetterNameThanAccessToken"].ToString();
//        var refreshToken = Application.Current.Properties["someBetterNameThanRefreshToken"].ToString();

//        IsRefreshing = true;

//        //var webAgent = new WebAgent(accessToken: tokensFromAuthorization.AccessToken, userAgent: USER_AGENT);
//        var webAgent = new RefreshTokenWebAgent(refreshToken: refreshToken, CLIENT_ID, null, REDIRECT_URL, USER_AGENT, accessToken);

//        var reddit = new Reddit(webAgent, true);

//        var myPosts = reddit.User.GetPosts(max: 30);

//        var myComments = reddit.User.GetComments(max: 100);

//        var mySubreddits = reddit.User.GetSubscribedSubreddits(max: 200);

//        var rSlashAll = reddit.RSlashAll.GetPosts(max: 200);

//        ListOfSomething = new ObservableCollection<Subreddit>();

//        await mySubreddits.ForEachAsync(post => { ListOfSomething.Add(post); });

//        ItemsListView.ItemsSource = ListOfSomething.OrderBy(x => x.DisplayName);

//        ItemCount.Text = $"Items ({ListOfSomething.Count.ToString()})";

//        IsRefreshing = false;
//    }
//    catch (Exception exception)
//    {
//    }
//}

/*
        private async Task DoSomething()
        {
            try
            {
                IsRefreshing = true;

                var rwa = new RefreshTokenWebAgent(AuthenticationHelper.RefreshToken, AuthenticationHelper.CLIENT_ID, null, AuthenticationHelper.REDIRECT_URL)
                {
                    UserAgent = AuthenticationHelper.USER_AGENT
                };

                var reddit = new Reddit(rwa, true);

                //var myPosts = reddit.User.GetPosts(max: 30);

                //var myComments = reddit.User.GetComments(max: 100);

                //var rSlashAll = reddit.RSlashAll.GetPosts(max: 200);

                var mySubreddits = reddit.User.GetSubscribedSubreddits(max: 200);

                ListOfSomething = new ObservableCollection<Subreddit>();

                await mySubreddits.ForEachAsync(post => { ListOfSomething.Add(post); });

                ItemsListView.ItemsSource = ListOfSomething.OrderBy(x => x.DisplayName);

                ItemCount.Text = $"Items ({ListOfSomething.Count.ToString()})";

                IsRefreshing = false;
            }
            catch (Exception exception)
            {
            }
        }
        */
//private async Task<Post> AddAPost()
//{
//    var reddit = AuthenticationManager.GetRedditObject();

//    var drumsSubredditTask = reddit.GetSubredditAsync("drums");

//    var drumsSubreddit = drumsSubredditTask.Result;

//    var newPost = await drumsSubreddit.SubmitPostAsync("Just testing", "https://forums.xamarin.com/discussion/169887/how-to-fix-xamarin-forms-build-error-failed-to-create-javatypeinfo-for-class");

//    ListOfSomething = new ObservableCollection<Post>
//    {
//        newPost
//    };

//    ItemsListView.ItemsSource = ListOfSomething;

//    ItemCount.Text = $"Items ({ListOfSomething.Count.ToString()})";

//    return newPost;
//}
