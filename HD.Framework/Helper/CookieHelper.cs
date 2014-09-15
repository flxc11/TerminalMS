//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;

namespace HD.Framework.Helper
{
    /// <summary>
    ///  Cookie帮助类
    /// </summary>
    public class CookieHelper
    {
        /// <summary>
        /// 写入Cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">键值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            WriteCookie(strName, strValue, 0);
        }
        /// <summary>
        /// 写入Cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">键值</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.HttpOnly = true;
            cookie.Value = strValue;
            if (expires > 0)
            {
                cookie.Expires = DateTime.Now.AddMinutes(expires);
            }
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// 写入Cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">键值</param>
        public static void WriteCookie(string strName, object strValue)
        {
            WriteCookie(strName, strValue, 0);
        }
        /// <summary>
        /// 写入Cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">键值</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void WriteCookie(string strName, object strValue, int expires)
        {
            //声明一个序列化的类
            BinaryFormatter bf = new BinaryFormatter();
            //声明一个内存流
            MemoryStream ms = new MemoryStream();
            //执行序列化操作
            bf.Serialize(ms, strValue);
            byte[] result = new byte[ms.Length];
            result = ms.ToArray();
            string temp = Convert.ToBase64String(result);
            ms.Flush();
            ms.Close();
            //保存Cookie值
            WriteCookie(strName, temp, expires);
        }
        /// <summary>
        /// 读Cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>Cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();
            }
            return "";
        }
        /// <summary>
        /// 读Cookie值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strName">名称</param>
        /// <returns>Cookie值</returns>
        public static T GetCookie<T>(string strName)
        {
            T _strValue = default(T);
            string result = GetCookie(strName);
            if (!string.IsNullOrEmpty(result))
            {
                try
                {   
                    //将得到的字符串根据相同的编码格式分成字节数组
                    byte[] b = System.Convert.FromBase64String(result);
                    //从字节数组中得到内存流
                    MemoryStream ms = new MemoryStream(b, 0, b.Length);
                    BinaryFormatter bf = new BinaryFormatter();
                    //反序列化得到Person类对象
                    _strValue = (T)bf.Deserialize(ms);
                    ms.Flush();
                    ms.Close();
                }
                catch
                {
                }
            }
            return _strValue;
        }
    }
}