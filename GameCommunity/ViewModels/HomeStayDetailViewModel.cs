using GameCommunity.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GameCommunity.ViewModels
{
    [QueryProperty(nameof(PageId), nameof(PageId))]
    public class HomeStayDetailViewModel: BaseViewModel
    {

        public Command LoadHomePageStayCommand { get; }

        public HomeStayDetailViewModel()
        {
            LoadHomePageStayCommand = new Command(async () => await getStayDetailByStayId());
            _homeStayDetail = new HomePageStayDetail();
            Comments = new ObservableCollection<Comment>();
            StayImages = new ObservableCollection<SingleImage>();
            Rooms = new ObservableCollection<Room>();
        }

        // 房源详细信息
        public HomePageStayDetail _homeStayDetail { get; private set; }

        // 评论列表
        public ObservableCollection<Comment> Comments { get; private set; }

        // 房间列表
        public ObservableCollection<Room> Rooms { get; private set; }

        public long StayId { get; set; }
        // 房源图片
        public List<SingleImage> stayImages { get; private set; }


        public ObservableCollection<SingleImage> StayImages { get; set; }

        // 房源名称
        public string stayName;
        public string StayName
        {
            get => stayName;
            set => SetProperty(ref stayName, value);
        }

        // 房源描述
        public string stayDescription;
        public string StayDescription
        {
            get => stayDescription;
            set => SetProperty(ref stayDescription, value);
        }

        // 房东头像
        public string hostAvatar;
        public string HostAvatar
        {
            get => hostAvatar;
            set => SetProperty(ref hostAvatar, value);
        }

        // 房东等级
        public string hostLevel;
        public string HostLevel
        {
            get => hostLevel;
            set => SetProperty(ref hostLevel, value);
        }

        // 房东评价数量
        public int hostCommentNum;
        public int HostCommentNum
        {
            get => hostCommentNum;
            set => SetProperty(ref hostCommentNum, value);
        }

        public string HostCommentNumShown
        {
            get => hostCommentNum + "条评论";
        }

        // 房东名称
        public string hostName;
        public string HostName
        {
            get => hostName;
            set => SetProperty(ref hostName, value);
        }

        // 经纬度坐标
        public double latitude;
        public double Latitude
        {
            get => latitude;
            set => SetProperty(ref latitude, value);
        }

        public double longitude;
        public double Longitude
        {
            get => longitude;
            set => SetProperty(ref longitude, value);
        }

        public int roomNum;
        public int RoomNum
        {
            get => roomNum;
            set => SetProperty(ref roomNum, value);
        }

        public int bedNum;
        public int BedNum
        {
            get => bedNum;
            set => SetProperty(ref bedNum, value);
        }

        public int publicBathroom;
        public int PublicBathroom
        {
            get => publicBathroom;
            set => SetProperty(ref publicBathroom, value);
        }

        public int publicToilet;
        public int PublicToilet
        {
            get => publicToilet;
            set => SetProperty(ref publicToilet, value);
        }

        public int stayCapacity;
        public int StayCapacity
        {
            get => stayCapacity;
            set => SetProperty(ref stayCapacity, value);
        }

        public bool nonBarrierFacility;
        public bool NonBarrierFacility
        {
            get => nonBarrierFacility;
            set => SetProperty(ref nonBarrierFacility, value);
        }

        // 入住开始和结束时间
        public string startTime;
        public string StartTime
        {
            get => startTime;
            set => SetProperty(ref startTime, value);
        }
        public string endTime;
        public string Endtime
        {
            get => endTime;
            set => SetProperty(ref endTime, value);
        }

        public long PageId
        {
            get => StayId;
            set
            {
                StayId = value;
                LoadHomeStayInfo(value);
            }
        }

        public async void LoadHomeStayInfo(long stayId)
        {
            try
            {
                await getStayDetailByStayId();
                // 获取评论信息
                await getCommentList();
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        /// <summary>
        /// 获取评论信息
        /// </summary>
        /// <returns></returns>
        public async Task getCommentList()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string text = await client.GetStringAsync(
                "http://124.223.171.21:8080/api/v1/stay/comment?stayId=" + StayId);

            // 解析json
            JObject stayJson = (JObject)JsonConvert.DeserializeObject(text);
            var commentList = stayJson["comments"].ToList();
            foreach(var comment in commentList)
            {
                Comments.Add(new Comment{
                    Id = comment["id"].ToString(),
                    NickName = comment["nickName"].ToString(),
                    Avatar = comment["avatar"].ToString(),
                    Date = comment["date"].ToString(),
                    CommentContent = comment["commentContent"].ToString(),
                    CommentScore = double.Parse(comment["commentScore"].ToString())
                }) ;
                Console.WriteLine(comment["commentContent"].ToString());
            }

        }


        public async Task getStayDetailByStayId()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string text = await client.GetStringAsync(
                "http://124.223.171.21:8080/api/v1/stay?stayId=" + StayId);

            // 解析json
            JObject stayJson = (JObject)JsonConvert.DeserializeObject(text);

            // 民宿图片
            
            var photos = stayJson["stayImages"].ToList();
            foreach (var photo in photos)
            {
                StayImages.Add(new SingleImage(photo.ToString()));
                _homeStayDetail.StayImages.Add(new SingleImage(photo.ToString()));
            }

            // 房间信息
            var rooms = stayJson["rooms"].ToList();
            foreach (var room in rooms)
            {
                Console.WriteLine(room["price"].ToString());

                Rooms.Add(new Room
                {
                    Id = room["id"].ToString(),
                    Area = room["area"].ToString(),
                    BathRoom = room["bathroom"].ToString(),
                    RoomCapacity = room["roomCapacity"].ToString(),
                    RoomImage = room["roomImage"].ToString(),
                    Price = room["price"].ToString(),

                });
            }


            // 民宿名称
            StayName = stayJson["stayName"].ToString();
            // 民宿描述
            StayDescription = stayJson["stayDescription"].ToString();
            // 房东头像
            HostAvatar = stayJson["hostAvatar"].ToString();
            // 房东等级
            HostLevel = stayJson["hostLevel"].ToString();
            // 房东评论总数
            hostCommentNum = int.Parse(stayJson["hostCommentNum"].ToString());
            HostCommentNum = int.Parse(stayJson["hostCommentNum"].ToString());
            // 房东名称
            HostName = stayJson["hostName"].ToString();
            // 地理位置
            var stayPosition = stayJson["stayPosition"].ToList();
            Latitude = double.Parse(stayPosition[0].ToString());
            Longitude = double.Parse(stayPosition[1].ToString());
            // 房间数量
            RoomNum = int.Parse(stayJson["roomNum"].ToString());
            // 床数量
            BedNum = int.Parse(stayJson["bedNum"].ToString());
            // 房间容量
            StayCapacity = int.Parse(stayJson["stayCapacity"].ToString());
            // 公共浴室数量
            PublicBathroom = int.Parse(stayJson["publicBathroom"].ToString());
            // 公共卫生间数量
            PublicToilet = int.Parse(stayJson["publicToilet"].ToString());
            // 是否有无障碍设施
            NonBarrierFacility = Boolean.Parse(stayJson["nonBarrierFacility"].ToString());
            // 开始时间
            StartTime = stayJson["startTime"].ToString();
            // 结束时间
            Endtime = stayJson["endTime"].ToString();


            // kknd
            Console.WriteLine("done" +hostCommentNum.ToString());
        }
    }
}
