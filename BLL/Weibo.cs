using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Weibo
    {
        private static readonly int GroupIntervalDay = 5; //群间隔时间

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
        /// <summary>
        /// 更新cookies
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static CookieContainer UpdateCookies(string userName ,string password)
        {
            Model.User user = PrepareLogin(userName, password);
            if (StartLogin(user).Equals("0"))
            {
                return user.Cookies;
            }
            else
            {
                bool isSuccess = false;
                //解码
                for (int i = 0; i < 5; i++)
                {
                    Image image = GetCodeImage(user);
                    string code = BLL.CheckCode.DecodeCheckCode(image, out int resultId);
                    if (code.Equals(""))
                    {
                        continue;
                    }
                    string result = BLL.Weibo.StartLogin(user, code);
                    if (result.Equals("0"))
                    {
                        //登录成功
                        isSuccess = true;
                        break;
                    }
                    else
                    {
                        //解码失败
                    }
                }
                //判断是否登录成功
                if (isSuccess)
                {
                    return user.Cookies;
                }
                else
                {
                    return null;
                }
            }
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
        /// <summary>
        /// 获取最后一页未回粉列表
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static int CancelFollowFakerUser(CookieContainer cookie, string uid)
        {
            return DAL.Weibo.CancelFollowFakerUser(cookie, uid);
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
        /// <summary>
        /// 退群
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="uid">用户id</param>
        /// <param name="gid">群id</param>
        /// <param name="groupName">群名</param>
        /// <returns></returns>
        public static bool ExitGroup(CookieContainer cookie, string uid, string gid, string groupName)
        {
            //退群
            if (DAL.Weibo.ExitGroup(cookie, gid, groupName))
            {
                //修改数据库
                DAL.WinClientSQLiteHelper.ExitGroup(uid, gid);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据加群时间退群
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="uid"></param>
        /// <param name="gid"></param>
        /// <param name="groupName">群名</param>
        /// <returns></returns>
        public static bool ExitGroupByTime(CookieContainer cookie, string uid, string gid, string groupName)
        {
            DateTime enterTime = DAL.WinClientSQLiteHelper.GetGroupEnterTime(uid, gid);
            if (enterTime.Equals(new DateTime()))
            {
                return false;
            }
            TimeSpan span = DateTime.Now.Subtract(enterTime);
            int days = Convert.ToInt32(span.Days);

            if (days > GroupIntervalDay)
            {
                if (ExitGroup(cookie, uid, gid, groupName))
                {
                    //记录数据库
                    DAL.WinClientSQLiteHelper.ExitGroup(uid, gid);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 根据上次退群时间间隔加群
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="uid"></param>
        /// <param name="gid"></param>
        /// <param name="groupName"></param>
        public static void EnterGroupByTime(CookieContainer cookie, string uid, string gid, string groupName)
        {
            bool isAddedBefore = DAL.WinClientSQLiteHelper.IsGroupAddedBefore(uid, gid);
            if (!isAddedBefore)
            {
                AddGroup(cookie, gid, groupName);
            }
            else
            {
                DateTime exitTime = DAL.WinClientSQLiteHelper.GetGroupExitTime(uid, gid);
                TimeSpan span = DateTime.Now.Subtract(exitTime);
                int days = Convert.ToInt32(span.Days);
                if (days > GroupIntervalDay)
                {
                    AddGroup(cookie, gid, groupName);
                }
            }
        }
        #endregion

        #region [聊天内容]
        //聊天内容
        public static List<string> GroupInviteFollowMe = new List<string>()
        {
            "互粉！",
            "求互粉，永不取关！",
            "互粉秒回，欢迎互动！",
            "求互粉，老铁们！绝对不取关！",
            "互粉，有诚意的来，绝不取关！",
            "互粉！互赞！互评！",
            "互粉，稳定的来！",
            "互粉，如果今天到上限了，明天一早绝对回粉！",
            "求互粉，朋友们！有群的拉我一下！",
            "朋友们求互粉，在线秒回！",

            //广告
            "亲爱的朋友们，我正在使用@小火箭互粉精灵 辅助互粉。互粉我三分钟之内秒回，绝对智能！",
            "有互粉的亲吗，智能回粉，绝不漏粉     ——来自@小火箭互粉精灵 智能互粉",
            "在线秒回，欢迎互粉[心][心][心]   ——自动回粉功能由@小火箭互粉精灵 提供",

            "在线互粉[doge]!",
            "互粉，秒回[心]",
            "在线秒回，求互粉，有群的记得拉我！永不取消[笑cry]",
            "求互粉，诚信的来！",
            "良心互粉 拒绝骗粉 有粉必回",
            "小伙伴们，来互粉了！",
            "互粉啦[心][心][心]",
            "长期互动，真诚互粉，有群记得拉我！",
            "真诚互粉，骗粉绕行！",
            "[doge][doge][doge] 互粉，骗粉的不要来！",

            "真诚互粉，永不取关的来粉我，秒回！",
            "有互粉的来关注我，真诚互粉，频繁互动，绝不取关。",
            "[心][心][心]互粉[心][心][心]",
            "[互粉][互粉][互粉]",
            "主动关注我的，马上回粉，不用提醒，诚信互粉！",
            "互粉秒回，绝不遗漏[握手]",
            "[心]诚信互粉，长期在线的朋友在哪里[心]",
            "可以互粉的好友在哪里，关注我秒回！",
            "在线互粉，不用艾特提醒，最迟三分钟绝对回粉！",
            "诚信互粉，有目共睹，长期在线，及时回粉！",

            "还没互粉的关注我一下，立刻回！",
            "[心]真诚互粉，秒回[心]",
            "互粉 互粉 互粉 ，重要的互粉立即回[二哈]",
            "关注必回，互粉必真[心]",
            "互粉互动，在线秒回",
            "欢迎互粉，偷偷取关不是好孩子[握手][握手][握手]",
            "有粉必回，信誉保证",
            "粉我的都已回关，请继续[坏笑][坏笑][坏笑]",
            "真实互粉，诚信为主，有粉必回",
            "欢迎关注我，立即回关！"
        };
        #endregion
    }
}
