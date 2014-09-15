//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;

namespace HD.Framework.Aop
{
    /// <summary>
    /// AOP代理类
    /// </summary>
    public class AspectProxy : RealProxy
    {
        private Type serverType;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serverType"></param>
        public AspectProxy(Type serverType)
            : base(serverType)
        {
            this.serverType = serverType;
        }
        /// <summary>
        /// 函数消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override IMessage Invoke(IMessage msg)
        {
            IMessage message;

            var callMessage = msg as IConstructionCallMessage;

            if (callMessage != null)
            {
                message = InitializeServerObject(callMessage);
                if (message != null)
                {
                    SetStubData(this, ((IConstructionReturnMessage)message).ReturnValue);
                }
            }
            else
            {
                var callMsg = (IMethodCallMessage)msg;
                var attributes = serverType.GetMethod(callMsg.MethodName).GetCustomAttributes(false);
                var args = callMsg.Args;

                try
                {
                    OnBegin(attributes, callMsg);
                    var ret = callMsg.MethodBase.Invoke(GetUnwrappedServer(), args);
                    OnComplete(attributes, ref ret, callMsg);
                    message = new ReturnMessage(ret, args, args.Length, callMsg.LogicalCallContext, callMsg);
                }
                catch (Exception e)
                {
                    OnException(attributes, e.InnerException, callMsg);
                    message = new ReturnMessage(e.InnerException, callMsg);
                }
            }
            return message;
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="message"></param>
        private static void OnBegin(IEnumerable<object> attributes, IMethodMessage message)
        {
            foreach (var attr in attributes)
            {
                if (attr.GetType() == typeof(TransactionAttribute))
                {
                    var tran = attr as TransactionAttribute;
                    if (tran != null) tran.OnBegin();
                }
            }
        }
        /// <summary>
        /// 完成事务
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="ret"></param>
        /// <param name="message"></param>
        private void OnComplete(IEnumerable<object> attributes, ref object ret, IMethodMessage message)
        {
            foreach (var attr in attributes)
            {
                if (attr.GetType() == typeof(TransactionAttribute))
                {
                    var tran = attr as TransactionAttribute;
                    if (tran != null) tran.OnComplete();
                }
            }
        }
        /// <summary>
        /// 事务回滚
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        private void OnException(IEnumerable<object> attributes, Exception exception, IMethodMessage message)
        {
            foreach (var attr in attributes)
            {
                if (attr.GetType() == typeof(TransactionAttribute))
                {
                    var tran = attr as TransactionAttribute;
                    if (tran != null) tran.OnException();
                }
            }
        }
    }
}