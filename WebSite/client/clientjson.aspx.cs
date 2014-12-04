using HD.Config;
using HD.Framework.DataAccess;
using HD.Framework.Helper;
using HD.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.client
{
    public partial class clientjson : System.Web.UI.Page
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
                case "GetClientList":
                    this.GetClientList();
                    break;
                case "Delete":
                    this.Delete();
                    break;
                case "ClientSearch":
                    this.ClientSearch();
                    break;
                default:
                    break;
            }
        }

        #region 客户列表
        private void GetClientList()
        {
            string pageIndex = Request.Params["Page"];
            string StartTime = Request.Params["StartTime"];
            string EndTime = Request.Params["EndTime"];
            string SelectType = Request.Params["SelectType"];
            string Keyword = Request.Params["Keyword"];
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
            sqlWhere = UIConfig.Prefix + "Client where 1=1 ";

            if (!string.IsNullOrEmpty(SelectType))
            {
                if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
                {
                    if (SelectType == "StartTime" || SelectType == "EndTime")
                    {
                        sqlWhere += " and ClientPostTime between '" + StartTime + "' and '" + EndTime + "'";
                    }
                    else
                    {
                        sqlWhere += " and " + SelectType + " like '%" + Keyword + "%' and ClientPostTime between '" + StartTime + "' and '" + EndTime + "'";
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

            DataTable dt = DataFactory.GetInstance().ExecutePage("*",
                sqlWhere, "ClientId", "ClientPostTime desc", Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), ref recordCount, ref pageCount);
            string easyGrid_Sort = Request.Params["easyGrid_Sort"];

            string str = JsonHelper.EasyGridTable(dt, easyGrid_Sort, recordCount);
            Response.Write(str);
            Response.End();
        }
        #endregion

        #region 客户检索
        private void ClientSearch()
        {
            string pageIndex = Request.Params["Page"];
            string StartTime = Request.Params["StartTime"];
            string EndTime = Request.Params["EndTime"];
            string SelectType = Request.Params["SelectType"];
            string Keyword = Request.Params["Keyword"];
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
            sqlWhere = UIConfig.Prefix + "Client where 1=1 ";

            if (!string.IsNullOrEmpty(SelectType))
            {
                if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
                {
                    if (SelectType == "StartTime" || SelectType == "EndTime")
                    {
                        sqlWhere += " and ClientPostTime between '" + StartTime + "' and '" + EndTime + "'";
                    }
                    else
                    {
                        sqlWhere += " and " + SelectType + " like '%" + Keyword + "%' and ClientPostTime between '" + StartTime + "' and '" + EndTime + "'";
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

            DataTable dt = DataFactory.GetInstance().ExecutePage("*",
                sqlWhere, "ClientId", "ClientPostTime desc", Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), ref recordCount, ref pageCount);
            string easyGrid_Sort = Request.Params["easyGrid_Sort"];

            string str = JsonHelper.EasyGridTable(dt, easyGrid_Sort, recordCount);
            Response.Write(str);
            Response.End();
        }
        #endregion

        #region 客户删除
        private void Delete()
        {
            string guidList = Request.Params["ClientGuid"];
            string rslt = string.Empty;
            if (!string.IsNullOrEmpty(guidList))
            {
                HD.Model.Client client = new HD.Model.Client();
                string[] guid = guidList.Split(',');
                foreach (var item in guid)
                {
                    client.ClientPGuid = item;

                    try
                    {
                        client.Delete(client.ClientPGuid);
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