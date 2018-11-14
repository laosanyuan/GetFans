using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServerData
    {
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
        /// 向服務器傳送一組群信息
        /// </summary>
        /// <param name="groups">群列表</param>
        public static void SendGroupToServer(List<Model.Group> groups)
        {
            //解析群列表
        }
    }
}
