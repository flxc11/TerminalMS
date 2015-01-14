//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;
using HD.Framework.Utils;

namespace HD.Framework.Helper
{
    /// <summary>
    /// 转换Json格式帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 对象转Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ObjectToJson<T>(T t)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string json = "";
                if (t != null)
                {
                    sb.Append("{");
                    PropertyInfo[] properties = t.GetType().GetProperties();
                    foreach (PropertyInfo pi in properties)
                    {
                        sb.Append("\"" + pi.Name.ToString() + "\"");
                        sb.Append(":");
                        sb.Append("\"" + pi.GetValue(t, null) + "\"");
                        sb.Append(",");
                    }
                    json = sb.ToString().TrimEnd(',');
                    json += "}";
                }
                return json;
            }
            catch (Exception ex)
            {
                string LogContent = string.Format("[JsonHelper][{0}]", ex.Message);
                LogHelper.WriteLog(LogContent);
                return "";
            }
        }
        /// <summary>
        /// IList转Json
        /// </summary>
        /// <param name="jsonName"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DropToJson<T>(IList list, string jsonName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        T obj = Activator.CreateInstance<T>();
                        PropertyInfo[] pi = obj.GetType().GetProperties();
                        sb.Append("{");
                        for (int j = 0; j < pi.Length; j++)
                        {
                            sb.Append("\"");
                            sb.Append(pi[j].Name.ToString());
                            sb.Append("\":\"");
                            if (pi[j].GetValue(list[i], null) != null && pi[j].GetValue(list[i], null) != DBNull.Value && pi[j].GetValue(list[i], null).ToString() != "")
                            {
                                sb.Append(pi[j].GetValue(list[i], null)).Replace("\\", "/");
                            }
                            else
                            {
                                sb.Append("");
                            }
                            sb.Append("\",");
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append("},");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                string LogContent = string.Format("[JsonHelper][{0}]", ex.Message);
                LogHelper.WriteLog(LogContent);
                return "";
            }
        }
        /// <summary>
        /// DataTable转Json
        /// </summary>
        /// <param name="dt">table数据集</param>
        /// <param name="dtName">json名</param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dt, string dtName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"");
                sb.Append(dtName);
                sb.Append("\":[");
                if (dt!= null && dt.Rows.Count>0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.Append("{");
                        foreach (DataColumn dc in dr.Table.Columns)
                        {
                            sb.Append("\"");
                            sb.Append(dc.ColumnName);
                            sb.Append("\":\"");
                            if (dr[dc] != null && dr[dc] != DBNull.Value && dr[dc].ToString() != "")
                                sb.Append(dr[dc]).Replace("\\", "/");
                            else
                                sb.Append("");
                            sb.Append("\",");
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append("},");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]}");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                string LogContent = string.Format("[JsonHelper][{0}]", ex.Message);
                LogHelper.WriteLog(LogContent);
                return "";
            }
        }
        /// <summary>
        /// IList转Json
        /// </summary>
        /// <param name="dt">IList</param>
        /// <param name="dtName">json名</param>
        /// <returns></returns>
        public static string ListToJson<T>(IList list, string dtName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"");
                sb.Append(dtName);
                sb.Append("\":[");
                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        T obj = Activator.CreateInstance<T>();
                        PropertyInfo[] pi = obj.GetType().GetProperties();
                        sb.Append("{");
                        for (int j = 0; j < pi.Length; j++)
                        {
                            sb.Append("\"");
                            sb.Append(pi[j].Name.ToString());
                            sb.Append("\":\"");
                            if (pi[j].GetValue(list[i], null) != null && pi[j].GetValue(list[i], null) != DBNull.Value && pi[j].GetValue(list[i], null).ToString() != "")
                            {
                                sb.Append(pi[j].GetValue(list[i], null)).Replace("\\", "/");
                            }
                            else
                            {
                                sb.Append("");
                            }
                            sb.Append("\",");
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append("},");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]}");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                string LogContent = string.Format("[JsonHelper][{0}]", ex.Message);
                LogHelper.WriteLog(LogContent);
                return "";
            }
        }

        
        
        /// <summary>
        /// 返回表格Json格式
        /// </summary>
        /// <param name="fields">显示字段</param>
        /// <returns></returns>
        public static string GridTable(DataTable dt, string fields)
        {
            try
            {
                string[] field = fields.Split(',');
                StringBuilder sb = new StringBuilder();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.Append("<tr>");
                        foreach (string item in field)
                        {
                            if (dr[item] != null && dr[item] != DBNull.Value && dr[item].ToString() != "")
                                sb.Append("<td>" + dr[item] + "</td>");
                            else
                                sb.Append("<td></td>");
                        }
                        sb.Append("</tr>");
                    }
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                string LogContent = string.Format("[JsonHelper][{0}]", ex.Message);
                LogHelper.WriteLog(LogContent);
                return "";
            }
        }
        #region "转换数据格式"
        /// <summary>
        /// PqGrid列表格式
        /// </summary>
        /// <param name="dt">数据集合</param>
        /// <param name="fields">显示字段</param>
        /// <returns></returns>
        public static string PqGridJson(DataTable dt, string fields)
        {
            try
            {
                string[] field = fields.Split(',');
                StringBuilder sb = new StringBuilder();
                if (dt != null && dt.Rows.Count > 0)
                {
                    sb.Append("[");
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.Append("[");
                        foreach (string item in field)
                        {
                            sb.Append("\"");
                            if (dr[item] != null && dr[item] != DBNull.Value && dr[item].ToString() != "")
                                sb.Append(dr[item]);
                            else
                                sb.Append("");
                            sb.Append("\",");
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append("],");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                    sb.Append("]");
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                string LogContent = string.Format("[JsonHelper][{0}]", ex.Message);
                LogHelper.WriteLog(LogContent);
                return "";
            }
        }
        /// <summary>
        /// PqGrid列表格式
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="list">数据集合</param>
        /// <param name="fields">显示字段</param>
        /// <returns></returns>
        public static string PqGridJson<T>(IList list, string fields)
        {
            try
            {
                string[] field = fields.Split(',');
                StringBuilder sb = new StringBuilder();
                if (list.Count > 0)
                {
                    sb.Append("[");
                    foreach (T model in list)
                    {
                        Hashtable ht = HashTableHelper.GetModelToHashtable<T>(model);
                        sb.Append("[");
                        foreach (string item in field)
                        {
                            sb.Append("\"");
                            if (ht[item] != null && ht[item] != DBNull.Value && ht[item].ToString() != "")
                                sb.Append(Public.JsonFilter(ht[item].ToString()));
                            else
                                sb.Append("");
                            sb.Append("\",");
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append("],");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                    sb.Append("]");
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                string LogContent = string.Format("[JsonHelper][{0}]", ex.Message);
                LogHelper.WriteLog(LogContent);
                return "";
            }
        }        
        /// <summary>
        /// PqGrid分页格式
        /// </summary>
        /// <param name="dt">数据行</param>
        /// <param name="pageIndex">当前页面</param>
        /// <param name="fields">显示字段</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string PqGridPageJson(DataTable dt, int pageIndex, string fields, int count)
        {
            try
            {
                string[] field = fields.Split(',');
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                sb.Append("\"totalRecords\": " + count + ",");
                sb.Append("\"curPage\": " + pageIndex + ",");
                sb.Append("\"data\": [");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.Append("[");
                        foreach (string item in field)
                        {
                            sb.Append("\"");
                            if (dr[item] != null && dr[item] != DBNull.Value && dr[item].ToString() != "")
                                sb.Append(dr[item]);
                            else
                                sb.Append("");
                            sb.Append("\",");
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append("],");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]");
                sb.Append("}");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                string LogContent = string.Format("[JsonHelper][{0}]", ex.Message);
                LogHelper.WriteLog(LogContent);
                return "";
            }
        }
        /// <summary>
        /// PqGird分页格式
        /// </summary>
        /// <param name="dt">数据行</param>
        /// <param name="pageIndex">当前页面</param>
        /// <param name="pqGrid_Sort">要显示字段</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string PqGridPageJson<T>(IList list, int pageIndex, string pqGrid_Sort, int count)
        {
            try
            {
                string[] Sort_Field = pqGrid_Sort.Split(',');
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                sb.Append("\"totalRecords\": " + count + ",");
                sb.Append("\"curPage\": " + pageIndex + ",");
                sb.Append("\"data\": [");
                if (list.Count > 0)
                {
                    foreach (T entity in list)
                    {
                        Hashtable ht = HashTableHelper.GetModelToHashtable<T>(entity);
                        sb.Append("[");
                        foreach (string item in Sort_Field)
                        {
                            sb.Append("\"");
                            if (ht[item] != null && ht[item] != DBNull.Value && ht[item].ToString() != "")
                                sb.Append(ht[item]);
                            else
                                sb.Append("");
                            sb.Append("\",");
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append("],");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]");
                sb.Append("}");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                string LogContent = string.Format("[JsonHelper][{0}]", ex.Message);
                LogHelper.WriteLog(LogContent);
                return "";
            }
        }
        #endregion

        #region EasyUI 转换数据格式
        /// <summary>
        /// EasyUI 分页格式
        /// </summary>
        /// <param name="dt">数据行</param>
        /// <param name="count">总条数</param>
        /// <returns></returns>
        public static string EasyGridTable(DataTable dt, string fields, int count)
        {
            try
            {
                string[] field = fields.Split(',');
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                sb.Append("\"total\": " + count + ",");
                sb.Append("\"rows\": [");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.Append("{");
                        foreach (string item in field)
                        {
                            sb.Append("\"");
                            sb.Append(item);
                            sb.Append("\":");
                            sb.Append("\"");
                            if (dr[item] != null && dr[item] != DBNull.Value && dr[item].ToString() != "")
                                sb.Append(dr[item].ToString().Replace("\r\n", "").Replace("  ", "").Replace("　", "").Replace("	",""));
                            else
                                sb.Append("");
                            sb.Append("\",");
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append("},");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]");
                sb.Append("}");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                string LogContent = string.Format("[JsonHelper][{0}]", ex.Message);
                LogHelper.WriteLog(LogContent);
                return "";
            }
        }
        #endregion

    }
}