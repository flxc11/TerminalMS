//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HD.Framework.DataAccess
{
    /// <summary>
    /// 数据库参数
    /// </summary>
    public class SqlParam
    {
        /// <summary>
        /// 目标字段
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public DbType DataType { get; set; }
        /// <summary>
        /// 字段的值
        /// </summary>
        public object FieldValue { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlParam()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_FieldName">目标字段</param>
        /// <param name="_FieldValue">字段的值</param>
        public SqlParam(string _FieldName, object _FieldValue)
            : this(_FieldName, DbType.AnsiString, _FieldValue)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_FieldName">目标字段</param>
        /// <param name="_DbType">数据类型</param>
        /// <param name="_FieldValue">字段的值</param>
        public SqlParam(string _FieldName, DbType _DbType, object _FieldValue)
        {
            this.FieldName = _FieldName;
            this.DataType = _DbType;
            this.FieldValue = _FieldValue;
        }
    }
}