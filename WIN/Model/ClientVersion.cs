using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    //客户端版本
    public class ClientVersion
    {
        public string Version { get; set; } //版本号
        public string VersionDirection { get; set; } //版本说明
        public string DownloadPath { get; set; } //下载地址
    }
}
