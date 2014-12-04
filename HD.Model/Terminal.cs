using System;
using System.Text;
using System.ComponentModel;
using HD.Framework.DataAccess;
using HD.Framework.Utils;

namespace HD.Model
{
    [Description("Terminal")]
    [Key("Id")]
    public class Terminal : DbUtils<Terminal>
    {
        private int? _Id = null;
        /// <summary>  
        /// ID  
        /// </summary>  
        /// <returns></returns>  
        [Description("ID")]
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
        private string _Guid = null;
        /// <summary>  
        /// Guid  
        /// </summary>  
        /// <returns></returns>  
        [Description("Guid")]
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
        private string _Manufacturer = null;
        /// <summary>  
        /// Manufacturer  
        /// </summary>  
        /// <returns></returns>  
        [Description("Manufacturer")]
        public string Manufacturer
        {
            get
            {
                return this._Manufacturer;
            }
            set
            {
                this._Manufacturer = value;
            }
        }
        private string _MachineSize = null;
        /// <summary>  
        /// MachineSize  
        /// </summary>  
        /// <returns></returns>  
        [Description("MachineSize")]
        public string MachineSize
        {
            get
            {
                return this._MachineSize;
            }
            set
            {
                this._MachineSize = value;
            }
        }
        private string _Screen = null;
        /// <summary>  
        /// Screen  
        /// </summary>  
        /// <returns></returns>  
        [Description("Screen")]
        public string Screen
        {
            get
            {
                return this._Screen;
            }
            set
            {
                this._Screen = value;
            }
        }
        private int? _OutIn = null;
        /// <summary>  
        /// OutIn  
        /// </summary>  
        /// <returns></returns>  
        [Description("OutIn")]
        public int? OutIn
        {
            get
            {
                return this._OutIn;
            }
            set
            {
                this._OutIn = value;
            }
        }
        private string _Area = null;
        /// <summary>  
        /// Area  
        /// </summary>  
        /// <returns></returns>  
        [Description("Area")]
        public string Area
        {
            get
            {
                return this._Area;
            }
            set
            {
                this._Area = value;
            }
        }
        private string _Location = null;
        /// <summary>  
        /// Location  
        /// </summary>  
        /// <returns></returns>  
        [Description("Location")]
        public string Location
        {
            get
            {
                return this._Location;
            }
            set
            {
                this._Location = value;
            }
        }
        private string _SignIn = null;
        /// <summary>  
        /// SignIn  
        /// </summary>  
        /// <returns></returns>  
        [Description("SignIn")]
        public string SignIn
        {
            get
            {
                return this._SignIn;
            }
            set
            {
                this._SignIn = value;
            }
        }
        private string _OpenTime = null;
        /// <summary>  
        /// OpenTime  
        /// </summary>  
        /// <returns></returns>  
        [Description("OpenTime")]
        public string OpenTime
        {
            get
            {
                return this._OpenTime;
            }
            set
            {
                this._OpenTime = value;
            }
        }
        private string _Numb = null;
        /// <summary>  
        /// Numb  
        /// </summary>  
        /// <returns></returns>  
        [Description("Numb")]
        public string Numb
        {
            get
            {
                return this._Numb;
            }
            set
            {
                this._Numb = value;
            }
        }
        private string _System = null;
        /// <summary>  
        /// System  
        /// </summary>  
        /// <returns></returns>  
        [Description("System")]
        public string System
        {
            get
            {
                return this._System;
            }
            set
            {
                this._System = value;
            }
        }
        private string _Stituation = null;
        /// <summary>  
        /// Stituation  
        /// </summary>  
        /// <returns></returns>  
        [Description("Stituation")]
        public string Stituation
        {
            get
            {
                return this._Stituation;
            }
            set
            {
                this._Stituation = value;
            }
        }
        private string _Remark = null;
        /// <summary>  
        /// Remark  
        /// </summary>  
        /// <returns></returns>  
        [Description("Remark")]
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
        private string _Recores = null;
        /// <summary>  
        /// Recores  
        /// </summary>  
        /// <returns></returns>  
        [Description("Recores")]
        public string Recores
        {
            get
            {
                return this._Recores;
            }
            set
            {
                this._Recores = value;
            }
        }
        private DateTime? _PostTime = null;
        /// <summary>  
        /// PostTime  
        /// </summary>  
        /// <returns></returns>  
        [Description("PostTime")]
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
        private string _ContentTel = null;
        /// <summary>  
        /// ContentTel  
        /// </summary>  
        /// <returns></returns>  
        [Description("ContentTel")]
        public string ContentTel
        {
            get
            {
                return this._ContentTel;
            }
            set
            {
                this._ContentTel = value;
            }
        }
        private string _Address = null;
        /// <summary>  
        /// Address  
        /// </summary>  
        /// <returns></returns>  
        [Description("Address")]
        public string Address
        {
            get
            {
                return this._Address;
            }
            set
            {
                this._Address = value;
            }
        }
        private string _Sponsor = null;
        /// <summary>  
        /// Address  
        /// </summary>  
        /// <returns></returns>  
        [Description("Sponsor")]
        public string Sponsor
        {
            get
            {
                return this._Sponsor;
            }
            set
            {
                this._Sponsor = value;
            }
        }
        private string _ClassID = null;
        /// <summary>  
        /// Class  
        /// </summary>  
        /// <returns></returns>  
        [Description("ClassID")]
        public string ClassID
        {
            get
            {
                return this._ClassID;
            }
            set
            {
                this._ClassID = value;
            }
        }
        private string _LocationCoordinate = null;
        /// <summary>  
        /// 坐标地址  
        /// </summary>  
        /// <returns></returns>  
        [Description("LocationCoordinate")]
        public string LocationCoordinate
        {
            get
            {
                return this._LocationCoordinate;
            }
            set
            {
                this._LocationCoordinate = value;
            }
        }
        private int? _Status = null;
        /// <summary>  
        /// 坐标地址  
        /// </summary>  
        /// <returns></returns>  
        [Description("Status")]
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
    }
}