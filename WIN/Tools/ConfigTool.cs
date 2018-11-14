using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIN.Tools
{
    public class ConfigTool
    {
        public static string Serial //序列号
        {
            get
            {
                return ConfigurationManager.AppSettings["Serial"];
            }
        }
    }
}
