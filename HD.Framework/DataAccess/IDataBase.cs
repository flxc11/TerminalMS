//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HD.Framework.DataAccess
{
    /// <summary>
    /// 数据库操作接口
    /// </summary>
    public interface IDataBase : IDisposable, IDataBaseQuery,IDataBaseConfig
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        IDataParameter CreateParameter();
        Transaction GetTransaction();
        Transaction GetTransaction(IsolationLevel isolationLevel);
        void SetTransaction(IDbTransaction tran);
        void BeginTransaction();
        void BeginTransaction(IsolationLevel isolationLevel);
        void AbortTransaction();
        void CompleteTransaction();
    }
    public interface IDataBaseQuery
    {
        SqlParam MakeParam(string fieldName, object fieldValue);
        object ExecuteScalar(string commandText);
        object ExecuteScalar(string commandText, SqlParam[] sqlParams);
        object ExecuteScalar(string commandText, SqlParam[] sqlParams, CommandType commandType);
        int ExecuteNonQuery(string commandText);
        int ExecuteNonQuery(string commandText, SqlParam[] sqlParams);
        int ExecuteNonQuery(string commandText, SqlParam[] sqlParams, CommandType commandType);
        IDataReader ExecuteReader(string commandText);
        IDataReader ExecuteReader(string commandText, SqlParam[] sqlParams);
        IDataReader ExecuteReader(string commandText, SqlParam[] sqlParams, CommandType commandType);
        T ExecuteReader<T>(string commandText);
        T ExecuteReader<T>(string commandText, SqlParam[] sqlParams);
        T ExecuteReader<T>(string commandText, SqlParam[] sqlParams, CommandType commandType);
        DataTable ExecuteTable(string commandText);
        DataTable ExecuteTable(string commandText, SqlParam[] sqlParams);
        DataTable ExecuteTable(string commandText, SqlParam[] sqlParams, CommandType commandType);
        IList ExecuteTable<T>(string commandText);
        IList ExecuteTable<T>(string commandText, SqlParam[] sqlParams);
        IList ExecuteTable<T>(string commandText, SqlParam[] sqlParams, CommandType commandType);
        DataTable ExecutePage(string sqlAllFields, string sqlTablesAndWhere, string indexField, string orderFields, int pageIndex, int pageSize, ref int recordCount, ref int pageCount);
        DataTable ExecutePage(string sqlAllFields, string sqlTablesAndWhere, string indexField, string orderFields, int pageIndex, int pageSize, ref int recordCount, ref int pageCount, SqlParam[] sqlParams);
        IList ExecutePage<T>(string sqlAllFields, string sqlTablesAndWhere, string indexField, string orderFields, int pageIndex, int pageSize, ref int recordCount, ref int pageCount);
        IList ExecutePage<T>(string sqlAllFields, string sqlTablesAndWhere, string indexField, string orderFields, int pageIndex, int pageSize, ref int recordCount, ref int pageCount, SqlParam[] sqlParams);
    }
    /// <summary>
    /// 数据库属性接口
    /// </summary>
    public interface IDataBaseConfig
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        DataBaseType DataBaseType { get; }
        /// <summary>
        /// 数据库链接
        /// </summary>
        string ConnectionString { get; }
    }
}