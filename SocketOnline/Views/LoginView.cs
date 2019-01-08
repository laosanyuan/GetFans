using CCWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SocketOnline.Views
{
    public partial class LoginView : Skin_Mac
    {
        public bool IsSuccess { get; private set; } //是否登录成功
        public Model.User User { get; private set; } //登录信息

        public LoginView()
        {
            InitializeComponent();
        }

        #region 界面布局
        private void LoginView_Load(object sender, EventArgs e)
        {
            this.NormalLogin();

            //输入框获得焦点隐藏错误图标
            this.skinTextBoxUserName.Enter += this.HideError;
            this.skinTextBoxPassword.Enter += this.HideError;
            this.skinTextBoxCheck.Enter += this.HideError;
        }
        //无验证码登录
        private void NormalLogin()
        {
            //隐藏验证码相关控件
            this.pictureBoxCode.Visible = false;
            this.skinTextBoxCheck.Visible = false;

            //修改布局
            this.Height = 205;

               
        }
        //验证码登录
        private void CheckCodeLogin()
        {
            //隐藏验证码相关控件
            this.pictureBoxCode.Visible = true;
            this.skinTextBoxCheck.Visible = true;

            //修改布局
            this.Height = 235;
        }
        #endregion

        #region 登录条件验证
        //判定登录信息是否有效
        private bool IsValid()
        {

            if (this.skinTextBoxUserName.Text.Equals(""))
            {
                this.pictureBoxErrorUserName.Visible = true;
                return false;
            }

            if (this.skinTextBoxPassword.Text.Equals(""))
            {
                this.pictureBoxErrorPassword.Visible = true;
                return false;
            }

            if (this.pictureBoxCode.Visible && this.skinTextBoxCheck.Text.Equals(""))
            {
                this.pictureBoxErrorCheck.Visible = true;
            }

            return true;
        }
        //隐藏错误图标
        private void HideError(object sender, EventArgs e)
        {
            this.pictureBoxErrorUserName.Visible = false;
            this.pictureBoxErrorPassword.Visible = false;
            this.pictureBoxErrorCheck.Visible = false;
        }
        #endregion

        #region 按钮事件
        //登录按钮
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (!this.IsValid())
            {
                return;
            }

            string result;
            if (!this.pictureBoxCode.Visible)
            {
                this.User = BLL.Weibo.PrepareLogin(this.skinTextBoxUserName.Text, this.skinTextBoxPassword.Text);
                result = BLL.Weibo.StartLogin(this.User);
            }
            else
            {
                result = BLL.Weibo.StartLogin(this.User,this.skinTextBoxCheck.Text);
            }

            if (result.Equals("0"))
            {
                if (this.User.NickName.IndexOf('<') > -1)
                {
                    MessageBox.Show("账号被锁，请登录网页微博解锁后再登录","提示");
                    this.Close();
                }
                else
                {
                    //登录成功
                    this.IsSuccess = true;
                    this.Close();
                }
            }
            else if (result.Equals("2070") || result.Equals("4096") || result.Equals("4049"))
            {
                //验证码错误或者为空
                if (this.pictureBoxCode.Visible)
                {
                    this.pictureBoxErrorCheck.Visible = true;
                }
                else
                {
                    this.CheckCodeLogin();
                }
                this.pictureBoxCode.Image = BLL.Weibo.GetCodeImage(this.User);
            }
            else if (result.Equals("101&"))
            {
                //密码错误
                this.pictureBoxErrorUserName.Visible = true;
                this.pictureBoxErrorPassword.Visible = true;
            }
            else
            {
                MessageBox.Show("未知错误！请关闭登录保护后重试！", "提示");
            }
        }
        //获取验证码
        private void pictureBoxCode_Click(object sender, EventArgs e)
        {
            this.pictureBoxCode.Image = BLL.Weibo.GetCodeImage(this.User);
        }
        //关闭窗口
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
