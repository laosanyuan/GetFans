using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Weibo
    {
        #region 登录
        /// <summary>
        /// 预登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Model.User PrepareLogin(string userName, string password)
        {
            return DAL.Weibo.PrepareLogin(userName, password);
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Image GetCodeImage(Model.User user)
        {
            return DAL.Weibo.GetCodeImage(user);
        }

        /// <summary>
        /// 正式开始登录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="door">验证码，如没有验证码则不用传</param>
        /// <returns></returns>
        public static string StartLogin(Model.User user, string door = null)
        {
            return DAL.Weibo.StartLogin(user, door);
        }
        #endregion

        #region 关注
        /// <summary>
        /// 关注用户
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="nickName"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static bool Follow(string uid, string nickName, CookieContainer cookie)
        {
            return DAL.Weibo.Follow(uid, nickName, cookie);
        }
        #endregion

        #region 取消关注
        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="nickName"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static bool CancelFollow(string uid, string nickName, CookieContainer cookie)
        {
            return DAL.Weibo.CancelFollow(uid, nickName, cookie);
        }
        #endregion
    }
}
