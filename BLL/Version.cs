﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Version
    {
        public static Model.ClientVersion NewClientVersion; //最新版本信息

        /// <summary>
        /// 获取最新版版本信息
        /// </summary>
        /// <returns></returns>
        public static Model.ClientVersion GetNewestClientVersion()
        {
            try
            {
                Model.WebMessage webMessage = DAL.WebAPI.GetNewestClientVersion();
                Model.ClientVersion version = new Model.ClientVersion();
                if (webMessage.c.Equals("200"))
                {
                    version.DownloadPath = ((Dictionary<string, object>)webMessage.d)["link"].ToString();
                    version.Version = ((Dictionary<string, object>)webMessage.d)["version"].ToString();
                    version.VersionDirection = ((Dictionary<string, object>)webMessage.d)["des"].ToString();
                }
                return version;
            }
            catch
            {
                return new Model.ClientVersion()
                {
                    Version = DAL.ConfigRW.Version
                };
            }
        }
        /// <summary>
        /// 判断当前版本是否已停用
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static bool IsCurrentClientValid()
        {
            string version = DAL.ConfigRW.Version;
            Model.WebMessage webMessage = DAL.WebAPI.CurrentClientValid(version);
            try
            {
                if (webMessage.c.Equals("200"))
                {
                    if (webMessage.d.ToString().Equals("0"))
                    {
                        return false;
                    }
                }
            }
            catch
            {

            }
            return true;
        }
        /// <summary>
        /// 判断当前版本是否为最新版本
        /// </summary>
        /// <returns></returns>
        public static bool CheckThisVersionIsNewest()
        {
            Model.ClientVersion version = GetNewestClientVersion();
            if (version.Version.Equals(DAL.ConfigRW.Version))
            {
                return true;
            }
            else
            {
                NewClientVersion = version;
                return false;
            }
        }
        /// <summary>
        /// 获取当前版本版本号
        /// </summary>
        /// <returns></returns>
        public static string CurrentClientVersion()
        {
            return DAL.ConfigRW.Version;
        }
    }
}
