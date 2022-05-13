using System;
using System.Collections.Generic;
using System.Text;

namespace GameCommunity.Models
{
    public class HomePageStayGroup: List<HomePageStay>
    {
        // 组别名臣
        public string Name { get; private set; }

        // 组别图标
        public string ImgUrl { get; private set; }

        // 组别描述符
        public string Description { get; private set; }

        public HomePageStayGroup(string name, string imgUrl, 
            string description,
            List<HomePageStay> homePageStays): base(homePageStays)
        {
            Name = name;
            ImgUrl = imgUrl;
            Description = description;
        }
    }
}
