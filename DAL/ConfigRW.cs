using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static string Version //版本号
        {
            get
            {
                return ConfigurationManager.AppSettings["Version"];
            }
        }

    }
}
