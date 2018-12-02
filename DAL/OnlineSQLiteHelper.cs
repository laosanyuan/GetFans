using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    //在线SQLite版数据库
    public class OnlineSQLiteHelper
    {
        private static string DataBasePath = Environment.CurrentDirectory + "\\DataBase.db";

        /// <summary>
        /// 创建数据库文件
        /// </summary>
        public static void CreateDataBase()
        {
            if (!File.Exists(DataBasePath))
            {
                File.Create(DataBasePath).Close();
            }
        }

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static SQLiteConnection DataBaseConnection()
        {
            return new SQLiteConnection("data source = " + DataBasePath);
        }

        /// <summary>
        /// 创建users表
        /// </summary>
        public static void CreateUsersTable()
        {
            SQLiteConnection connection = DataBaseConnection();

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = connection;

                //判断Users table是否存在
                command.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE TYPE = 'table' AND NAME = 'users'";
                command.ExecuteNonQuery();
                SQLiteDataReader reader = command.ExecuteReader();
                reader.Read();
                int count = reader.GetInt32(0);
                reader.Close();

                if (count == 0)
                {
                    //创建users表
                    command.CommandText = "Create Table users (number varchar(200),username varchar(200),password varchar(200),nickname varchar(200),uid varchar(200),starttime varchar(200),endtime varchar(200),email varchar(200))";
                    command.ExecuteNonQuery();
                }

            }
            connection.Close();
        }
    }
}
