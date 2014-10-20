using System;
using System.Text;
using System.ComponentModel;
using HD.Framework.DataAccess;
using HD.Framework.Utils;

namespace HD.Model
{
    [Description("业务受理单")]
    [Key("AcceptGuid")]
    public class AcceptForm : DbUtils<AcceptForm>
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
        private string _AcceptGuid = null;
        /// <summary>  
        /// 受理单GUID  
        /// </summary>  
        /// <returns></returns>  
        [Description("受理单GUID")]
        public string AcceptGuid
        {
            get
            {
                return this._AcceptGuid;
            }
            set
            {
                this._AcceptGuid = value;
            }
        }
        private string _ADGuid = null;
        /// <summary>  
        /// 广告GUID  
        /// </summary>  
        /// <returns></returns>  
        [Description("广告GUID")]
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
        private string _ClientGuid = null;
        /// <summary>  
        /// 客户GUID  
        /// </summary>  
        /// <returns></returns>  
        [Description("客户GUID")]
        public string ClientGuid
        {
            get
            {
                return this._ClientGuid;
            }
            set
            {
                this._ClientGuid = value;
            }
        }
        private string _ContractNo = null;
        /// <summary>  
        /// 合同编号  
        /// </summary>  
        /// <returns></returns>  
        [Description("合同编号")]
        public string ContractNo
        {
            get
            {
                return this._ContractNo;
            }
            set
            {
                this._ContractNo = value;
            }
        }
        private string _Num = null;
        /// <summary>  
        /// 编号  
        /// </summary>  
        /// <returns></returns>  
        [Description("编号")]
        public string Num
        {
            get
            {
                return this._Num;
            }
            set
            {
                this._Num = value;
            }
        }
        private int? _Status = null;
        /// <summary>  
        /// 受理单状态 0-未受理 1-已受理  
        /// </summary>  
        /// <returns></returns>  
        [Description("受理单状态 0-未受理 1-已受理")]
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
        private DateTime? _PostTime = null;
        /// <summary>  
        /// 添加时间  
        /// </summary>  
        /// <returns></returns>  
        [Description("添加时间")]
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
        private string _TotalPrice = null;
        /// <summary>  
        /// 合计价  
        /// </summary>  
        /// <returns></returns>  
        [Description("合计价")]
        public string TotalPrice
        {
            get
            {
                return this._TotalPrice;
            }
            set
            {
                this._TotalPrice = value;
            }
        }
        private Single? _Discount = null;
        /// <summary>  
        /// 折扣  
        /// </summary>  
        /// <returns></returns>  
        [Description("折扣")]
        public Single? Discount
        {
            get
            {
                return this._Discount;
            }
            set
            {
                this._Discount = value;
            }
        }
        private string _RealPrice = null;
        /// <summary>  
        /// 实收价  
        /// </summary>  
        /// <returns></returns>  
        [Description("实收价")]
        public string RealPrice
        {
            get
            {
                return this._RealPrice;
            }
            set
            {
                this._RealPrice = value;
            }
        }
        private string _Cash = null;
        /// <summary>  
        /// 现金  
        /// </summary>  
        /// <returns></returns>  
        [Description("现金")]
        public string Cash
        {
            get
            {
                return this._Cash;
            }
            set
            {
                this._Cash = value;
            }
        }
        private string _InvoiceNum = null;
        /// <summary>  
        /// 发票号码  
        /// </summary>  
        /// <returns></returns>  
        [Description("发票号码")]
        public string InvoiceNum
        {
            get
            {
                return this._InvoiceNum;
            }
            set
            {
                this._InvoiceNum = value;
            }
        }
        private string _Transfer = null;
        /// <summary>  
        /// 转账  
        /// </summary>  
        /// <returns></returns>  
        [Description("转账")]
        public string Transfer
        {
            get
            {
                return this._Transfer;
            }
            set
            {
                this._Transfer = value;
            }
        }
        private string _ChequeNum = null;
        /// <summary>  
        /// 支票号码  
        /// </summary>  
        /// <returns></returns>  
        [Description("支票号码")]
        public string ChequeNum
        {
            get
            {
                return this._ChequeNum;
            }
            set
            {
                this._ChequeNum = value;
            }
        }
        private DateTime? _PayTime = null;
        /// <summary>  
        /// 支付时间  
        /// </summary>  
        /// <returns></returns>  
        [Description("支付时间")]
        public DateTime? PayTime
        {
            get
            {
                return this._PayTime;
            }
            set
            {
                this._PayTime = value;
            }
        }
        private string _PayClass = null;
        /// <summary>  
        /// 类别  
        /// </summary>  
        /// <returns></returns>  
        [Description("类别")]
        public string PayClass
        {
            get
            {
                return this._PayClass;
            }
            set
            {
                this._PayClass = value;
            }
        }
        private string _SalesMan = null;
        /// <summary>  
        /// 业务承接人  
        /// </summary>  
        /// <returns></returns>  
        [Description("业务承接人")]
        public string SalesMan
        {
            get
            {
                return this._SalesMan;
            }
            set
            {
                this._SalesMan = value;
            }
        }
        private string _Verifier = null;
        /// <summary>  
        /// 审查人  
        /// </summary>  
        /// <returns></returns>  
        [Description("审查人")]
        public string Verifier
        {
            get
            {
                return this._Verifier;
            }
            set
            {
                this._Verifier = value;
            }
        }
        private string _Director = null;
        /// <summary>  
        /// 部门主任审批  
        /// </summary>  
        /// <returns></returns>  
        [Description("部门主任审批")]
        public string Director
        {
            get
            {
                return this._Director;
            }
            set
            {
                this._Director = value;
            }
        }
        private string _Lead = null;
        /// <summary>  
        /// 报社领导审批  
        /// </summary>  
        /// <returns></returns>  
        [Description("报社领导审批")]
        public string Lead
        {
            get
            {
                return this._Lead;
            }
            set
            {
                this._Lead = value;
            }
        }
    }
}