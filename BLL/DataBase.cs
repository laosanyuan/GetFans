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
    }
}
