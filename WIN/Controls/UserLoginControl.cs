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

        Timer FollowTimer = new Timer() { Interval = 120000 };//群聊定时(被动互粉)，2分钟
        Timer DrivingFollowTimer = new Timer() { Interval = 600000 }; //主动互粉定时器
        Timer UpdateGroupTimer = new Timer() { Interval = 10 };//更新群 定时器

        public UserLoginControl(Model.User user)
        {
            InitializeComponent();
            this.User = user;
            this.FirstDisplay();
        }

        #region [界面加载]
        private void UserLoginControl_Load(object sender, EventArgs e)
        {
            //被动互粉定时器
            FollowTimer.Tick += GroupTimer_Tick;
            //更新群定时器
            UpdateGroupTimer.Tick += UpdateGropuTimer_Tick;
            this.UpdateGroupTimer.Enabled = true;
            //主动互粉定时器
            DrivingFollowTimer.Tick += DrivingFollowTimer_Tick;
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
            this.labelGrouCount.Text = this.Groups.Count.ToString();
        }
        #endregion

        #region [群列表更新]
        //更新群列表定时器
        private void UpdateGropuTimer_Tick(object sender, EventArgs e)
        {
            this.UpdateGroupTimer.Interval = 600000;//定时十分钟
            this.AddGroups();
            this.UpdateGroupList();
        }
        //加群
        private void AddGroups()
        {
            List<Model.Group> groups = BLL.ServerData.GetGroups();
            foreach (Model.Group group in groups)
            {
                //判断是否已加入
                if (!BLL.Weibo.IsAddedThisGroup(this.User.Cookies, group.Gid))
                {
                    BLL.Weibo.AddGroup(this.User.Cookies, group.Gid, group.Name);
                }
            }
        }
        //更新群列表
        private void UpdateGroupList()
        {
            List<Model.Group> groups = BLL.Weibo.GetGroups(this.User.Cookies);
            //假如总列表
            foreach (Model.Group group in groups)
            {
                if(this.Groups.FindIndex(t => t.Gid.Equals(group.Gid)) >= 0)
                {
                    continue;
                }
                this.Groups.Add(group);
            }
        }
        #endregion

        #region [互粉]
        //开始互粉
        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (this.buttonStart.Text.Equals("开始"))
            {
                this.buttonStart.Text = "停止";
                this.labelBeginTime.Text = DateTime.Now.ToShortTimeString();

                this.FollowTimer.Enabled = true;
                this.DrivingFollowTimer.Enabled = true;

            }
            else
            {
                this.buttonStart.Text = "开始";
                this.FollowTimer.Enabled = false;
                this.DrivingFollowTimer.Enabled = false;
                this.UpdateGroupTimer.Enabled = false;

                this.OptionEvent(String.Format("互粉结束，本次共互粉【{0}】个好友", this.labelSuccessCount.Text));
            }
        }
        #endregion

        #region [主动互粉]
        private List<Model.GroupFriend> WaitFriendFollowMeList = new List<Model.GroupFriend>();//等待对方回粉列表
        private int GroupStartFollowIndex = 0;//下次开始主动关注群索引
        private void DrivingFollowTimer_Tick(object sender, EventArgs e)
        {
            //修改定时时长,与群数量相关
            if (this.GroupCount <= 10)
            {
                this.DrivingFollowTimer.Interval = 600000;//十分钟
            }
            else
            {
                this.DrivingFollowTimer.Interval = 60000 * this.GroupCount;
            }
            //获取聊天群信息
            List<Model.Group> groups20 = this.Get20Groups();
            List<Model.GroupFriend> allFriendsList = new List<Model.GroupFriend>();//20个群获得的所有好友
            foreach (Model.Group group in groups20)
            {
                List<Model.GroupFriend> groupFriends = BLL.Weibo.GetGroupFriendsList(this.User.Cookies, this.User.Uid, group.Gid, group.Name);
                allFriendsList.AddRange(groupFriends);
            }
            //清洗已关注好友
            foreach (Model.GroupFriend friend in allFriendsList)
            {
                if (BLL.Weibo.GetFriendFollowStatus(this.User.Cookies, friend.Fan.Uid) == Model.FriendStatus.FollowEachOther ||
                    BLL.Weibo.GetFriendFollowStatus(this.User.Cookies, friend.Fan.Uid) == Model.FriendStatus.OnlyFollowHe)
                {
                    //已关注对方，移出列表
                    allFriendsList.Remove(friend);
                }
                else
                {
                    //开始关注
                    if (BLL.Weibo.Follow(friend.Fan.Uid, friend.Fan.NickName, this.User.Cookies))
                    {
                        friend.FollowTime = DateTime.Now;
                    }
                    else
                    {
                        //关注失败。则认为到达关注上限
                        this.StopFollowString();
                        this.buttonExit_Click(sender, e);
                    }
                }
            }
            this.WaitFriendFollowMeList.AddRange(allFriendsList);
        }
        //获取最多20个聊天群信息
        private List<Model.Group> Get20Groups()
        {
            //如果当前群聊数大于20，则只取最后20个群活跃成员
            List<Model.Group> groups20;
            if (this.Groups.Count >= 20)
            {
                if (this.GroupStartFollowIndex + 20 > this.Groups.Count)
                {
                    groups20 = this.Groups.GetRange(this.GroupStartFollowIndex, this.Groups.Count - this.GroupStartFollowIndex);
                    this.GroupStartFollowIndex += 20;
                    this.GroupStartFollowIndex -= this.Groups.Count;
                    groups20.AddRange(this.Groups.GetRange(0, this.GroupStartFollowIndex - 1));
                }
                else
                {
                    groups20 = this.Groups.GetRange(this.GroupStartFollowIndex, 20);
                    this.GroupStartFollowIndex += 20;
                }
            }
            else
            {
                groups20 = this.Groups;
            }
            return groups20;
        }
        #endregion

        #region [群聊/被动互粉]
        int GroupCount = 0;
        private void GroupTimer_Tick(object sender, EventArgs e)
        {
            GroupCount++;

            if (GroupCount == 5)
            {
                GroupCount = 0;
            }
            //发送求粉消息 10分钟
            if (GroupCount == 1)
            {
                foreach (Model.Group group in this.Groups)
                {
                    BLL.Weibo.SendMessage2Group(this.User.Cookies, group.Gid, "互粉秒回！永不取消！");
                }
            }
            else if (GroupCount == 2 && this.WaitFriendFollowMeList.Count != 0) //提醒对方好友
            {
                //如果对方超过三分钟未回粉，发送提醒消息
                foreach (Model.GroupFriend friend in this.WaitFriendFollowMeList)
                {
                    TimeSpan timeSpan = DateTime.Now - friend.FollowTime;
                    if (timeSpan.Minutes > 3)
                    {
                        string message = String.Format("@{0} 请记得回粉哟！<br><br> 此消息由@{1} 免费发布", friend.Fan.NickName, "小火箭互粉精灵");
                        BLL.Weibo.SendMessage2Group(this.User.Cookies, friend.Gid, message);
                    }
                }
            }
            //回粉好友
            List<Model.Fan> UnfollowFans = BLL.Weibo.GetUnfollowFansList(this.User.Cookies, this.User.Uid);
            foreach (Model.Fan fan in UnfollowFans)
            {
                if (!BLL.Weibo.Follow(fan.Uid, fan.NickName, this.User.Cookies))
                {
                    //关注不成功则停止
                    this.StopFollowString();
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

        #region [退出登录操作]
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.SendGroupToServer();
            //退出登录事件
            this.ExitWeiboEvent(this);
        }
        /// <summary>
        /// 向服務器存儲一組群信息
        /// </summary>
        public void SendGroupToServer()
        {
            BLL.ServerData.SendGroupToServer(this.Groups);
        }
        /// <summary>
        /// 向主窗口发送停止互粉消息
        /// </summary>
        private void StopFollowString()
        {
            this.OptionEvent("今日互粉已达上限，请明天继续加油！");
        }
        #endregion
    }
}
