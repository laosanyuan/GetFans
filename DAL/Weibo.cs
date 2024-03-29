﻿using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace DAL
{
    public class Weibo
    {
        #region 登录过程
        /// <summary>
        /// 登陆准备
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Model.User PrepareLogin(string userName, string password)
        {
            Model.User User = new Model.User();
            User.UserName = userName;
            User.Password = password;
            User.LoginPara = new Model.LoginParameter();
            //加密用户名
            Encoding myEncoding = Encoding.GetEncoding("utf-8");
            byte[] suByte = myEncoding.GetBytes(HttpUtility.UrlEncode(userName));
            User.LoginPara.su = Convert.ToBase64String(suByte);

            return User;
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Image GetCodeImage(Model.User user)
        {
            //获取登录参数
            string url = "http://login.sina.com.cn/sso/prelogin.php?entry=weibo&callback=sinaSSOController.preloginCallBack&su="
            + user.LoginPara.su + "&rsakt=mod&checkpin=1&client=ssologin.js(v1.4.18)";
            string content = HttpHelper.Get(url);
            int pos;
            pos = content.IndexOf("servertime");
            user.LoginPara.servertime = content.Substring(pos + 12, 10);
            pos = content.IndexOf("pcid");
            user.LoginPara.pcid = content.Substring(pos + 7, 39);
            pos = content.IndexOf("nonce");
            user.LoginPara.nonce = content.Substring(pos + 8, 6);
            pos = content.IndexOf("showpin");
            user.LoginPara.showpin = content.Substring(pos + 9, 1);

            user.LoginPara.IsForcedPin = true;

            //获取验证码
            url = "http://login.sina.com.cn/cgi/pin.php?p=" + user.LoginPara.pcid;
            return HttpHelper.GetImage(url);
        }
        /// <summary>
        /// 正式开始登录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="door">验证码，如没有验证码则不用传</param>
        /// <returns></returns>
        public static string StartLogin(Model.User user, string door = null)
        {
            string securityPassword = GetSecurityPassword(user.Password, user.LoginPara.servertime, user.LoginPara.nonce, user.LoginPara.PUBKEY);
            if (securityPassword == null)
            {
                return "RSA加密失败";
            }

            string postData = "entry=weibo&gateway=1&from=&savestate=7&useticket=1&pagerefer=&vsnf=1&su=" + user.LoginPara.su
                + "&service=miniblog&servertime=" + user.LoginPara.servertime
                + "&nonce=" + user.LoginPara.nonce
                + "&pwencode=rsa2&rsakv=" + user.LoginPara.RSAKV + "&sp=" + securityPassword
                + "&sr=1366*768&encoding=UTF-8&prelt=104&url=http%3A%2F%2Fweibo.com%2Fajaxlogin.php%3Fframelogin%3D1%26callback%3Dparent.sinaSSOController.feedBackUrlCallBack&returntype=META";

            if (((user.LoginPara.showpin !=null && user.LoginPara.showpin.Equals("1")) || user.LoginPara.IsForcedPin)&&door != null)
            {
                postData += "&pcid=" + user.LoginPara.pcid + "&door=" + door;
            }

            string content = HttpHelper.Post("http://login.sina.com.cn/sso/login.php?client=ssologin.js(v1.4.18)", postData);
            int pos = content.IndexOf("retcode=");
            string retcode = content.Substring(pos + 8, 1);

            if (retcode == "0")
            {
                //获取cookie
                user.Cookies = new CookieContainer();
                pos = content.IndexOf("location.replace");
                string url = content.Substring(pos + 18, 276);//285->276 fuck!! 
                content = HttpHelper.Get(url, user.Cookies, false);
                //获取头像、uid、昵称
                GetUidNickNameAndSoOn(user);
            }
            else
            {
                retcode = content.Substring(pos + 8, 4);
            }


            return retcode;
        }
        #endregion

        #region 粉丝相关
        /// <summary>
        /// 关注一个用户
        /// </summary>
        /// <param name="uid">uid</param>
        /// <param name="nickName">昵称</param>
        /// <param name="cookie">登录cookie</param>
        /// <returns>是否关注成功</returns>
        public static bool Follow(string uid,string nickName,CookieContainer cookie)
        {
            string data = String.Format("uid={0}&objectid=&f=1&extra=&refer_sort=&refer_flag=1005050001_&location=page_100505_home&oid={0}&wforce=1&nogroup=1&fnick={1}&refer_lflag=1005050005_&refer_from=profile_headerv6&template=7&special_focus=1&isrecommend=1&is_special=0&redirect_url=%252Fp%252F1005056676557674%252Fmyfollow%253Fgid%253D4279893657022870%2523place&_t=0", uid, nickName);
            string url = @"https://weibo.com/aj/f/followed?ajwvr=6&__rnd=" + GetTimeStamp();

            var s = HttpHelper.SendDataByPost(url, cookie, data);

            return s.Equals("") ? false : CheckBackCode(JsonHelper.GetBackJson(s).code);
        }
        /// <summary>
        /// 取消关注用户
        /// </summary>
        /// <param name="uid">uid</param>
        /// <param name="nickName">昵称</param>
        /// <param name="cookies">cookie</param>
        /// <returns>是否取消关注成功</returns>
        public static bool CancelFollow(string uid, string nickName, CookieContainer cookie)
        {
            string data = String.Format("uid={0}&objectid=&f=1&extra=&refer_sort=&refer_flag=1005050001_&location=page_100505_home&oid={0}&wforce=1&nogroup=1&fnick={1}&refer_lflag=1005050005_&refer_from=profile_headerv6&template=7&special_focus=1&isrecommend=1&is_special=0&redirect_url=%2Fp%2F1005056676557674%2Fmyfollow%3Fgid%3D4279893657022870%23place", uid, nickName);
            string url = @"https://weibo.com/aj/f/unfollow?ajwvr=6";

            string s = HttpHelper.SendDataByPost(url, cookie, data);

            return s.Equals("") ? false : CheckBackCode(JsonHelper.GetBackJson(s).code);
        }
        /// <summary>
        /// 获取未关注粉丝列表
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static List<Model.Fan> GetUnfollowFansList(CookieContainer cookie, string uid)
        {
            List<Model.Fan> Fans = new List<Model.Fan>();
            string url = String.Format(@"https://weibo.com/{0}/fans?topnav=1&wvr=6&mod=message&need_filter=1", uid);
            string result = HttpHelper.Get(url, cookie, true);
            string regexString = @"followlist&uid=(\d)*?&fnick=(.)*?&f=1";
            MatchCollection matches = Regex.Matches(result, regexString);

            foreach (Match match in matches)
            {
                string data = match.Value.Replace(@"followlist&uid=", "").Replace("fnick=", "").Replace("&f=1", "");
                string[] values = data.Split('&');
                Model.Fan fan = new Model.Fan();
                fan.Uid = values[0];
                fan.NickName = values[1];
                Fans.Add(fan);
            }
   
            return Fans;
        }
        /// <summary>
        /// 更新粉丝数、关注数
        /// </summary>
        /// <param name=""></param>
        public static void UpdateUsersFansCount(Model.User user)
        {
            string userHomePageTxt = HttpHelper.Get("https://weibo.com", user.Cookies, true);

            GetFansAndFollowCount(userHomePageTxt, out string fansCount, out string followCount);
            user.FansCount = fansCount;
            user.FollowCount = followCount;
        }
        /// <summary>
        /// 获取特定用户的关注状态
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="uid">对方uid</param>
        /// <returns>关注状态</returns>
        public static Model.FriendStatus GetFriendFollowStatus(CookieContainer cookie, string uid)
        {
            string url = String.Format(@"https://weibo.com/aj/v6/user/newcard?ajwvr=6&id={0}&refer_flag=1005050005_&type=1&callback=STK_{1}", uid, GetTimeStamp());
            string s = HttpHelper.Get(url, cookie, false);

            //判断是否互相关注
            if (Regex.IsMatch(s, @"\\u4e92\\u76f8\\u5173\\u6ce8") || Regex.IsMatch(s,"互相关注"))
            {
                return Model.FriendStatus.FollowEachOther;
            }
            //判断是否已关注但是未回粉
            if (Regex.IsMatch(s, @"\\u5df2\\u5173\\u6ce8") || Regex.IsMatch(s,"已关注"))
            {
                return Model.FriendStatus.OnlyFollowHe;
            }
            //判断是否未关注对方
            if (Regex.IsMatch(s, @"\\u5173\\u6ce8") || Regex.IsMatch(s,"关注"))
            {
                //判断对方是否已关注我
                if (Regex.IsMatch(s, ">Y<"))
                {
                    return Model.FriendStatus.OnlyFollowMe;
                }
                else
                {
                    return Model.FriendStatus.UnFollowEachOther;
                }
            }
            return Model.FriendStatus.Unknown;
        }
        /// <summary>
        /// 自关注列表末尾取消faker好友
        /// </summary>
        /// <param name="cookies"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static int CancelFollowFakerUser(CookieContainer cookies, string uid)
        {
            int count = 0;
            string url = String.Format("https://weibo.com/{0}/follow?from=page_100505&wvr=6&mod=headfollow#place", uid);
            string request = HttpHelper.Get(url, cookies, false);

            string regexString = "(关注<\\\\/span><em class=\\\\\"num S_txt1\\\\\">)[\\d]*?(<\\\\/em>)";
            MatchCollection matches = Regex.Matches(request, regexString);
            int followersCount = 0;
            foreach (Match match in matches)
            {
                followersCount = Convert.ToInt32(match.Value.Replace("关注<\\/span><em class=\\\"num S_txt1\\\">", "").Replace("<\\/em>", ""));
            }

            if (followersCount > 0)
            {
                int endPage = followersCount / 30 + 1;
                for (; endPage > 0; endPage--)
                {
                    url = String.Format("https://weibo.com/p/100505{0}/myfollow?t=1&cfs=&Pl_Official_RelationMyfollow__95_page={1}#Pl_Official_RelationMyfollow__95", uid, endPage);
                    request = HttpHelper.Get(url, cookies, false);
                    regexString = "(uid=)[\\d]*?(&nick=)(.)*?(>私信)";
                    matches = Regex.Matches(request, regexString);

                    foreach (Match match in matches)
                    {
                        //count--;
                        string[] users = match.Value.Replace("uid=", "").Replace("\\\">私信", "").Replace("&nick=", "#").Split('#');
                        if (users[1].Equals("小火箭互粉精灵"))
                        {
                            continue;
                        }
                        regexString = String.Format("{0}(.)*?已关注(.)*?{0}", users[1]);
                        matches = Regex.Matches(request, regexString);
                        if (matches.Count == 0)
                        {
                            continue;
                        }
                        else
                        {
                            Model.Fan fan = new Model.Fan() { Uid = users[0], NickName = users[1] };

                            if (CancelFollow(users[0], users[1], cookies))
                            {
                                count++;
                            }
                            else
                            {
                                return count;
                            }
                        }
                    }
                }
            }
            return count;
        }
        #endregion

        #region 群聊相关
        /// <summary>
        /// 发布一条群聊信息
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="gid">群id</param>
        /// <param name="message">文字内容</param>
        public static bool SendMessage2Group(CookieContainer cookie, string gid, string message)
        {
            string data = String.Format("source=209678993&text={0}&gid={1}&fids=", message, gid);
            string url = @"https://weibo.com/aj/message/groupchatadd?_wv=5";//&ajwvr=6&__rnd=" + GetTimeStamp();
            string s = HttpHelper.SendDataByPost(url, cookie, data);

            return s.Equals("") ? false : CheckBackCode(JsonHelper.GetBackJson(s).code);
        }
        /// <summary>
        /// 加群
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="gid"></param>
        /// <param name="groupName">群名</param>
        public static bool AddGroup(CookieContainer cookie, string gid, string groupName)
        {
            string data = String.Format("gid={0}&name={1}&isadmin=&_t=0", gid, groupName);
            string url = @"https://weibo.com/p/aj/groupchat/applygroup?ajwvr=6&__rnd=" + GetTimeStamp();
            string s = HttpHelper.SendDataByPost(url, cookie, data);

            return s.Equals("") ? false : CheckBackCode(JsonHelper.GetBackJson(s).code);
        }
        /// <summary>
        /// 获取当前登录用户私信列表的所有群（群名、gid）
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static List<Model.Group> GetGroups(CookieContainer cookie)
        {
            List<Model.Group> Groups = new List<Model.Group>();

            string referer = "https://api.weibo.com/chat/";
            string url = @"https://api.weibo.com/webim/groupchat/query_join_groups.json?source=209678993&t="+GetTimeStamp();

            string result = HttpHelper.Get(url, cookie, referer, true);

            var list = JsonConvert.DeserializeObject<GroupList>(result);
            if (list != null && list.join_groups != null)
            {
                foreach (var group in list.join_groups)
                {
                    if (group.name.Contains("互") &&
                        (group.name.Contains("评") || group.name.Contains("赞") || group.name.Contains("粉")))
                    {
                        Model.Group g = new Model.Group();
                        g.Gid = group.id.ToString();
                        g.Name = group.name;
                        Groups.Add(g);
                    }
                }
            }
            return Groups;
        }
        /// <summary>
        /// 获取特定mid以上的二十条信息的群成员信息
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="gid">群id</param>
        /// <param name="groupName">群名</param>
        /// <param name="mid">定位聊天信息</param>
        /// <returns></returns>
        public static List<Model.MessagesItem> GetGroupBeforePageFriends(CookieContainer cookie,string mid)
        {
            List<Model.MessagesItem> Messages = new List<MessagesItem>();
            string referer = "https://api.weibo.com/chat/";
            string url = String.Format("https://api.weibo.com/webim/groupchat/query_messages.json?convert_emoji=1&query_sender=1&count=20&id={0}&max_mid=0&source=209678993&t={1}", mid, GetTimeStamp());

            string s = HttpHelper.Get(url, cookie, referer, true);

            if (!s.Equals(""))
            {
                var messages = JsonConvert.DeserializeObject<Model.GroupMessageResponse>(s);
                Messages = messages.messages;
            }

            return Messages;
        }
        /// <summary>
        /// 判断是否已经加过此群
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="gid">群id</param>
        /// <returns></returns>
        public static bool IsAddedThisGroup(CookieContainer cookie,string gid)
        {
            bool isAdded = false;
            string url = String.Format(@"https://weibo.com/p/230491{0}?source=webim", gid);
            string s = HttpHelper.Get(url, cookie, false);
            if (Regex.IsMatch(s, "已加入"))
            {
                isAdded = true;
            }
            return isAdded;
        }
        /// <summary>
        /// 退群
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="gid">群id</param>
        /// <param name="groupName">群名</param>
        /// <returns></returns>
        public static bool ExitGroup(CookieContainer cookie,string gid,string groupName)
        {
            string data = String.Format("gid={0}&name={1}&isadmin=&islast=0&_t=0", gid, groupName);
            string url = @"https://weibo.com/p/aj/groupchat/exitgroup?ajwvr=6&__rnd=" + GetTimeStamp();
            string s = HttpHelper.SendDataByPost(url, cookie, data);

            return s.Equals("") ? false : CheckBackCode(JsonHelper.GetBackJson(s).code);
        }
        #endregion

        #region [获取微博相关]
        /// <summary>
        /// 获取自身微博列表
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static List<string> GetNewestWeiboUrls(CookieContainer cookie, string uid)
        {
            List<string> urls = new List<string>();
            try
            {
                string url = String.Format("https://weibo.com/{0}/profile?profile_ftype=1&is_all=1#_0", uid);
                string request = DAL.HttpHelper.Get(url, cookie, false);
                string regexStr = "(&url=https)(.)*?&";
                MatchCollection matches = Regex.Matches(request, regexStr);
                foreach (Match match in matches)
                {
                    urls.Add(match.Value.Replace("&url=", "").Replace("&", "").Replace("\\", ""));
                }
            }
            catch
            {
                //TODO 
            }
            return urls;
        }
        #endregion

        #region [互动相关]
        public static List<Model.CommentWeibo> GetFriendNewestWeibo(CookieContainer cookie)
        {

            return null;     
        }

        /// <summary>
        /// 评论微博
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="mid">微博id</param>
        /// <param name="uid">评论账号uid（自己）</param>
        /// <param name="commmentUid">被评论用户uid</param>
        /// <param name="message">评论内容</param>
        /// <param name="result">评论返回数据</param>
        /// <returns></returns>
        public static bool CommentWeibo(CookieContainer cookie, string mid, string uid, string commmentUid, string message, ref string result)
        {
            string data = String.Format("act=post&mid={0}&uid={1}&forward=0&isroot=0&content={2}&location=page_100505_home&module=scommlist&group_source=&pdetail=100505{3}&_t=0", mid, uid, message, commmentUid);
            string url = "https://weibo.com/aj/v6/comment/add?ajwvr=6&__rnd=" + GetTimeStamp();
            string s = HttpHelper.SendDataByPost(url, cookie, data);

            if (!s.Equals(""))
            {
                Model.OperatingWeiboBackJson operatingWeiboBackJson = JsonHelper.GetWeiboBackJson(s);
                if (CheckBackCode(operatingWeiboBackJson.code))
                {
                    result = s;
                }

                return CheckBackCode(operatingWeiboBackJson.code);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 点赞特定微博
        /// </summary>
        /// <param name="mid">单条微博id</param>
        /// <returns></returns>
        public static bool LikeWeibo(CookieContainer cookie, string mid)
        {
            string data = String.Format("location=page_100505_home&version=mini&qid=heart&mid={0}&loc=profile&cuslike=1&floating=0&_t=0", mid);
            string url = @"https://weibo.com/aj/v6/like/add?ajwvr=6&__rnd=" + GetTimeStamp();
            string s = HttpHelper.SendDataByPost(url, cookie, data);
            return CheckBackCode(JsonHelper.GetWeiboBackJson(s).code);
        }
        #endregion 

        #region 私有方法
        /// <summary>
        /// 获取加密后的密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="servertime"></param>
        /// <param name="nonce"></param>
        /// <param name="pubkey"></param>
        /// <returns></returns>
        private static string GetSecurityPassword(string pwd, string servertime, string nonce, string pubkey)
        {
            StreamReader sr = new StreamReader("sinaSSOEncoder"); //从文本中读取修改过的JS
            string js = sr.ReadToEnd();
            //自定义function进行加密
            js += "function getpass(pwd,servicetime,nonce,rsaPubkey){var RSAKey=new sinaSSOEncoder.RSAKey();RSAKey.setPublic(rsaPubkey,'10001');var password=RSAKey.encrypt([servicetime,nonce].join('\\t')+'\\n'+pwd);return password;}";
            ScriptEngine se = new ScriptEngine(ScriptLanguage.JavaScript);
            object obj = se.Run("getpass", new object[] { pwd, servertime, nonce, pubkey }, js);
            sr.Close();
            return obj.ToString();
        }
        /// <summary>
        /// 获取昵称、uid、头像
        /// </summary>
        /// <param name="user"></param>
        private static void GetUidNickNameAndSoOn(Model.User user)
        {
            try
            {
                var userHomePageTxt = HttpHelper.Get("https://weibo.com", user.Cookies, true);

                GetFansAndFollowCount(userHomePageTxt, out string fansCount, out string followCount);
                user.FansCount = fansCount;
                user.FollowCount = followCount;

                //获取用户uid
                int indexStart = userHomePageTxt.IndexOf("$CONFIG['uid']='") + "$CONFIG['uid']='".Length;
                userHomePageTxt = userHomePageTxt.Substring(indexStart);
                user.Uid = userHomePageTxt.Substring(0, userHomePageTxt.IndexOf("';"));
                //获取昵称
                indexStart = userHomePageTxt.IndexOf("$CONFIG['nick']='") + "$CONFIG['nick']='".Length;
                userHomePageTxt = userHomePageTxt.Substring(indexStart);
                user.NickName = userHomePageTxt.Substring(0, userHomePageTxt.IndexOf("';"));
                //获取头像
                indexStart = userHomePageTxt.IndexOf("$CONFIG['avatar_large']='") + "$CONFIG['avatar_large']='".Length;
                userHomePageTxt = userHomePageTxt.Substring(indexStart);
                string url = userHomePageTxt.Substring(0, userHomePageTxt.IndexOf("';"));
                WebClient wc = new WebClient();
                user.HeaderPicture = Image.FromStream(wc.OpenRead(url));
            }
            catch (Exception ex)
            {
                //获取失败
            }
        }
        /// <summary>
        /// 从网页文本中获取关注数、粉丝数
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="fansCount">粉丝数</param>
        /// <param name="followCount">关注数</param>
        private static void GetFansAndFollowCount(string txt, out string fansCount, out string followCount)
        {
            fansCount = "";
            followCount = "";

            string regexFans = "fans\\\\\">(\\d)+?<";
            string regexFollow = "follow\\\\\">(\\d)+?<";

            MatchCollection FansMatchs = Regex.Matches(txt, regexFans);
            MatchCollection FollowMatchs = Regex.Matches(txt, regexFollow);

            foreach (Match match in FansMatchs)
            {
                fansCount = match.Value.Replace("fans\\\">","").Replace("<","");
            }

            foreach (Match match in FollowMatchs)
            {
                followCount = match.Value.Replace("follow\\\">","").Replace("<","");
            }
        }
        /// <summary>
        /// 识别微博操作返回值
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static bool CheckBackCode(string code)
        {
            
            switch (code)
            {
                case "100000":
                    return true;
                default:
                    return false;
            }    
        }

        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns></returns>
        private static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }
        #endregion
    }
}
