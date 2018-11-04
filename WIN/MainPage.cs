using CCWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

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
    }
}
