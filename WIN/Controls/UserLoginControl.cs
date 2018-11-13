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

        List<Model.Group> Groups = new List<Model.Group>();//互粉群列表

        Timer GroupTimer = new Timer() { Interval = 120000 };//群聊定时，2分钟

        public UserLoginControl(Model.User user)
        {
            InitializeComponent();
            this.User = user;
            this.FirstDisplay();
        }

        #region [界面加载]
        private void UserLoginControl_Load(object sender, EventArgs e)
        {
            //加群

            //获取已有群
            Groups = BLL.Weibo.GetGroups(this.User.Cookies);

            GroupTimer.Tick += GroupTimer_Tick;
        }


        #endregion

        #region [显示内容]
        /// <summary>
        /// 首次登录时更新
        /// </summary>
        private void FirstDisplay()
        {
            this.skinGroupBox1.Text = User.NickName;
            this.labelLoginFansCount.Text = User.FansCount;
            this.labelNowFansCount.Text = User.FansCount;
            this.pictureBox1.Image = this.User.HeaderPicture;
        }

        private void UpdateDisplay()
        {
            BLL.Weibo.UpdateUsersFansCount(this.User);
            this.labelNowFansCount.Text = this.User.FansCount;
            this.labelSuccessCount.Text = (Convert.ToInt32(this.User.FansCount) - Convert.ToInt32(this.labelLoginFansCount.Text)).ToString();
        }
        #endregion

        #region [互粉]
        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (this.buttonStart.Text.Equals("开始"))
            {
                this.buttonStart.Text = "停止";
                this.labelBeginTime.Text = DateTime.Now.ToShortTimeString();

                this.GroupTimer.Enabled = true;

            }
            else
            {
                this.buttonStart.Text = "开始";
                this.GroupTimer.Enabled = false;

                this.OptionEvent(String.Format("互粉结束，本次共互粉【{0}】个好友", this.labelSuccessCount.Text));
            }
        }
        #endregion

        #region [群聊]
        int GroupCount = 0;
        private void GroupTimer_Tick(object sender, EventArgs e)
        {
            GroupCount++;

            if (GroupCount == 5)
            {
                GroupCount = 0;
            }

            if (GroupCount == 1)
            {
                foreach (Model.Group group in this.Groups)
                {
                    BLL.Weibo.SendMessage2Group(this.User.Cookies, group.Gid, "互粉秒回！永不取消！");
                }
            }

            List<Model.Fan> UnfollowFans = BLL.Weibo.GetUnfollowFansList(this.User.Cookies, this.User.Uid);
            foreach (Model.Fan fan in UnfollowFans)
            {
                if (!BLL.Weibo.Follow(fan.Uid, fan.NickName, this.User.Cookies))
                {
                    //关注不成功则停止
                    this.buttonStart_Click(sender, e);
                }
            }

            this.UpdateDisplay();
        }
        #endregion

        #region [委托]
        //向主界面发送显示消息
        public delegate void OptionMessageHandler(string message);
        public event OptionMessageHandler OptionEvent;
        //退出登录事件
        public delegate void ExitWeiboHandler(UserLoginControl name);
        public event ExitWeiboHandler ExitWeiboEvent;
        #endregion

        private void buttonExit_Click(object sender, EventArgs e)
        {
            //退出登录事件
            this.ExitWeiboEvent(this);
        }
    }
}
