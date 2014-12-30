using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HD.Config;
using HD.Framework.DataAccess;
using HD.Framework.Helper;
using HD.Framework.Utils;

namespace WebSite.Repair
{
    public partial class RepairJson : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";

            string action = Request.Params["action"];

            switch (action)
            {
                case "GetRepairList":
                    this.GetRepairList();
                    break;
                case "Delete":
                    this.Delete();
                    break;
                case "RepairSearch":
                    this.RepairSearch();
                    break;
                case "SearchTitle":
                    this.SearchTitle();
                    break;
                case "GetTerminal":
                    this.GetTerminal();
                    break;
                default:
                    break;
            }
        }

        #region 报修单列表
        private void GetRepairList()
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
                orderStr = "wzrb_Repair.RepairTime desc";
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
            sqlWhere = UIConfig.Prefix + "Repair left join wzrb_Reply on Guid=RepairGuid where 1=1";
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
                        sqlWhere += " and " + SelectType + " like '%" + Keyword + "%' and wzrb_Repair.RepairTime between '" + StartTime + "' and '" + EndTime + "'";
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
                sqlWhere, "Guid", orderStr, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), ref recordCount, ref pageCount);
            string easyGrid_Sort = Request.Params["easyGrid_Sort"];

            string str = JsonHelper.EasyGridTable(dt, easyGrid_Sort, recordCount);
            Response.Write(str);
            Response.End();
        }
        #endregion

        #region 报修单检索
        private void RepairSearch()
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
                orderStr = "wzrb_Repair.RepairTime desc";
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
            sqlWhere = UIConfig.Prefix + "Repair left join wzrb_Reply on Guid=RepairGuid where 1=1";
            if (!string.IsNullOrEmpty(SelectType))
            {
                sqlWhere += " and " + SelectType + " like '%" + Keyword + "%'";
            }
            if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
            {
                sqlWhere += " and RepairTime between '" + StartTime + "' and '" + EndTime + "'";
            }
            int recordCount = 0;
            int pageCount = 0;

            //string strSql = "select  * from " + UIConfig.Prefix + "Application order by createtime desc";

            DataTable dt = DataFactory.GetInstance().ExecutePage("*",
                sqlWhere, "Guid", orderStr, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), ref recordCount, ref pageCount);
            string easyGrid_Sort = Request.Params["easyGrid_Sort"];

            string str = JsonHelper.EasyGridTable(dt, easyGrid_Sort, recordCount);
            Response.Write(str);
            Response.End();
        }
        #endregion

        #region 报修单删除
        private void Delete()
        {
            string guidList = Request.Params["Guid"];
            string rslt = string.Empty;
            if (!string.IsNullOrEmpty(guidList))
            {
                Hashtable hs = new Hashtable();
                //HD.Model.Repair repair = new HD.Model.Repair();
                HD.Model.Reply reply = new HD.Model.Reply();
                string[] guid = guidList.Split(',');
                foreach (var item in guid)
                {
                    //accept.AcceptGuid = item;
                    hs.Clear();
                    hs.Add("Guid", item);
                    HD.Model.Repair repair = HD.Model.Repair.Instance.GetModelById(hs);
                    try
                    {
                        repair.Delete(repair.Guid);
                        //reply.Delete(reply.RepairGuid);
                        hs.Clear();
                        hs.Add("RepairGuid", repair.Guid);
                        reply.Delete(hs);
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

        #region 标题智能搜索
        private void SearchTitle()
        {
            string keyword = Request.Params["keyword"];
            string str = string.Empty;
            //str = "{";
            if (!string.IsNullOrEmpty(keyword))
            {
                using (DataTable dt = DataFactory.GetInstance().ExecuteTable(
                    "select * from wzrb_Terminal Where location like '%" + keyword + "%'"))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i < dt.Rows.Count - 1)
                            {
                                str += dt.Rows[i]["location"] + ",";
                            }
                            else
                            {
                                str += dt.Rows[i]["location"];
                            }
                        }
                    }
                }
            }
            //str += "}";
            Response.Write(str);
            Response.End();
        }
        #endregion

        #region 获取终端信息

        private void GetTerminal()
        {
            string keyword = Request.Params["keyword"];
            string str = string.Empty;
            str = "{";
            if (!string.IsNullOrEmpty(keyword))
            {
                using (DataTable dt = DataFactory.GetInstance().ExecuteTable(
                    "select * from wzrb_Terminal Where location = '" + keyword + "'"))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        str += "\"return\":\"1\",\"ContentTel\":\"" + dt.Rows[0]["ContentTel"] + "\",\"Address\":\"" + dt.Rows[0]["Address"] + "\",\"TerminalID\":\"" + dt.Rows[0]["ID"] + "\"";
                    }
                    else
                    {
                        str += "\"return\":\"0\"";
                    }
                }
            }
            str += "}";
            Response.Write(str);
            Response.End();
        }
        #endregion
    }
}