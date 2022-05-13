using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace GameCommunity.Views
{
    public partial class AboutPage : ContentPage
    {
        int clickTime = 0;
        public AboutPage()
        {
            
            InitializeComponent();
        }

        public async void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.DisplayAlert("Network Error", "请检查你的网络", "YES");
                return;
            }


            //if (await this.DisplayAlert(
            //    "Dial a Number",
            //    "Would you like to add " + this.clickTime + "?",
            //    "Yes",
            //    "No"))
            //{
            //    this.clickTime++;
            //    clickLabel.Text = $"You have clicked {clickTime} times";
            //}
        }

        
    }
}