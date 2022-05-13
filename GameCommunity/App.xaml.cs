using GameCommunity.Services;
using GameCommunity.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameCommunity
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<HomePageStayDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
