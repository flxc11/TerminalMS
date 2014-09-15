using HD.Framework.DataAccess;
//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Framework.Aop
{
    /// <summary>
    /// 执行事务扩展
    /// </summary>
    public class TransactionAttribute : Attribute
    {
        /// <summary>
        /// 数据库实例
        /// </summary>
        private readonly IDataBase _dbHelper;
        /// <summary>
        /// 事务扩展属性
        /// </summary>
        public TransactionAttribute()
        {
            _dbHelper = DataFactory.GetInstance();
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        public void OnBegin()
        {
            _dbHelper.BeginTransaction();
        }
        /// <summary>
        /// 完成事务
        /// </summary>
        public void OnComplete()
        {
            _dbHelper.CompleteTransaction();
        }
        /// <summary>
        /// 事务回滚
        /// </summary>
        public void OnException()
        {
            _dbHelper.AbortTransaction();
        }
    }
}