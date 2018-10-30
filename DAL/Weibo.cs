﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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

        #region 关注
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

            string s = HttpHelper.SendDataByPost(url, cookie, data);

            //返回是否关注成功

            return true;
        }
        #endregion

        #region 取消关注
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

            //返回成功与否

            return true;
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
                user.HeaderPicture = Image.FromStream(wc.OpenRead("http:" + url));
            }
            catch (Exception ex)
            {
                //获取失败
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