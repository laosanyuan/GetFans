using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Version
    {
        /// <summary>
        /// 获取最新版版本信息
        /// </summary>
        /// <returns></returns>
        public static Model.ClientVersion GetNewestClientVersion()
        {
            return new Model.ClientVersion() { Version = "1.0.0", VersionDirection = "测试版本", DownloadPath = "下载地址" };
        }
        /// <summary>
        /// 判断当前版本是否已停用
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static bool IsCurrentClientValid(string version)
        {
            return true;
        }
    }
}
