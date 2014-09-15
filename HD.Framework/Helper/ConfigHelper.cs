//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web;
using System.Xml;
using HD.Framework.Utils;
using System.IO;

namespace HD.Framework.Helper
{
    /// <summary>
    /// 配置信息类
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfigHelper()
        {

        }
        /// <summary>
        /// 获取数据库链接
        /// </summary>
        /// <param name="Key">键值</param>
        /// <returns></returns>
        public static string GetConnectionString(string Key)
        {
            if (ConfigurationManager.ConnectionStrings[Key] != null)
            {
                return ConfigurationManager.ConnectionStrings[Key].ConnectionString;
            }
            throw new Exception(string.Format("获取数据库链接时找不到值为{0}的节点。", Key));
        }
        /// <summary>
        /// 获取数据库驱动
        /// </summary>
        /// <param name="Key">键值</param>
        /// <returns></returns>
        public static string GetProviderName(string Key)
        {
            if (ConfigurationManager.ConnectionStrings[Key] != null)
            {
                return ConfigurationManager.ConnectionStrings[Key].ProviderName;
            }
            throw new Exception(string.Format("获取数据库驱动时找不到值为{0}的节点。", Key));
        }
        /// <summary>
        /// 根据Key获取Value值
        /// </summary>
        /// <param name="Key">键值</param>
        /// <returns></returns>
        public static string GetValue(string Key)
        {
            string Value = string.Empty;
            try
            {
                if (ConfigurationManager.AppSettings[Key] != null)
                {
                    Value = ConfigurationManager.AppSettings[Key].ToString().Trim();
                }
            }
            catch (Exception e)
            {
                string LogContent = string.Format("键值[{0}]获取失败，错误原因：{1}", Key, e.Message.ToString());
                LogHelper.WriteLog(LogContent);
            }
            return Value;
        }
        /// <summary>
        /// 根据Key修改Value
        /// </summary>
        /// <param name="Key">要修改的Key</param>
        /// <param name="Value">要修改的Value值</param>
        public static void SetValue(string Key, string Value)
        {
            SetValue("~/XmlConfig/Config.config", Key, Value);
        }
        /// <summary>
        /// 根据Key修改Value
        /// </summary>
        /// <param name="ConfigFile">配置文件地址</param>
        /// <param name="Key">要修改的Key</param>
        /// <param name="Value">要修改的Value值</param>
        public static void SetValue(string ConfigFile,string Key,string Value)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(Public.GetMapPath(ConfigFile));
                XmlNode xNode;
                XmlElement xElem1, xElem2;
                xNode = xDoc.SelectSingleNode("//appSettings");

                xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key='" + Key + "']");
                if (xElem1 != null)
                {
                    //存在节点直接更新
                    xElem1.SetAttribute("value", Value);
                }
                else
                {
                    xElem2 = xDoc.CreateElement("add");
                    xElem2.SetAttribute("key", Key);
                    xElem2.SetAttribute("value", Value);
                    xNode.AppendChild(xElem2);
                }
                xDoc.Save(Public.GetMapPath(ConfigFile));
            }
            catch (Exception e)
            {
                string LogContent = string.Format("键值[{0}]保存失败，错误原因：{1}", Key, e.Message.ToString());
                LogHelper.WriteLog(LogContent);
            }
        }
    }
}