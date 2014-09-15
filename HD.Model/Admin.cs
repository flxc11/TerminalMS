using System;
using System.Text;
using System.ComponentModel;
using HD.Framework.DataAccess;
using HD.Framework.Utils;

namespace HD.Model
{
    [Description("管理员")]
    [Key("Id")]
    public class Admin : DbUtils<Admin>
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
        private string _UserName = null;
        /// <summary>  
        /// UserName  
        /// </summary>  
        /// <returns></returns>  
        [Description("UserName")]
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
        private string _UserPass = null;
        /// <summary>  
        /// UserPass  
        /// </summary>  
        /// <returns></returns>  
        [Description("UserPass")]
        public string UserPass
        {
            get
            {
                return this._UserPass;
            }
            set
            {
                this._UserPass = value;
            }
        }
        private string _TrueName = null;
        /// <summary>  
        /// TrueName  
        /// </summary>  
        /// <returns></returns>  
        [Description("TrueName")]
        public string TrueName
        {
            get
            {
                return this._TrueName;
            }
            set
            {
                this._TrueName = value;
            }
        }
        private string _UserTel = null;
        /// <summary>  
        /// UserTel  
        /// </summary>  
        /// <returns></returns>  
        [Description("UserTel")]
        public string UserTel
        {
            get
            {
                return this._UserTel;
            }
            set
            {
                this._UserTel = value;
            }
        }
        private string _UserUnit = null;
        /// <summary>  
        /// UserUnit  
        /// </summary>  
        /// <returns></returns>  
        [Description("UserUnit")]
        public string UserUnit
        {
            get
            {
                return this._UserUnit;
            }
            set
            {
                this._UserUnit = value;
            }
        }
        private string _UserEmail = null;
        /// <summary>  
        /// UserEmail  
        /// </summary>  
        /// <returns></returns>  
        [Description("UserEmail")]
        public string UserEmail
        {
            get
            {
                return this._UserEmail;
            }
            set
            {
                this._UserEmail = value;
            }
        }
        private DateTime? _CreateTime = null;
        /// <summary>  
        /// CreateTime  
        /// </summary>  
        /// <returns></returns>  
        [Description("CreateTime")]
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
    }
}