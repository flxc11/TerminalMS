using HD.Framework.Utils;
using HD.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.user
{
    public partial class GetPoint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ClassID = Request.Params["ClassID"];
                string AreaID = Request.Params["AreaID"];
                string ManuFacturer = Request.Params["ManuFacturer"];
                string Keyword = Request.Params["Keyword"];
                Response.ContentType = "text/xml";
                string str = string.Empty;
                string strSql = "select * from wzrb_Terminal where 1=1 and status=1";
                if (!string.IsNullOrEmpty(ClassID) && Public.IsNumber(ClassID))
                {
                    strSql += " and ClassID=" + ClassID;
                }
                if (!string.IsNullOrEmpty(AreaID))
                {
                    strSql += " and Area='" + AreaID + "'";
                }
                if (!string.IsNullOrEmpty(ManuFacturer))
                {
                    strSql += " and ManuFacturer='" + ManuFacturer + "'";
                }
                if (!string.IsNullOrEmpty(Keyword))
                {
                    strSql += " and Location like '%" + Keyword + "%'";
                }
                DataTable dt = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable(strSql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    str += "<?xml version='1.0' encoding='UTF-8' ?>";
                    str += "<DataTable>";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i]["LocationCoordinate"].ToString()))
                        {
                            str += "<Rows>";
                            str += "<Location>" + dt.Rows[i]["Location"] + "</Location>";
                            str += "<Address>" + dt.Rows[i]["Address"] + "</Address>";
                            str += "<ContentTel>" + dt.Rows[i]["ContentTel"] + "</ContentTel>";
                            str += "<Coordinate>" + dt.Rows[i]["LocationCoordinate"] + "</Coordinate>";
                            str += "</Rows>";
                        }
                    }
                    str += "</DataTable>";
                }
                else
                {
                    str = "<?xml version='1.0' encoding='UTF-8' ?><DataTable></DataTable>";
                }
                Response.Write(str);
                Response.End();
            }
        }
    }
}