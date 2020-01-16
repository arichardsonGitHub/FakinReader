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

            RefreshPosts();
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

            await Task.Run(() =>
             {
                 var reddit = AuthenticationManager.Reddit;

                 newItems = reddit.FrontPage.GetPosts(max: 100);
             });

            return newItems;
        }

        public async void RefreshPosts()
        {
            var newPosts = new ObservableCollection<Post>();

            var refreshFrontPageItems = await GetFrontPageItems();

            await refreshFrontPageItems.ForEachAsync(post => { newPosts.Add(post); });

            FrontPageItems = newPosts;
        }
        #endregion Methods
    }
}