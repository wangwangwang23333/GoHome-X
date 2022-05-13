using GameCommunity.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace GameCommunity.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}