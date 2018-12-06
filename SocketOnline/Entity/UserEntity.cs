using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketOnline.Entity
{
    public class UserEntity
    {
        public Model.OnlineUser User { get; }

        public UserEntity(Model.OnlineUser onlineUser)
        {
            this.User = onlineUser;
            userTimer = new Timer(new TimerCallback(TimerCallBackFunction), null, 100, 3600000); //一小时定时器
        }

        #region [线程停起]
        private Timer userTimer;
        int HourCount = 0;
        //24小时开启线程
        private void TimerCallBackFunction(object state)
        {
            HourCount++;
            //晚上23：00-早8：00不启动
            if (DateTime.Now.Hour > 23 || DateTime.Now.Hour < 8)
            {
                return;
            }
            if (HourCount >= 24)
            {
                //开启互粉线程
                this.FollowThread = new Thread(new ParameterizedThreadStart(FollowThreadFunction));
                this.FollowThread.Start(this.User);
                //群聊线程
                this.GroupChatThread = new Thread(new ParameterizedThreadStart(GroupChatThreadFunction));
                this.GroupChatThread.Start(this.User);
                //群列表更新线程
                this.UpdateGroupListThread = new Thread(new ParameterizedThreadStart(UpdateGroupListThreadFuntion));
                this.UpdateGroupListThread.Start(this.User);
                //加群线程开启
                this.AddGroupThread = new Thread(new ParameterizedThreadStart(AddGroupThreadFuntion));
                this.AddGroupThread.Start(this.User);

                HourCount = 0;
            }
        }
        //关闭所有线程
        private void StopThread()
        {
            this.FollowThread.Abort();
            this.GroupChatThread.Abort();
            this.UpdateGroupListThread.Abort();
            this.AddGroupThread.Abort();
        }
        #endregion

        #region [回粉]
        private Thread FollowThread;
        private void FollowThreadFunction(object obj)
        {

        }
        #endregion

        #region [群聊]
        private Thread GroupChatThread;
        private void GroupChatThreadFunction(object obj)
        {

        }
        #endregion

        #region [群列表更新]
        private Thread UpdateGroupListThread;
        private void UpdateGroupListThreadFuntion(object obj)
        {

        }
        #endregion

        #region [加群]
        private Thread AddGroupThread;
        private void AddGroupThreadFuntion(object obj)
        {

        }
        #endregion
    }
}
