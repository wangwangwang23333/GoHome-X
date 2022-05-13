using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GameCommunity.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;

namespace GameCommunity.Services
{
    public class HomePageStayDataStore : IDataStore<HomePageStay>
    {
        readonly List<HomePageStay> homePageStays;

        readonly List<HomePageStay> hottestHomePageStays;

        readonly List<HomePageStay> cheapestHomePageStays;

        readonly List<HomePageStayGroup> homePageStayGroups;



        public HomePageStayDataStore()
        {
            homePageStays = new List<HomePageStay>();
            hottestHomePageStays = new List<HomePageStay>();
            cheapestHomePageStays = new List<HomePageStay>();

            homePageStayGroups = new List<HomePageStayGroup>();

            // 网络检测
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                return;
            }

        }

        public Task<bool> AddItemAsync(HomePageStay item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task getCheapestStay()
        {
            // 获取特惠房源
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string text = await client.GetStringAsync("http://124.223.171.21:8080/api/v1/statistics/stay/price");

            JObject jo = (JObject)JsonConvert.DeserializeObject(text);

            List<JToken> stayList = jo["stayList"].ToList();

            // 分别调用API
            List<Task<string>> taskList = new List<Task<string>>();
            foreach (JToken stayId in stayList)
            {
                taskList.Add(
                    client.GetStringAsync("http://124.223.171.21:8080/api/v1/stay/brief?stayId=" + stayId.ToString()));
            }

            // 同时开始任务
            await new TaskFactory().ContinueWhenAll(taskList.ToArray(), tArray =>
            {
                Console.WriteLine("API都调用完了");
                for (int index = 0; index < taskList.Count; ++index)
                {
                    // 解析json
                    JObject stayJson = (JObject)JsonConvert.DeserializeObject(taskList[index].Result);

                    // 图片列表
                    List<SingleImage> stayPhotoImages = new List<SingleImage>();
                    var photos = stayJson["stayPositionInfo"]["stayPhoto"].ToList();
                    foreach (var photo in photos)
                    {
                        stayPhotoImages.Add(new SingleImage(photo.ToString()));
                    }

                    cheapestHomePageStays.Add(new HomePageStay
                    {
                        StayId = long.Parse(stayList[index].ToString()),
                        StayPhoto = stayJson["stayPositionInfo"]["stayPhoto"].ToList()[0].ToString(),
                        StayName = stayJson["stayPositionInfo"]["stayName"].ToString(),
                        StayPrice = Double.Parse(stayJson["stayPositionInfo"]["stayPrice"].ToString()),
                        StayScore = Double.Parse(stayJson["stayPositionInfo"]["stayScore"].ToString()),
                        HostAvatar = stayJson["stayPositionInfo"]["hostAvatar"].ToString(),
                        StayDescription = stayJson["stayPositionInfo"]["stayDescribe"].ToString(),
                        StayCommentNum = int.Parse(stayJson["stayPositionInfo"]["stayCommentNum"].ToString()),
                        StayPhotoList = stayPhotoImages
                    });
                    Console.WriteLine("Done for it" + taskList.Count.ToString());
                }

                // 加入分组中
                homePageStayGroups.Add(new HomePageStayGroup("特惠房源",
                    "https://wwwtypora.oss-cn-shanghai.aliyuncs.com/%E8%B4%AD%E4%BE%BF%E5%AE%9C%EF%BC%8D9.9.png",
                    "低价优质房源等你来挑！",
                    cheapestHomePageStays));
            });
        }

        public async Task getHottestStay()
        {
            // 最热销的房源
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string text = await client.GetStringAsync("http://124.223.171.21:8080/api/v1/statistics/stay/order");

            JObject jo = (JObject)JsonConvert.DeserializeObject(text);

            List<JToken> stayList = jo["stayList"].ToList();

            // 分别调用API
            List<Task<string>> taskList = new List<Task<string>>();
            foreach (JToken stayId in stayList)
            {
                taskList.Add(
                    client.GetStringAsync("http://124.223.171.21:8080/api/v1/stay/brief?stayId=" + stayId.ToString()));
            }

            // 同时开始任务
            await new TaskFactory().ContinueWhenAll(taskList.ToArray(), tArray =>
            {
                Console.WriteLine("API都调用完了");
                for (int index = 0; index < taskList.Count; ++index)
                {
                    // 解析json
                    JObject stayJson = (JObject)JsonConvert.DeserializeObject(taskList[index].Result);

                    // 图片列表
                    List<SingleImage> stayPhotoImages = new List<SingleImage>();
                    var photos = stayJson["stayPositionInfo"]["stayPhoto"].ToList();
                    foreach(var photo in photos)
                    {
                        stayPhotoImages.Add(new SingleImage(photo.ToString()));
                    }

                    hottestHomePageStays.Add(new HomePageStay
                    {
                        StayId = long.Parse(stayList[index].ToString()),
                        StayPhoto = stayJson["stayPositionInfo"]["stayPhoto"].ToList()[0].ToString(),
                        StayName = stayJson["stayPositionInfo"]["stayName"].ToString(),
                        StayPrice = Double.Parse(stayJson["stayPositionInfo"]["stayPrice"].ToString()),
                        StayScore = Double.Parse(stayJson["stayPositionInfo"]["stayScore"].ToString()),
                        HostAvatar = stayJson["stayPositionInfo"]["hostAvatar"].ToString(),
                        StayDescription = stayJson["stayPositionInfo"]["stayDescribe"].ToString(),
                        StayCommentNum = int.Parse(stayJson["stayPositionInfo"]["stayCommentNum"].ToString()),
                        StayPhotoList = stayPhotoImages
                    });
                    Console.WriteLine("Done for it" + taskList.Count.ToString());
                }

                // 加入分组中
                homePageStayGroups.Add(new HomePageStayGroup("热销房源",
                    "https://wwwtypora.oss-cn-shanghai.aliyuncs.com/%E7%83%AD%E9%94%80.png",
                    "爆款房源，速来体验！",
                    hottestHomePageStays));
            });
        }

        public async Task getBestStay()
        {
            // 评分最高的民宿
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string text = await client.GetStringAsync("http://124.223.171.21:8080/api/v1/statistics/stay/score");

            JObject jo = (JObject)JsonConvert.DeserializeObject(text);
            
            List<JToken> stayList = jo["stayList"].ToList();
            
            // 分别调用API
            List<Task<string>> taskList = new List<Task<string>>();
            foreach (JToken stayId in stayList)
            {
                taskList.Add(
                    client.GetStringAsync("http://124.223.171.21:8080/api/v1/stay/brief?stayId=" + stayId.ToString()));
            }
            
            // 同时开始任务
            await new TaskFactory().ContinueWhenAll(taskList.ToArray(), tArray =>
            {
                Console.WriteLine("API都调用完了");
                for (int index = 0; index < taskList.Count; ++index)
                {
                    // 解析json
                    JObject stayJson = (JObject)JsonConvert.DeserializeObject(taskList[index].Result);

                    // 图片列表
                    List<SingleImage> stayPhotoImages = new List<SingleImage>();
                    var photos = stayJson["stayPositionInfo"]["stayPhoto"].ToList();
                    foreach (var photo in photos)
                    {
                        stayPhotoImages.Add(new SingleImage(photo.ToString()));
                    }

                    homePageStays.Add(new HomePageStay
                    {
                        StayId = long.Parse(stayList[index].ToString()),
                        StayPhoto = stayJson["stayPositionInfo"]["stayPhoto"].ToList()[0].ToString(),
                        StayName = stayJson["stayPositionInfo"]["stayName"].ToString(),
                        StayPrice = Double.Parse(stayJson["stayPositionInfo"]["stayPrice"].ToString()),
                        StayScore = Double.Parse(stayJson["stayPositionInfo"]["stayScore"].ToString()),
                        HostAvatar = stayJson["stayPositionInfo"]["hostAvatar"].ToString(),
                        StayDescription = stayJson["stayPositionInfo"]["stayDescribe"].ToString(),
                        StayCommentNum = int.Parse(stayJson["stayPositionInfo"]["stayCommentNum"].ToString()),
                        StayPhotoList = stayPhotoImages
                    });
                    Console.WriteLine("Done for it" + taskList.Count.ToString());
                }

                // 加入分组中
                homePageStayGroups.Add(new HomePageStayGroup("精品房源", 
                    "https://wwwtypora.oss-cn-shanghai.aliyuncs.com/%E4%BC%98%E8%B4%A8.png",
                    "高分房源，肆意探索！",
                    homePageStays));
            });

        }


        public async Task<IEnumerable<HomePageStay>> GetHomePageStaysAsync(bool forceRefresh = false)
        {
            //await getHottestStay();
            return await Task.FromResult(hottestHomePageStays);
        }

        

        public Task<HomePageStay> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<HomePageStay>> GetHottestHomeStaysAsync(bool forceRefresh = false)
        {
            await getHottestStay();
            return await Task.FromResult(hottestHomePageStays);
        }

        public async Task<IEnumerable<HomePageStay>> GetItemsAsync(bool forceRefresh = false)
        {
            await getBestStay();
            Console.WriteLine("=====执行完毕=======");
            return await Task.FromResult(homePageStays);
        }

        public Task<bool> UpdateItemAsync(HomePageStay item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<HomePageStayGroup>> GetHomePageStayGroupsAsync(bool forceRefresh = false)
        {
            await getHottestStay();
            await getBestStay();
            await getCheapestStay();
            Console.WriteLine("================成功获取HomePageStayGroups===============length:" + homePageStayGroups.Count);
            return await Task.FromResult(homePageStayGroups);
        }
    }
}
