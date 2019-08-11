using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GroupMessageResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public long last_read_mid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object significant_msgs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public List<string> group_tips { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MessagesItem> messages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ts { get; set; }
    }

    public class MessagesItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long from_uid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public From_user from_user { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long gid { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string content { get; set; }
    }

    public class From_user
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string screen_name { get; set; }
        /// <summary>
        /// uid
        /// </summary>
        public string profile_url { get; set; }
    }
}
