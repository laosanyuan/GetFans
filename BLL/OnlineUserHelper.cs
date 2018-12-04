using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OnlineUserHelper
    {
        /// <summary>
        /// 获取新登陆用户信息
        /// </summary>
        /// <returns></returns>
        public static List<Model.OnlineUser> GetOnlineUserList()
        {
            List<Model.OnlineUser> users = new List<Model.OnlineUser>();

            //Model.OnlineUser onlineUser = new Model.OnlineUser();

            //onlineUser.UserName = "2227622716@qq.com";
            //onlineUser.Password = "yuan?10ymglw";
            //onlineUser.Email = "yhonglai@163.com";
            //onlineUser.StartTime = DateTime.Now;
            //onlineUser.EndTime = DateTime.Now;

            //users.Add(onlineUser);

            return users; 
        }
    }
}
