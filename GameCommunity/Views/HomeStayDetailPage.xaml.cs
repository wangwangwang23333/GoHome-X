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
        public HomeStayDetailPage()
        {
            InitializeComponent();
            // BindingContext = new ItemDetailViewModel();
            BindingContext = new HomeStayDetailViewModel();
        }
    }
}