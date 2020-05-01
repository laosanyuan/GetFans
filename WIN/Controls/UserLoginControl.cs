using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Model;
using WIN.Common;

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
            //创建用户数据表
            BLL.WinClientSQLiteHelper.CreateUserGroupsTable(User.Uid);
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
                Thread.Sleep(600000); //10分钟更新一次
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
                //向数据库中插入群数据
                BLL.WinClientSQLiteHelper.InsertGroup(User.Uid, group.Name, group.Gid);
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
                        try
                        {
                            //根据退群时间加群
                            BLL.Weibo.EnterGroupByTime(addGroupUser.Cookies, addGroupUser.Uid, group.Gid, group.Name);
                        }
                        catch
                        {
                            //加群异常
                        }
                    }
                    else
                    {
                        //如果从服务器获取到的群已经添加过了，根据时间退群
                        if (BLL.Weibo.ExitGroupByTime(addGroupUser.Cookies, addGroupUser.Uid, group.Gid, group.Name))
                        {
                            //更新群列表
                            this.BeginInvoke(new UpdateGroupListDelegate(DeleteGroupToList), new List<Model.Group>() { group });
                        }
                    }
                    Thread.Sleep(10000);
                }

                Thread.Sleep(600000); //一轮间隔10分钟
            }
        }

        private void DeleteGroupToList(List<Group> list)
        {
            this.Groups.RemoveAll(t => t.Gid.Equals(list[0].Gid));
        }
        #endregion

        #region [互粉]
        //开始互粉
        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (this.buttonStart.Text.Equals("开始互粉"))
            {
                this.buttonStart.Text = "停止互粉";
                this.labelBeginTime.Text = DateTime.Now.ToShortTimeString();
                this.OptionEvent(String.Format("【{0}】开始互粉…", User.NickName));

                this.PrepareFollow();

                //记录用户名
                BLL.ServerData.SendNickNameToServer(User.NickName);
            }
            else
            {
                this.buttonStart.Text = "开始互粉";

                this.EndFollow();

                this.OptionEvent(String.Format("【{0}】互粉结束，本次共互粉【{1}】个好友", User.NickName, this.labelSuccessCount.Text));
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
            //主动关注线程
            this.FollowSocketUserThread = new Thread(new ParameterizedThreadStart(FollowSocketUserFunction));
            this.FollowSocketUserThread.Start(this.User);
        }
        //停止互粉设置
        public void EndFollow()
        {
            //关闭加群线程
            try
            {
                this.AddGroupThread.Abort();
            }
            catch
            {
            }
            //关闭更新群线程
            try
            {
                this.UpdateListThread.Abort();
            }
            catch
            {

            }
            //关闭群聊线程
            try
            {
                this.GroupChatThread.Abort();
            }
            catch
            {

            }
            //关闭回粉线程
            try
            {
                this.FollowFriendThread.Abort();
            }
            catch
            {
            }
            //关闭主动关注线程
            try
            {
                this.FollowSocketUserThread.Abort();
            }
            catch
            {
            }
        }
        #endregion

        #region [群聊线程]
        //群聊线程
        private delegate List<Model.Group> GetAllGroupListDelegate();
        private Thread GroupChatThread;
        private Random random = new Random();
        private void GroupChatFunction(object user)
        {
            string serialType = BLL.Serial.GetSerialType();
            Model.User followUser = (Model.User)user;
            while (true)
            {
                List<Model.Group> groupList = (List<Model.Group>)Invoke(new GetAllGroupListDelegate(GetAllGroupList));
                int intervalTime =3000;//三秒

                for (int i = 0; i < groupList.Count; i++)
                {
                    string message = BLL.Weibo.GroupInviteFollowMe[random.Next(BLL.Weibo.GroupInviteFollowMe.Count - 1)];
                    if (serialType.Equals("试用号"))
                    {
                        message += "@极光互粉助手";
                    }
                    try
                    {
                        BLL.Weibo.SendMessage2Group(followUser.Cookies, groupList[i].Gid, message);
                    }
                    catch
                    {
                        //
                    }
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

        #region [主动关注线程]
        //主动关注小火箭其他用户
        private Thread FollowSocketUserThread;
        private delegate void FollowSocketUserDelegate();
        private void FollowSocketUserFunction(object user)
        {
            Model.User followUser = (Model.User)user;
            while (true)
            {
                List<Model.Group> groups = BLL.Weibo.GetGroups(followUser.Cookies);
                foreach (Model.Group group in groups)
                {
                    List<MessagesItem> groupFriends = BLL.Weibo.GetGroupFriendsList(followUser.Cookies, followUser.Uid, group.Gid, group.Name);
                    foreach (var friend in groupFriends)
                    {
                        //所获取到的聊天内容是否存在于聊天库中
                        if (BLL.Weibo.GroupInviteFollowMe.Contains(friend.content))
                        {
                            //如果互相未关注，关注对方
                            if (BLL.Weibo.GetFriendFollowStatus(followUser.Cookies, friend.from_uid.ToString()) == FriendStatus.UnFollowEachOther)
                            {
                                BLL.Weibo.Follow(friend.from_uid.ToString(), friend.from_user.screen_name, followUser.Cookies);
                                Thread.Sleep(20000);    //间隔20秒
                            }
                        }
                        Thread.Sleep(500);
                    }
                }
                Thread.Sleep(600000 / (groups.Count + 1));
            }
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
            this.StopComment();
            this.StopGroupPush();

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
            this.OptionEvent(String.Format("【{0}】今日互粉已达上限，请明天继续加油！", User.NickName));
        }
        #endregion

        #region [一键清粉]
        private void buttonClean_Click(object sender, EventArgs e)
        {
            this.OptionEvent("账号【" + User.NickName + "】正在清粉，此操作将无差别取关未回粉好友，请谨慎使用！");
            this.buttonClean.Text = "正在清粉";
            this.buttonClean.Enabled = false;
            //清粉线程
            Thread thread = new Thread(new ThreadStart(CancelUserFunction));
            thread.Start();
        }

        private delegate void CancelFollowUserMessageDelegate(int count);
        private void CancelUserFunction()
        {
            int i = BLL.Weibo.CancelFollowFakerUser(User.Cookies, User.Uid);
            this.BeginInvoke(new CancelFollowUserMessageDelegate(UnfollowUserMessage), i);
            this.Invoke(new UpdateDisplayDelegate(UpdateDisplay));
        }
        //向提示框输出清粉结果
        private void UnfollowUserMessage(int count)
        {
            this.buttonClean.Text = "一键清粉";
            string message = "";
            if (count == 0)
            {
                message = String.Format("账号【{0}】今日取关已达上限，请24小时后尝试下次清粉！",User.NickName);
            }
            else
            {
                message = String.Format("账号【{0}】本次清粉【{1}】人已完成！", User.NickName, count);
                this.buttonClean.Enabled = true;
            }
            this.OptionEvent(message);
        }

        #endregion

        #region [群推]
        private Thread GroupPushThread;
        private void ButtonChat_Click(object sender, EventArgs e)
        {
            if (this.buttonChat.Text.Equals("开始群推"))
            {
                this.buttonChat.Text = "停止群推";
                this.OptionEvent($"【{User.NickName}】开始群推...");

                this.GroupPushThread = new Thread(new ParameterizedThreadStart(this.PushFunction));
                this.GroupPushThread.Start(this.User);
            }
            else
            {
                this.buttonChat.Text = "开始群推";
                this.OptionEvent($"【{User.NickName}】群推已停止，本次共计推送微博[{this.labelChat.Text}]次");
                this.StopGroupPush();
            }
        }

        //群推
        private void PushFunction(object obj)
        {
            var user = obj as User;

            List<Group> groupList = new List<Group>();          //群列表
            List<string> pushWeiboUrls = new List<string>();    //最新微博列表
            int times = 0;

            while (true)
            {
                //获取群列表
                List<Group> groups = (List<Group>)Invoke(new GetAllGroupListDelegate(GetAllGroupList));

                if (groups.Count == 0)
                {
                    //获取当前群列表顺序第一页群聊
                    groups = BLL.Weibo.GetGroups(user.Cookies);
                    //加入总列表
                    this.BeginInvoke(new UpdateGroupListDelegate(AddGroupToList), groups);
                }

                foreach (var g in groups)
                {
                    if (groupList.FindIndex(t => t.Gid.Equals(g.Gid)) >= 0)
                    {
                        continue;
                    }
                    groupList.Add(g);
                }
                //获取最新微博分享链接
                List<string> urls = BLL.Weibo.GetNewestWeiboUrls(user.Cookies, user.Uid);
                if (urls.Count > 0)
                {
                    pushWeiboUrls = urls;
                }
                //开始群推
                foreach (var url in pushWeiboUrls)
                {
                    foreach (var g in groupList)
                    {
                        try
                        {
                            BLL.Weibo.SendMessage2Group(user.Cookies, g.Gid, url);
                            this.Invoke(new Action(() =>
                            {
                                this.OptionEvent($"【{user.NickName}】向群聊[{g.Name}]分享一条微博");
                                this.labelChat.Text = times++.ToString();
                            }));
                        }
                        catch
                        {
                            this.Invoke(new Action(() =>
                            {
                                this.OptionEvent($"【{user.NickName}】向群聊[{g.Name}]分享微博失败");
                            }));
                        }
                        Thread.Sleep(1000);
                    }
                    Thread.Sleep(300000);
                }
            }
        }

        public void StopGroupPush()
        {
            if (this.GroupPushThread != null && this.GroupPushThread.IsAlive)
            {
                try
                {
                    this.GroupPushThread.Abort();
                }
                catch
                {
                }
            }
        }
        #endregion

        #region [互动]
        private Thread CommentThread;
        private void ButtonComment_Click(object sender, EventArgs e)
        {
            if (this.buttonComment.Text.Equals("开始互动"))
            {
                this.buttonComment.Text = "停止互动";
                this.OptionEvent($"【{User.NickName}】开始互动...");

                this.CommentThread = new Thread(new ParameterizedThreadStart(this.CommentFunction));
                this.CommentThread.Start(this.User);
            }
            else
            {
                this.buttonComment.Text = "开始互动";
                this.OptionEvent($"【{User.NickName}】互动已停止,本次攻击互动[{this.labelComment.Text}]次");
                this.StopComment();
            }
        }

        private void CommentFunction(object obj)
        {
            var user = obj as User;
            int times = 0;
            var comments = FileUtil.ReadFile(System.Environment.CurrentDirectory + "\\CommentDetails.co").Split('%');
            while (true)
            {
                var weibos = BLL.Weibo.GetFriendNewestWeiboMids(user.Cookies);
                foreach (var weibo in weibos)
                {
                    try
                    {
                        var comment = comments[new Random().Next(comments.Length - 1)];
                        //点赞
                        var likeResult = BLL.Weibo.LikeWeibo(user.Cookies, weibo.Mid);
                        Thread.Sleep(1000);
                        //评论
                        var result = "";
                        var commentResult = BLL.Weibo.CommentWeibo(user.Cookies, weibo.Mid, user.Uid, weibo.Uid, comment, ref result);
                        this.Invoke(new Action(() =>
                        {
                            if (likeResult && commentResult)
                            {
                                this.OptionEvent($"互动成功，评论并点赞用户【{weibo.NickName}】微博成功！评论内容为：{comment}。");
                            }
                            else if (likeResult)
                            {
                                this.OptionEvent($"点赞用户【{weibo.NickName}】微博成功！");
                            }
                            else if (commentResult)
                            {
                                this.OptionEvent($"互动成功，评论用户【{weibo.NickName}】微博成功！评论内容为：{comment}。");
                            }
                            this.labelComment.Text = times++.ToString();
                        }));
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            this.OptionEvent($"评论用户【{weibo.NickName}】微博失败");
                        }));
                    }
                    Thread.Sleep(10000);
                }
                Thread.Sleep(900000);
            }
        }

        public void StopComment()
        {
            if (this.CommentThread != null && this.CommentThread.IsAlive)
            {
                try
                {
                    this.CommentThread.Abort();
                }
                catch
                { }
            }
        }
        #endregion
    }
}
