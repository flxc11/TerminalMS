using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using HD.Framework.DataAccess;
using HD.Framework.Utils;

namespace HD.Model
{
    [Description("报修回复表")]
    [Key("ID")]
    public class Reply : DbUtils<Reply>
    {
        private int? _ID = null;
        /// <summary>  
        /// ID  
        /// </summary>  
        /// <returns></returns>  
        [Description("ID")]
        public int? ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }
        private string _RepairGuid = null;
        /// <summary>  
        /// RepairGuid  
        /// </summary>  
        /// <returns></returns>  
        [Description("RepairGuid")]
        public string RepairGuid
        {
            get
            {
                return this._RepairGuid;
            }
            set
            {
                this._RepairGuid = value;
            }
        }
        private string _ReplyContent = null;
        /// <summary>  
        /// 回复内容  
        /// </summary>  
        /// <returns></returns>  
        [Description("回复内容")]
        public string ReplyContent
        {
            get
            {
                return this._ReplyContent;
            }
            set
            {
                this._ReplyContent = value;
            }
        }
        private DateTime? _PostTime = null;
        /// <summary>  
        /// 回复时间  
        /// </summary>  
        /// <returns></returns>  
        [Description("回复时间")]
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
        private string _ReplyName = null;
        /// <summary>  
        /// 回复人  
        /// </summary>  
        /// <returns></returns>  
        [Description("回复人")]
        public string ReplyName
        {
            get
            {
                return this._ReplyName;
            }
            set
            {
                this._ReplyName = value;
            }
        }
        private DateTime? _ReplyRepairTime = null;
        /// <summary>  
        /// 修复时间  
        /// </summary>  
        /// <returns></returns>  
        [Description("修复时间")]
        public DateTime? ReplyRepairTime
        {
            get
            {
                return this._ReplyRepairTime;
            }
            set
            {
                this._ReplyRepairTime = value;
            }
        }
    }
}