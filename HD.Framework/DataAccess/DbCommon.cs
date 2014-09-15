//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using HD.Framework.Utils;

namespace HD.Framework.DataAccess
{
    /// <summary>
    /// 数据库公共帮助类
    /// </summary>
    public class DbCommon
    {
        #region "参数转化"
        /// <summary>
        /// 参数对象
        /// </summary>
        /// <param name="_FieldName">字段名称</param>
        /// <param name="_FieldValue">字段的值</param>
        /// <returns></returns>
        public static SqlParam MakeParam(string _FieldName, object _FieldValue)
        {
            return new SqlParam(_FieldName, _FieldValue);
        }
        /// <summary>
        /// Hashtable对象参数转换
        /// </summary>
        /// <param name="ht">对象</param>
        /// <returns></returns>
        public static SqlParam[] GetParameter(Hashtable ht)
        {
            DbType dbtype = new DbType();
            List<SqlParam> sqlparam = new List<SqlParam>();
            if (ht != null && ht.Count > 0)
            {
                foreach (string key in ht.Keys)
                {
                    if (ht[key] != null)
                    {
                        if (ht[key] is DateTime)
                        {
                            dbtype = DbType.DateTime;
                        }
                        else
                        {
                            dbtype = DbType.AnsiString;
                        }
                        sqlparam.Add(new SqlParam("@" + key, dbtype, ht[key]));
                    }
                }
            }
            return sqlparam.ToArray();
        }
        /// <summary>
        /// 实体类对象参数转换
        /// </summary>
        /// <param name="model">对象</param>
        /// <returns></returns>
        public static SqlParam[] GetParameter(object model)
        {
            DbType dbtype = new DbType();
            Type type = model.GetType();
            PropertyInfo[] props = type.GetProperties();
            List<SqlParam> sqlparam = new List<SqlParam>();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(model, null) != null)
                {
                    if (prop.PropertyType.ToString() == "System.Nullable`1[System.DateTime]")
                    {
                        dbtype = DbType.DateTime;
                    }
                    else
                    {
                        dbtype = DbType.AnsiString;
                    }
                    sqlparam.Add(new SqlParam("@" + prop.Name, dbtype, prop.GetValue(model, null)));
                }
            }
            return sqlparam.ToArray();
        }
        /// <summary>
        /// 实体类对象参数转换
        /// </summary>
        /// <param name="model">对象</param>
        /// <param name="sqlParams">参数</param>
        /// <returns></returns>
        public static SqlParam[] GetParameter(object model,SqlParam[] sqlParams)
        {
            DbType dbtype = new DbType();
            Type type = model.GetType();
            PropertyInfo[] props = type.GetProperties();
            List<SqlParam> sqlparam = new List<SqlParam>();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(model, null) != null)
                {
                    if (prop.PropertyType.ToString() == "System.Nullable`1[System.DateTime]")
                    {
                        dbtype = DbType.DateTime;
                    }
                    else
                    {
                        dbtype = DbType.AnsiString;
                    }
                    sqlparam.Add(new SqlParam("@" + prop.Name, dbtype, prop.GetValue(model, null)));
                }
            }
            if (sqlParams != null)
            {
                foreach (SqlParam param in sqlParams)
                {
                    sqlparam.Add(new SqlParam(param.FieldName, param.FieldValue));
                }
            }
            return sqlparam.ToArray();
        }
        /// <summary>
        /// 实体类对象参数转换
        /// </summary>
        /// <param name="sqlParams">现有参数</param>
        /// <param name="addParams">增加参数</param>
        /// <returns></returns>
        public static SqlParam[] AddParameter(SqlParam[] sqlParams,SqlParam[] addParams)
        {
            List<SqlParam> sqlparam = new List<SqlParam>(sqlParams);
            foreach (SqlParam param in addParams)
            {
                sqlparam.Add(new SqlParam("@" + param.FieldName, param.FieldValue));
            }
            return sqlparam.ToArray();
        }
        #endregion
        #region "记录条数"
        /// <summary>
        /// 记录条数
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        public static string RecordSql(object model)
        {
            return RecordSql(model, true);
        }
        /// <summary>
        /// 记录条数
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="allRecord">所有记录</param>
        /// <returns></returns>
        public static string RecordSql(object model,bool allRecord)
        {
            Type type = model.GetType();
            KeyAttribute Key = Public.GetAttribute(type);
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("Select Count(1) From {0}{1} Where 1=1", Key.Prefix, type.Name);
            if (!allRecord)
            {
                strSql.AppendFormat(" And {0}=@ID", Key.PkName);
            }
            return strSql.ToString();
        }
        /// <summary>
        /// 记录条数
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="ht">对象参数</param>
        /// <returns></returns>
        public static string RecordSql(object model, Hashtable ht)
        {
            Type type = model.GetType();
            KeyAttribute Key = Public.GetAttribute(type);
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("Select Count(1) From {0}{1} Where 1=1", Key.Prefix, type.Name);
            foreach (string key in ht.Keys)
            {
                strSql.Append(" And " + key + "=@" + key + "");
            }
            return strSql.ToString();
        }
        /// <summary>
        /// 记录条数
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="strWhere">条件语句</param>
        /// <returns></returns>
        public static string RecordSql(object model, string where)
        {
            Type type = model.GetType();
            KeyAttribute Key = Public.GetAttribute(type);
            return string.Format("Select Count(1) From {0}{1} Where 1=1 {2}", Key.Prefix, type.Name, where);
        }
        #endregion
        #region "最大数字"
        /// <summary>
        /// 最大数字
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="fieldName">查询字段</param>
        /// <returns></returns>
        public static string MaxIDSql(object model, string fieldName)
        {
            return MaxIDSql(model, fieldName, "");
        }
        /// <summary>
        /// 最大数字
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="fieldName">查询字段</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public static string MaxIDSql(object model, string fieldName, string where)
        {
            Type type = model.GetType();
            KeyAttribute Key = Public.GetAttribute(type);
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("Select Max({0}) From {1}{2} Where 1=1", fieldName, Key.Prefix, type.Name);
            if (!string.IsNullOrEmpty(where))
            {
                strSql.Append(where);
            }
            return strSql.ToString();
        }
        #endregion
        #region "单条记录"
        /// <summary>
        /// 单条记录
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        public static string SingleSql(object model)
        {
            Type type = model.GetType();
            KeyAttribute Key = Public.GetAttribute(type);
            string where = string.Format(" And {0}=@ID", Key.PkName);
            return SingleSql(model, where, null);
        }
        /// <summary>
        /// 单条记录
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="ht">参数列表</param>
        /// <returns></returns>
        public static string SingleSql(object model, Hashtable ht)
        {
            return SingleSql(model, "", ht);
        }
        /// <summary>
        /// 单条记录
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="where">条件语句</param>
        /// <returns></returns>
        public static string SingleSql(object model, string where)
        {
            return SingleSql(model, where, null);
        }
        /// <summary>
        /// 单条记录
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="where">条件语句</param>
        /// <param name="ht">参数条件</param>
        /// <returns></returns>
        public static string SingleSql(object model,string where,Hashtable ht)
        {
            Type type = model.GetType();
            KeyAttribute Key = Public.GetAttribute(type);
            string strSql = "Select{0} * From {1}{2} Where 1=1 {3}{4}";
            string strTop = " Top 1";

            //获取数据库类型
            DataBaseType dbType = DataFactory.GetInstance().DataBaseType;
            switch (dbType)
            {
                case DataBaseType.Oracle:
                case DataBaseType.MySql:
                case DataBaseType.Sqlite:
                    strSql = "Select * From {1}{2} Where 1=1 {3}{4}{0}";
                    strTop = " LIMIT 1";
                    break;
            }

            //增加查询条件
            StringBuilder sbKey = new StringBuilder();
            if (ht!=null && ht.Count > 0)
            {
                foreach (string key in ht.Keys)
                {
                    sbKey.AppendFormat(" And {0}=@{0}", key);
                }
            }
            
            //返回查询语句
            return string.Format(strSql, strTop, Key.Prefix, type.Name, where, sbKey.ToString());
        }
        #endregion
        #region "多条记录"
        /// <summary>
        /// 多条记录
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="where">条件语句</param>
        /// <param name="orderFields">排序字段</param>
        /// <returns></returns>
        public static string MultipleSql(object model, string where, string orderFields)
        {
            return MultipleSql(model, "*", where, orderFields);
        }
        /// <summary>
        /// 所有记录
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="fields">显示字段</param>
        /// <param name="where">条件语句</param>
        /// <param name="orderFields">排序字段</param>
        /// <returns></returns>
        public static string MultipleSql(object model, string fields, string where, string orderFields)
        {
            return MultipleSql(model, -1, fields, where, orderFields);
        }
        /// <summary>
        /// 多条记录
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="number">记录条数</param>
        /// <param name="fields">显示字段</param>
        /// <param name="where">条件语句</param>
        /// <param name="orderFields">排序字段及方式</param>
        /// <returns></returns>
        public static string MultipleSql(object model, int number, string fields, string where, string orderFields)
        {
            Type type = model.GetType();
            KeyAttribute Key = Public.GetAttribute(type);
            string strSql = "Select {0}{1} From {2}{3} Where 1=1{4}{5}";
            string topNumber = string.Format(" Top {0} ", number);

            //获取数据库类型
            DataBaseType dbType = DataFactory.GetInstance().DataBaseType;
            switch (dbType)
            {
                case DataBaseType.Oracle:
                case DataBaseType.MySql:
                case DataBaseType.Sqlite:
                    strSql = "Select {1} From {2}{3} Where 1=1{4}{5}{0}";
                    topNumber = string.Format(" LIMIT {0}", number);
                    break;
            }
            
            //显示所有记录
            if (number == -1) 
                topNumber = "";
            //显示所有字段
            if (string.IsNullOrEmpty(fields)) 
                fields = "*";
            if (!string.IsNullOrEmpty(orderFields))
                orderFields = string.Format(" Order By {0}", orderFields);

            //返回查询语句
            return string.Format(strSql, topNumber, fields, Key.Prefix, type.Name, where, orderFields);
        }
        #endregion
        #region "插入数据"
        /// <summary>
        /// 插入语句
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        public static string InsertSql(object model)
        {
            Type type = model.GetType();
            PropertyInfo[] props = type.GetProperties();
            KeyAttribute Key = Public.GetAttribute(type);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Insert Into {0}{1} (", Key.Prefix, type.Name);
            StringBuilder sp = new StringBuilder();
            StringBuilder sb_prame = new StringBuilder();

            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(model, null) != null)
                {
                    sb_prame.Append("," + prop.Name);
                    sp.Append(",@" + prop.Name);
                }
            }
            sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") Values (");
            sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")");
            return sb.ToString();
        }
        /// <summary>
        /// 插入语句
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="ht">参数</param>
        /// <returns></returns>
        public static string InsertSql(object model, Hashtable ht)
        {
            Type type = model.GetType();
            KeyAttribute Key = Public.GetAttribute(type);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Insert Into {0}{1} (", Key.Prefix, type.Name);

            StringBuilder sp = new StringBuilder();
            StringBuilder sb_prame = new StringBuilder();
            foreach (string key in ht.Keys)
            {
                if (ht[key] != null)
                {
                    sb_prame.Append("," + key);
                    sp.Append(",@" + key);
                }
            }
            sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") Values (");
            sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")");
            return sb.ToString();
        }
        #endregion
        #region "更新数据"
        /// <summary>
        /// 更新语句
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        public static string UpdateSql(object model)
        {
            Type type = model.GetType();
            PropertyInfo[] props = type.GetProperties();
            KeyAttribute Key = Public.GetAttribute(type);
            
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Update {0}{1} Set ", Key.Prefix, type.Name);

            StringBuilder sbKey = new StringBuilder();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(model, null) != null)
                {
                    if (prop.Name.CompareTo(Key.PkName) != 0)
                    {
                        sbKey.AppendFormat("{0}=@{1},", prop.Name, prop.Name);
                    }
                }
            }
            sb.Append(sbKey.ToString().TrimEnd(','));
            sb.Append(" Where ").Append(Key.PkName).Append("=").Append("@" + Key.PkName);
            return sb.ToString();
        }
        /// <summary>
        /// 更新语句
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="where">条件语句</param>
        /// <returns></returns>
        public static string UpdateSql(object model, string where)
        {
            Type type = model.GetType();
            PropertyInfo[] props = type.GetProperties();
            KeyAttribute Key = Public.GetAttribute(type);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Update {0}{1} Set ", Key.Prefix, type.Name);

            StringBuilder sbKey = new StringBuilder();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(model, null) != null)
                {
                    if (prop.Name.CompareTo(Key.PkName) != 0)
                    {
                        sbKey.AppendFormat("{0}=@{1},", prop.Name, prop.Name);
                    }
                }
            }
            sb.Append(sbKey.ToString().TrimEnd(','));
            sb.AppendFormat(" Where 1=1 {0}", where);
            return sb.ToString();
        }
        /// <summary>
        /// 更新语句
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="fields">更新字段</param>
        /// <param name="where">条件语句</param>
        /// <returns></returns>
        public static string UpdateSql(object model, string fields, string where)
        {
            Type type = model.GetType();
            KeyAttribute Key = Public.GetAttribute(type);
            return string.Format("Update {0}{1} Set {2} Where 1=1 {3}", Key.Prefix, type.Name, fields, where);
        }
        #endregion
        #region "删除数据"
        /// <summary>
        /// 删除语句
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        public static string DeleteSql(object model)
        {
            Type type = model.GetType();
            KeyAttribute Key = Public.GetAttribute(type);
            return string.Format("Delete From {0}{1} Where {2}=@ID", Key.Prefix, type.Name, Key.PkName);
        }
        /// <summary>
        /// 删除语句
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="ht">参数条件</param>
        /// <returns></returns>
        public static string DeleteSql(object model, Hashtable ht)
        {
            Type type = model.GetType();
            KeyAttribute Key = Public.GetAttribute(type);
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("Delete From {0}{1} Where 1=1", Key.Prefix, type.Name);
            foreach (string key in ht.Keys)
            {
                strSql.AppendFormat(" And {0}=@{0}", key);
            }
            return strSql.ToString();
        }
        /// <summary>
        /// 删除语句
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="where">条件语句</param>
        /// <returns></returns>
        public static string DeleteSql(object model,string where)
        {
            Type type = model.GetType();
            KeyAttribute Key = Public.GetAttribute(type);
            return string.Format("Delete From {0}{1} Where 1=1 {2}",Key.Prefix,type.Name,where);
        }
        #endregion
    }
}