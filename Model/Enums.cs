using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    //好友关注状态
    public enum FriendStatus
    {
        Unknown, //状态未知
        OnlyFollowMe, //关注了我但是我没有关注对方
        OnlyFollowHe, //我关注了他但是他没有关注我
        FollowEachOther, //互相关注
        UnFollowEachOther //互相都未关注
    }
}
