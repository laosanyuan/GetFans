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
            //判断序列号是否有效

            return true;
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
    }
}
