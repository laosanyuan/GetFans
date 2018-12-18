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
        private static string webApiUrl = "http://47.100.249.244:8080/weibo/";

        #region [序列號]
        /// <summary>
        /// 驗證序列號有效性
        /// </summary>
        /// <param name="serial"></param>
        /// <param name="IPAdress"></param>
        /// <param name="machineName"></param>
        /// <returns></returns>
        public static Model.WebMessage IsValidSerial(string serial, string IPAdress, string machineName)
        {
            //校验序列号可用性
            string str = String.Format("ip={0}&serialNum={1}&hostName={2}", IPAdress, serial, machineName);
            string s = HttpHelper.Post(webApiUrl + "check", str);
            return JsonHelper.WebMessage(s);
        }
        /// <summary>
        /// 获取序列号有效期
        /// </summary>
        /// <param name="serial">序列号</param>
        /// <returns><returns>
        public static Model.WebMessage GetSerialInvalidDate(string serial)
        {
            //获取序列号有效期
            //string str = "serialNum=fasdfasdfasdfasd";
            string str = "serialNum=" + serial;
            string s = HttpHelper.Post(webApiUrl + "expire", str);
            return JsonHelper.WebMessage(s);
        }
        /// <summary>
        /// 獲取序列號種類
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public static Model.WebMessage GetSerialType(string serial,string ipStr ,string machineName)
        {
            //string str = "ip=192.168.34.42&serialNum=fasdfasdfasdfasds&hostName=222";
            string str = String.Format("ip={0}&serialNum={1}&hostName={2}", ipStr, serial, machineName);
            string s = HttpHelper.Post(webApiUrl + "checkVersionType", str);
            return JsonHelper.WebMessage(s);
        }
        #endregion

        #region [群信息]
        /// <summary>
        /// 从服务器获得一组群
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public static Model.WebMessage GetGroups(string serial)
        {
            string str = String.Format("serialNum={0}", serial);
            string s = HttpHelper.Post(webApiUrl + "getGroupList", str);
            return JsonHelper.WebMessage(s);
        }
        /// <summary>
        /// 向服務器保存一組群信息
        /// </summary>
        /// <param name="groups">群列表</param>
        public static void SendGroupToServer(List<Model.Group> groups)
        {
            foreach (Model.Group group in groups)
            {
                string str = String.Format("groupId={0}&groupName={1}", group.Gid, group.Name);
                string s = HttpHelper.Post(webApiUrl + "uploadGroup", str);
            }
        }
        #endregion

        #region [版本信息]
        /// <summary>
        /// 获取最新版版本信息
        /// </summary>
        /// <returns></returns>
        public static Model.WebMessage GetNewestClientVersion()
        {
            //获取最新版本
            string s = HttpHelper.Get(webApiUrl + "latestVersion");
            return JsonHelper.WebMessage(s);
        }
        /// <summary>
        /// 获取版本可用信息
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static Model.WebMessage CurrentClientValid(string version)
        {
            //检验版本可用性
            string str = "version="+version;
            string s = HttpHelper.Post(webApiUrl + "checkVersion", str);
            return JsonHelper.WebMessage(s);
        }
        #endregion

        #region [用户信息]
        /// <summary>
        /// 向服务器传入用户名和序列号
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="serialNum"></param>
        public static void SendNickNameToServer(string nickName, string serialNum)
        {
            string str = String.Format("nickName={0}&serialNum={1}", nickName, serialNum);
            string s = HttpHelper.Post(webApiUrl + "nickName", str);
        }
        #endregion
    }
}
