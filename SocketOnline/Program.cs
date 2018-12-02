using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketOnline
{
    static class Program
    {
        public static List<Model.OnlineUser> OnlineUsers { get; set; } //所有登陆用户

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            OnlineUsers = new List<Model.OnlineUser>();
            //初始化数据库
            BLL.DataBase.InitDataBase();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Views.MainPageView());
        }
    }
}
