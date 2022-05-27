using System;
using System.Collections.Generic;
using System.Text;

namespace GameCommunity.Models
{
    public class Room
    {
        public string Id { get; set; }
        public string Area { get; set; }
        public string BathRoom { get; set; }
        public string Price { get; set; }
        public string RoomCapacity { get; set; }
        public string RoomImage { get; set; }

        public string AreaShown
        {
            get {
                return $"房间面积{Area}m²";
            }
        }

        public string BathRoomShown
        {
            get
            {
                return $"独享卫生间{BathRoom}个";
            }
        }

        public string RoomCapacityShown
        {
            get
            {
                return $"最多{RoomCapacity}人";
            }
        }

        public string PriceShown
        {
            get
            {
                return $"价格{Price}/晚";
            }
        }
    }
}
