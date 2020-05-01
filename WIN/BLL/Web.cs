using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Web
    {
        //帮助页面地址
        public static string HelpPath()
        {
            return DAL.ConfigRW.HelpPath;
        }
    }
}
