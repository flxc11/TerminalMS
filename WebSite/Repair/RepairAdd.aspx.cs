using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HD.Framework.Utils;
using HD.UI;

namespace WebSite.Repair
{
    public partial class RepairAdd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request.Params["Action"];
            switch (action)
            {
                case "Save":
                    this.AddRepair();
                    break;
            }
        }

        #region  添加保修单

        protected void AddRepair()
        {
            string terminalId = Request.Params["TerminalID"];
            //受理单内容
            HD.Model.Repair repair = new HD.Model.Repair();
            repair.UpdateModel();
            repair.Guid = Public.GetGuID;
            repair.TerminalId = Convert.ToInt32(terminalId);
            repair.AddTime = DateTime.Now;

            repair.Insert();
            MessageBox.ShowMessage("报修信息添加成功！", "RepairAdd.aspx");

        }
        #endregion
    }
}