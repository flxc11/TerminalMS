//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HD.Framework.Utils
{
    /// <summary>
    /// 类型转换类
    /// </summary>
    public class TypeConverter
    {
        #region "字符转成布尔"
        /// <summary>
        /// 将对象转成布尔类型
        /// </summary>
        /// <param name="StrValue">要转换的字符串</param>
        /// <param name="DefValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(object StrValue, bool DefValue)
        {
            if (StrValue != null)
            {
                return StrToBool(StrValue, DefValue);
            }
            return DefValue;
        }
        /// <summary>
        /// 将字符转成布尔类型
        /// </summary>
        /// <param name="StrValue">要转换的字符串</param>
        /// <param name="DefValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(string StrValue, bool DefValue)
        {
            if (!string.IsNullOrEmpty(StrValue))
            {
                if (string.Compare(StrValue, "true", true) == 0)
                {
                    return true;
                }
                else if (string.Compare(StrValue, "false", true) == 0)
                {
                    return false;
                }
            }
            return DefValue;
        }
        #endregion
        #region "对象转成数字"
        /// <summary>
        /// 将对象转换为数字类型
        /// </summary>
        /// <param name="StrValue">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ObjectToInt(object StrValue)
        {
            return ObjectToInt(StrValue, 0);
        }
        /// <summary>
        /// 将对象转换为数字类型
        /// </summary>
        /// <param name="StrValue">要转换的字符串</param>
        /// <param name="DefValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ObjectToInt(object StrValue, int DefValue)
        {
            if (StrValue != null)
            {
                return StrToInt(StrValue.ToString(), DefValue);
            }
            return DefValue;
        }
        /// <summary>
        /// 将字符转换成数字类型
        /// </summary>
        /// <param name="StrValue">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(string StrValue)
        {
            return StrToInt(StrValue, 0);
        }
        /// <summary>
        /// 将字符转换成数字
        /// </summary>
        /// <param name="Str">要转换的字符串</param>
        /// <param name="DefValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(string StrValue, int DefValue)
        {
            if (string.IsNullOrEmpty(StrValue) || StrValue.Trim().Length >= 11 || !Regex.IsMatch(StrValue.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
            {
                return DefValue;
            }
            int Result;
            if (Int32.TryParse(StrValue, out Result))
            {
                return Result;
            }
            return Convert.ToInt32(StrToFloat(StrValue, DefValue));
        }
        #endregion
        #region "对象转成浮点"
        /// <summary>
        /// 将对象转成浮点类型
        /// </summary>
        /// <param name="StrValue">要转换的字符串</param>
        /// <param name="DefValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(object StrValue, float DefValue)
        {
            if ((StrValue == null))
            {
                return DefValue;
            }
            return StrToFloat(StrValue.ToString(), DefValue);
        }
        /// <summary>
        /// 将对象转成浮点类型
        /// </summary>
        /// <param name="StrValue">要转换的字符串</param>
        /// <param name="DefValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float ObjectToFloat(object StrValue, float DefValue)
        {
            if ((StrValue == null))
            {
                return DefValue;
            }
            return StrToFloat(StrValue.ToString(), DefValue);
        }
        /// <summary>
        /// 将对象转成浮点类型
        /// </summary>
        /// <param name="StrValue">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static float ObjectToFloat(object StrValue)
        {
            return ObjectToFloat(StrValue.ToString(), 0);
        }
        /// <summary>
        /// 将字符转成浮点类型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(string StrValue)
        {
            if ((StrValue == null))
            {
                return 0;
            }
            return StrToFloat(StrValue.ToString(), 0);
        }
        /// <summary>
        /// 将字符转成浮点类型
        /// </summary>
        /// <param name="Str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(string StrValue, float DefValue)
        {
            if ((StrValue == null) || (StrValue.Length > 10))
            {
                return DefValue;
            }
            float IntValue = DefValue;
            if (StrValue != null)
            {
                bool IsFloat = Regex.IsMatch(StrValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                {
                    float.TryParse(StrValue, out IntValue);
                }
            }
            return IntValue;
        }
        #endregion
        #region "对象转成日期"
        /// <summary>
        /// 将对象转成日期时间类型
        /// </summary>
        /// <param name="StrValue">要转换的字符串</param>
        /// <param name="DefValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime StrToDateTime(string StrValue, DateTime DefValue)
        {
            if (!string.IsNullOrEmpty(StrValue))
            {
                DateTime dateTime;
                if (DateTime.TryParse(StrValue, out dateTime))
                {
                    return dateTime;
                }
            }
            return DefValue;
        }
        /// <summary>
        /// 将字符转成日期时间类型
        /// </summary>
        /// <param name="StrValue">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime StrToDateTime(string StrValue)
        {
            return StrToDateTime(StrValue, DateTime.Now);
        }
        /// <summary>
        /// 将对象转成日期时间类型
        /// </summary>
        /// <param name="StrValue">要转换的对象</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime ObjToDateTime(object StrValue)
        {
            return StrToDateTime(StrValue.ToString());
        }
        /// <summary>
        /// 将对象转成日期时间类型
        /// </summary>
        /// <param name="StrValue">要转换的对象</param>
        /// <param name="DefValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime ObjectToDateTime(object StrValue, DateTime DefValue)
        {
            return StrToDateTime(StrValue.ToString(), DefValue);
        }
        #endregion
    }
}