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
    public partial class AcceptAdd : AdminPage
    {
        public string publishType, pubNum = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string Action = Request.Params["Action"];
            this.pubNum = Request.Params["pubNum"];
            switch (Action)
            {
                case "Save":
                    this.AddAccept();
                    break;
            }
            
            if (!IsPostBack)
            {
                DataTable dt = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select * from wzrb_PublishType");
                if (dt != null && dt.Rows.Count > 0)
                {
                    publishType = "<table id=\"CheckBoxList2\">";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        publishType += "<tr><td><input id=\"CheckBoxList2_" + i + "\" type=\"checkbox\" name=\"CheckBoxList2\" value=\"" + dt.Rows[i]["ID"] + "\" /><label for=\"CheckBoxList2_" + i + "\">" + dt.Rows[i]["PbTypename"] + "</label></td><td><input type=\"text\" name=\"publishNum" + i + "\" value=\" \" class=\"readonly\" readonly=\"readonly\" /> 个</td></tr>";
                    }
                    publishType += "</table>";
                }
                //DataContext

                DataTable dt1 = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select wzrb_Class.ID, wzrb_Class.ClassName, (select COUNT(*) from wzrb_Terminal where ClassID=wzrb_Class.ID and status=1)  as rcount from wzrb_Class");
                //if (dt1 != null && dt1.Rows.Count > 0)
                //{
                //    this.CheckBoxList1.DataSource = dt1;
                //    this.CheckBoxList1.DataTextField = "ClassName";
                //    this.CheckBoxList1.DataValueField = "ID";
                //    this.CheckBoxList1.DataBind();
                //}

                rptClass.DataSource = dt1;
                rptClass.DataBind();
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

        #region 新增受理单
        private void AddAccept()
        {
            string adArea = Request.Params["checkTermi"];
            //受理单内容
            HD.Model.AcceptForm accept = new HD.Model.AcceptForm();
            HD.Model.AD ad = new HD.Model.AD();
            HD.Model.Client client = new HD.Model.Client();
            HD.Model.Publish publish = new HD.Model.Publish();
            accept.UpdateModel();
            ad.UpdateModel();
            client.UpdateModel();
            publish.UpdateModel();

            accept.AcceptGuid = Public.GetGuID;
            ad.ADPGuid = Public.GetGuID;
            //获取广告投放区域
            //string areaId = string.Empty;
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
            accept.ADGuid = ad.ADPGuid;
            accept.Status = 0;
            //客户信息插入或更新
            Hashtable hs = new Hashtable();
            hs.Add("ClientName", client.ClientName);
            //如果客户已存在，则更新客户信息，并且获取已存在的ClientPGuid插入到AcceptForm的ClientGuid里面
            if (client.IsExist(hs))
            {
                client.Update(
                    "ClientName='" + client.ClientName + "',Tel='" + client.Tel + "',Mobile='" + client.Mobile + "',Operator='" + client.Operator + "',AgencyCompany='" + client.AgencyCompany + "'",
                    " and ClientName ='" + client.ClientName + "'");
                HD.Model.Client newClient = HD.Model.Client.Instance.GetModelById(hs);
                accept.ClientGuid = newClient.ClientPGuid;
            }
            else
            {
                client.ClientPGuid = Public.GetGuID;
                client.ClientPostTime = DateTime.Now;
                client.Insert();
                accept.ClientGuid = client.ClientPGuid;
            }
            
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
            bll.Add(accept, ad, publish);

            MessageBox.ShowMessage("受理单信息添加成功！", "AcceptAdd.aspx");

        }
        #endregion
    }
}