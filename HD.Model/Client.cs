using System;
using System.Text;
using System.ComponentModel;
using HD.Framework.DataAccess;
using HD.Framework.Utils;

namespace HD.Model
{
    [Description("客户表")]
    [Key("ClientGuid")]
    public class Client : DbUtils<Client>
    {
        private int? _Id = null;
        /// <summary>  
        /// Id  
        /// </summary>  
        /// <returns></returns>  
        [Description("Id")]
        public int? Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }
        private string _ClientGuid = null;
        /// <summary>  
        /// Guid  
        /// </summary>  
        /// <returns></returns>  
        [Description("ClientGuid")]
        public string ClientGuid
        {
            get
            {
                return this._ClientGuid;
            }
            set
            {
                this._ClientGuid = value;
            }
        }
        private string _ClientName = null;
        /// <summary>  
        /// 客户名称  
        /// </summary>  
        /// <returns></returns>  
        [Description("客户名称")]
        public string ClientName
        {
            get
            {
                return this._ClientName;
            }
            set
            {
                this._ClientName = value;
            }
        }
        private string _Tel = null;
        /// <summary>  
        /// 电话  
        /// </summary>  
        /// <returns></returns>  
        [Description("电话")]
        public string Tel
        {
            get
            {
                return this._Tel;
            }
            set
            {
                this._Tel = value;
            }
        }
        private string _Operator = null;
        /// <summary>  
        /// 客户方经办人  
        /// </summary>  
        /// <returns></returns>  
        [Description("客户方经办人")]
        public string Operator
        {
            get
            {
                return this._Operator;
            }
            set
            {
                this._Operator = value;
            }
        }
        private string _AgencyCompany = null;
        /// <summary>  
        /// 代理公司  
        /// </summary>  
        /// <returns></returns>  
        [Description("代理公司")]
        public string AgencyCompany
        {
            get
            {
                return this._AgencyCompany;
            }
            set
            {
                this._AgencyCompany = value;
            }
        }
        private string _Mobile = null;
        /// <summary>  
        /// 手机  
        /// </summary>  
        /// <returns></returns>  
        [Description("手机")]
        public string Mobile
        {
            get
            {
                return this._Mobile;
            }
            set
            {
                this._Mobile = value;
            }
        }
        private string _Remark = null;
        /// <summary>  
        /// 备注  
        /// </summary>  
        /// <returns></returns>  
        [Description("备注")]
        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }
        private DateTime? _PostTime = null;
        /// <summary>  
        /// 添加时间  
        /// </summary>  
        /// <returns></returns>  
        [Description("添加时间")]
        public DateTime? PostTime
        {
            get
            {
                return this._PostTime;
            }
            set
            {
                this._PostTime = value;
            }
        }
    }
}