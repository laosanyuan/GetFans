using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DataBase
    {
        /// <summary>
        /// 初始化数据库
        /// </summary>
        public static void InitDataBase()
        {
            //创建数据库文件
            DAL.OnlineSQLiteHelper.CreateDataBase();
            //创建user表
            DAL.OnlineSQLiteHelper.CreateUsersTable();
        }

        /// <summary>
        /// 向数据库中插入一条用户信息
        /// </summary>
        /// <param name="user"></param>
        public static void InsertUser(Model.OnlineUser user)
        {
            DAL.OnlineSQLiteHelper.InsertUser(user);
        }

        public static List<Model.OnlineUser> GetLoginUsers()
        {
            return DAL.OnlineSQLiteHelper.GetLoginUsers();
        }
    }
}
