using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Position
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string longitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string latitude { get; set; }
        /// <summary>
        /// 曲阜师范大学
        /// </summary>
        public string name { get; set; }
    }

    public class Join_groupsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string page_objectid { get; set; }
        /// <summary>
        /// 大V互动共同成长
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 大V互动共同成长
        /// </summary>
        public string system_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatar_s { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string round_avatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string round_avatar_s { get; set; }
        /// <summary>
        /// 只有一起成长才是王道～
        /// </summary>
        public string summary { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int max_member { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long owner { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int member_count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long create_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int validate_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long group_ts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int publicity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<int> affi_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<long> affiliation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long last_msg_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long join_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long begin_mid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int push { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int addsession { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int filterfeed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int push_airborne { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string user_custom_msg_setting { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int filterquery { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string group_url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int global_max_admin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int max_admin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<long> admins { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Position position { get; set; }
    }

    public class GroupList
    {
        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Join_groupsItem> join_groups { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ts { get; set; }
    }
}
