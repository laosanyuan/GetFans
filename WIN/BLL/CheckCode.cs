using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CheckCode
    {
        //--> 云打码参数
        private static readonly int YunDaMaAppId = 5826;
        private static readonly string YunDaMaAppKey = "0025c106cd2868a094253c9fb40a8982";
        private static readonly int YunDaMaCodeType = 1005;
        private static readonly int YunDaMaTimeOut = 60;
        private static string YunDaMaUserName = DAL.ConfigRW.YunDaMaUserName;
        private static string YunDaMaPassword = DAL.ConfigRW.YunDaMaPassword;
        //<--

        /// <summary>
        /// 云打码解码
        /// </summary>
        /// <param name="img"></param>
        /// <param name="resultId"></param>
        /// <returns></returns>
        public static string DecodeCheckCode(Image img, out int resultId)
        {
            resultId = -1;

            if (YunDaMaPassword.Equals("") || YunDaMaUserName.Equals(""))
            {
                return "";
            }

            StringBuilder pCodeResult = new StringBuilder(new string(' ', 30));

            //保存文件到本地
            string jpgPath = System.Environment.CurrentDirectory + "\\Source\\code.jpg";
            if (!Directory.Exists(System.Environment.CurrentDirectory + "\\Source"))//若文件夹不存在则新建文件夹   
            {
                Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\Source"); //新建文件夹   
            }
            img.Save(jpgPath, img.RawFormat);

            //解码
            resultId = DAL.YunDaMaHelper.YDM_EasyDecodeByPath(YunDaMaUserName, YunDaMaPassword, YunDaMaAppId, YunDaMaAppKey, jpgPath, YunDaMaCodeType, YunDaMaTimeOut, pCodeResult);

            if (resultId > 0)
            {
                return pCodeResult.ToString();
            }
            else
            {
                return "";
            }
        }
    }
}
