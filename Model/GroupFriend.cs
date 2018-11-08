using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GroupFriend
    {
        public Model.Fan Fan { get; set; } //群好友

        public string Mid { get; set; } //消息id

        public string Gid { get; set; } //来源群

        public string GroupName { get; set; } //群名

        public DateTime FollowTime { get; set; } // 关注对方时间
    }
}
