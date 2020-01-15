using System.ComponentModel;
using Xamarin.Forms;

namespace FakinReader.Views.User
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class FindUserPage : ContentPage
    {
        #region Constructors
        public FindUserPage()
        {
            InitializeComponent();
        }
        #endregion Constructors

        #region Methods
        private void CancelButton_Clicked(object sender, System.EventArgs e)
        {
        }

        private void OkButton_Clicked(object sender, System.EventArgs e)
        {
        }
        #endregion Methods
    }
}