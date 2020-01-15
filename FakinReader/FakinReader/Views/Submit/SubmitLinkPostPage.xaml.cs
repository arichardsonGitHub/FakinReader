using FakinReader.Views.User;
using RedditSharp.Things;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace FakinReader.Views.Submit
{
    [DesignTimeVisible(false)]
    public partial class SubmitLinkPostPage : ContentPage
    {
        #region Constructors
        public SubmitLinkPostPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<MySubredditsPage, Subreddit>(this, "subredditHasBeenSelected", async (sender, selectedSubreddit) =>
            {
                TargetSubreddit.Text = selectedSubreddit.Name;
            });
        }
        #endregion Constructors

        #region Methods
        private async void OnSelectSubredditButtonClickedAsync(object sender, EventArgs e)
        {
            var manageSubredditsPage = new ManageSubredditsPage();

            await Navigation.PushAsync(manageSubredditsPage);
        }
        #endregion Methods
    }
}