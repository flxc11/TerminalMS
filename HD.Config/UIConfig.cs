//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using HD.Framework.Helper;
using HD.Framework.Utils;

namespace HD.Config
{
    /// <summary>
    /// 界面配置类
    /// </summary>
    public class UIConfig
    {
        /// <summary>
        /// 软件名称
        /// </summary>
        public static string SoftName
        {
            get
            {
                return ConfigHelper.GetValue("SoftName");
            }
        }
        /// <summary>
        /// 软件版本
        /// </summary>
        public static string Version
        {
            get
            {
                return Assembly.Load(MethodBase.GetCurrentMethod().DeclaringType.Namespace).GetName().Version.ToString();
            }
        }
        /// <summary>
        /// 安装路径
        /// </summary>
        public static string InstallDir
        {
            get
            {
                return ConfigHelper.GetValue("InstallDir");
            }
        }
        /// <summary>
        /// 管理目录
        /// </summary>
        public static string AdminDir
        {
            get
            {
                return ConfigHelper.GetValue("AdminDir");
            }
        }
        /// <summary>
        /// 上传文件最大值
        /// </summary>
        public static string FileMaxLength
        {
            get
            {
                return ConfigHelper.GetValue("FileMaxLength");
            }
        }
        /// <summary>
        /// 数据表前缀
        /// </summary>
        public static string Prefix
        {
            get
            {
                return ConfigHelper.GetValue("Prefix");
            }
        }
        /// <summary>
        /// Cookies名称
        /// </summary>
        public static string CookieName
        {
            get
            {
                return ConfigHelper.GetValue("CookieName");
            }
        }
        /// <summary>
        /// 前台用户Cookies名称
        /// </summary>
        public static string UserCookieName
        {
            get
            {
                return ConfigHelper.GetValue("UserCookieName");
            }
        }
        /// <summary>
        /// Cookie超时
        /// </summary>
        public static int Expires
        {
            get
            {
                return TypeConverter.StrToInt(ConfigHelper.GetValue("Expires"), 0);
            }
        }
        /// <summary>
        /// 版权信息
        /// </summary>
        public static string CopyRight
        {
            get
            {
                return "CopyRight © 2005-" + DateTime.Now.Year.ToString() + " <a href=\"http://www.cnvp.com.cn\" target=\"_blank\" title=\"捷点科技\">CNVP.Com.Cn</a> All Rights Reserved.";
            }
        }
        /// <summary>
        /// 默认GetGuID值
        /// </summary>
        public static string GetGuID
        {
            get
            {
                return "00000000-0000-0000-0000-000000000000";
            }
        }
        /// <summary>
        /// 获取通用消息
        /// </summary>
        /// <param name="MsgID">消息序号</param>
        /// <returns></returns>
        public static string GetMsgInfo(string MsgID)
        {
            return ConfigHelper.GetValue(MsgID); 
        }
        /// <summary>
        /// 缓存配置信息
        /// </summary>
        /// <param name="KeyID">缓存序号</param>
        /// <returns></returns>
        public static string GetKeyInfo(string KeyID)
        {
            return ConfigHelper.GetValue(KeyID); 
        }
    }
}