using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using FakinReader.Helpers;

namespace FakinReader.Droid
{
    [Activity(Label = "FakinReader", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        #region Methods

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;

            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, savedInstanceState);



            var lastActiveUser = await AuthenticationHelper.GetLastActiveUser();

            if(lastActiveUser != null)
            {
                AuthenticationHelper.LogUserIn(lastActiveUser.Username);
            }



            LoadApplication(new App());
        }
        #endregion Methods
    }
}