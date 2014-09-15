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
    /// 数据库事务类
    /// </summary>
    public class Transaction : IDisposable
    {
        DataBase _dbHelper;
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="database">数据库对象</param>
        /// <param name="isolationLevel">事务方法</param>
        public Transaction(DataBase database, IsolationLevel isolationLevel)
        {
            _dbHelper = database;
            _dbHelper.BeginTransaction(isolationLevel);
        }
        /// <summary>
        /// 事务提交
        /// </summary>
        public virtual void Complete()
        {
            _dbHelper.CompleteTransaction();
            _dbHelper = null;
        }
        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            if (_dbHelper != null)
            {
                _dbHelper.TransactionIsAborted = true;
                _dbHelper.AbortTransaction();
            }
        }
    }
}