using FakinReader.Services;
using RedditSharp;
using RedditSharp.Things;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FakinReader.ViewModels
{
    public class FrontPageViewModel : BaseViewModel
    {
        #region Constructors
        public FrontPageViewModel()
        {
            Title = "";

            Refresh();
        }
        #endregion Constructors

        #region Fields
        private ObservableCollection<Post> _posts;
        #endregion Fields

        #region Properties
        public IAccountManager AccountManager => DependencyService.Get<IAccountManager>();
        public IAuthenticationManager AuthenticationManager => DependencyService.Get<IAuthenticationManager>();
        public ObservableCollection<Post> FrontPageItems
        {
            get { return _posts; }
            private set { SetProperty(ref _posts, value, "FrontPageItems"); }
        }
        #endregion Properties

        #region Methods
        public async Task<Listing<Post>> GetFrontPageItems()
        {
            Listing<Post> newItems = null;

            if (AuthenticationManager.Reddit != null)
            {
                await Task.Run(() =>
                 {
                     var reddit = AuthenticationManager.Reddit;

                     newItems = reddit.FrontPage.GetPosts(max: 100);
                 });
            }

            return newItems;
        }

        public async Task<Subreddit> GetFrontPage()
        {
            if (AuthenticationManager.Reddit != null)
            {
                var reddit = AuthenticationManager.Reddit;

                return reddit.FrontPage;
            }

            return null;
        }

        public async void Refresh()
        {
            var newPosts = new ObservableCollection<Post>();

            var frontPage = await GetFrontPage();

            if (frontPage != null)
            {
                Title = frontPage.DisplayName;

                await Task.Run(() =>
               {
                   var refreshedPosts = frontPage.GetPosts(max: 100);

                   refreshedPosts.ForEachAsync(post => { newPosts.Add(post); });
               });
            }
        }
        #endregion Methods
    }
}