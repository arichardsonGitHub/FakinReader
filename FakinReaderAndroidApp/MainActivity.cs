using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using System;
using Android.Support.V7.Widget;

namespace FakinReaderAndroidApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        #region Methods
        public override void OnBackPressed()
        {
            var drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            if (drawerLayout.IsDrawerOpen(GravityCompat.Start))
            {
                drawerLayout.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);

            return true;
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.nav_camera:
                    break;

                case Resource.Id.nav_gallery:
                    break;

                case Resource.Id.nav_slideshow:
                    break;

                case Resource.Id.nav_manage:
                    break;

                case Resource.Id.nav_share:
                    break;

                case Resource.Id.nav_send:
                    break;

                case Resource.Id.nav_test:
                    StartActivity(typeof(TestingActivity));
                    break;
            }

            var drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            drawerLayout.CloseDrawer(GravityCompat.Start);

            return true;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var itemId = item.ItemId;

            if (itemId == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            SetSupportActionBar(toolbar);

            var floatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.floatingActionButton);

            floatingActionButton.Click += FloatingActionButtonOnClick;

            var drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            var actionBarDrawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);

            drawerLayout.AddDrawerListener(actionBarDrawerToggle);

            actionBarDrawerToggle.SyncState();

            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            navigationView.SetNavigationItemSelectedListener(this);
        }
        private void FloatingActionButtonOnClick(object sender, EventArgs eventArgs)
        {
            var view = (View)sender;

            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null)
                .Show();
        }
        #endregion Methods
    }
}