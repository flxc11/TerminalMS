using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HD.Framework.DataAccess;
using HD.Model;
using HD.UI;

namespace WebSite.Repair
{
    public partial class RepairEdit : AdminPage
    {
        public string Guid, PubNum, Address, ContentTel, TerminalId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request.Params["Action"];
            this.PubNum = Request.Params["pubNum"];
            switch (action)
            {
                case "Edit":
                    this.EditRepair();
                    break;
            }
            if (!IsPostBack)
            {
                Guid = Request.Params["Guid"];
                if (!string.IsNullOrEmpty(Guid))
                {
                    Hashtable hs = new Hashtable();
                    hs.Add("Guid", Guid);
                    HD.Model.Repair repair = HD.Model.Repair.Instance.GetModelById(hs);
                    repair.SetWebControls(this.Page);
                    RepairTime.Text = Convert.ToDateTime(repair.RepairTime).ToString("yyyy-MM-dd");
                    Status.SelectedValue = repair.Status.ToString();

                    hs.Clear();
                    hs.Add("RepairGuid", Guid);
                    HD.Model.Reply reply = HD.Model.Reply.Instance.GetModelById(hs);
                    reply.SetWebControls(this.Page);
                    //获取终端信息

                    using (DataTable dt = DataFactory.GetInstance().ExecuteTable("select ContentTel,Address,ID from wzrb_Terminal where ID=" + repair.TerminalId))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Address = dt.Rows[0]["Address"].ToString();
                            ContentTel = dt.Rows[0]["ContentTel"].ToString();
                            TerminalId = dt.Rows[0]["ID"].ToString();
                        }
                    }
                }
            }
        }

        #region 编辑报修单

        protected void EditRepair()
        {
            string guid = Request.Params["Guid"];
            //pubNum = Request.Params["pubNum"];
            HD.Model.Repair repair = new HD.Model.Repair();
            HD.Model.Reply reply = new HD.Model.Reply();
            repair.UpdateModel();
            reply.UpdateModel();
            repair.Guid = guid;
            repair.TerminalId = Convert.ToInt32(Request.Params["TerminalID"]);
            reply.RepairGuid = guid;
            reply.PostTime = DateTime.Now;

            HD.Data.Repair bllRepair = new HD.Data.Repair();
            bllRepair.Add(repair, reply);
            MessageBox.ShowMessage("保修单信息修改成功！", "RepairList.aspx");
        }
        #endregion
    }
}