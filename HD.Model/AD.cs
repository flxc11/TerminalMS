using System;
using System.Text;
using System.ComponentModel;
using HD.Framework.DataAccess;
using HD.Framework.Utils;

namespace HD.Model
{
    [Description("广告单")]
    [Key("ADPGuid")]
    public class AD : DbUtils<AD>
    {
        private int? _ADId = null;
        /// <summary>  
        /// ADId  
        /// </summary>  
        /// <returns></returns>  
        [Description("ADId")]
        public int? ADId
        {
            get
            {
                return this._ADId;
            }
            set
            {
                this._ADId = value;
            }
        }
        private string _ADPGuid = null;
        /// <summary>  
        /// ADGuid  
        /// </summary>  
        /// <returns></returns>  
        [Description("ADPGuid")]
        public string ADPGuid
        {
            get
            {
                return this._ADPGuid;
            }
            set
            {
                this._ADPGuid = value;
            }
        }
        private string _ADTitle = null;
        /// <summary>  
        /// 业务内容  
        /// </summary>  
        /// <returns></returns>  
        [Description("业务内容")]
        public string ADTitle
        {
            get
            {
                return this._ADTitle;
            }
            set
            {
                this._ADTitle = value;
            }
        }
        private DateTime? _StartTime = null;
        /// <summary>  
        /// 上屏时间  
        /// </summary>  
        /// <returns></returns>  
        [Description("上屏时间")]
        public DateTime? StartTime
        {
            get
            {
                return this._StartTime;
            }
            set
            {
                this._StartTime = value;
            }
        }
        private DateTime? _EndTime = null;
        /// <summary>  
        /// 下屏时间  
        /// </summary>  
        /// <returns></returns>  
        [Description("下屏时间")]
        public DateTime? EndTime
        {
            get
            {
                return this._EndTime;
            }
            set
            {
                this._EndTime = value;
            }
        }
        private string _VideoTime = null;
        /// <summary>  
        /// 视频秒数  
        /// </summary>  
        /// <returns></returns>  
        [Description("视频秒数")]
        public string VideoTime
        {
            get
            {
                return this._VideoTime;
            }
            set
            {
                this._VideoTime = value;
            }
        }
        private string _PicNum = null;
        /// <summary>  
        /// 整屏幅数  
        /// </summary>  
        /// <returns></returns>  
        [Description("整屏幅数")]
        public string PicNum
        {
            get
            {
                return this._PicNum;
            }
            set
            {
                this._PicNum = value;
            }
        }
        private string _CountTime = null;
        /// <summary>  
        /// 投放时间合计  
        /// </summary>  
        /// <returns></returns>  
        [Description("投放时间合计")]
        public string CountTime
        {
            get
            {
                return this._CountTime;
            }
            set
            {
                this._CountTime = value;
            }
        }
        private string _ADArea = null;
        /// <summary>  
        /// 投放地域  
        /// </summary>  
        /// <returns></returns>  
        [Description("投放地域")]
        public string ADArea
        {
            get
            {
                return this._ADArea;
            }
            set
            {
                this._ADArea = value;
            }
        }
        private string _ADRemark = null;
        /// <summary>  
        /// 备注  
        /// </summary>  
        /// <returns></returns>  
        [Description("备注")]
        public string ADRemark
        {
            get
            {
                return this._ADRemark;
            }
            set
            {
                this._ADRemark = value;
            }
        }
        private int? _PublishId = null;
        /// <summary>  
        /// 发布形式ID  
        /// </summary>  
        /// <returns></returns>  
        [Description("发布形式ID")]
        public int? PublishId
        {
            get
            {
                return this._PublishId;
            }
            set
            {
                this._PublishId = value;
            }
        }
    }
}