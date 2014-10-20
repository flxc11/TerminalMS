using System;
using System.Text;
using System.ComponentModel;
using HD.Framework.DataAccess;
using HD.Framework.Utils;

namespace HD.Model
{
    [Description("广告单")]
    [Key("ADGuid")]
    public class AD : DbUtils<AD>
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
        /// ADGuid  
        /// </summary>  
        /// <returns></returns>  
        [Description("ADGuid")]
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
        private int? _ADArea = null;
        /// <summary>  
        /// 投放地域  
        /// </summary>  
        /// <returns></returns>  
        [Description("投放地域")]
        public int? ADArea
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