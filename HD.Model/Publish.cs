using System;
using System.Text;
using System.ComponentModel;
using HD.Framework.DataAccess;
using HD.Framework.Utils;

namespace HD.Model
{
    [Description("发布形式表")]
    [Key("Id")]
    public class Publish : DbUtils<Publish>
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
        private string _ADGuid = null;
        /// <summary>  
        /// 广告Guid  
        /// </summary>  
        /// <returns></returns>  
        [Description("广告Guid")]
        public string ADGuid
        {
            get
            {
                return this._ADGuid;
            }
            set
            {
                this._ADGuid = value;
            }
        }
        private string _PublishType = null;
        /// <summary>  
        /// 发布形式  
        /// </summary>  
        /// <returns></returns>  
        [Description("发布形式")]
        public string PublishType
        {
            get
            {
                return this._PublishType;
            }
            set
            {
                this._PublishType = value;
            }
        }
        private string _PublishQuantity = null;
        /// <summary>  
        /// 数量  
        /// </summary>  
        /// <returns></returns>  
        [Description("数量")]
        public string PublishQuantity
        {
            get
            {
                return this._PublishQuantity;
            }
            set
            {
                this._PublishQuantity = value;
            }
        }
    }
}