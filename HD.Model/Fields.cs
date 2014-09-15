using System;
using System.Text;
using System.ComponentModel;
using HD.Framework.DataAccess;
using HD.Framework.Utils;

namespace HD.Model
{
    [Description("Fields")]
    [Key("Id")]
    public class Fields : DbUtils<Fields>
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
        private int? _UserId = null;
        /// <summary>  
        /// UserId  
        /// </summary>  
        /// <returns></returns>  
        [Description("UserId")]
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
        private string _UserFields = null;
        /// <summary>  
        /// UserFields  
        /// </summary>  
        /// <returns></returns>  
        [Description("UserFields")]
        public string UserFields
        {
            get
            {
                return this._UserFields;
            }
            set
            {
                this._UserFields = value;
            }
        }
        private string _UserType = null;
        /// <summary>  
        /// UserType  
        /// </summary>  
        /// <returns></returns>  
        [Description("UserType")]
        public string UserType
        {
            get
            {
                return this._UserType;
            }
            set
            {
                this._UserType = value;
            }
        }
        private string _FieldsExplain = null;
        /// <summary>  
        /// FieldsExplain  
        /// </summary>  
        /// <returns></returns>  
        [Description("FieldsExplain")]
        public string FieldsExplain
        {
            get
            {
                return this._FieldsExplain;
            }
            set
            {
                this._FieldsExplain = value;
            }
        }
    }
}