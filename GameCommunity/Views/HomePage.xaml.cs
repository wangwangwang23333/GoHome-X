using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCommunity.Models;
using GameCommunity.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameCommunity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        // 绑定viewmodel
        public HomePageStaysViewModel _viewModel;

        public HomePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new HomePageStaysViewModel();

            
        }

        async protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();

            // 网络检测
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await DisplayAlert("提示", "网络连接异常，请检查连接", "确定");
                return;
            }
        }
    }
}