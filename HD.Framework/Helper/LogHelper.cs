//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HD.Framework.Utils;

namespace HD.Framework.Helper
{
    /// <summary>
    /// 日志信息类
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 对象用于锁
        /// </summary>
        private static readonly object locker = new object();

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="LogContent">日志内容</param>
        public static void WriteLog(string LogContent)
        {
            lock (locker)
            {
                StreamWriter sw = null;
                try
                {
                    string LogPath = Public.GetMapPath("~/App_Logs");
                    #region 检测日志目录是否存在
                    if (!Directory.Exists(LogPath))
                    {
                        Directory.CreateDirectory(LogPath);
                    }

                    //检测日志文件是否存在
                    string LogFile = string.Format("{0}/{1}.txt", LogPath, DateTime.Now.ToString("yyyyMMddHH"));
                    if (!File.Exists(LogFile))
                    {
                        sw = File.CreateText(LogFile);
                    }
                    else
                    {
                        sw = File.AppendText(LogFile);
                    }
                    #endregion
                    sw.WriteLine("IP地址：" + Public.GetUserIP + "\r");
                    sw.WriteLine("时  间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r");
                    sw.WriteLine("内  容：" + LogContent + "\r");
                    sw.WriteLine("********************************************************************\r");
                    sw.Flush();
                }
                finally
                {
                    if (sw != null)
                    {
                        sw.Flush();
                        sw.Close();
                        sw.Dispose();
                        sw = null;
                    }
                }
            }
        }
    }
}