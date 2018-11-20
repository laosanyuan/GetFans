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

        //获取帮助页面
        public static string HelpPath //序列号
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
