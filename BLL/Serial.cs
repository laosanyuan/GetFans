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
        /// <param name="serial"></param>
        /// <param name="IPAdress"></param>
        /// <param name="machineName"></param>
        /// <returns></returns>
        public static bool IsValidSerial(string serial,string IPAdress,string machineName)
        {
            return false;
        }
        /// <summary>
        /// 获取序列号有效期
        /// </summary>
        /// <param name="serial">序列号</param>
        /// <returns></returns>
        public static string GetSerialInvalidDate(string serial)
        {
            return "2018-12-30";
        }
        /// <summary>
        /// 获取序列号种类
        /// </summary>
        /// <param name="serial">序列号</param>
        /// <returns></returns>
        public static Model.SerialType GetSerialType(string serial)
        {
            return Model.SerialType.FreeSerial;
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
