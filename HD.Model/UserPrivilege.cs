using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using HD.Framework.DataAccess;
using HD.Framework.Utils;

namespace HD.Model
{
    [Description("用户权限表")]
    [Key("ID")]
    public class UserPrivilege : DbUtils<UserPrivilege>
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
        private int? _UserID = null;
        /// <summary>  
        /// ID  
        /// </summary>  
        /// <returns></returns>  
        [Description("UserID")]
        public int? UserID
        {
            get
            {
                return this._UserID;
            }
            set
            {
                this._UserID = value;
            }
        }
        private string _UserType = null;
        /// <summary>  
        /// 用户类型  
        /// </summary>  
        /// <returns></returns>  
        [Description("用户类型")]
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
        private string _UserPrivilegeList = null;
        /// <summary>  
        /// 用户权限  
        /// </summary>  
        /// <returns></returns>  
        [Description("用户权限")]
        public string UserPrivilegeList
        {
            get
            {
                return this._UserPrivilegeList;
            }
            set
            {
                this._UserPrivilegeList = value;
            }
        }
    }
}
