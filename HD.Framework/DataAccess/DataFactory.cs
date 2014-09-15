using HD.Framework.Helper;
//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Framework.DataAccess
{
    /// <summary>
    /// 数据库服务工厂
    /// </summary>
    public class DataFactory
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        [ThreadStatic]
        private static IDataBase _DbHelper;
        /// <summary>
        /// 对象用于锁
        /// </summary>
        private static object locker = new object();
        /// <summary>
        /// 数据库实例
        /// </summary>
        /// <returns>数据库对象</returns>
        public static IDataBase GetInstance()
        {
            lock (locker)
            {
                if (_DbHelper != null)
                {
                    return _DbHelper;
                }
                else
                {
                    return _DbHelper = new DataBase("DbConn");
                }
            }
        }
        /// <summary>
        /// 数据库实例
        /// </summary>
        /// <param name="connectionStringsName">数据库链接名称</param>
        /// <returns></returns>
        public static IDataBase GetInstance(string connectionStringsName)
        {
            return new DataBase(connectionStringsName);
        }
    }
}