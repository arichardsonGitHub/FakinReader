using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;

namespace FakinReaderAndroidApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class TestingActivity : AppCompatActivity
    {
        #region Methods
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_testing);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar2);

            SetSupportActionBar(toolbar);

            var drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout2);

            var actionBarDrawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);

            drawerLayout.AddDrawerListener(actionBarDrawerToggle);

            actionBarDrawerToggle.SyncState();

            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            //navigationView.SetNavigationItemSelectedListener(this);
        }
        #endregion Methods
    }
}