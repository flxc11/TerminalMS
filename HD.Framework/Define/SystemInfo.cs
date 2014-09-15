//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Framework.Define
{
    /// <summary>
    /// 系统信息类
    /// </summary>
    [Serializable]
    public class SystemInfo
    {
        /// <summary>
        /// 登录序号
        /// </summary>
        public string LoginID { get; set; }
        /// <summary>
        /// 登录帐号
        /// </summary>
        public string LoginName { get; set; }
    }
}