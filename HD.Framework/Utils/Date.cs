//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;

namespace HD.Framework.Utils
{
    /// <summary>
    /// 时间信息类
    /// </summary>
    public class Date
    {
        /// <summary>
        /// 获取年份
        /// </summary>
        /// <returns></returns>
        public static int GetYear()
        {
            return DateTime.Now.Year;
        }
        /// <summary>
        /// 获取月份
        /// </summary>
        /// <returns></returns>
        public static int GetMonth()
        {
            return DateTime.Now.Month;
        }
        /// <summary>
        /// 获取日期
        /// </summary>
        /// <returns></returns>
        public static int GetDay()
        {
            return DateTime.Now.Day;
        }
        /// <summary>
        /// 返回标准日期(年-月-日)
        /// </summary>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 返回标准时间(时-分-秒)
        /// </summary>
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
        /// <summary>
        /// 返回标准时间(年-月-日 时-分-秒)
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
                /// <summary>
        /// 返回标准时间(年-月-日 时-分-秒)
        /// </summary>
        /// <param name="days">相对天数</param>
        /// <returns></returns>
        public static string GetDateTime(int days)
        {
            return DateTime.Now.AddDays(days).ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 返回标准时间(年-月-日 时-分-秒-毫秒)
        /// </summary>
        public static string GetDateTimeF()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }
    }
}