using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Serial
    {
        /// <summary>
        /// 验证序列号有效性
        /// </summary>
        /// <returns></returns>
        public static bool IsValidSerial()
        {
            string serial = BLL.Serial.GetSerial();
            return IsValidSerial(serial);
        }
        /// <summary>
        /// 序列号是否有效
        /// </summary>
        /// <param name="serial">序列号</param>
        /// <returns></returns>
        public static bool IsValidSerial(string serial)
        {
            if (serial.Equals(""))
            {
                return false;
            }

            string machineName = System.Environment.MachineName;
            string IPAddress = BLL.LocalMachineHelper.GetIP();
            Model.WebMessage webMessage = DAL.WebAPI.IsValidSerial(serial, IPAddress, machineName);

            if (webMessage.c.Equals("200"))
            {
                if (webMessage.d.ToString().Equals("1"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取序列号有效期
        /// </summary>
        /// <param name="serial">序列号</param>
        /// <returns></returns>
        public static string GetSerialInvalidDate()
        {
            Model.WebMessage webMessage = DAL.WebAPI.GetSerialInvalidDate(DAL.ConfigRW.Serial);
            if (webMessage.c.Equals("200")&& !webMessage.d.ToString().Equals("0"))
            {
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                DateTime dt = startTime.AddSeconds(Convert.ToInt64(webMessage.d));
                return dt.ToString("yyyy/MM/dd HH:mm:ss");
            }
            else if(webMessage.c.Equals("200") && webMessage.d.ToString().Equals("0"))
            {
                return "无效序列号";
            }
            return "有效期获取失败";
        }
        /// <summary>
        /// 获取序列号种类
        /// </summary>
        /// <param name="serial">序列号</param>
        /// <returns></returns>
        public static string GetSerialType()
        {
            string serial = DAL.ConfigRW.Serial;
            string machineName = System.Environment.MachineName;
            string IPAddress = BLL.LocalMachineHelper.GetIP();
            Model.WebMessage webMessage = DAL.WebAPI.GetSerialType(serial, IPAddress, machineName);

            if (webMessage.c.Equals("200"))
            {
                if (webMessage.d.ToString().Equals("1"))
                {
                    return "试用号";
                }
                else if (webMessage.d.ToString().Equals("2"))
                {
                    return "普通号";
                }
                else if (webMessage.d.ToString().Equals("3"))
                {
                    return "VIP账号";
                }
            }
            return "未知种类";
        }
        /// <summary>
        /// 获取当前版本号
        /// </summary>
        /// <returns></returns>
        public static string GetSerial()
        {
            return DAL.ConfigRW.Serial;
        }
        /// <summary>
        /// 获取购买序列号路径
        /// </summary>
        /// <returns></returns>
        public static string GetBuySerialPath()
        {
            return DAL.ConfigRW.BuySerialPath;
        }
    }
}
