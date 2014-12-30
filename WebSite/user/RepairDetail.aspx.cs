using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HD.UI;

namespace WebSite.user
{
    public partial class RepairDetail : UserPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string guid = Request.Params["Guid"];
                if (!string.IsNullOrEmpty(guid))
                {
                    Hashtable hs = new Hashtable();
                    hs.Add("Guid", guid);
                    HD.Model.Repair repair = HD.Model.Repair.Instance.GetModelById(hs);
                    repair.SetWebControls(this.Page);
                    RepairTime.Text = Convert.ToDateTime(repair.RepairTime).ToString("yyyy-MM-dd");

                    hs.Clear();
                    hs.Add("RepairGuid", guid);
                    HD.Model.Reply reply = HD.Model.Reply.Instance.GetModelById(hs);
                    reply.SetWebControls(this.Page);
                    ReplyRepairTime.Text = Convert.ToDateTime(reply.ReplyRepairTime).ToString("yyyy-MM-dd");
                    Response.Write(repair.Status);
                    if (repair.Status == 0)
                    {
                        RepairStatus.Text = "未受理";
                    }
                    if (repair.Status == 1)
                    {
                        RepairStatus.Text = "已受理";
                    }
                    if (repair.Status == 2)
                    {
                        RepairStatus.Text = "已解决";
                    }
                    hs.Clear();
                    hs.Add("ID", repair.TerminalId);
                    HD.Model.Terminal ternimal = HD.Model.Terminal.Instance.GetModelById(hs);
                    ternimal.SetWebControls(this.Page);
                }
            }
        }
    }
}