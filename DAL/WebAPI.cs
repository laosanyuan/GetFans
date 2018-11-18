using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    
    //網站接口
    public class WebAPI
    {
        //private static string webApiUrl = "http://192.168.62.58:8080/";
        private static string webApiUrl = "http://47.100.249.244:8080/";

        #region [序列號]
        /// <summary>
        /// 驗證序列號有效性
        /// </summary>
        /// <param name="serial"></param>
        /// <param name="IPAdress"></param>
        /// <param name="machineName"></param>
        /// <returns></returns>
        public static bool IsValidSerial(string serial, string IPAdress, string machineName)
        {
            //获取最新版本
            //string s = HttpHelper.Get( webApiUrl + "weibo/latestVersion");

            //上传群号
            //string str = "groupId=1234&groupName=we3张3";
            //string s = HttpHelper.Post(webApiUrl + "weibo/uploadGroup", str);

            //获取群号
            //string str = "serialNum=fasdfasdfasdfasd";
            //string s = HttpHelper.Post(webApiUrl + "weibo/getGroupList", str);

            //校验可用性
            //string str = "ip=192.168.34.42&serialNum=fasdfasdfasdfasd&hostName=互粉";
            //string s = HttpHelper.Post(webApiUrl + "weibo/check", str);

            //检验版本可用性
            //string str = "version=1.0";
            //string s = HttpHelper.Post(webApiUrl + "weibo/checkVersion", str);

            //获取序列号有效期
            //string str = "serialNum=fasdfasdfasdfasd";
            //string s = HttpHelper.Post(webApiUrl + "weibo/expire", str);

            //序列号种类
            //string str = "ip=192.168.34.42&serialNum=fasdfasdfasdfasds&hostName=222";
            //string s = HttpHelper.Post(webApiUrl + "weibo/checkVersionType", str);

            return true;
        }
        /// <summary>
        /// 获取序列号有效期
        /// </summary>
        /// <param name="serial">序列号</param>
        /// <returns></returns>
        public static string GetSerialInvalidDate(string serial)
        {
            return "";
        }
        /// <summary>
        /// 獲取序列號種類
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public static Model.SerialType GetSerialType(string serial)
        {
            return Model.SerialType.FreeSerial;
        }
        #endregion

        #region [群信息]
        /// <summary>
        /// 从服务器获得一组群
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public static List<Model.Group> GetGroups(string serial)
        {
            return new List<Model.Group>();
        }
        /// <summary>
        /// 向服務器保存一組群信息
        /// </summary>
        /// <param name="groups">群列表</param>
        public static void SendGroupToServer(List<Model.Group> groups)
        {
            //解析群列表
        }
        #endregion

        #region [版本信息]
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
        #endregion
    }
}
