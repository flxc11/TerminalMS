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
    public partial class UserAcceptDetail : UserPage
    {
        public string publishType, pubNum, acceptGuid, adAreaId,ChooseCount = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                acceptGuid = Request.Params["AcceptGuid"];
                string adPGuid, clientPGuid, adClass = string.Empty;
                if (!string.IsNullOrEmpty(acceptGuid))
                {
                    Hashtable hs = new Hashtable();
                    hs.Add("AcceptGuid", acceptGuid);
                    HD.Model.AcceptForm accept = HD.Model.AcceptForm.Instance.GetModelById(hs);
                    accept.SetWebControls(this.Page);
                    PostTime.Text = Convert.ToDateTime(accept.PostTime).ToString("yyyy-MM-dd");

                    adPGuid = accept.ADGuid;
                    hs.Clear();
                    hs.Add("ADPGuid", adPGuid);
                    HD.Model.AD ad = HD.Model.AD.Instance.GetModelById(hs);
                    ad.SetWebControls(this.Page);
                    adClass = ad.ADArea;
                    adAreaId = ad.ADArea;
                    StartTime.Text = Convert.ToDateTime(ad.StartTime).ToString("yyyy-MM-dd");
                    EndTime.Text = Convert.ToDateTime(ad.EndTime).ToString("yyyy-MM-dd");
                    if (!string.IsNullOrEmpty(adAreaId))
                    {
                        ChooseCount = (adAreaId.Split(',').Length - 2).ToString();
                    }
                    clientPGuid = accept.ClientGuid;
                    hs.Clear();
                    hs.Add("ClientPGuid", clientPGuid);
                    HD.Model.Client client = HD.Model.Client.Instance.GetModelById(hs);
                    client.SetWebControls(this.Page);

                    DataTable dt2 = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select wzrb_Class.ID, wzrb_Class.ClassName, (select COUNT(*) from wzrb_Terminal where ClassID=wzrb_Class.ID and Status=1)  as rcount from wzrb_Class");
                    rptClass.DataSource = dt2;
                    rptClass.DataBind();

                    DataTable dttype = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select * from wzrb_Publish where ADGuid='" + ad.ADPGuid + "'");
                    publishType = "<table id=\"CheckBoxList2\">";
                    for (int k = 0; k < dttype.Rows.Count; k++)
                    {
                        publishType += "<tr><td>" + GetPublishName(dttype.Rows[k]["PublishType"].ToString());
                        publishType += " -- 数量：" + dttype.Rows[k]["PublishQuantity"];
                        publishType += "</td></tr>";
                    }
                    publishType += "</table>";
                }
            }
        }

        protected void rptClass_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptTer = (Repeater)e.Item.FindControl("rptTerminal");
                //找到分类Repeater关联的数据项
                DataRowView rowv = (DataRowView)e.Item.DataItem;
                //提取分类ID
                int classId = Convert.ToInt32(rowv["ID"]);
                //根据分类ID查询该分类下的产品，并绑定产品Repeater
                rptTer.DataSource = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select * from wzrb_Terminal where ClassID=" + classId + " and Status=1");
                rptTer.DataBind();
            }
        }
 
        private string GetPublishName(string pubId)
        {
            string str = string.Empty;
            using (DataTable dt = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select * from wzrb_PublishType where ID='" + pubId + "'"))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    str = dt.Rows[0]["PbTypeName"].ToString();
                }
            }
            return str;
        }

        #region  判断是否选中
        protected string IsChecked(string terminalId, string adAreaList)
        {
            string str = string.Empty;
            string temp = "," + terminalId + ",";
            if (adAreaList.IndexOf(temp) >= 0)
            {
                str = " class='div-db'";
            }
            else
            {
                str = " class='div-dn'";
            }
            return str;
        }
        #endregion
    }
}