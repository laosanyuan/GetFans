using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EMailHelper
    {
        private static readonly string UserName = DAL.ConfigRW.EMailSendUserName;
        private static readonly string Password = DAL.ConfigRW.EMailSendPasswrd;
        private static readonly string SendFromName = DAL.ConfigRW.EMailTitle;
        private static readonly string Receiver1 = DAL.ConfigRW.EMailReceiver1;
        private static readonly string Receiver2 = DAL.ConfigRW.EMailReceiver2;

        /// <summary>
        /// 向用户发送邮件
        /// </summary>
        /// <param name="to">接受者</param>
        /// <param name="title">标题</param>
        /// <param name="body">内容</param>
        public static void SendEMailToUser(string to,string title,string body)
        {
            SendMail(UserName, Password, SendFromName, to, title, body);
        }

        /// <summary>
        /// 向管理员发送报告邮件
        /// </summary>
        /// <param name="title"></param>
        /// <param name="body"></param>
        public static void SendEMailToHost(string title,string body)
        {
            if (!Receiver1.Equals(""))
            {
                SendMail(UserName, Password, SendFromName, Receiver1, title, body);
            }
            if (!Receiver2.Equals(""))
            {
                SendMail(UserName, Password, SendFromName, Receiver2, title, body);
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="fromname"></param>
        /// <param name="to"></param>
        /// <param name="title"></param>
        /// <param name="body"></param>
        private static void SendMail(string username, string password, string fromname, string to, string title, string body)
        {
            try
            {
                //邮件发送类 
                MailMessage mail = new MailMessage();
                //是谁发送的邮件 
                mail.From = new MailAddress(username, fromname);
                //发送给谁 
                mail.To.Add(to);
                //标题 
                mail.Subject = title;
                //内容编码 
                mail.BodyEncoding = Encoding.Default;
                //发送优先级 
                mail.Priority = MailPriority.High;
                //邮件内容 
                mail.Body = body;
                //是否HTML形式发送 
                mail.IsBodyHtml = true;

                mail.Priority = MailPriority.High;

                //网易邮件服务器和端口 （只支持网易邮箱）
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.qq.com";
                smtp.Port = 587;

                smtp.UseDefaultCredentials = false;

                //指定发送方式 
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                //指定登录名和密码 
                smtp.Credentials = new System.Net.NetworkCredential(username, password);

                smtp.EnableSsl = true;

                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                //超时时间 
                smtp.Timeout = 30000;
                smtp.Send(mail);
                mail.Dispose();
            }
            catch (Exception exp)
            {
                //邮件发送失败
            }
        }

    }
}
