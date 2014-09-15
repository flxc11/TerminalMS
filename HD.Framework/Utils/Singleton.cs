using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Framework.Utils
{
    /// <summary>
    /// 单例模式类
    /// </summary>
    public class Singleton<T> where T : new()
    {
        /// <summary>
        /// 对象用于锁
        /// </summary>
        private static readonly object locker = new object();
        /// <summary>
        /// 自身对象定义
        /// </summary>
        [ThreadStatic]
        private static T _instance;
        /// <summary>
        /// 取得自身实例
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    return _instance = new T();
                }
                else
                {
                    lock (locker)
                    {
                        return _instance;
                    }
                }
            }
        }
    }
}