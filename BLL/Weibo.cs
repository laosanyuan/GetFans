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
        /// <summary>
        /// 获取未关注粉丝列表
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static List<Model.Fan> GetUnfollowFansList(CookieContainer cookie,string uid)
        {
            return DAL.Weibo.GetUnfollowFansList(cookie, uid);
        }
        //更新关注数
        public static void UpdateUsersFansCount(Model.User user)
        {
            DAL.Weibo.UpdateUsersFansCount(user);
        }
        #endregion

        #region 群聊
        /// <summary>
        /// 加群
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="gid"></param>
        /// <param name="groupName">群名</param>
        public static void AddGroup(CookieContainer cookie, string gid, string groupName)
        {
            //判断是否已入此群
            //加群
            DAL.Weibo.AddGroup(cookie, gid, groupName);
        }
        /// <summary>
        /// 向群内发布一条消息
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="gid"></param>
        /// <param name="message"></param>
        public static void SendMessage2Group(CookieContainer cookie, string gid, string message)
        {
            DAL.Weibo.SendMessage2Group(cookie, gid, message);
        }
        /// <summary>
        /// 获取当前用户的互粉群列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static List<Model.Group> GetGroups(CookieContainer cookie)
        {
            return DAL.Weibo.GetGroups(cookie);
        }
        /// <summary>
        /// 获取群聊天成员信息
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="uid">uid</param>
        /// <param name="gid">群id</param>
        /// <param name="groupName">群名</param>
        /// <returns></returns>
        public static List<Model.GroupFriend> GetGroupFriendsList(CookieContainer cookie,string uid, string gid,string groupName )
        {
            List<Model.GroupFriend> friends = DAL.Weibo.EnterGroup(cookie, gid, groupName);

            //如在进入群聊后获取到当前登录用户信息，说明在此期间聊天不活跃，不再继续获取前页
            if (friends.Find(t => t.Fan.Uid.Equals(uid)) == null)
            {
                friends.AddRange(DAL.Weibo.GetGroupBeforePageFriends(cookie, gid, groupName, friends[0].Mid));
            }
            else
            {
                friends.Clear();
            }
            return friends;
        }
        /// <summary>
        /// 获取特定好友的关注状态
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="uid">对方uid</param>
        /// <returns></returns>
        public static Model.FriendStatus GetFriendFollowStatus(CookieContainer cookie, string uid)
        {
            return DAL.Weibo.GetFriendFollowStatus(cookie, uid);
        }
        /// <summary>
        /// 判断是否已加入过此群
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="gid">群id</param>
        /// <returns>true:已加入</returns>
        public static bool IsAddedThisGroup(CookieContainer cookie,string gid)
        {
            return DAL.Weibo.IsAddedThisGroup(cookie, gid);
        }
        #endregion


        #region [聊天内容]
        //聊天内容
        public static List<string> GroupInviteFollowMe = new List<string>()
        {
            "互粉！",
            "求互粉，永不取消！",
            "互粉秒回，欢迎互动！",
            "求互粉，老铁们！绝对不取关！",
            "互粉，有诚意的来，绝不取关！",
            "互粉！互赞！互评！",
            "互粉，稳定的来！",
            "互粉，如果今天到上限了，明天一早绝对回粉！",
            "求互粉，朋友们！有群的拉我一下！",

            "朋友们求互粉，在线秒回！",
            "在线互粉[doge]!",
            "互粉，秒回[心]",
            "在线秒回，求互粉，有群的记得拉我！永不取消[笑cry]",
            "求互粉，诚信的来！"
        };
        #endregion
    }
}
