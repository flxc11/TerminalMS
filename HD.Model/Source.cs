//=========================================================================  
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.  
//=========================================================================  
using System;
using System.Text;
using System.ComponentModel;
using HD.Framework.DataAccess;
using HD.Framework.Utils;

namespace HD.Model
{
    [Description("附件表")]
    [Key("Id")]
    public class Source : DbUtils<Source>
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
        private string _SourceName = null;
        /// <summary>  
        /// 附件名称  
        /// </summary>  
        /// <returns></returns>  
        [Description("附件名称")]
        public string SourceName
        {
            get
            {
                return this._SourceName;
            }
            set
            {
                this._SourceName = value;
            }
        }
        private string _SourceUrl = null;
        /// <summary>  
        /// 附件路径  
        /// </summary>  
        /// <returns></returns>  
        [Description("附件路径")]
        public string SourceUrl
        {
            get
            {
                return this._SourceUrl;
            }
            set
            {
                this._SourceUrl = value;
            }
        }
        private DateTime? _CreateTime = null;
        /// <summary>  
        /// 上传时间  
        /// </summary>  
        /// <returns></returns>  
        [Description("上传时间")]
        public DateTime? CreateTime
        {
            get
            {
                return this._CreateTime;
            }
            set
            {
                this._CreateTime = value;
            }
        }
        private string _TerGuid = null;
        /// <summary>  
        /// 申请单Guid  
        /// </summary>  
        /// <returns></returns>  
        [Description("申请单Guid")]
        public string TerGuid
        {
            get
            {
                return this._TerGuid;
            }
            set
            {
                this._TerGuid = value;
            }
        }
        private int? _UserId = null;
        /// <summary>  
        /// 上传人  
        /// </summary>  
        /// <returns></returns>  
        [Description("上传人")]
        public int? UserId
        {
            get
            {
                return this._UserId;
            }
            set
            {
                this._UserId = value;
            }
        }
        private string _SourceType = null;
        /// <summary>  
        /// 证件类型  
        /// </summary>  
        /// <returns></returns>  
        [Description("证件类型")]
        public string SourceType
        {
            get
            {
                return this._SourceType;
            }
            set
            {
                this._SourceType = value;
            }
        }
    }
}