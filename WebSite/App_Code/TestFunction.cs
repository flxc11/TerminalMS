using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.App_Code
{
    public class TestFunction
    {
        /// <summary>
        /// 转换为Int
        /// </summary>
        /// <param name="value">要转换的文本</param>
        /// <returns>转换后的值</returns>
        public int ToInt(string value)
        {
            int result = 0;
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (!int.TryParse(value, out result))
                {
                    throw new Exception("文本内容无法转换为Int类型。");
                }
            }
            else
            {
                throw new Exception("文本不能为空。");
            }
            return result;
        }
    }
}