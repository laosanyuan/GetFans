using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    //桌面版SQLite数据库
    public class WinClientSQLiteHelper
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
        /// 为每个用户单独创建群表
        /// </summary>
        /// <param name="uid">用户id</param>
        public static void CreateUserGroupsTable(string uid)
        {
            string tableName = "group" + uid;

            SQLiteConnection connection = DataBaseConnection();

            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand();
                    command.Connection = connection;

                    //判断Users table是否存在
                    command.CommandText = String.Format("SELECT COUNT(*) FROM sqlite_master WHERE TYPE = 'table' AND NAME = '{0}'", tableName);
                    command.ExecuteNonQuery();
                    SQLiteDataReader reader = command.ExecuteReader();
                    reader.Read();
                    int count = reader.GetInt32(0);
                    reader.Close();

                    if (count == 0)
                    {
                        //创建users表
                        command.CommandText = String.Format("CREATE TABLE {0} (name varchar(200),gid varchar(200),enterTime varchar(200),exitTime varchar(200),isAdded varchar(200))",tableName);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// 向群表中插入群
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="name">群名</param>
        /// <param name="gid">群id</param>
        public static void InsertGroup(string uid, string name, string gid)
        {
            string tableName = "group" + uid;

            SQLiteConnection connection = DataBaseConnection();

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = connection;

                //确认是否已存在
                command.CommandText = String.Format("SELECT COUNT(*) From {0} WHERE gid = '{1}'", tableName, gid);
                command.ExecuteNonQuery();
                SQLiteDataReader reader = command.ExecuteReader();
                reader.Read();
                int count = reader.GetInt32(0);
                reader.Close();

                if (count == 0)
                {
                    //添加
                    command.CommandText = String.Format("INSERT INTO {0} (name,gid,enterTime,isAdded) VALUES ('{1}','{2}','{3}','{4}')", tableName, name, gid, DateTime.Now.ToString(), "true");
                    command.ExecuteNonQuery();
                }
                else
                {
                    //更新
                    command.CommandText = String.Format("UPDATE {0} SET isAdded 'true' WHERE gid = '{1}'", tableName, gid);
                    command.ExecuteNonQuery();
                }
            }
            connection.Close();
        }
    }
}
