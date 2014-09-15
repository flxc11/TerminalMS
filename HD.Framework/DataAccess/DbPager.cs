//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HD.Framework.DataAccess
{
    /// <summary>
    /// 数据库分页函数
    /// </summary>
    public class DbPager
    {
        /// <summary> 
        /// 是否为SqlServer2000版本
        /// </summary>
        public static bool IsSql2000
        {
            get
            {
                bool flg = false;
                string StrSql = "SELECT ServerProperty('ProductVersion') AS Version";
                string Version = DataFactory.GetInstance().ExecuteScalar(StrSql).ToString();
                Regex reg = new Regex(@"\d+", RegexOptions.Compiled);
                Match m = reg.Match(Version);
                if (m.Success)
                {
                    Version = m.Groups[0].Value;
                    if (Version == "8")
                    {
                        flg = true;
                    }
                }
                return flg;
            }
        }
        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="sqlTablesAndWhere">查询的表如果包含查询条件一并带上</param>
        /// <returns></returns>
        public static string GetRecordSql(DataBaseType dbType, string sqlTablesAndWhere)
        {
            string StrSql = string.Empty;
            switch (dbType)
            {
                case DataBaseType.Oracle:
                    StrSql = string.Format("SELECT Count(1) FROM {0} t", sqlTablesAndWhere);
                    break;
                case DataBaseType.SqlServer:
                    StrSql = string.Format("SELECT Count(1) FROM {0}", sqlTablesAndWhere);
                    break;
                case DataBaseType.MySql:
                    StrSql = string.Format("SELECT Count(1) FROM {0} t", sqlTablesAndWhere);
                    break;
                case DataBaseType.Access:
                    StrSql = string.Format("SELECT Count(1) FROM {0}", sqlTablesAndWhere);
                    break;
                case DataBaseType.Sqlite:
                    StrSql = string.Format("SELECT Count(1) FROM {0} t", sqlTablesAndWhere);
                    break;
            }
            return StrSql;
        }
        /// <summary>
        /// 生成分页语句
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="sqlAllFields">查询字段，支持多表查询</param>
        /// <param name="sqlTablesAndWhere">查询的表如果包含查询条件一并带上</param>
        /// <param name="indexField">用以分页的不能重复的索引字段名</param>
        /// <param name="orderFields">排序字段以及排序方式</param>
        /// <param name="pageIndex">当前的页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        public static string GetPageSql(DataBaseType dbType, string sqlAllFields, string sqlTablesAndWhere, string indexField, string orderFields, int pageIndex, int pageSize)
        {
            switch (dbType)
            {
                case DataBaseType.SqlServer:
                    return GetPageSqlServer(sqlAllFields, sqlTablesAndWhere, indexField, orderFields, pageIndex, pageSize);
                default:
                    return "分页算法暂未实现。";
            }
        }
        /// <summary>
        /// Oracle分页语句
        /// </summary>
        /// <param name="sqlAllFields">查询字段，支持多表查询</param>
        /// <param name="sqlTablesAndWhere">查询的表如果包含查询条件一并带上</param>
        /// <param name="indexField">用以分页的不能重复的索引字段名</param>
        /// <param name="orderFields">排序字段以及排序方式</param>
        /// <param name="pageIndex">当前的页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        private static string GetPageOracle(string sqlAllFields, string sqlTablesAndWhere, string indexField, string orderFields, int pageIndex, int pageSize)
        {
            return null;
        }
        /// <summary>
        /// SqlServer分页算法
        /// </summary>
        /// <param name="sqlAllFields">查询字段，支持多表查询</param>
        /// <param name="sqlTablesAndWhere">查询的表如果包含查询条件一并带上</param>
        /// <param name="indexField">用以分页的不能重复的索引字段名</param>
        /// <param name="orderFields">排序字段以及排序方式</param>
        /// <param name="pageIndex">当前的页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        private static string GetPageSqlServer(string sqlAllFields, string sqlTablesAndWhere, string indexField, string orderFields, int pageIndex, int pageSize)
        {
            string StrSql = string.Empty;
            if (IsSql2000)
            {
                #region "Sql2000分页算法"
                if (pageIndex == 1)
                {
                    StrSql = "SELECT TOP " + pageSize + " " + sqlAllFields + " FROM " + sqlTablesAndWhere + " Order By " + orderFields;
                }
                else
                {
                    StrSql = "SELECT TOP " + pageSize + " " + sqlAllFields + " FROM ";
                    if (sqlTablesAndWhere.ToUpper().IndexOf(" WHERE ") > 0)
                    {
                        string _where = Regex.Replace(sqlTablesAndWhere, @"\ WHERE\ ", " WHERE (", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                        StrSql += _where + ") AND (";
                    }
                    else
                    {
                        StrSql += sqlTablesAndWhere + " WHERE (";
                    }
                    StrSql += indexField + " NOT IN (SELECT TOP " + (pageIndex - 1) * pageSize + " " + indexField + " FROM " + sqlTablesAndWhere + " Order By " + orderFields;
                    StrSql += ")) Order By " + orderFields;
                }
                #endregion
            }
            else
            {
                #region "Sql2005分页算法"
                int StarIndex = (pageIndex - 1) * pageSize;
                int EndIndex = (pageIndex) * pageSize;
                StrSql = string.Format("SELECT * FROM (SELECT *,ROW_NUMBER() OVER (ORDER BY {0}) as RowNum FROM {1}) as T WHERE RowNum >{2} And RowNum<={3}", orderFields, sqlTablesAndWhere, StarIndex, EndIndex);
                return StrSql;
                #endregion
            }
            return StrSql;
        }

        /// <summary>
        /// MySql分页语句
        /// </summary>
        /// <param name="sqlAllFields">查询字段，支持多表查询</param>
        /// <param name="sqlTablesAndWhere">查询的表如果包含查询条件一并带上</param>
        /// <param name="indexField">用以分页的不能重复的索引字段名</param>
        /// <param name="orderFields">排序字段以及排序方式</param>
        /// <param name="pageIndex">当前的页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        private static string GetPageMySql(string sqlAllFields, string sqlTablesAndWhere, string indexField, string orderFields, int pageIndex, int pageSize)
        {
            return null;
        }
        /// <summary>
        /// Access分页语句
        /// </summary>
        /// <param name="sqlAllFields">查询字段，支持多表查询</param>
        /// <param name="sqlTablesAndWhere">查询的表如果包含查询条件一并带上</param>
        /// <param name="indexField">用以分页的不能重复的索引字段名</param>
        /// <param name="orderFields">排序字段以及排序方式</param>
        /// <param name="pageIndex">当前的页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        private static string GetPageAccess(string sqlAllFields, string sqlTablesAndWhere, string indexField, string orderFields, int pageIndex, int pageSize)
        {
            return null;
        }
        /// <summary>
        /// SQLite分页语句
        /// </summary>
        /// <param name="sqlAllFields">查询字段，支持多表查询</param>
        /// <param name="sqlTablesAndWhere">查询的表如果包含查询条件一并带上</param>
        /// <param name="indexField">用以分页的不能重复的索引字段名</param>
        /// <param name="orderFields">排序字段以及排序方式</param>
        /// <param name="pageIndex">当前的页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        private static string GetPageSQLite(string sqlAllFields, string sqlTablesAndWhere, string indexField, string orderFields, int pageIndex, int pageSize)
        {
            return null;
        }
    }
}