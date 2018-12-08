using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Model;

namespace SocketOnline.Entity
{
    public class UserEntity
    {
        public Model.OnlineUser User { get; }

        private List<Model.Group> Groups = new List<Model.Group>(); //群聊列表

        public UserEntity(Model.OnlineUser onlineUser)
        {
            this.User = onlineUser;
            userTimer = new Timer(new TimerCallback(TimerCallBackFunction), null, 100, 3600000); //一小时定时器
        }

        #region [线程停起]
        private Timer userTimer;
        int HourCount = 24;
        //24小时开启线程
        private void TimerCallBackFunction(object state)
        {
            HourCount++;

            //每隔20小时更新cookie
            if (HourCount == 20)
            {
                User.Cookies = BLL.Weibo.UpdateCookies(User.UserName, User.Password);
            }

            //晚上23：00-早8：00不启动
            if (DateTime.Now.Hour > 23 || DateTime.Now.Hour < 8)
            {
                return;
            }
            if (HourCount >= 24)
            {
                //关闭线程
                this.StopThread();
                //开启互粉线程
                this.FollowThread = new Thread(new ParameterizedThreadStart(FollowThreadFunction));
                this.FollowThread.IsBackground = true;
                this.FollowThread.Start(this.User);
                //群聊线程
                this.GroupChatThread = new Thread(new ParameterizedThreadStart(GroupChatThreadFunction));
                this.GroupChatThread.IsBackground = true;
                this.GroupChatThread.Start(this.User);
                //群列表更新线程
                this.UpdateGroupListThread = new Thread(new ParameterizedThreadStart(UpdateGroupListThreadFuntion));
                this.UpdateGroupListThread.IsBackground = true;
                this.UpdateGroupListThread.Start(this.User);
                //加群线程开启
                this.AddGroupThread = new Thread(new ParameterizedThreadStart(AddGroupThreadFuntion));
                this.AddGroupThread.IsBackground = true;
                this.AddGroupThread.Start(this.User);

                HourCount = 0;
            }
        }
        //关闭所有线程
        private void StopThread()
        {
            try
            {
                this.FollowThread.Abort();
                this.GroupChatThread.Abort();
                this.UpdateGroupListThread.Abort();
                this.AddGroupThread.Abort();
            }
            catch
            {
                //关闭线程失败
            }
        }
        #endregion

        #region [回粉]
        private Thread FollowThread;
        private void FollowThreadFunction(object obj)
        {
            Model.OnlineUser followUser = (Model.OnlineUser)obj;
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
                        //this.BeginInvoke(new StopFollowDelegate(StopFollowFunction));
                        this.StopThread();
                        isCanFollow = false;
                        break;
                    }
                    Thread.Sleep(10000);
                }
                //this.Invoke(new UpdateDisplayDelegate(UpdateDisplay)); //更新显示
                Thread.Sleep(120000);//间隔2分钟

                //停止互粉时间段
                if (DateTime.Now.Hour >= 23 || DateTime.Now.Hour < 8)
                {
                    this.StopThread();
                }
            }
        }
        #endregion

        #region [群聊]
        private Thread GroupChatThread;
        private Random random = new Random();
        private void GroupChatThreadFunction(object obj)
        {
            Model.OnlineUser followUser = (Model.OnlineUser)obj;
            while (true)
            {
                List<Model.Group> groupList = this.Groups;
                int intervalTime = 3000;//三秒

                for (int i = 0; i < groupList.Count; i++)
                {
                    string message = BLL.Weibo.GroupInviteFollowMe[random.Next(BLL.Weibo.GroupInviteFollowMe.Count - 1)];
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
        #endregion

        #region [群列表更新]
        private Thread UpdateGroupListThread;
        private void UpdateGroupListThreadFuntion(object obj)
        {
            Model.OnlineUser onlineUser = (Model.OnlineUser)obj;
            while (true)
            {
                //获取当前群列表顺序第一页群聊
                List<Model.Group> groups = BLL.Weibo.GetGroups(onlineUser.Cookies);
                //加入总列表
                this.AddGroupToList(groups);

                Thread.Sleep(180000); //三分钟更新一次
            }
        }

        private void AddGroupToList(List<Group> groups)
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
        #endregion

        #region [加群]
        private Thread AddGroupThread;
        private void AddGroupThreadFuntion(object obj)
        {
            Model.OnlineUser addGroupUser = (Model.OnlineUser)obj;

            while (true)
            {
                List<Model.Group> groups = BLL.ServerData.GetGroups();
                foreach (Model.Group group in groups)
                {
                    if (!BLL.Weibo.IsAddedThisGroup(addGroupUser.Cookies, group.Gid))
                    {
                        try
                        {
                            BLL.Weibo.AddGroup(addGroupUser.Cookies, group.Gid, group.Name);
                        }
                        catch
                        {
                            //加群异常
                        }
                    }
                    Thread.Sleep(10000);
                }
                Thread.Sleep(600000); //一轮间隔10分钟
            }
        }
        #endregion
    }
}
