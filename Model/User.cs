using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        public string UserName { get; set; } 
        public string Password { get; set; }
        public string Uid { get; set; }

        public string NickName { get; set; }//昵称
        public Image HeaderPicture { get; set; }//头像

        public CookieContainer Cookies { get; set; }//Cookies

        public LoginParameter LoginPara { get; set; }//登录参数

    }
}
