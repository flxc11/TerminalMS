//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Framework.Helper
{
    /// <summary>
    /// 头文件格式
    /// </summary>
    public class AuthSoapHeader
    {
        /// <summary>
        /// 应用序号
        /// </summary>
        public string AppID { get; set; }
        /// <summary>
        /// 应用密钥
        /// </summary>
        public string AppKey { get; set; }
    }
}