using System;
using System.Collections.Generic;
using System.Text;

namespace GameCommunity.Models
{
    /// <summary>
    /// 单张图片的类
    /// </summary>
    public class SingleImage
    {
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public SingleImage(string imageUrl)
        {
            ImageUrl = imageUrl;
        }
        public SingleImage(string imageUrl, string description)
        {
            ImageUrl = imageUrl;
            Description = description;
        }
    }
}
