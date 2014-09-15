//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Proxies;
using System.Text;

namespace HD.Framework.Aop
{
    /// <summary>
    /// AOP扩展属性
    /// </summary>
    public class AspectAttribute : ProxyAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AspectAttribute()
        {
 
        }
        /// <summary>
        /// 跨程序调用方法
        /// </summary>
        /// <param name="serverType"></param>
        /// <returns></returns>
        public override MarshalByRefObject CreateInstance(Type serverType)
        {
            var realProxy = new AspectProxy(serverType);
            return realProxy.GetTransparentProxy() as MarshalByRefObject;
        }
    }
}