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
        public static List<Model.Group> GetGroups()
        {
            string serial = DAL.ConfigRW.Serial;
            Model.WebMessage webMessage = DAL.WebAPI.GetGroups(serial);
            List<Model.Group> groups = new List<Model.Group>();
            if (webMessage.c.Equals("200"))
            {
                foreach (object g in (object[])webMessage.d)
                {
                    Model.Group group = new Model.Group();
                    group.Name = ((Dictionary<string, object>)g)["groupName"].ToString();
                    group.Gid = ((Dictionary<string, object>)g)["groupId"].ToString();
                    groups.Add(group);
                }
            }
            return groups;
        }
        /// <summary>
        /// 向服務器傳送一組群信息
        /// </summary>
        /// <param name="groups">群列表</param>
        public static void SendGroupToServer(List<Model.Group> groups)
        {
            DAL.WebAPI.SendGroupToServer(groups);
        }
    }
}
