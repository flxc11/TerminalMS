using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;
using System.Text;
using System.ComponentModel;
using HD.Framework.DataAccess;
using HD.Framework.Utils;

namespace HD.Model
{
    [Description("报修表")]
    [Key("Guid")]
    public class Repair : DbUtils<Repair>
    {
        private string _Guid = null;
        /// <summary>  
        /// GUID  
        /// </summary>  
        /// <returns></returns>  
        [Description("GUID")]
        public string Guid
        {
            get
            {
                return this._Guid;
            }
            set
            {
                this._Guid = value;
            }
        }
        private string _RepairTitle = null;
        /// <summary>  
        /// 标题  
        /// </summary>  
        /// <returns></returns>  
        [Description("标题")]
        public string RepairTitle
        {
            get
            {
                return this._RepairTitle;
            }
            set
            {
                this._RepairTitle = value;
            }
        }
        private string _RepairContent = null;
        /// <summary>  
        /// 报修内容  
        /// </summary>  
        /// <returns></returns>  
        [Description("报修内容")]
        public string RepairContent
        {
            get
            {
                return this._RepairContent;
            }
            set
            {
                this._RepairContent = value;
            }
        }
        private DateTime? _RepairTime = null;
        /// <summary>  
        /// 报修时间  
        /// </summary>  
        /// <returns></returns>  
        [Description("报修时间")]
        public DateTime? RepairTime
        {
            get
            {
                return this._RepairTime;
            }
            set
            {
                this._RepairTime = value;
            }
        }
        private DateTime? _AddTime = null;
        /// <summary>  
        /// 添加时间  
        /// </summary>  
        /// <returns></returns>  
        [Description("添加时间")]
        public DateTime? AddTime
        {
            get
            {
                return this._AddTime;
            }
            set
            {
                this._AddTime = value;
            }
        }
        private int? _Status = null;
        /// <summary>  
        /// 报修状态 0-未受理，1-已受理，2-已解决  
        /// </summary>  
        /// <returns></returns>  
        [Description("报修状态 0-未受理，1-已受理，2-已解决")]
        public int? Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }
        private int? _UserId = null;
        /// <summary>  
        /// 报修人ID  
        /// </summary>  
        /// <returns></returns>  
        [Description("报修人ID")]
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
        private string _UserName = null;
        /// <summary>  
        /// 报修人姓名  
        /// </summary>  
        /// <returns></returns>  
        [Description("报修人姓名")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }
        private int? _TerminalId = null;
        /// <summary>  
        /// 终端ID  
        /// </summary>  
        /// <returns></returns>  
        [Description("报修人ID")]
        public int? TerminalId
        {
            get
            {
                return this._TerminalId;
            }
            set
            {
                this._TerminalId = value;
            }
        }
    }
}