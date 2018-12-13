using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class WinClientSQLiteHelper
    {
        /// <summary>
        /// 创建数据库文件
        /// </summary>
        public static void CreateDataBase()
        {
            DAL.WinClientSQLiteHelper.CreateDataBase();
        }
        /// <summary>
        /// 为每个用户单独创建群表
        /// </summary>
        /// <param name="uid">用户id</param>
        public static void CreateUserGroupsTable(string uid)
        {
            DAL.WinClientSQLiteHelper.CreateUserGroupsTable(uid);
        }
        /// <summary>
        /// 向群表中插入群
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="name">群名</param>
        /// <param name="gid">群id</param>
        public static void InsertGroup(string uid, string name, string gid)
        {
            DAL.WinClientSQLiteHelper.InsertGroup(uid, name, gid);
        }
        /// <summary>
        /// 退群记录到数据库
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="gid">群id</param>
        public static void ExitGroup(string uid, string gid)
        {
            DAL.WinClientSQLiteHelper.ExitGroup(uid, gid);
        }
    }
}
