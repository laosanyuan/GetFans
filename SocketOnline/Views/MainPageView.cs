using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;

namespace SocketOnline.Views
{
    public partial class MainPageView : Form
    {
        public MainPageView()
        {
            InitializeComponent();
        }

        #region [界面加载]
        private void MainPageView_Load(object sender, EventArgs e)
        {
            //数据库账号登录线程
            Thread getDBUser = new Thread(new ThreadStart(GetStartUserList));
            getDBUser.IsBackground = true;
            getDBUser.Start();
            //账号在线获取、登录线程
            Thread getUsersthread = new Thread(new ThreadStart(UpdateUserList));
            getUsersthread.IsBackground = true;
            getUsersthread.Start();
        }
        #endregion

        #region [登录账号]
        //从网页获取登陆用户线程
        private void UpdateUserList()
        {
            while (true)
            {
                List<Model.OnlineUser> users = BLL.OnlineUserHelper.GetOnlineUserList();
                //登录
                LoginUser(users);
                Thread.Sleep(600000); //十分钟间隔
            }
        }
        //从数据库获取登录用户线程
        private void GetStartUserList()
        {
            List<Model.OnlineUser> onlineUsers = BLL.DataBase.GetLoginUsers();
            LoginUser(onlineUsers);
        }
        //登录账号
        object lockObject = new object();
        private void LoginUser(List<Model.OnlineUser> users)
        {
            lock (lockObject)
            {
                if (users.Count != 0)
                {
                    //登陆并更新cookie
                    foreach (Model.OnlineUser user in users)
                    {
                        Model.User loginUser = BLL.Weibo.PrepareLogin(user.UserName, user.Password);
                        string result = BLL.Weibo.StartLogin(loginUser);
                        switch (result)
                        {
                            case "0":
                                if (loginUser.NickName.IndexOf('<') > -1)
                                {
                                    //账号被锁
                                    ResolveLoginErr(user.UserName + " 账号被锁");
                                }
                                else
                                {
                                    //登陆成功
                                    user.NickName = loginUser.NickName;
                                    user.Uid = loginUser.Uid;
                                    user.Cookies = loginUser.Cookies;

                                    user.LoginTime = DateTime.Now;

                                    if (Program.OnlineUsers.Count == 0)
                                    {
                                        user.Number = 1;
                                    }
                                    else
                                    {
                                        user.Number = Program.OnlineUsers.Last().Number + 1;
                                    }

                                    Program.OnlineUsers.Add(user);
                                    //插入数据库
                                    BLL.DataBase.InsertUser(user);

                                    //更新显示列表
                                    this.BeginInvoke(new UpdateListDelegate(UpdateListFunction));
                                }
                                break;
                            case "2070":
                            case "4096":
                            case "4049":
                                //验证码问题
                                for (int i = 0; i < 5; i++)
                                {
                                    Image checkCodeImage = BLL.Weibo.GetCodeImage(loginUser);
                                    string checkStr = BLL.Weibo.DecodeCheckCode(checkCodeImage, out int resultId);

                                    if (checkStr.Equals("")) //解码失败
                                    {
                                        continue;
                                    }

                                    //验证码登录
                                    result = BLL.Weibo.StartLogin(loginUser, checkStr);
                                    if (result.Equals("0"))
                                    {
                                        user.NickName = loginUser.NickName;
                                        user.Uid = loginUser.Uid;
                                        user.Cookies = loginUser.Cookies;

                                        user.LoginTime = DateTime.Now;

                                        if (Program.OnlineUsers.Count == 0)
                                        {
                                            user.Number = 1;
                                        }
                                        else
                                        {
                                            user.Number = Program.OnlineUsers.Last().Number + 1;
                                        }

                                        Program.OnlineUsers.Add(user);
                                        //插入数据库
                                        BLL.DataBase.InsertUser(user);
                                        //更新显示列表
                                        this.BeginInvoke(new UpdateListDelegate(UpdateListFunction));

                                        break;
                                    }
                                    else
                                    {
                                        //解码错误回传，暂留
                                    }
                                }
                                if (String.IsNullOrEmpty(user.NickName))
                                {
                                    ResolveLoginErr(user.UserName + " 五次解码全部失败,登录失败！");
                                }
                                break;
                            case "101&":
                                //账号密码错误
                                ResolveLoginErr(user.UserName + " 账号或者密码错误");
                                break;
                            default:
                                //未知错误
                                ResolveLoginErr(user.UserName + " 登录时发生未知错误");
                                break;
                        }
                    }
                }
            }
        }
        //处理登录失败问题
        static void ResolveLoginErr(string message)
        {

        }
        #endregion

        #region [更新界面显示]
        private delegate void UpdateListDelegate();
        private void UpdateListFunction()
        {
            this.dataGridView1.Rows.Clear();

            foreach (OnlineUser user in Program.OnlineUsers)
            {
                this.dataGridView1.Rows.Add(user.Number.ToString(),
                    user.UserName,
                    user.NickName,
                    user.StartTime.ToString(),
                    user.EndTime.ToString(),
                    user.Email);

                this.UpdateOutputMessage(String.Format("账号[{0}]登陆成功！", user.NickName));
            }
        }

        //更新输出提示
        private void UpdateOutputMessage(string message)
        {
            this.UpdateOutputMessage(new string[] { message });
        }
        private void UpdateOutputMessage(string[] messages)
        {
            string timeStr = DateTime.Now.ToString();
            this.richTextBox1.Text += timeStr + ":" + System.Environment.NewLine;

            foreach (string str in messages)
            {
                this.richTextBox1.Text += str + Environment.NewLine;
            }
        }

        #endregion
    }
}
