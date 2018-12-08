using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DAL
{
    public class ConfigRW
    {
        #region [序列号]
        public static string Serial //序列号
        {
            get
            {
                return ConfigurationManager.AppSettings["Serial"];
            }
        }

        public static string BuySerialPath //购买序列号路径
        {
            get
            {
                return ConfigurationManager.AppSettings["BuySerialPath"];
            }
        }
        #endregion

        #region [云打码]
        public static string YunDaMaUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["YunDaMaUserName"];
            }
        }
        public static string YunDaMaPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["YunDaMaPassword"];
            }
        }
        #endregion

        #region [邮件]
        public static string EMailSendUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["EMailSendUserName"];
            }
        }
        public static string EMailSendPasswrd
        {
            get
            {
                return ConfigurationManager.AppSettings["EMailSendPassword"];
            }
        }
        public static string EMailTitle
        {
            get
            {
                return ConfigurationManager.AppSettings["EMailTitle"];
            }
        }
        public static string EMailReceiver1
        {
            get
            {
                return ConfigurationManager.AppSettings["EMailReceiver1"];
            }
        }
        public static string EMailReceiver2
        {
            get
            {
                return ConfigurationManager.AppSettings["EMailReceiver2"];
            }
        }
        #endregion

        public static string HelpPath //帮助页面路径
        {
            get
            {
                return ConfigurationManager.AppSettings["HelpPath"];
            }
        }

        public static string Version //版本号
        {
            get
            {
                var versionInfo = FileVersionInfo.GetVersionInfo("小火箭互粉精灵.exe");
                return versionInfo.ProductVersion;
            }
        }



    }
}
