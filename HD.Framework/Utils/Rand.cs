//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Framework.Utils
{
    /// <summary>
    /// 随机函数
    /// </summary>
    public class Rand
    {
        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <returns></returns>
        public static string Number(int Length)
        {
            System.Threading.Thread.Sleep(3);
            string result = "";
            System.Random random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < Length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }
        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <returns></returns>
        public static string Character(int Length)
        {
            System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }
        /// <summary>
        /// 生成随机纯字母随机数
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <returns></returns>
        public static string Letter(int Length)
        {
            System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }
        /// <summary>
        /// 日期随机函数
        /// </summary>
        /// <returns></returns>
        public static string DateRndName()
        {
            System.Threading.Thread.Sleep(3);
            Random ra = new Random(DateTime.Now.Millisecond);
            string s = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            s += ra.Next(100, 999).ToString();
            return s;
        }
    }
}