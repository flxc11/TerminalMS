using System;
using System.Collections.Generic;
using System.Text;
using HD.Framework.Helper;

namespace HD.Framework.Utils
{
    /// <summary>
    /// 操作WebService助手类
    /// </summary>
    public class WebServiceUtils
    {
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="AppID">站点GUID</param>
        /// <param name="AppKey">通讯密钥</param>
        /// <param name="WebServiceUrl">服务地址</param>
        /// <param name="Mothod">服务名称</param>
        /// <param name="args">例如:string[] args = new string[1];args[0] = "Test";</param>
        /// <returns></returns>
        public static object GetMothod(string AppID, string AppKey, string WebServiceUrl, string Mothod, object[] args)
        {
            WebServiceHelper.SoapHeader Header = new WebServiceHelper.SoapHeader("AuthSoapHeader");
            Header.AddProperty("AppID", AppID);
            Header.AddProperty("AppKey", AppKey);

            object Obj = WebServiceHelper.InvokeWebService(WebServiceUrl, Mothod, Header, args);
            return Obj;
        }
    }
}