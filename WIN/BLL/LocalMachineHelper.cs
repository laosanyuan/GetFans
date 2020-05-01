using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LocalMachineHelper
    {
        /// <summary>
        /// 获取本机IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            return DAL.HttpHelper.GetMachineIP();
        }
        /// <summary>
        /// 获取cpu id + mac地址
        /// </summary>
        /// <returns></returns>
        public static string GetCpuID()
        {
            string cpuInfo = "";//cpu序列号
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
            }
            moc = null;
            mc = null;
            return cpuInfo + GetMacByWMI();
        }

        /// <summary>
        /// 获取第一个网卡的mac地址
        /// </summary>
        /// <returns></returns>
        public static string GetMacByWMI()
{
            string mac = "";
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"])
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
            }
            catch
            {
            }

            return mac;
        }
    }
}
