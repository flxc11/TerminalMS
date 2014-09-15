using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Framework.Define
{
    [Serializable]
    public class UserInfo
    {
        /// <summary>
        /// 登录序号
        /// </summary>
        public string UserLoginID { get; set; }
        /// <summary>
        /// 登录帐号
        /// </summary>
        public string UserLoginName { get; set; }
    }
}
