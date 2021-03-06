﻿using FakinReader.Services;
using FakinReader.Views;
using Xamarin.Forms;

namespace FakinReader
{
    public partial class App : Application
    {
        #region Constructors
        public App()
        {
            InitializeComponent();

            DependencyService.Register<SettingsManager>();

            DependencyService.Register<AccountManager>();

            DependencyService.Register<AuthenticationManager>();

            MainPage = new MainPage();
        }
        #endregion Constructors

        #region Methods
        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnStart()
        {
        }
        #endregion Methods
    }
}