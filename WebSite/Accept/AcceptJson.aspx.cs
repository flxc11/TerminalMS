using HD.Config;
using HD.Framework.DataAccess;
using HD.Framework.Helper;
using HD.Framework.Utils;
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
    public partial class AcceptJson : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";

            string Action = Request.Params["action"];

            switch (Action)
            {
                case "GetAcceptList":
                    this.GetAcceptList();
                    break;
                case "Delete":
                    this.Delete();
                    break;
                case "AcceptSearch":
                    this.AcceptSearch();
                    break;
                default:
                    break;
            }
        }

        #region 受理单列表
        private void GetAcceptList()
        {
            string pageIndex = Request.Params["Page"];
            string sort = Request.Params["sort"];
            string order = Request.Params["order"];
            string StartTime = Request.Params["StartTime"];
            string EndTime = Request.Params["EndTime"];
            string SelectType = Request.Params["SelectType"];
            string Keyword = Request.Params["Keyword"];
            string orderStr = "";
            if (string.IsNullOrEmpty(sort))
            {
                orderStr = "wzrb_AcceptForm.PostTime desc,Id Desc";
            }
            else
            {
                orderStr = sort + " " + order;
            }
            string sqlWhere = string.Empty;
            if (string.IsNullOrEmpty(pageIndex) || !Public.IsNumber(pageIndex))
            {
                pageIndex = "1";
            }
            string pageSize = Request.Params["Rows"];
            if (string.IsNullOrEmpty(pageSize) || !Public.IsNumber(pageSize))
            {
                pageIndex = "20";
            }
            sqlWhere = UIConfig.Prefix + "AcceptForm left join wzrb_AD on wzrb_AcceptForm.ADGuid=wzrb_AD.ADPGuid left join wzrb_Client on wzrb_AcceptForm.ClientGuid=wzrb_Client.ClientPGuid where 1=1 ";
            if (!string.IsNullOrEmpty(SelectType))
            {
                if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
                {
                    if (SelectType == "StartTime" || SelectType == "EndTime")
                    {
                        sqlWhere += " and " + SelectType + " between '" + StartTime + "' and '" + EndTime + "'";
                    }
                    else
                    {
                        sqlWhere += " and " + SelectType + " like '%" + Keyword + "%' and wzrb_AcceptForm.PostTime between '" + StartTime + "' and '" + EndTime + "'";
                    }
                }
                else
                {
                    sqlWhere += " and " + SelectType + " like '%" + Keyword + "%'";
                }
            }
            
            int recordCount = 0;
            int pageCount = 0;

            //string strSql = "select  * from " + UIConfig.Prefix + "Application order by createtime desc";

            DataTable dt = DataFactory.GetInstance().ExecutePage("Id,wzrb_AcceptForm.PostTime,AcceptGuid,ADTitle,ClientName,StartTime,EndTime",
                sqlWhere, "AcceptGuid", orderStr, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), ref recordCount, ref pageCount);
            string easyGrid_Sort = Request.Params["easyGrid_Sort"];

            string str = JsonHelper.EasyGridTable(dt, easyGrid_Sort, recordCount);
            Response.Write(str);
            Response.End();
        }
        #endregion

        #region 受理单检索
        private void AcceptSearch()
        {
            string pageIndex = Request.Params["Page"];
            string sort = Request.Params["sort"];
            string order = Request.Params["order"];
            string StartTime = Request.Params["StartTime"];
            string EndTime = Request.Params["EndTime"];
            string SelectType = Request.Params["SelectType"];
            string Keyword = Request.Params["Keyword"];
            string orderStr = "";
            if (string.IsNullOrEmpty(sort))
            {
                orderStr = "wzrb_AcceptForm.PostTime desc,Id Desc";
            }
            else 
            {
                orderStr = sort + " " + order;
            }
            string sqlWhere = string.Empty;
            if (string.IsNullOrEmpty(pageIndex) || !Public.IsNumber(pageIndex))
            {
                pageIndex = "1";
            }
            string pageSize = Request.Params["Rows"];
            if (string.IsNullOrEmpty(pageSize) || !Public.IsNumber(pageSize))
            {
                pageIndex = "20";
            }
            sqlWhere = UIConfig.Prefix + "AcceptForm left join wzrb_AD on wzrb_AcceptForm.ADGuid=wzrb_AD.ADPGuid left join wzrb_Client on wzrb_AcceptForm.ClientGuid=wzrb_Client.ClientPGuid where 1=1";
            if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
            {
                if (SelectType == "StartTime" || SelectType == "EndTime")
                {
                    sqlWhere += " and " + SelectType + " between '" + StartTime + "' and '" + EndTime + "'";
                }
                else
                {
                    sqlWhere += " and " + SelectType + " like '%" + Keyword + "%' and wzrb_AcceptForm.PostTime between '" + StartTime + "' and '" + EndTime + "'";
                }
            }
            else
            {
                sqlWhere += " and " + SelectType + " like '%" + Keyword + "%'";
            }
            int recordCount = 0;
            int pageCount = 0;

            //string strSql = "select  * from " + UIConfig.Prefix + "Application order by createtime desc";

            DataTable dt = DataFactory.GetInstance().ExecutePage("Id,wzrb_AcceptForm.PostTime,AcceptGuid,ADTitle,ClientName,StartTime,EndTime",
                sqlWhere, "AcceptGuid", orderStr, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), ref recordCount, ref pageCount);
            string easyGrid_Sort = Request.Params["easyGrid_Sort"];

            string str = JsonHelper.EasyGridTable(dt, easyGrid_Sort, recordCount);
            Response.Write(str);
            Response.End();
        }
        #endregion

        #region 受理单删除
        private void Delete()
        {
            string guidList = Request.Params["AcceptGuid"];
            string rslt = string.Empty;
            if (!string.IsNullOrEmpty(guidList))
            {
                Hashtable hs = new Hashtable();
                HD.Model.AD ad = new HD.Model.AD();
                HD.Model.Publish publish = new HD.Model.Publish();
                string[] guid = guidList.Split(',');
                foreach (var item in guid)
                {
                    //accept.AcceptGuid = item;
                    hs.Clear();
                    hs.Add("AcceptGuid", item);
                    HD.Model.AcceptForm accept = HD.Model.AcceptForm.Instance.GetModelById(hs);
                    try
                    {
                        accept.Delete(accept.AcceptGuid);
                        ad.Delete(accept.ADGuid);
                        hs.Clear();
                        hs.Add("ADGuid", accept.ADGuid);
                        publish.Delete(hs);
                        rslt = "1";
                    }
                    catch (Exception e)
                    {
                        rslt = "0";
                    }
                    finally
                    {

                    }
                }
                Response.Write("{\"result\":\"" + rslt + "\"}");
                Response.End();
            }
        }
        #endregion
    }
}