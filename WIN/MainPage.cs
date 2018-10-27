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

namespace WIN
{
    public partial class MainPage : Skin_Mac
    {
        public MainPage()
        {
            InitializeComponent();
        }
        Model.User user;
        private void skinButton1_Click(object sender, EventArgs e)
        {
            user = BLL.Weibo.PrepareLogin(this.skinTextBox1.Text, this.skinTextBox2.Text);
            this.skinPictureBox1.Image = BLL.Weibo.GetCodeImage(user);
            
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            BLL.Weibo.StartLogin(user, this.skinTextBox3.Text);
        }
    }
}
