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

            //晚8点发送邮件
            if (DateTime.Now.Hour == 20)
            {
                this.SendEmail();
            }

            //每隔20小时更新cookie
            if (HourCount == 20)
            {
                User.Cookies = BLL.Weibo.UpdateCookies(User.UserName, User.Password);
            }

            //晚上23：00-早8：00不启动
            if (DateTime.Now.Hour >= 23 || DateTime.Now.Hour < 8)
            {
                return;
            }
            if (HourCount >= 24)
            {
                //关闭线程
                this.StopThread();
                //开启互粉线程
                this.FollowThread = new Thread(new ThreadStart(FollowThreadFunction));
                this.FollowThread.IsBackground = true;
                this.FollowThread.Start();
                //群聊线程
                this.GroupChatThread = new Thread(new ThreadStart(GroupChatThreadFunction));
                this.GroupChatThread.IsBackground = true;
                this.GroupChatThread.Start();
                //群列表更新线程
                this.UpdateGroupListThread = new Thread(new ThreadStart(UpdateGroupListThreadFuntion));
                this.UpdateGroupListThread.IsBackground = true;
                this.UpdateGroupListThread.Start();
                //加群线程开启
                this.AddGroupThread = new Thread(new ThreadStart(AddGroupThreadFuntion));
                this.AddGroupThread.IsBackground = true;
                this.AddGroupThread.Start();

                HourCount = 0;
            }
        }
        //关闭所有线程
        private void StopThread()
        {
            if (this.FollowThread != null &&
                this.GroupChatThread != null &&
                this.UpdateGroupListThread != null &&
                this.AddGroupThread != null)
            {
                this.FollowThread.Abort();
                this.GroupChatThread.Abort();
                this.UpdateGroupListThread.Abort();
                this.AddGroupThread.Abort();
            }
        }
        #endregion

        #region [回粉]
        private Thread FollowThread;
        private void FollowThreadFunction()
        {
            bool isCanFollow = true;
            while (isCanFollow)
            {
                //回粉好友
                List<Model.Fan> UnfollowFans = BLL.Weibo.GetUnfollowFansList(User.Cookies, User.Uid);
                foreach (Model.Fan fan in UnfollowFans)
                {
                    if (!BLL.Weibo.Follow(fan.Uid, fan.NickName, User.Cookies))
                    {
                        //关注不成功则停止
                        //this.BeginInvoke(new StopFollowDelegate(StopFollowFunction));
                        this.StopThread();
                        isCanFollow = false;
                        break;
                    }
                    else
                    {

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
        private void GroupChatThreadFunction()
        {
            while (true)
            {
                List<Model.Group> groupList = this.Groups;
                int intervalTime = 3000;//三秒

                for (int i = 0; i < groupList.Count; i++)
                {
                    string message = BLL.Weibo.GroupInviteFollowMe[random.Next(BLL.Weibo.GroupInviteFollowMe.Count - 1)];
                    try
                    {
                        BLL.Weibo.SendMessage2Group(User.Cookies, groupList[i].Gid, message);
                    }
                    catch
                    {
                        followCount++;
                    }
                    Thread.Sleep(intervalTime);
                }
                Thread.Sleep(120000); //等待2分钟，开始下一轮消息
            }
        }
        #endregion

        #region [群列表更新]
        private Thread UpdateGroupListThread;
        private void UpdateGroupListThreadFuntion()
        {
            while (true)
            {
                //获取当前群列表顺序第一页群聊
                List<Model.Group> groups = BLL.Weibo.GetGroups(User.Cookies);
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
        private void AddGroupThreadFuntion()
        {
            while (true)
            {
                List<Model.Group> groups = BLL.ServerData.GetGroups();
                foreach (Model.Group group in groups)
                {
                    if (!BLL.Weibo.IsAddedThisGroup(User.Cookies, group.Gid))
                    {
                        try
                        {
                            BLL.Weibo.AddGroup(User.Cookies, group.Gid, group.Name);
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

        #region [发送邮件]
        int followCount = 0;
        private void SendEmail()
        {
            if (User.Email.Equals(""))
            {
                return;
            }

            string message = String.Format("尊敬的用户您好，您的小火箭互粉精灵今日运行数据如下：<br/><br/>账号昵称：{0}<br/>今日互粉成功数：{1}<br/>当前有效群聊数：{2}<br/>到期时间：{3}", User.NickName, this.followCount.ToString(), this.Groups.Count.ToString(), this.User.EndTime.ToString());
            BLL.EMail.SendEMailToUser(User.Email, "小火箭日报告", message);

            followCount = 0;
        }
        #endregion
    }
}
