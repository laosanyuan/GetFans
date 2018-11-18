using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Version
    {
        public static Model.ClientVersion ClientVersion; //最新版本信息

        /// <summary>
        /// 获取最新版版本信息
        /// </summary>
        /// <returns></returns>
        public static Model.ClientVersion GetNewestClientVersion()
        {
            return new Model.ClientVersion() { Version = "1.0.0", VersionDirection = "测试版本", DownloadPath = "www.baidu.com" };
        }
        /// <summary>
        /// 判断当前版本是否已停用
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static bool IsCurrentClientValid()
        {
            string version = DAL.ConfigRW.Version;
            return true;
        }
        /// <summary>
        /// 判断当前版本是否为最新版本
        /// </summary>
        /// <returns></returns>
        public static bool CheckThisVersionIsNewest()
        {
            Model.ClientVersion version = GetNewestClientVersion();
            if (version.Version.Equals(DAL.ConfigRW.Version))
            {
                return true;
            }
            else
            {
                ClientVersion = version;
                return false;
            }
        }
    }
}
