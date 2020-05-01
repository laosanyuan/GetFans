using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    //api返回
    public class WebMessage
    {
        public string c { get; set; }
        public string m { get; set; }
        public object d { get; set; }
    }

    public class OperatingWeiboBackJson
    {
        public string code { get; set; } = "";
        public string msg { get; set; } = "";
        public object data { get; set; }
    }
}
