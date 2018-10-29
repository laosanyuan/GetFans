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

namespace WIN.Views
{
    public partial class LoginView : Skin_Mac
    {
        public LoginView()
        {
            InitializeComponent();
        }

        #region 界面布局
        private void LoginView_Load(object sender, EventArgs e)
        {
            this.NormalLogin();
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
        #endregion

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            this.CheckCodeLogin();
        }
    }
}
