using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WIN.Controls
{
    public partial class UserLoginControl : UserControl
    {
        public Model.User User { get; private set; }

        public UserLoginControl(Model.User user)
        {
            InitializeComponent();
            this.User = user;
            this.FirstDisplay();
        }

        #region [显示内容]
        /// <summary>
        /// 首次登录时更新
        /// </summary>
        private void FirstDisplay()
        {
            this.labelLoginFansCount.Text = User.FansCount;
            this.labelNowFansCount.Text = User.FansCount;
            this.labelBeginTime.Text = DateTime.Now.ToShortTimeString();
            this.pictureBox1.Image = this.User.HeaderPicture;
        }

        private void UpdateDisplay()
        {

        }

        #endregion
    }
}
