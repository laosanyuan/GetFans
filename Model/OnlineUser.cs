using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    //在线小火箭账号
    public class OnlineUser
    {
        //登录前获取
        public string UserName { get; set; } //账号
        public string Password { get; set; } //密码
        public DateTime StartTime { get; set; } //开始时间
        public DateTime EndTime { get; set; } //结束时间
        public string Email { get; set; } // 收信邮箱

        //登录后设定
        public int Number { get; set; } //序号
        public DateTime LoginTime { get; set; } //登陆时间

        //登录后获取
        public CookieContainer Cookies { get; set; } //登陆cookie
        public string NickName { get; set; } //昵称  
        public string Uid { get; set; } //uid

    }
}
