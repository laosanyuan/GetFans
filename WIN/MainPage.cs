using CCWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIN.Controls;

namespace WIN
{
    public partial class MainPage : Skin_Mac
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Views.LoginView loginView = new Views.LoginView();
            loginView.ShowDialog();
            Model.User user = loginView.User;

            BLL.Weibo.SendMessage2Group(user.Cookies, "4297236412779327", "@搞笑大掰嗑 互粉");
            //List<Model.Fan> Fans = BLL.Weibo.GetUnfollowFansList(user.Cookies, user.Uid);
            //foreach (Model.Fan fan in Fans)
            //{
            //    BLL.Weibo.Follow(fan.Uid, fan.NickName, user.Cookies);
            //}

            //BLL.Weibo.AddGroup(user.Cookies, "4296821419377098", "千人微博 互 粉互 动群");
        }

        #region [登录账号]
        private void buttonLogin_Click(object sender, EventArgs e)
        {


            Views.LoginView loginView = new Views.LoginView();
            loginView.ShowDialog();

            if (loginView.IsSuccess)
            {
                Model.User user = loginView.User;

                LayoutControl(user);

                this.WriteOutputMessages(new string[] { String.Format("账号【{0}】登陆成功！",user.NickName),
                    String.Format("当前关注数：{0}",user.FollowCount),
                String.Format("当前粉丝数：{0}",user.FansCount)});
            }
            else
            {
                this.WriteOutputMessage("取消登录");
                return;
            }
        }
        #endregion

        #region [页面布局]
        /// <summary>
        /// 根据页面情况对登录后控件进行布局
        /// </summary>
        private void LayoutControl(Model.User user)
        {
            UserLoginControl userLogin = new UserLoginControl(user);

            int columnCount = (this.panelWeibo.Width - 20) / 533;//求列数
            int controlCount = this.panelWeibo.Controls.Count;//求已存在微博控件数

            int height = 147; //控件默认长宽
            int width = 400;


            if (columnCount < 1)
            {
                columnCount = 1;
            }

            int row = controlCount / columnCount;
            int column = controlCount % columnCount;

            if (row == 0)
            {
                userLogin.Location = new Point((width + 4) * column + 4, 4);
            }
            else
            {
                if (column == 0)
                {
                    userLogin.Location = new Point(4, (int)(this.panelWeibo.Controls[this.panelWeibo.Controls.Count - 1].Location.Y / 1.165) + height +4);

                }
                else
                {
                    userLogin.Location = new Point((width + 4) * column + 4,
                        (int)(this.panelWeibo.Controls[this.panelWeibo.Controls.Count - 1].Location.Y/1.165));
                }

            }

            userLogin.OptionEvent += UserLogin_OptionEvent;
            this.panelWeibo.Controls.Add(userLogin);
        }
        #endregion

        #region [输出信息]
        /// <summary>
        /// 向状态窗口输出一组信息
        /// </summary>
        /// <param name="messages"></param>
        private void WriteOutputMessages(string[] messages)
        {
            string timeStr = DateTime.Now.ToString();

            this.richTextBoxOutput.Text += timeStr + ":" + System.Environment.NewLine;

            foreach (string str in messages)
            {
                this.richTextBoxOutput.Text += str + Environment.NewLine;
            }
        }
        /// <summary>
        /// 向状态窗口输出一条信息
        /// </summary>
        /// <param name="message"></param>
        private void WriteOutputMessage(string message)
        {
            this.WriteOutputMessages(new string[] { message });
        }
        /// <summary>
        /// 账号消息
        /// </summary>
        /// <param name="message"></param>
        private void UserLogin_OptionEvent(string message)
        {
            this.WriteOutputMessage(message);
        }
        #endregion
    }
}
