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
        private static readonly string DataBasePath = Environment.CurrentDirectory + "\\DataBase.db";

        private static object obj = new object();

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
            lock (obj)
            {
                string tableName = "group" + uid;
                using (SQLiteConnection connection = DataBaseConnection())
                {
                    try
                    {
                        if (connection.State != System.Data.ConnectionState.Open)
                        {
                            connection.Open();
                            using (SQLiteCommand command = new SQLiteCommand())
                            {
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
                                    command.CommandText = String.Format("CREATE TABLE {0} (name varchar(200),gid varchar(200),enterTime varchar(200),exitTime varchar(200),isAdded varchar(200))", tableName);
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                    connection.Close();
                }
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
            lock (obj)
            {
                string tableName = "group" + uid;

                using (SQLiteConnection connection = DataBaseConnection())
                {
                    try
                    {

                        if (connection.State != System.Data.ConnectionState.Open)
                        {
                            connection.Open();
                            using (SQLiteCommand command = new SQLiteCommand())
                            {
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
                                    command.CommandText = String.Format("UPDATE {0} SET isAdded = 'true' WHERE gid = '{1}'", tableName, gid);
                                    command.ExecuteNonQuery();
                                }
                            }
                        }

                    }
                    catch
                    {

                    }
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 退群
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="gid">群id</param>
        public static void ExitGroup(string uid, string gid)
        {
            lock (obj)
            {
                string tableName = "group" + uid;

                using (SQLiteConnection connection = DataBaseConnection())
                {

                    try
                    {
                        if (connection.State != System.Data.ConnectionState.Open)
                        {
                            connection.Open();
                            using (SQLiteCommand command = new SQLiteCommand())
                            {
                                command.Connection = connection;

                                command.CommandText = String.Format("UPDATE {0} SET  isAdded = 'false',exitTime = '{1}' WHERE gid = '{2}'", tableName, DateTime.Now.ToString(), gid);
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                    catch
                    {
                    }
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 获取加群时间
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static DateTime GetGroupEnterTime(string uid,string gid)
        {
            lock (obj)
            {
                string tableName = "group" + uid;

                DateTime dt = new DateTime();

                using (SQLiteConnection connection = DataBaseConnection())
                {
                    try
                    {
                        if (connection.State != System.Data.ConnectionState.Open)
                        {
                            connection.Open();
                            using (SQLiteCommand command = new SQLiteCommand())
                            {
                                command.Connection = connection;

                                command.CommandText = String.Format("SELECT enterTime FROM {0} WHERE gid = '{1}'", tableName, gid);
                                command.ExecuteNonQuery();
                                SQLiteDataReader reader = command.ExecuteReader();
                                while (reader.Read())
                                {
                                    dt = Convert.ToDateTime(reader.GetString(0));
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                    connection.Close();
                }
                return dt;
            }
        }
        /// <summary>
        /// 获取退群时间
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static DateTime GetGroupExitTime(string uid, string gid)
        {
            lock (obj)
            {
                string tableName = "group" + uid;

                DateTime dt = new DateTime();

                using (SQLiteConnection connection = DataBaseConnection())
                {
                    try
                    {
                        if (connection.State != System.Data.ConnectionState.Open)
                        {
                            connection.Open();
                            using (SQLiteCommand command = new SQLiteCommand())
                            {
                                command.Connection = connection;

                                command.CommandText = String.Format("SELECT exitTime FROM {0} WHERE gid = '{1}'", tableName, gid);
                                command.ExecuteNonQuery();
                                SQLiteDataReader reader = command.ExecuteReader();
                                while (reader.Read())
                                {
                                    dt = Convert.ToDateTime(reader.GetString(0));
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                    connection.Close();
                }
                return dt;
            }
        }
        /// <summary>
        /// 判断群是否曾经加入并退出过
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static bool IsGroupAddedBefore(string uid, string gid)
        {
            lock (obj)
            {
                string tableName = "group" + uid;
                bool isExist = false;
                using (SQLiteConnection connection = DataBaseConnection())
                {
                    try
                    {
                        if (connection.State != System.Data.ConnectionState.Open)
                        {
                            connection.Open();
                            using (SQLiteCommand command = new SQLiteCommand())
                            {
                                command.Connection = connection;

                                //判断Users table是否存在
                                command.CommandText = String.Format("SELECT COUNT(*) FROM {0} WHERE gid = '{1}'", tableName, gid);
                                command.ExecuteNonQuery();
                                SQLiteDataReader reader = command.ExecuteReader();
                                reader.Read();
                                int count = reader.GetInt32(0);
                                reader.Close();

                                if (count == 0)
                                {
                                    isExist = false;
                                }
                                else
                                {
                                    isExist = true;
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                    connection.Close();
                }
                return isExist;
            }
        }
    }  
}
