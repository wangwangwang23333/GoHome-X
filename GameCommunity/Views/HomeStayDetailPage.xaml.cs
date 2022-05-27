using GameCommunity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameCommunity.Views
{

    public partial class HomeStayDetailPage : ContentPage
    {
        public HomeStayDetailViewModel _viewModel;
        public HomeStayDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new HomeStayDetailViewModel();

        }
    }
}