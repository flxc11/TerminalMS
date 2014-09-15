//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Web;
using System.Text.RegularExpressions;

namespace HD.Framework.Utils
{
    /// <summary>
    /// 通用函数类
    /// </summary>
    public class Public
    {
        #region "判断网络状态"
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int lpdwFlags, int dwReserved);
        /// <summary>
        /// 判断网络状态
        /// </summary>
        /// <returns></returns>
        public static bool IsOnline()
        {
            var lfag = 0;
            bool isInternet = InternetGetConnectedState(out lfag, 0);
            return isInternet;
        }
        #endregion
        #region "获取绝对路径"
        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current == null)
            {
                //非Web程序引用
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
            else
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
        }
        #endregion
        #region "获取用户地址"
        /// <summary>
        /// 获取用户IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetUserIP
        {
            get
            {
                string UserIP = String.Empty;
                UserIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(UserIP))
                {
                    UserIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (string.IsNullOrEmpty(UserIP))
                {
                    UserIP = HttpContext.Current.Request.UserHostAddress;
                }
                if (string.IsNullOrEmpty(UserIP) || UserIP == "::1")
                {
                    return "127.0.0.1";
                }
                return UserIP;
            }
        }
        #endregion
        #region "获取字符长度"
        /// <summary>
        /// 获取字符长度
        /// </summary>
        /// <param name="Str">文字内容</param>
        /// <returns></returns>
        public static int StrLength(string Str)
        {
            byte[] bytes = new ASCIIEncoding().GetBytes(Str);
            int num = 0;
            for (int i = 0; i <= (bytes.Length - 1); i++)
            {
                if (bytes[i] == 0x3f)
                {
                    num++;
                }
                num++;
            }
            return num;
        }
        #endregion
        #region "字符参数转化"
        /// <summary>
        /// 字符转成数字类型
        /// </summary>
        /// <param name="ParamName">参数名称</param>
        /// <returns></returns>
        public static int GetParamInt(string ParamName)
        {
            return TypeConverter.StrToInt(HttpContext.Current.Request.Params[ParamName], 0);
        }
        /// <summary>
        /// 字符转成数字类型
        /// </summary>
        /// <param name="ParamName">参数名称</param>
        /// <param name="DefValue">默认值</param>
        /// <returns></returns>
        public static int GetParamInt(string ParamName,int DefValue)
        {
            return TypeConverter.StrToInt(HttpContext.Current.Request.Params[ParamName], DefValue);
        }
        #endregion
        #region "枚举类型转换"
        /// <summary>
        /// 枚举类型转换
        /// </summary>
        /// <param name="value">类型值</param>
        /// <returns></returns>
        public static T EnumParse<T>(string value)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            catch
            {
                throw new Exception(string.Format("传入值{0}与枚举值不匹配。", value));
            }
        }
        #endregion
        #region "根据类型转换"
        /// <summary>
        /// 根据类型转换值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static object GetDefaultValue(object obj, Type type)
        {
            try
            {
                if (obj == null || obj == DBNull.Value)
                {
                    obj = default(object);
                }
                else
                {
                    if (type == typeof(String))
                        obj = obj.ToString().Trim();
                    obj = Convert.ChangeType(obj, Nullable.GetUnderlyingType(type) ?? type);
                }
                return obj;
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region "实体数据类型"
        /// <summary>
        /// C#实体数据类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string FindModelsType(string name)
        {
            if (name == "int" || name == "smallint")
            {
                return "int?";
            }
            else if (name == "tinyint")
            {
                return "byte?";
            }
            else if (name == "numeric" || name == "real" || name == "float")
            {
                return "Single?";
            }
            else if (name == "float")
            {
                return "float?";
            }
            else if (name == "decimal")
            {
                return "decimal?";
            }
            else if (name == "char" || name == "varchar" || name == "text" || name == "nchar" || name == "nvarchar" || name == "ntext")
            {
                return "string";
            }
            else if (name == "bit")
            {
                return "bool?";
            }
            else if (name == "datetime" || name == "smalldatetime")
            {
                return "DateTime?";
            }
            else if (name == "money" || name == "smallmoney")
            {
                return "double?";
            }
            else
            {
                return "string";
            }
        }
        #endregion
        #region "获取唯一编码"
        /// <summary>
        /// 获取唯一编码
        /// </summary>
        public static string GetGuID
        {
            get
            {
                System.Guid guid = new Guid();
                guid = Guid.NewGuid();
                return guid.ToString().ToUpper();
            }
        }
        #endregion
        #region "获取扩展属性"
        /// <summary>
        /// 获取扩展属性
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <returns></returns>
        public static KeyAttribute GetAttribute(Type type)
        {
            KeyAttribute _Attribute = (KeyAttribute)Attribute.GetCustomAttribute(type, typeof(KeyAttribute));
            return _Attribute;
        }
        #endregion
        #region "过滤特殊字符"
        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        /// <param name="strJson">Json字符</param>
        /// <returns></returns>
        public static string JsonFilter(string strJson)
        {
            StringBuilder sb = new StringBuilder(strJson);
            sb.Replace("\\", "\\\\");
            sb.Replace("\r", "\\r");
            sb.Replace("\n", "\\n");
            sb.Replace("\"", "\\\""); 
            sb.Replace("'", "\'");
            return sb.ToString();
        }
        #endregion
        #region "过滤特殊符号"
        /// <summary>
        /// 过滤特殊特号(完全过滤)
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string FilterSql(string Str)
        {
            string[] aryReg = { "'", "\"", "\r", "\n", "<", ">", "%", "?", ",", "=", "-", "_", ";", "|", "[", "]", "&", "/" };
            if (!string.IsNullOrEmpty(Str))
            {
                foreach (string str in aryReg)
                {
                    Str = Str.Replace(str, string.Empty);
                }
                return Str;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 过滤单个字符
        /// </summary>
        /// <param name="Str"></param>
        /// <param name="Character"></param>
        /// <returns></returns>
        public static string FilterSql(string Str, string Character)
        {
            if (!string.IsNullOrEmpty(Str))
            {
                Str = Str.Replace(Character, string.Empty);
                return Str;
            }
            else
            {
                return "";
            }
        }
        #endregion
        #region 数字格式判断
        /// <summary>
        /// 数字格式判断
        /// </summary>
        /// <param name="Number">数字</param>
        /// <returns></returns>
        public static bool IsNumber(string Number)
        {
            return Regex.IsMatch(Number, @"^[0-9]*$");
        }
        #endregion
    }
}