using HD.Framework.Utils;
using HD.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.Accept
{
    public partial class AcceptEdit : AdminPage
    {
        public string publishType, pubNum, acceptGuid, adAreaId, Count = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string Action = Request.Params["Action"];
            this.pubNum = Request.Params["pubNum"];
            switch (Action)
            {
                case "Edit":
                    this.EditAccept();
                    break;
            }
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
                    if (string.IsNullOrEmpty(adAreaId))
                    {
                        Count = "0";
                    }
                    else
                    {
                        Count = (adAreaId.Split(',').Length - 2).ToString();
                    }
                    StartTime.Text = Convert.ToDateTime(ad.StartTime).ToString("yyyy-MM-dd");
                    EndTime.Text = Convert.ToDateTime(ad.EndTime).ToString("yyyy-MM-dd");

                    clientPGuid = accept.ClientGuid;
                    hs.Clear();
                    hs.Add("ClientPGuid", clientPGuid);
                    HD.Model.Client client = HD.Model.Client.Instance.GetModelById(hs);
                    client.SetWebControls(this.Page);

                    DataTable dt2 = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select wzrb_Class.ID, wzrb_Class.ClassName, (select COUNT(*) from wzrb_Terminal where ClassID=wzrb_Class.ID and status=1)  as rcount from wzrb_Class");
                    rptClass.DataSource = dt2;
                    rptClass.DataBind();

                    DataTable dt = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select * from wzrb_PublishType");
                    DataTable dttype = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select * from wzrb_Publish where ADGuid='" + ad.ADPGuid + "'");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        publishType = "<table id=\"CheckBoxList2\">";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            publishType += "<tr><td><input id=\"CheckBoxList2_" + i + "\" type=\"checkbox\" name=\"CheckBoxList2\" value=\"" + dt.Rows[i]["ID"] + "\"";
                            for (int k = 0; k < dttype.Rows.Count; k++)
                            {
                                if (dt.Rows[i]["Id"].ToString() == dttype.Rows[k]["PublishType"].ToString())
                                {
                                    publishType += " checked=\"checked\"";
                                }
                            }
                            publishType += " /><label for=\"CheckBoxList2_" + i + "\">" + dt.Rows[i]["PbTypename"] + "</label></td><td><input type=\"text\" name=\"publishNum" + i + "\" value=\"";
                            for (int k = 0; k < dttype.Rows.Count; k++)
                            {
                                if (dt.Rows[i]["Id"].ToString() == dttype.Rows[k]["PublishType"].ToString())
                                {
                                    publishType += dttype.Rows[k]["PublishQuantity"];
                                }
                            }
                            publishType += "\" class=\"accept-input6\" />";
                            publishType += "个</td>";
                            publishType += "<td>上屏时间</td>";
                            publishType += "<td>";
                            publishType += "</td>";
                            publishType += "<td>下屏时间</td>";
                            publishType += "<td>";
                            publishType += "</td>";
                            publishType += "</tr>";
                        }
                        publishType += "</table>";
                    }
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
                int ClassID = Convert.ToInt32(rowv["ID"]);
                //根据分类ID查询该分类下的产品，并绑定产品Repeater
                rptTer.DataSource = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select * from wzrb_Terminal where ClassID=" + ClassID + " and status=1");
                rptTer.DataBind();
            }
        } 

        #region 编辑受理单
        private void EditAccept()
        {
            acceptGuid = Request.Params["acceptGuid"];
            pubNum = Request.Params["pubNum"];
            string adArea = Request.Params["checkTermi"];
            if (string.IsNullOrEmpty(adArea))
            {
                adArea = "";
            }
            //受理单内容
            Hashtable hs = new Hashtable();
            hs.Add("AcceptGuid", acceptGuid);
            HD.Model.AcceptForm accept = HD.Model.AcceptForm.Instance.GetModelById(hs);
            hs.Clear();
            hs.Add("ADPGuid", accept.ADGuid);
            HD.Model.AD ad = HD.Model.AD.Instance.GetModelById(hs);
            hs.Clear();
            hs.Add("ClientPGuid", accept.ClientGuid);
            HD.Model.Client client = HD.Model.Client.Instance.GetModelById(hs);
            HD.Model.Publish publish = new HD.Model.Publish();
            accept.UpdateModel();
            ad.UpdateModel();
            client.UpdateModel();
            //publish.UpdateModel();

            //accept.AcceptGuid = acceptGuid;

            ad.ADPGuid = accept.ADGuid;
            //获取广告投放区域
            string areaId = string.Empty;
            //foreach (ListItem item in CheckBoxList1.Items)
            //{
            //    if (item.Selected)
            //    {
            //        areaId += item.Value + ",";
            //    }
            //}
            //areaId = areaId.Substring(0, areaId.Length - 1);
            if (!string.IsNullOrEmpty(adArea))
            {
                adArea = "," + adArea + ",";
            }
            ad.ADArea = adArea;
            client.ClientPGuid = accept.ClientGuid;
            if (string.IsNullOrEmpty(accept.TotalPrice))
            {
                accept.TotalPrice = "0";
            }
            if (string.IsNullOrEmpty(accept.Discount.ToString()))
            {
                accept.Discount = 0;
            }
            //广告内容
            publish.ADGuid = ad.ADPGuid;
            publish.PublishType = Request.Params["CheckBoxList2"];
            publish.PublishQuantity = pubNum;

            HD.Data.AcceptForm bll = new HD.Data.AcceptForm();
            bll.Edit(accept, ad, client, publish);

            MessageBox.ShowMessage("受理单信息修改成功！", "AcceptList.aspx");
        }
        #endregion

        #region  判断是否选中
        protected string IsChecked(string terminalId, string adAreaList)
        {
            string str = string.Empty;
            string temp = "," + terminalId + ",";
            if (!string.IsNullOrEmpty(adAreaList) && adAreaList.IndexOf(temp) >= 0)
            {
                str = "checked='checked'";
            }
            return str;
        }
        #endregion
    }


}