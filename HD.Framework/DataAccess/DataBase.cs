//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Text;
using HD.Framework.Helper;
using System.Collections;
using HD.Framework.Utils;

namespace HD.Framework.DataAccess
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    public class DataBase : IDataBase
    {
        #region "属性定义"
        /// <summary>
        /// 数据库链接
        /// </summary>
        private readonly string _connectionString;
        /// <summary>
        /// 数据库链接
        /// </summary>
        public string ConnectionString { get { return _connectionString; } }
        /// <summary>
        /// 数据库驱动
        /// </summary>
        private readonly string _providerName;
        /// <summary>
        /// 数据库驱动
        /// </summary>
        public string ProviderName { get { return _providerName; } }
        /// <summary>
        /// 数据库参数
        /// </summary>
        private string _paramFormat = "";
        /// <summary>
        /// 数据库类型
        /// </summary>
        private DataBaseType _dataBaseType;
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DataBaseType DataBaseType { get { return _dataBaseType; } }
        /// <summary>
        /// 数据库工厂
        /// </summary>
        private DbProviderFactory _factory;
        /// <summary>
        /// 数据库链接
        /// </summary>
        private IDbConnection _connection;
        /// <summary>
        /// 数据库链接
        /// </summary>
        public IDbConnection Connection { get { return _connection; } }
        /// <summary>
        /// 事务对象
        /// </summary>
        private IDbTransaction _transaction;
        /// <summary>
        /// 事务对象
        /// </summary>
        public IDbTransaction Transaction { get { return _transaction; } }
        /// <summary>
        /// 事务锁定行为
        /// </summary>
        private IsolationLevel _isolationLevel;
        #endregion
        #region "构造函数"
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionStringsName">数据库链接名称</param>
        public DataBase(string connectionStringsName)
            : this(ConfigHelper.GetConnectionString(connectionStringsName),
                   ConfigHelper.GetProviderName(connectionStringsName))
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">数据库链接</param>
        /// <param name="providerName">数据库驱动</param>
        public DataBase(string connectionString, string providerName)
        {
            _connectionString = connectionString;
            _providerName = providerName;
            _factory = GetDbProviderFactory(_providerName);
            _isolationLevel = IsolationLevel.ReadCommitted;
        }
        #endregion
        #region "链接方法"
        /// <summary>
        /// 保持链接
        /// </summary>
        public bool KeepConnectionAlive { get; set; }
        /// <summary>
        /// 自动关闭
        /// </summary>
        private bool ShouldCloseConnection { get; set; }
        /// <summary>
        /// 打开链接
        /// </summary>
        private void OpenShareConnection()
        {
            //可以嵌套多个语句
            OpenSharedConnectionImpl(false);
        }
        /// <summary>
        /// 打开链接
        /// </summary>
        private void OpenSharedConnectionInternal()
        {
            //完成自动注销对象
            OpenSharedConnectionImpl(true);
        }
        /// <summary>
        /// 打开链接
        /// </summary>
        /// <param name="isShouldClose">是否关闭链接</param>
        private void OpenSharedConnectionImpl(bool isShouldClose)
        {
            if (_connection != null && _connection.State != ConnectionState.Broken && _connection.State != ConnectionState.Closed)
                return;

            ShouldCloseConnection = isShouldClose;
            _connection = _factory.CreateConnection();
            if (_connection == null) throw new Exception("SQL属性配置错误，请检查Connection.config文件。");

            _connection.ConnectionString = _connectionString;
            if (_connection.State == ConnectionState.Broken)
            {
                _connection.Close();
            }
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }

            _connection = OnConnectionOpened(_connection);
        }
        /// <summary>
        /// 打开链接
        /// </summary>
        /// <param name="dbConnection">数据库链接</param>
        /// <returns></returns>
        protected virtual IDbConnection OnConnectionOpened(IDbConnection dbConnection)
        {
            return dbConnection;
        }
        /// <summary>
        /// 关闭链接
        /// </summary>
        private void CloseSharedConnectionInternal()
        {
            if (ShouldCloseConnection && _transaction == null)
                CloseSharedConnection();
        }
        /// <summary>
        /// 关闭链接
        /// </summary>
        public void CloseSharedConnection()
        {
            if (KeepConnectionAlive) return;
            if (_connection == null) return;

            OnConnectionClosing(_connection);

            _connection.Close();
            _connection.Dispose();
            _connection = null;
        }
        /// <summary>
        /// 关闭链接
        /// </summary>
        /// <param name="dbConnection">数据库链接</param>
        protected virtual void OnConnectionClosing(IDbConnection dbConnection)
        {

        }
        #endregion
        #region "事务方法"
        /// <summary>
        /// 创建事务
        /// </summary>
        /// <returns></returns>
        public Transaction GetTransaction()
        {
            return GetTransaction(_isolationLevel);
        }
        /// <summary>
        /// 创建事务
        /// </summary>
        /// <param name="isolationLevel">事务方法</param>
        /// <returns></returns>
        public Transaction GetTransaction(IsolationLevel isolationLevel)
        {
            return new Transaction(this, isolationLevel);
        }
        /// <summary>
        /// 设置事务
        /// </summary>
        /// <param name="tran">事务对象</param>
        public void SetTransaction(IDbTransaction tran)
        {
            _transaction = tran;
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        protected virtual void OnBeginTransaction()
        {

        }
        /// <summary>
        /// 事务回滚
        /// </summary>
        protected virtual void OnAbortTransaction()
        {

        }
        /// <summary>
        /// 完成事务
        /// </summary>
        protected virtual void OnCompleteTransaction()
        {

        }
        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTransaction()
        {
            BeginTransaction(_isolationLevel);
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="isolationLevel"></param>
        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (_transaction == null)
            {
                TransactionCount = 0;
                OpenSharedConnectionInternal();
                _transaction = _connection.BeginTransaction(isolationLevel);
                OnBeginTransaction();
            }
            if (_transaction != null)
            {
                TransactionCount++;
            }
        }
        /// <summary>
        /// 事务回滚
        /// </summary>
        public void AbortTransaction()
        {
            AbortTransaction(false);
        }
        /// <summary>
        /// 事务回滚
        /// </summary>
        /// <param name="fromComplete"></param>
        public void AbortTransaction(bool fromComplete)
        {
            if (_transaction == null)
                return;

            if (fromComplete == false)
            {
                TransactionCount--;
                if (TransactionCount >= 1)
                {
                    TransactionIsAborted = true;
                    return;
                }
            }

            if (TransactionIsOk())
                _transaction.Rollback();

            if (_transaction != null)
                _transaction.Dispose();

            _transaction = null;
            TransactionIsAborted = false;

            if (_connection != null)
            {
                _connection.Close();
                _connection.Open();
            }

            OnAbortTransaction();
            CloseSharedConnectionInternal();
        }
        /// <summary>
        /// 事务完成
        /// </summary>
        public void CompleteTransaction()
        {
            if (_transaction == null)
                return;

            TransactionCount--;
            if (TransactionCount >= 1)
                return;

            if (TransactionIsAborted)
            {
                AbortTransaction(true);
                return;
            }

            if (TransactionIsOk())
                _transaction.Commit();

            if (_transaction != null)
                _transaction.Dispose();

            _transaction = null;

            OnCompleteTransaction();
            CloseSharedConnectionInternal();
        }
        /// <summary>
        /// 是否回滚
        /// </summary>
        internal bool TransactionIsAborted { get; set; }
        /// <summary>
        /// 事务数量
        /// </summary>
        internal int TransactionCount { get; set; }
        /// <summary>
        /// 事务完成
        /// </summary>
        /// <returns></returns>
        private bool TransactionIsOk()
        {
            return _connection != null
                && _transaction != null
                && _transaction.Connection != null
                && _transaction.Connection.State == ConnectionState.Open;
        }
        #endregion
        #region "服务工厂"
        /// <summary>
        /// 数据工厂
        /// </summary>
        /// <returns></returns>
        private DbProviderFactory GetDbProviderFactory(string providerName)
        {
            switch (ProviderName)
            {
                case "System.Data.SqlClient":
                    _paramFormat = "@";
                    _dataBaseType = DataBaseType.SqlServer;
                    return DbProviderFactories.GetFactory("System.Data.SqlClient");
                case "System.Data.OracleClient":
                    _paramFormat = ":";
                    _dataBaseType = DataBaseType.Oracle;
                    return DbProviderFactories.GetFactory("System.Data.SqlClient");
                case "Oracle.DataAccess.Client":
                    _paramFormat = ":";
                    _dataBaseType = DataBaseType.Oracle;
                    return DbProviderFactories.GetFactory("Oracle.DataAccess.Client");
                case "MySql.Data.MySQLClient":
                    _paramFormat = "?";
                    _dataBaseType = DataBaseType.MySql;
                    return DbProviderFactories.GetFactory("MySql.Data.MySQLClient");
                case "System.Data.SQLite":
                    _paramFormat = "@";
                    _dataBaseType = DataBaseType.Sqlite;
                    return DbProviderFactories.GetFactory("System.Data.SQLite");
                case "System.Data.OleDb":
                    _paramFormat = "?";
                    _dataBaseType = DataBaseType.Access;
                    return DbProviderFactories.GetFactory("System.Data.OleDb");
                default:
                    _paramFormat = "@";
                    _dataBaseType = DataBaseType.SqlServer;
                    return DbProviderFactories.GetFactory("System.Data.SqlClient");
            }
            throw new Exception(string.Format("数据库驱动{0}类型不正确。", providerName));
        }
        #endregion
        #region "构造参数"
        /// <summary>
        /// 参数对象
        /// </summary>
        /// <returns></returns>
        public IDataParameter CreateParameter()
        {
            using (var conn = Connection ?? _factory.CreateConnection())
            {
                if (conn == null) throw new Exception("数据库链接处于断开状态");
                using (var command = conn.CreateCommand())
                {
                   return command.CreateParameter();
                }
            }
        }
        /// <summary>
        /// 加入参数
        /// </summary>
        /// <param name="cmd">命令对象</param>
        /// <param name="fieldName">参数名称</param>
        /// <param name="fieldValue">参数的值</param>
        private void AddParam(IDbCommand cmd, string fieldName, object fieldValue)
        {
            //fieldName = fieldName.TrimStart('@', ':', '?');
            fieldName = fieldName.Replace("@", _paramFormat);

            var param = cmd.CreateParameter();
            param.ParameterName = fieldName;
            param.Value = fieldValue;
            cmd.Parameters.Add(param);
        }
        /// <summary>
        /// 构造参数
        /// </summary>
        /// <param name="fieldName">目标字段</param>
        /// <param name="fieldValue">字段的值</param>
        /// <returns></returns>
        public SqlParam MakeParam(string fieldName, object fieldValue)
        {
            return new SqlParam(fieldName, fieldValue);
        }
        /// <summary>
        /// 构造命令
        /// </summary>
        /// <param name="connection">数据库链接</param>
        /// <param name="commandText">命令语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        IDbCommand CreateCommand(IDbConnection connection,string commandText,SqlParam[] sqlParams, CommandType commandType)
        {
            commandText = commandText.Replace("@", _paramFormat);

            IDbCommand cmd = connection.CreateCommand();
            cmd.Connection = connection;
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            cmd.Transaction = _transaction;

            if (sqlParams != null)
            {
                foreach (SqlParam param in sqlParams)
                {
                    if (param != null && param.FieldValue != null)
                    {
                        AddParam(cmd, param.FieldName, param.FieldValue);
                    }
                }
            }
            return cmd;
        }
        /// <summary>
        /// 创建数据适配器
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <returns></returns>
        IDataAdapter CreateDataAdapter(IDbCommand command)
        {
            IDbDataAdapter adaper = _factory.CreateDataAdapter();
            adaper.SelectCommand = command;
            return adaper;
        }
        #endregion
        #region "首行首列"
        /// <summary>
        /// 执行Sql语句，返回首行首列
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(commandText, (SqlParam[])null);
        }
        /// <summary>
        /// 执行Sql语句，返回首行首列
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText, SqlParam[] sqlParams)
        {
            return ExecuteScalar(commandText, sqlParams, CommandType.Text);
        }
        /// <summary>
        /// 执行Sql语句，返回首行首列
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText, SqlParam[] sqlParams, CommandType commandType)
        {
            try
            {
                OpenSharedConnectionInternal();
                using (var command = CreateCommand(_connection, commandText, sqlParams, commandType))
                {
                    object Value = command.ExecuteScalar();
                    return Value;
                }
            }
            catch (Exception x)
            {
                throw;
            }
            finally
            {
                CloseSharedConnectionInternal();
            }
        }
        #endregion
        #region "无返回值"
        /// <summary>
        /// 执行Sql语句，返回受影响的记录数
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(commandText, (SqlParam[])null);
        }
        /// <summary>
        /// 执行Sql语句，返回受影响的记录数
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText,SqlParam[] sqlParams)
        {
            return ExecuteNonQuery(commandText,sqlParams,CommandType.Text);
        }
        /// <summary>
        /// 执行Sql语句，返回受影响的记录数
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText,SqlParam[] sqlParams,CommandType commandType)
        {
            try
            {
                OpenSharedConnectionInternal();
                using (var command = CreateCommand(_connection, commandText, sqlParams,commandType))
                {
                    var result = command.ExecuteNonQuery();
                    return result;
                }
            }
            catch (Exception x)
            {
                throw;
            }
            finally
            {
                CloseSharedConnectionInternal();
            }
        }
        #endregion
        #region "单条记录"
        /// <summary>
        /// 执行查询语句，返回单条实体
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(commandText, (SqlParam[])null);
        }
        /// <summary>
        /// 执行查询语句，返回单条实体
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string commandText, SqlParam[] sqlParams)
        {
            return ExecuteReader(commandText, sqlParams, CommandType.Text);
        }
        /// <summary>
        /// 执行查询语句，返回单条实体
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string commandText, SqlParam[] sqlParams, CommandType commandType)
        {
            try
            {
                OpenSharedConnectionInternal();
                using (var command = CreateCommand(_connection, commandText, sqlParams, commandType))
                {
                    IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    return reader;
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                CloseSharedConnectionInternal();
            }
        }
        /// <summary>
        /// 执行查询语句，返回IDataReader
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="commandText">Sql语句</param>
        /// <returns></returns>
        public T ExecuteReader<T>(string commandText)
        {
            return ExecuteReader<T>(commandText, (SqlParam[])null);
        }
        /// <summary>
        /// 执行查询语句，返回IDataReader
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="commandText">Sql语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <returns></returns>
        public T ExecuteReader<T>(string commandText, SqlParam[] sqlParams)
        {
            return ExecuteReader<T>(commandText, sqlParams, CommandType.Text);
        }
        /// <summary>
        /// 执行查询语句，返回IDataReader
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="commandText">Sql语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <param name="commandType">命令对象</param>
        /// <returns></returns>
        public T ExecuteReader<T>(string commandText, SqlParam[] sqlParams, CommandType commandType)
        {
            IDataReader reader = ExecuteReader(commandText, sqlParams, commandType);
            return DbReader.ReaderToModel<T>(DbReader.ReaderToDataTable(reader).Rows[0]);
        }
        #endregion
        #region "多条记录"
        /// <summary>
        /// 执行Sql语句，返回记录集
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns></returns>
        public DataTable ExecuteTable(string commandText)
        {
            return ExecuteTable(commandText, (SqlParam[])null);
        }
        /// <summary>
        /// 执行Sql语句，返回记录集
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <returns></returns>
        public DataTable ExecuteTable(string commandText, SqlParam[] sqlParams)
        {
            return ExecuteTable(commandText, sqlParams, CommandType.Text);
        }
        /// <summary>
        /// 执行Sql语句，返回记录集
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public DataTable ExecuteTable(string commandText, SqlParam[] sqlParams, CommandType commandType)
        {
            try
            {
                OpenSharedConnectionInternal();
                using (var command = CreateCommand(_connection, commandText, sqlParams, commandType))
                {
                    IDataAdapter adapter = CreateDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                CloseSharedConnectionInternal();
            }
        }
        /// <summary>
        /// 执行Sql语句，返回实体类
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="commandText">Sql语句</param>
        /// <returns></returns>
        public IList ExecuteTable<T>(string commandText)
        {
            return ExecuteTable<T>(commandText, (SqlParam[])null);
        }
        /// <summary>
        /// 执行Sql语句，返回实体类
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="commandText">Sql语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <returns></returns>
        public IList ExecuteTable<T>(string commandText, SqlParam[] sqlParams)
        {
            return ExecuteTable<T>(commandText, sqlParams, CommandType.Text);
        }
        /// <summary>
        /// 执行Sql语句，返回实体类
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="commandText">Sql语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public IList ExecuteTable<T>(string commandText, SqlParam[] sqlParams, CommandType commandType)
        {
            DataTable Dt = ExecuteTable(commandText, sqlParams, commandType);
            return DbReader.DataTableToIList<T>(Dt);
        }
        #endregion
        #region "分页集合"
        /// <summary>
        /// 执行Sql语句，返回分页数据
        /// </summary>
        /// <param name="sqlAllFields">查询字段，支持多表查询</param>
        /// <param name="sqlTablesAndWhere">查询的表如果包含查询条件一并带上</param>
        /// <param name="indexField">用以分页的不能重复的索引字段名</param>
        /// <param name="orderFields">排序字段以及排序方式</param>
        /// <param name="pageIndex">当前的页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="pageCount">分页总数</param>
        /// <returns></returns>
        public DataTable ExecutePage(string sqlAllFields, string sqlTablesAndWhere, string indexField, string orderFields, int pageIndex, int pageSize, ref int recordCount, ref int pageCount)
        {
            return ExecutePage(sqlAllFields, sqlTablesAndWhere, indexField, orderFields, pageIndex, pageSize, ref recordCount, ref pageCount, (SqlParam[])null);
        }
        /// <summary>
        /// 执行Sql语句，返回分页数据
        /// </summary>
        /// <param name="sqlAllFields">查询字段，支持多表查询</param>
        /// <param name="sqlTablesAndWhere">查询的表如果包含查询条件一并带上</param>
        /// <param name="indexField">用以分页的不能重复的索引字段名</param>
        /// <param name="orderFields">排序字段以及排序方式</param>
        /// <param name="pageIndex">当前的页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="pageCount">分页总数</param>
        /// <param name="sqlParams">对象参数</param>
        /// <returns></returns>
        public DataTable ExecutePage(string sqlAllFields, string sqlTablesAndWhere, string indexField, string orderFields, int pageIndex, int pageSize, ref int recordCount, ref int pageCount, SqlParam[] sqlParams)
        {
            try
            {
                string RecordSql = DbPager.GetRecordSql(_dataBaseType, sqlTablesAndWhere);
                recordCount = TypeConverter.ObjectToInt(ExecuteScalar(RecordSql, sqlParams), 0);

                if (pageSize <= 0)
                {
                    pageSize = 10;
                }
                if (recordCount % pageSize == 0)
                {
                    pageCount = recordCount / pageSize;
                }
                else
                {
                    pageCount = recordCount / pageSize + 1;
                }
                if (pageIndex > pageCount)
                {
                    pageIndex = pageCount;
                }
                if (pageIndex < 1)
                {
                    pageIndex = 1;
                }

                string commandText = DbPager.GetPageSql(_dataBaseType, sqlAllFields, sqlTablesAndWhere, indexField, orderFields, pageIndex, pageSize);

                OpenSharedConnectionInternal();
                using (var command = CreateCommand(_connection, commandText, sqlParams, CommandType.Text))
                {
                    IDataAdapter adapter = CreateDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds.Tables[0];
                }
            }
            catch (Exception x)
            {
                throw;
            }
            finally
            {
                CloseSharedConnectionInternal();
            }
        }
        /// <summary>
        /// 执行Sql语句，返回分页数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="sqlAllFields">查询字段</param>
        /// <param name="sqlTablesAndWhere">查询的表如果包含查询条件一并带上</param>
        /// <param name="indexField">用以分页的不能重复的索引字段名</param>
        /// <param name="orderFields">排序字段以及排序方式</param>
        /// <param name="pageIndex">当前的页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="pageCount">分页总数</param>
        /// <returns></returns>
        public IList ExecutePage<T>(string sqlAllFields, string sqlTablesAndWhere, string indexField, string orderFields, int pageIndex, int pageSize, ref int recordCount, ref int pageCount)
        {
            return ExecutePage<T>(sqlAllFields, sqlTablesAndWhere, indexField, orderFields, pageIndex, pageSize, ref recordCount, ref pageCount, (SqlParam[])null);
        }
        /// <summary>
        /// 执行Sql语句，返回分页数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="sqlAllFields">查询字段</param>
        /// <param name="sqlTablesAndWhere">查询的表如果包含查询条件一并带上</param>
        /// <param name="indexField">用以分页的不能重复的索引字段名</param>
        /// <param name="orderFields">排序字段以及排序方式</param>
        /// <param name="pageIndex">当前的页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="pageCount">分页总数</param>
        /// <param name="sqlParams">对象参数</param>
        /// <returns></returns>
        public IList ExecutePage<T>(string sqlAllFields, string sqlTablesAndWhere, string indexField, string orderFields, int pageIndex, int pageSize, ref int recordCount, ref int pageCount, SqlParam[] sqlParams)
        {
            DataTable Dt = ExecutePage(sqlAllFields, sqlTablesAndWhere, indexField, orderFields, pageIndex, pageSize, ref recordCount, ref pageCount, sqlParams);
            return DbReader.DataTableToIList<T>(Dt);
        }
        #endregion
        #region "释放资源"
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (KeepConnectionAlive) return;
            CloseSharedConnection();
        }
        #endregion
    }
}