//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;

namespace HD.Framework.Utils
{
    /// <summary>
    /// 分页函数类
    /// </summary>
    public class Pager
    {
        #region "分页函数"
        /// <summary>
        /// DataTable分页函数
        /// </summary>
        /// <param name="Dt">记录集合</param>
        /// <param name="PageSize">分页条数</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="RecordCount">记录总数</param>
        /// <param name="PageCount">分页数量</param>
        /// <returns></returns>
        public static DataTable GetPageTable(DataTable Dt, int PageSize, int PageIndex, ref int RecordCount, ref int PageCount)
        {
            if (PageIndex == 0)
            {
                return Dt;
            }
            DataTable newdt = Dt.Copy();
            newdt.Clear();

            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= Dt.Rows.Count)
            {
                return newdt;
            }
            if (rowend > Dt.Rows.Count)
            {
                rowend = Dt.Rows.Count;
            }
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = Dt.Rows[i];
                foreach (DataColumn column in Dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }
            //记录总数
            RecordCount = Dt.Rows.Count;
            PageCount = RecordCount / PageSize;
            if (RecordCount % PageSize >0)
            {
                PageCount = PageCount +1;
            }
            return newdt;
        }
        #endregion
        #region "分页算法"
        /// <summary>
        /// 页面参数
        /// </summary>
        /// <returns></returns>
        private static string QueryUrl()
        {
            Regex _Regex = new Regex(@"^&PageIndex=\d+", RegexOptions.Compiled);
            string Str = HttpContext.Current.Request.Url.Query.Replace("?", "&");
            Str = _Regex.Replace(Str, string.Empty);
            return Str;
        }
        /// <summary>
        /// 分页函数
        /// </summary>
        /// <param name="RecordCount">记录总数</param>
        /// <param name="PageCount">页面总数</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="PageIndex">当前页面</param>
        /// <returns></returns>
        public static string GetPageNormal(int RecordCount, int PageCount, int PageSize, int PageIndex)
        {
            string Str = "";

            Str = "共" + RecordCount + "条记录 页次：" + PageIndex + "/" + PageCount + "页 ";
            Str += PageSize + "条/页 ";

            if (Convert.ToInt32(PageIndex) < 2)
            {
                Str += "首页 上页 ";
            }
            else
            {
                Str += "<a href=\"?PageIndex=1" + QueryUrl() + "\">首页</a> ";
                Str += "<a href=\"?PageIndex=" + (PageIndex - 1) + QueryUrl() + "\">上页</a> ";
            }
            if (PageCount - Convert.ToInt32(PageIndex) < 1)
            {
                Str += "下页 尾页 ";
            }
            else
            {
                Str += "<a href=\"?PageIndex=" + (PageIndex + 1) + QueryUrl() + "\">下页</a> ";
                Str += "<a href=\"?PageIndex=" + PageCount + QueryUrl() + "\">尾页</a>  ";
            }
            return Str;
        }
        /// <summary>
        /// 分页函数
        /// </summary>
        /// <param name="RecordCount">记录总数</param>
        /// <param name="PageCount">分页总数</param>
        /// <param name="PageSize">每个条数</param>
        /// <param name="PageIndex">当前页码</param>
        /// <returns></returns>
        public static string GetPageGoogle(int RecordCount, int PageCount, int PageSize, int PageIndex)
        {
            int Next, Pre, StartCount, EndCount = 0;
            if (PageIndex < 1) { PageIndex = 1; }
            Next = PageIndex + 1;
            Pre = PageIndex - 1;
            StartCount = (PageIndex + 5) > PageCount ? PageCount - 9 : PageIndex - 1;
            EndCount = PageIndex < 5 ? 10 : PageIndex + 5;
            if (StartCount < 1) { StartCount = 1; }
            if (PageCount < EndCount) { EndCount = PageCount; }
            string Str = "";
            Str += "共" + RecordCount + "条记录，共" + PageCount + "页&nbsp;";
            Str += PageIndex > 1 ? "<a href=\"?PageIndex=1" + QueryUrl() + "\">首页</a>&nbsp;<a href=\"?PageIndex=" + Pre + QueryUrl() + "\">上一页</a>" : "首页 上一页";
            for (int i = StartCount; i <= EndCount; i++)
            {
                Str += PageIndex == i ? "&nbsp;<font color=\"#ff0000\">" + i + "</font>" : "&nbsp;<a href=\"?PageIndex=" + i + QueryUrl() + "\">" + i + "</a>";
            }
            Str += PageIndex != PageCount ? "&nbsp;<a href=\"?PageIndex=" + Next + QueryUrl() + "\">下一页</a>&nbsp;<a href=\"?PageIndex=" + PageCount + QueryUrl() + "\">末页</a>" : " 下一页 末页";
            return Str;
        }
        #endregion
    }
}