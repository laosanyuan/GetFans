﻿using System;
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

            try
            {
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
        /// 向数据库中插入一条用户数据
        /// </summary>
        /// <param name="user"></param>
        public static void InsertUser(Model.OnlineUser user)
        {
            SQLiteConnection connection = DataBaseConnection();

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = connection;

                //确认是否已存在
                command.CommandText = String.Format( "SELECT COUNT(*) From users WHERE username = '{0}'",user.UserName) ;
                command.ExecuteNonQuery();
                SQLiteDataReader reader = command.ExecuteReader();
                reader.Read();
                int count = reader.GetInt32(0);
                reader.Close();

                if (count == 0)
                {
                    //添加
                    command.CommandText = String.Format("INSERT INTO users VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                        user.Number, user.UserName, user.Password, user.NickName, user.Uid, user.StartTime, user.EndTime, user.Email);
                    command.ExecuteNonQuery();
                }
                else
                {
                    //更新
                    command.CommandText = String.Format("UPDATE users SET number = '{0}',password = '{1}',nickname = '{2}',starttime = '{3}',endtime = '{4}',email = '{5}',uid = '{6}' WHERE username = '{7}'",
                        user.Number, user.Password, user.NickName, user.StartTime, user.EndTime, user.Email, user.Uid, user.UserName);
                    command.ExecuteNonQuery();

                }
            }
            connection.Close();
        }

        public static List<Model.OnlineUser> GetLoginUsers()
        {
            List<Model.OnlineUser> users = new List<Model.OnlineUser>();
            SQLiteConnection connection = DataBaseConnection();

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = connection;

                //获取end日期在现在之后的内容 , 现在是全部
                command.CommandText =String.Format("SELECT * FROM users");
                command.ExecuteNonQuery();
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Model.OnlineUser user = new Model.OnlineUser();
                    user.Number = Convert.ToInt32(reader.GetString(0));
                    user.UserName = reader.GetString(1);
                    user.Password = reader.GetString(2);
                    user.NickName = reader.GetString(3);
                    user.Uid = reader.GetString(4);
                    user.StartTime = Convert.ToDateTime(reader.GetString(5));
                    user.EndTime = Convert.ToDateTime(reader.GetString(6));
                    user.Email = reader.GetString(7);
                    users.Add(user);
                }
            }
            connection.Close();
            return users;
        }
    }
}
