using FakinReader.Services;
using System.ComponentModel;
using Xamarin.Forms;

namespace FakinReader.Views.Submit
{
    [DesignTimeVisible(false)]
    public partial class SubmitTextPostPage : ContentPage
    {
        #region Constructors
        public SubmitTextPostPage()
        {
            InitializeComponent();
        }
        #endregion Constructors

        #region Properties
        public IAuthenticationManager AuthenticationManager => DependencyService.Get<IAuthenticationManager>();
        #endregion Properties

        #region Methods
        private async void OnSubmitButtonClicked(object sender, System.EventArgs e)
        {
            var reddit = AuthenticationManager.Reddit;

            var targetSubredditTask = reddit.GetSubredditAsync(TargetSubreddit.Text.Trim());

            var targetSubreddit = targetSubredditTask.Result;

            var newPost = await targetSubreddit.SubmitPostAsync(LinkTitle.Text.Trim(), LinkUrl.Text.Trim());

            //var asdf = await targetSubreddit.SubmitTextPostAsync(Title.Text.Trim(), Link.Text.Trim());
        }
        #endregion Methods
    }
}