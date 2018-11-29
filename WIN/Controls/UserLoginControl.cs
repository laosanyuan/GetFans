using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WIN.Controls
{
    public partial class UserLoginControl : UserControl
    {
        public Model.User User { get; private set; }

        List<Model.Group> Groups = new List<Model.Group>();//互粉群列表

        public UserLoginControl(Model.User user)
        {
            InitializeComponent();
            this.User = user;
            this.FirstDisplay();
        }

        #region [界面加载]
        private void UserLoginControl_Load(object sender, EventArgs e)
        {

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
        //更新界面显示
        private void UpdateDisplay()
        {
            try
            {
                BLL.Weibo.UpdateUsersFansCount(this.User);
                this.labelNowFansCount.Text = this.User.FansCount;
                this.labelSuccessCount.Text = (Convert.ToInt32(this.User.FansCount) - Convert.ToInt32(this.labelLoginFansCount.Text)).ToString();
                this.labelGrouCount.Text = this.Groups.Count.ToString();
            }
            catch
            {

            }
        }
        #endregion

        #region [群处理]
        //更新群线程
        private Thread UpdateListThread;
        private delegate void UpdateGroupListDelegate(List<Model.Group> list);
        private void UpdateGroupListFunction(object obj)
        {
            Model.User updateGroupUser = (Model.User)obj;

            while (true)
            {
                //获取当前群列表顺序第一页群聊
                List<Model.Group> groups = BLL.Weibo.GetGroups(updateGroupUser.Cookies);
                //加入总列表
                this.BeginInvoke(new UpdateGroupListDelegate(AddGroupToList) , groups);
                Thread.Sleep(180); //三分钟更新一次
            }
        }
        private void AddGroupToList(List<Model.Group> groups)
        {
            foreach (Model.Group group in groups)
            {
                if (this.Groups.FindIndex(t => t.Gid.Equals(group.Gid)) >= 0)
                {
                    continue;
                }
                this.Groups.Add(group);
            }
        }

        //加群线程
        private Thread AddGroupThread;
        private void AddGroupFunction(object obj)
        {
            Model.User addGroupUser = (Model.User)obj;

            while (true)
            {
                List<Model.Group> groups = BLL.ServerData.GetGroups();
                foreach (Model.Group group in groups)
                {
                    if (!BLL.Weibo.IsAddedThisGroup(addGroupUser.Cookies, group.Gid))
                    {
                        BLL.Weibo.AddGroup(addGroupUser.Cookies, group.Gid, group.Name);
                    }
                    Thread.Sleep(10000);
                }
                Thread.Sleep(600000); //一轮间隔10分钟
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

                this.PrepareFollow();
            }
            else
            {
                this.buttonStart.Text = "开始";

                this.EndFollow();

                this.OptionEvent(String.Format("互粉结束，本次共互粉【{0}】个好友", this.labelSuccessCount.Text));
            }
        }
        //开始互粉设置
        private void PrepareFollow()
        {
            //开启加群线程
            this.AddGroupThread = new Thread(new ParameterizedThreadStart(AddGroupFunction));
            this.AddGroupThread.Start(this.User);
            //开启更新群列表线程
            this.UpdateListThread = new Thread(new ParameterizedThreadStart(UpdateGroupListFunction));
            this.UpdateListThread.Start(this.User);
            //开始群聊
            this.GroupChatThread = new Thread(new ParameterizedThreadStart(GroupChatFunction));
            this.GroupChatThread.Start(this.User);
            //回粉线程
            this.FollowFriendThread = new Thread(new ParameterizedThreadStart(FollowFriendFunction));
            this.FollowFriendThread.Start(this.User);
        }
        //停止互粉设置
        public void EndFollow()
        {
            //关闭加群线程
            this.AddGroupThread.Abort();
            //关闭更新群线程
            this.UpdateListThread.Abort();
            //关闭群聊线程
            this.GroupChatThread.Abort();
            //关闭回粉线程
            this.FollowFriendThread.Abort();
        }
        #endregion

        #region [群聊线程]
        //群聊线程
        private delegate List<Model.Group> GetAllGroupListDelegate();
        private Thread GroupChatThread;
        private Random random = new Random();
        private void GroupChatFunction(object user)
        {
            Model.User followUser = (Model.User)user;
            while (true)
            {
                List<Model.Group> groupList = (List<Model.Group>)Invoke(new GetAllGroupListDelegate(GetAllGroupList));
                int intervalTime =3000;//三秒

                for (int i = 0; i < groupList.Count; i++)
                {
                    string message = BLL.Weibo.GroupInviteFollowMe[random.Next(BLL.Weibo.GroupInviteFollowMe.Count - 1)];
                    BLL.Weibo.SendMessage2Group(followUser.Cookies, groupList[i].Gid, message);
                    Thread.Sleep(intervalTime);
                }
                Thread.Sleep(120000); //等待2分钟，开始下一轮消息
            }
        }
        private List<Model.Group> GetAllGroupList()
        {
            return this.Groups;
        }
        //回粉线程
        private delegate void StopFollowDelegate();
        private delegate void UpdateDisplayDelegate();
        private Thread FollowFriendThread;
        private void FollowFriendFunction(object user)
        {
            Model.User followUser = (Model.User)user;
            bool isCanFollow = true;
            while (isCanFollow)
            {
                //回粉好友
                List<Model.Fan> UnfollowFans = BLL.Weibo.GetUnfollowFansList(followUser.Cookies, followUser.Uid);
                foreach (Model.Fan fan in UnfollowFans)
                {
                    if (!BLL.Weibo.Follow(fan.Uid, fan.NickName, followUser.Cookies))
                    {
                        //关注不成功则停止
                        this.BeginInvoke(new StopFollowDelegate(StopFollowFunction));
                        isCanFollow = false;
                        break;
                    }
                    Thread.Sleep(10000);
                }
                this.Invoke(new UpdateDisplayDelegate(UpdateDisplay)); //更新显示
                Thread.Sleep(120000);//间隔2分钟
            }
        }
        private void StopFollowFunction()
        {
            this.StopFollowString();
            //系统提示音
            System.Media.SystemSounds.Hand.Play();
            this.buttonStart_Click(new object(), new EventArgs());
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

            this.EndFollow();
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
