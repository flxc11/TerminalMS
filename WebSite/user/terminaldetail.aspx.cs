using HD.Framework.DataAccess;
using HD.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.user
{
    public partial class terminaldetail : UserPage
    {
        public string terPage = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 当前页面赋值
                string guid = Request.Params["guid"];
                terPage = Request.Params["page"];

                if (!string.IsNullOrEmpty(guid))
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("Guid", guid);
                    HD.Model.Terminal terminal = HD.Model.Terminal.Instance.GetModelById(ht);
                    terminal.SetWebControls(this.Page);
                    if (terminal.OutIn == 1)
                    {
                        OutIn.Text = "室外";
                    }
                    else
                    {
                        OutIn.Text = "室内";
                    }
                    if (terminal.SignIn == "1")
                    {
                        SignIn.Text = "已签收";
                    }
                    else if (terminal.SignIn == "0")
                    {
                        SignIn.Text = "未签收";
                    }
                    PostTime.Text = Convert.ToDateTime(terminal.PostTime).ToString("yyyy-MM-dd");

                    //图片附件


                    DataTable dt = DataFactory.GetInstance().
                    ExecuteTable("select * from wzrb_source where TerGuid='" + guid + "'");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        scwOthers.DataSource = dt;
                        scwOthers.DataBind();
                    }
                }
                #endregion
            }
        }
    }
}