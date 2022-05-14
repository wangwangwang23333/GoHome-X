using GameCommunity.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public long StayId;
        public List<SingleImage> StayImages;
        public string StayName;
        public string StayDescription;
        public string HostAvatar;
        public string HostLevel;
        public int HostCommentNum;
        public string HostName;
        // 经纬度坐标
        public double Latitude;
        public double Longitude;

        public int RoomNum;
        public int PublicBathroom;
        public int StayCapacity;
        public bool NonBarrierFacility;

        // 入住开始和结束时间
        public string StartTime;
        public string Endtime;

        public long PageId
        {
            get
            {
                return StayId;
            }
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
            // kknd
            Console.WriteLine(text);
            Console.WriteLine(text);
            Console.WriteLine(text);
            Console.WriteLine(text);
        }
    }
}
