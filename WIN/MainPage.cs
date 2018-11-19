using CCWin;
using Microsoft.Win32;
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
using System.Configuration;

namespace WIN
{
    public partial class MainPage : Skin_Mac
    {
        public MainPage()
        {
            InitializeComponent();
            BLL.Serial.GetSerialType();
            //主页面打开之前判断更新版本
            this.UpdateVersion();
        }

        #region [界面加载]
        Timer timerSerial = new Timer() { Interval = 10 };
        private void MainPage_Load(object sender, EventArgs e)
        {
            this.timerSerial.Tick += TimerSerial_Tick;
            this.timerSerial.Enabled = true;
        }
        //判断序列号、版本有效性
        private void TimerSerial_Tick(object sender, EventArgs e)
        {
            this.timerSerial.Enabled = false;

            this.CheckVersion();
            
            this.CheckSerial();
        }

        #endregion

        #region [登录账号]
        private void buttonLogin_Click(object sender, EventArgs e)
        {


            Views.LoginView loginView = new Views.LoginView();
            loginView.ShowDialog();

            if (loginView.IsSuccess)
            {
                Model.User user = loginView.User;

                LayoutControl(user);

                this.WriteOutputMessages(new string[] { String.Format("账号【{0}】登录成功！",user.NickName),
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

            userLogin.OptionEvent += UserLogin_OptionEvent;//输出信息事件
            userLogin.ExitWeiboEvent += UserLogin_ExitWeiboEvent;//退出微博事件
            this.panelWeibo.Controls.Add(userLogin);

            //List<Model.GroupFriend> friends = BLL.Weibo.GetGroupFriendsList(user.Cookies,userLogin.User.Uid, "4300602894782087", "沧海互粉 粉评赞");
        }
        //退出账号登录状态
        private void UserLogin_ExitWeiboEvent(UserLoginControl name)
        {
            this.panelWeibo.Controls.Remove(name);

            this.panelWeibo_SizeChanged(new object(), new EventArgs());//重新布局
        }
        //切换选项卡后刷新
        private void panelWeibo_SizeChanged(object sender, EventArgs e)
        {
            //重新定位滚动条
            this.panelWeibo.VerticalScroll.Value = 0;

            int columnCount = (this.panelWeibo.Width - 20) / 533;//求列数
            int controlCount = this.panelWeibo.Controls.Count;//求已存在微博控件数

            if (columnCount <= 0)
            {
                return;
            }

            //int height = 147; //控件默认长宽
            //int width = 400;
            if (this.panelWeibo.Controls.Count == 0)
            {
                return;
            }

            int height = this.panelWeibo.Controls[0].Height;
            int width = this.panelWeibo.Controls[0].Width;

            int row = 0;
            for (int i = 0; i < this.panelWeibo.Controls.Count; i++)
            {
                int column = i % columnCount;

                this.panelWeibo.Controls[i].Location = new Point(4 + (4 + width) * column, 4 + (4 + height) * row);

                if (column == columnCount - 1)
                {
                    row++;
                }
            }
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
        #endregion

        #region [控件事件]
        /// <summary>
        /// 账号消息
        /// </summary>
        /// <param name="message"></param>
        private void UserLogin_OptionEvent(string message)
        {
            this.WriteOutputMessage(message);
        }
        #endregion

        #region [序列号]
        //购买序列号
        private void buttonBuySerial_Click(object sender, EventArgs e)
        {
            //使用系统默认浏览器打开购买页面
            System.Diagnostics.Process.Start(BLL.Serial.GetBuySerialPath());
        }
        //更新本机序列号
        private void buttonUpdateSerial_Click(object sender, EventArgs e)
        {
            Views.SerialNumberView serialNumberView = new Views.SerialNumberView();
            serialNumberView.ShowDialog();
        }
        //验证序列号
        private void CheckSerial()
        {
            if (!BLL.Serial.IsValidSerial())
            {
                this.buttonLogin.Enabled = false;
                this.skinTabControl1.SelectedTab = this.skinTabPageSerial;
                //弹出购买页面
                Views.SerialNumberView serialNumberView = new Views.SerialNumberView();
                serialNumberView.ShowDialog();
            }
            else
            {
                this.skinTabControl1.SelectedTab = this.skinTabPageFans;
            }
        }
        #endregion

        #region [版本]
        //版本更新提醒
        private void UpdateVersion()
        {
            if (!BLL.Version.CheckThisVersionIsNewest())
            {
                Views.VersionUpdateView updateView = new Views.VersionUpdateView(BLL.Version.ClientVersion);
                updateView.ShowDialog();
            }
        }
        //验证版本有效性
        private void CheckVersion()
        {
            if (!BLL.Version.IsCurrentClientValid())
            {
                Model.ClientVersion version = BLL.Version.GetNewestClientVersion();
                Views.VersionInvalidView versionView = new Views.VersionInvalidView(version.DownloadPath);
                versionView.ShowDialog();
            }
        }
        #endregion

        #region [頁面關閉]
        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            //如果存在未退出的賬號，則分別上傳群信息
            foreach (UserLoginControl control in this.panelWeibo.Controls)
            {
                control.SendGroupToServer();
            }
        }
        #endregion
    }
}
