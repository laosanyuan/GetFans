﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class YunDaMaHelper
    {

        /// <summary>
        /// 初始化云打码
        /// </summary>
        /// <param name="nAppId">开发者软件id</param>
        /// <param name="lpAppKey">key</param>
        [DllImport("yundamaAPI.dll")]
        public static extern void YDM_SetAppInfo(int nAppId, string lpAppKey);

        /// <summary>
        /// 普通用户登录
        /// </summary>
        /// <param name="lpUserName">用户名</param>
        /// <param name="lpPassWord">密码（可以是md5）</param>
        /// <returns>云打码用户id，大于0为登录成功</returns>
        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_Login(string lpUserName, string lpPassWord);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_DecodeByPath(string lpFilePath, int nCodeType, StringBuilder pCodeResult);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_UploadByPath(string lpFilePath, int nCodeType);

        /// <summary>
        /// 通过验证码文件路径解码
        /// </summary>
        /// <param name="lpUserName">用户名</param>
        /// <param name="lpPassWord">密码（或密码的Md5值）</param>
        /// <param name="nAppId">软件id</param>
        /// <param name="lpAppKey">key（分成）</param>
        /// <param name="lpFilePath">验证码文件路径</param>
        /// <param name="nCodeType">验证码类型</param>
        /// <param name="nTimeOut">超时时间（秒）</param>
        /// <param name="pCodeResult">解码值</param>
        /// <returns>验证码id，用以返回打码结果是否正确</returns>
        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_EasyDecodeByPath(string lpUserName, string lpPassWord, int nAppId, string lpAppKey, string lpFilePath, int nCodeType, int nTimeOut, StringBuilder pCodeResult);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_DecodeByBytes(byte[] lpBuffer, int nNumberOfBytesToRead, int nCodeType, StringBuilder pCodeResult);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_UploadByBytes(byte[] lpBuffer, int nNumberOfBytesToRead, int nCodeType);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_EasyDecodeByBytes(string lpUserName, string lpPassWord, int nAppId, string lpAppKey, byte[] lpBuffer, int nNumberOfBytesToRead, int nCodeType, int nTimeOut, StringBuilder pCodeResult);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_GetResult(int nCaptchaId, StringBuilder pCodeResult);

        /// <summary>
        /// 报告解码结果是否正确
        /// </summary>
        /// <param name="nCaptchaId">验证码id</param>
        /// <param name="bCorrect">错误：false 正确为true（可以不调用）</param>
        /// <returns>报错成功返回0</returns>
        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_Report(int nCaptchaId, bool bCorrect);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_EasyReport(string lpUserName, string lpPassWord, int nAppId, string lpAppKey, int nCaptchaId, bool bCorrect);

        /// <summary>
        /// 获取用户余额
        /// </summary>
        /// <param name="lpUserName"></param>
        /// <param name="lpPassWord"></param>
        /// <returns></returns>
        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_GetBalance(string lpUserName, string lpPassWord);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_EasyGetBalance(string lpUserName, string lpPassWord, int nAppId, string lpAppKey);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_SetTimeOut(int nTimeOut);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_Reg(string lpUserName, string lpPassWord, string lpEmail, string lpMobile, string lpQQUin);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_EasyReg(int nAppId, string lpAppKey, string lpUserName, string lpPassWord, string lpEmail, string lpMobile, string lpQQUin);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_Pay(string lpUserName, string lpPassWord, string lpCard);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_EasyPay(string lpUserName, string lpPassWord, long nAppId, string lpAppKey, string lpCard);

        //获取云打码余额
        private const int YunDaMaAppId = 5826;
        private const string YunDaMaAppKey = "0025c106cd2868a094253c9fb40a8982";
        //public static string GetYDMBalance()
        //{
        //    if (AppConfigRWTool.ReadSetting("YunDaMaUserName").Equals(""))
        //    {
        //        return YDM_EasyGetBalance(AppConfigRWTool.ReadSetting("YunDaMaUserName"),
        //            AppConfigRWTool.ReadSetting("YunDaMaPasswordMD5"),
        //            YunDaMaAppId,
        //            YunDaMaAppKey).ToString();
        //    }
        //    else
        //    {
        //        return "0(未登录)";
        //    }
        //}
    }
}
