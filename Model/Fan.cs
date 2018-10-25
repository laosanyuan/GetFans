using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class Fan
    {
        public String Uid { get; set; }//uid
        public String NickName { get; set; }//昵称

        public bool IsFollowMe { get; set; }//是否关注我
        public bool IsFollowed { get; set; }//我是否关注
    }
}
