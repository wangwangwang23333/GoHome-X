using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace GameCommunity.Models
{
    public class HomePageStay
    {
        public long StayId { get; set; }
        public string StayPhoto { get; set; }
        public List<SingleImage> StayPhotoList { get; set; } = new List<SingleImage>();
        public string StayName { get; set; }
        public double StayPrice { get; set; }
        public double StayScore { get; set; }
        public string HostAvatar { get; set; }
        public string StayDescription { get; set; }
        public int StayCommentNum { get; set; }

        public string StayPriceShown
        {
            get
            {
                return "￥" + this.StayPrice.ToString() + " / 晚";
            }

        }

        public string StayScoreShown
        {
            get
            {
                int index = 0;
                string result = "";
                while (index++ < StayScore)
                {
                    result += "★";
                }
                while (index++ < 5)
                {
                    result += "☆";
                }
                return result;
            }

        }

    }
}
