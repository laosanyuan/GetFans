using CCWin;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
            //主页面打开之前判断更新版本
            this.UpdateVersion();
        }

        #region [界面加载]
        Timer timerSerial = new Timer() { Interval = 10 };
        private void MainPage_Load(object sender, EventArgs e)
        {
            this.timerSerial.Tick += TimerSerial_Tick;
            this.timerSerial.Enabled = true;

            this.labelSerialTime.Text = "有效期：" + BLL.Serial.GetSerialInvalidDate();
            this.labelSerialType.Text = "序列号种类：" + BLL.Serial.GetSerialType();

            //创建数据库文件
            BLL.WinClientSQLiteHelper.CreateDataBase();
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

            int height = userLogin.Height; //控件默认长宽
            int width = userLogin.Width;

            int columnCount = (this.panelWeibo.Width - 20) / width;//求列数
            int controlCount = this.panelWeibo.Controls.Count;//求已存在微博控件数

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
                    userLogin.Location = new Point(4, (int)(this.panelWeibo.Controls[this.panelWeibo.Controls.Count - 1].Location.Y) + height +4);

                }
                else
                {
                    userLogin.Location = new Point((width + 4) * column + 4,
                        (int)(this.panelWeibo.Controls[this.panelWeibo.Controls.Count - 1].Location.Y));
                }

            }

            userLogin.OptionEvent += UserLogin_OptionEvent;//输出信息事件
            userLogin.ExitWeiboEvent += UserLogin_ExitWeiboEvent;//退出微博事件
            this.panelWeibo.Controls.Add(userLogin);
        }
        //退出账号登录状态
        private void UserLogin_ExitWeiboEvent(UserLoginControl name)
        {
            this.panelWeibo.Controls.Remove(name);

            this.panelWeibo_SizeChanged(new object(), new EventArgs());//重新布局
        }
        //窗口大小改变后刷新
        private void panelWeibo_SizeChanged(object sender, EventArgs e)
        {
            //重新定位滚动条
            this.panelWeibo.VerticalScroll.Value = 0;

            int controlCount = this.panelWeibo.Controls.Count;//求已存在微博控件数

            if (controlCount <= 0)
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
            int columnCount = (this.panelWeibo.Width - 20) / width;//求列数

            if (columnCount == 0)
            {
                return;
            }

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
            //更新panel布局
            this.panelWeibo.ResumeLayout();
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
            if (serialNumberView.IsValid)
            {
                this.RefreshWindowAfterGetSerial();
            }
        }
        //验证序列号
        private void CheckSerial()
        {
            if (!BLL.Serial.IsValidSerial())
            {
                //this.labelSeriaPoint.Visible = true;
                //this.labelSeriaPoint.Refresh();
                //this.skinTabControl1.SelectedTab = this.skinTabPageSerial;
                this.buttonLogin.Enabled = false;
                //this.skinTabControl1.SelectedTab = this.skinTabPageSerial;
                //弹出购买页面
                Views.SerialNumberView serialNumberView = new Views.SerialNumberView();
                serialNumberView.ShowDialog();
                //检验序列号有效恢复可用
                if (serialNumberView.IsValid)
                {
                    this.RefreshWindowAfterGetSerial();
                }
            }
            else
            {
                //this.skinTabControl1.SelectedTab = this.skinTabPageFans;
            }
        }
        //恢复序列号使用权限
        private void RefreshWindowAfterGetSerial()
        {
            this.labelSerialTime.Text = "有效期：" + BLL.Serial.GetSerialInvalidDate();
            this.labelSerialType.Text = "序列号种类：" + BLL.Serial.GetSerialType();

            //this.labelSeriaPoint.Visible = false;

            this.buttonLogin.Enabled = true;
        }
        #endregion

        #region [版本]
        //版本更新提醒
        private void UpdateVersion()
        {
            if (!BLL.Version.CheckThisVersionIsNewest())
            {
                Views.VersionUpdateView updateView = new Views.VersionUpdateView(BLL.Version.NewClientVersion);
                updateView.ShowDialog();
            }

            //标题栏 显示版本号
            this.Text = "小火箭互粉精灵 V" + BLL.Version.CurrentClientVersion();
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
                control.EndFollow();
                control.SendGroupToServer();
            }
        }
        #endregion

        #region [最小化到托盘]
        private void MainPage_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.notifyIcon1.Visible = true;
                this.notifyIcon1.ShowBalloonTip(5000, "已为您将小火箭切换到系统托盘显示", "单击图标可恢复显示，右键菜单可选择退出。", ToolTipIcon.Info);
            }
        }
        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Left)
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                this.notifyIcon1.Visible = false;
            }
        }

        private void 关于小火箭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(BLL.Web.HelpPath());
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
