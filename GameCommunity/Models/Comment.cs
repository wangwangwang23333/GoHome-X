using System;
using System.Collections.Generic;
using System.Text;

namespace GameCommunity.Models
{
    public class Comment
    {
        public string Id { get; set; }
        public string NickName { get; set; }
        public string Avatar { get; set; }
        public string Date { get; set; }
        public string CommentContent { get; set; }
        public double CommentScore { get; set; }

        public string DateShown
        {
            get
            {
                return "时间：" + this.Date.Substring(0, 10);
            }

        }

        public string CommentScoreShown
        {
            get
            {
                int index = 0;
                string result = "";
                while (index < CommentScore)
                {
                    result += "★";
                    index++;
                }
                while (index < 5)
                {
                    result += "☆";
                    index++;
                }
                return result;
            }

        }
    }
}
