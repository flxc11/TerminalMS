//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Text;
using HD.Framework.Helper;

namespace HD.Framework.Utils
{
    /// <summary>
    /// 扩展属性类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class KeyAttribute : Attribute
    {
        #region "属性定义"
        /// <summary>
        /// 主键名称
        /// </summary>
        private string _pkName;
        /// <summary>
        /// 主键名称
        /// </summary>
        public string PkName
        {
            get
            {
                return _pkName;
            }
            set
            {
                _pkName = value;
            }
        }
        /// <summary>
        /// 表的前缀
        /// </summary>
        private string _prefix;
        /// <summary>
        /// 表的前缀
        /// </summary>
        public string Prefix
        {
            get
            {
                return _prefix;
            }
            set
            {
                _prefix = value;
            }
        }
        #endregion
        #region "构造函数"
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="primarykey">主键名称</param>
        public KeyAttribute(string primarykey)
            : this(primarykey,ConfigHelper.GetValue("Prefix"))
        { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pkName">主键名称</param>
        /// <param name="prefix">表的前缀</param>
        public KeyAttribute(string pkName,string prefix)
        {
            _pkName = pkName;
            _prefix = prefix;
        }
        #endregion
    }
}