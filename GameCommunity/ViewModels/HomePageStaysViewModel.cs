using GameCommunity.Models;
using GameCommunity.Services;
using GameCommunity.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GameCommunity.ViewModels
{
    public class HomePageStaysViewModel: BaseViewModel
    {
        private HomePageStay _selectedHomePageStay;

        // 评分最高房源
        public ObservableCollection<HomePageStay> HomePageStays { get; }

        // 最热销的房源
        public ObservableCollection<HomePageStay> HottestHomePageStays { get; }

        // 特惠房源
        public ObservableCollection<HomePageStay> CheapestHomePageStays { get; }

        // 房源列表
        public ObservableCollection<HomePageStayGroup> HomePageStayGroups { get; private set; }

        // 首页图片
        public List<SingleImage> HeaderImages { get; private set; }

        public Command LoadHomePageStaysCommand { get; }
        public Command<HomePageStay> HomePageStayTapped { get; }

        public HomePageStayDataStore _homePageStayDataStore;

        public HomePageStaysViewModel()
        {
            

            Title = "首页";
            HomePageStays = new ObservableCollection<HomePageStay>();
            HottestHomePageStays = new ObservableCollection<HomePageStay>();
            CheapestHomePageStays = new ObservableCollection<HomePageStay>();

            HeaderImages = new List<SingleImage>
            {
                new SingleImage("https://joes-bucket.oss-cn-shanghai.aliyuncs.com/img/a.gif",
                "探索你想要的归宿"),
                new SingleImage("https://joes-bucket.oss-cn-shanghai.aliyuncs.com/img/mixkit-woman-lying-on-a-couch-looking-at-her-mobile-phone-65-original.png",
                "像在家中一样悠闲"),
                new SingleImage("https://joes-bucket.oss-cn-shanghai.aliyuncs.com/img/mixkit-person-cooking-on-a-stovetop-in-the-kitchen-11-original.png",
                "在民俗中体验烹饪乐趣"),
                new SingleImage("https://joes-bucket.oss-cn-shanghai.aliyuncs.com/img/mixkit-man-sitting-in-front-of-a-fire-reading-from-a-66-original.png",
                "享受特别的舒适感"),
                new SingleImage("https://joes-bucket.oss-cn-shanghai.aliyuncs.com/img/mixkit-person-reading-a-book-while-wrapped-in-warm-blankets-with-75-original.png",
                "体验归宿的温暖"),
            };

            HomePageStayGroups = new ObservableCollection<HomePageStayGroup>();

            LoadHomePageStaysCommand = new Command(async () => await ExecuteLoadHomePageStaysCommand());
            _homePageStayDataStore = new HomePageStayDataStore();

            HomePageStayTapped = new Command<HomePageStay>(OnHomePageStaySelected);

            

        }

        async Task ExecuteLoadHomePageStaysCommand()
        {
            IsBusy = true;

            try
            {

                // groups
                HomePageStayGroups.Clear();
                var homePageStaysGroup = await HomePageStayStore.GetHomePageStayGroupsAsync(true);
                foreach (var item in homePageStaysGroup)
                {
                    HomePageStayGroups.Add(item);
                }

                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[error]" + ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelecetedHomePageStay = null;
        }

        public HomePageStay SelecetedHomePageStay
        {
            get => _selectedHomePageStay;
            set
            {
                SetProperty(ref _selectedHomePageStay, value);
                OnHomePageStaySelected(value);
            }
        }

        async void OnHomePageStaySelected(HomePageStay homePageStay)
        {
/*            Console.WriteLine("start to loading......................");
            Console.WriteLine("start to loading......................");
            Console.WriteLine("start to loading......................");
            Console.WriteLine("start to loading......................");*/
            if (homePageStay == null)
            {
                return;
            }

            // 跳转到民宿的详细页面
            await Shell.Current.GoToAsync(nameof(HomeStayDetailPage));
            //await Shell.Current.GoToAsync($"{nameof(HomeStayDetailPage)}");
        }


    }
}
