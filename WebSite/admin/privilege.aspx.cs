using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HD.Framework.Utils;
using HD.UI;

namespace WebSite.admin
{
    public partial class Privilege : AdminPage
    {
        public string Privilist = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Public.FilterSql(Request.Params["Type"]);
            string userId = Public.FilterSql(Request.Params["UserID"]);
            if (string.IsNullOrEmpty(type))
            {
                type = "user";
            }
            if (!string.IsNullOrEmpty(userId) && Public.IsNumber(userId))
            {
                using (DataTable dt = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select * from wzrb_UserPrivilege where UserId=" + userId + " and UserType='" + type + "'"))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Privilist = dt.Rows[0]["UserPrivilegeList"].ToString();
                    }
                }
            }
            using (DataTable dt = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select * from wzrb_Module where UserGroup='" + type + "'"))
            {
                if (dt != null & dt.Rows.Count > 0)
                {
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
            }
        }

        public string Checked(string priviId, string privilist)
        {
            string str = string.Empty;
            priviId = "," + priviId + ",";
            if (!string.IsNullOrEmpty(privilist) && privilist.IndexOf(priviId) >= 0)
            {
                str = "checked='checked'";
            }
            return str;
        }
    }
}