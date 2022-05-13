using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCommunity.Models;
using GameCommunity.ViewModels;

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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}