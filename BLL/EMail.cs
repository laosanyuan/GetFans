using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EMail
    {
        /// <summary>
        /// 向用户发送邮件
        /// </summary>
        /// <param name="to"></param>
        /// <param name="title"></param>
        /// <param name="body"></param>
        public static void SendEMailToUser(string to, string title, string body)
        {
            DAL.EMailHelper.SendEMailToUser(to, title, body);
        }

        /// <summary>
        /// 向管理者发送邮件
        /// </summary>
        /// <param name="title"></param>
        /// <param name="body"></param>
        public static void SendEMailToHost(string title, string body)
        {
            DAL.EMailHelper.SendEMailToHost(title, body);
        }
    }
}
