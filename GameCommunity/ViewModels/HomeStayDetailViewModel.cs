﻿using GameCommunity.Models;
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

        public long StayId { get; set; }
        // 房源图片
        public List<SingleImage> stayImages;

        public ObservableCollection<SingleImage> StayImages { get; set; }

        public int[] s = { 1, 2, 3 };

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
            get => HostCommentNum + "条评论";
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


        public int PublicBathroom;
        public int PublicToilet;
        public int StayCapacity;
        public bool NonBarrierFacility;

        // 入住开始和结束时间
        public string StartTime;
        public string Endtime;

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
                StayDescription = "testtest";
                await getStayDetailByStayId();
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
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
            StayImages = new ObservableCollection<SingleImage>();
            var photos = stayJson["stayImages"].ToList();
            foreach (var photo in photos)
            {
                StayImages.Add(new SingleImage(photo.ToString()));
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
            Console.WriteLine("done" + StayImages.Count.ToString());
        }
    }
}
