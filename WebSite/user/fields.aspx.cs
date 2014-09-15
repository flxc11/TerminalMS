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
    public partial class fields : UserPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = DataFactory.GetInstance().ExecuteTable(
                    "SELECT field= convert(varchar(100), a.name), explain=convert(varchar(50), isnull(g.[value],'')) FROM dbo.syscolumns a left join dbo.systypes b on a.xusertype=b.xusertype inner join dbo.sysobjects d on a.id=d.id and d.xtype='U' and d.name<>'dtproperties' left join dbo.syscomments e on a.cdefault=e.id left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id left join sys.extended_properties f on d.id=f.major_id and f.minor_id=0 where d.name ='wzrb_terminal'");
                dt.Rows[0].Delete();
                dt.Rows[1].Delete();
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
        }

        #region 添加或修改
        protected void Button1_Click(object sender, EventArgs e)
        {
            string fields = Request.Params["single-fields"];
            string explain = Request.Params["checkfields"];
            if (explain.Length > 0)
            {
                explain = explain.Substring(0, explain.Length - 1);
            }
            HD.Model.Fields fie = new HD.Model.Fields();
            fie.UserId = Convert.ToInt32(UserLoginInfo.UserLoginID);
            fie.UserType = "user";
            fie.UserFields = fields;
            fie.FieldsExplain = explain;

            Hashtable ht = new Hashtable();
            ht.Add("UserId", fie.UserId);
            ht.Add("UserType", "user");
            if (fie.IsExist(ht))
            {
                fie.Update("UserFields='" + fields + "', FieldsExplain='" + explain + "'",
                    " and UserId='" + fie.UserId + "' and UserType='" + fie.UserType + "'");
            }
            else
            {
                fie.Insert();
            }
            MessageBox.ShowMessage("保存成功", "fields.aspx");
        }
        #endregion

        #region 绑定选中
        /// <summary>
        /// 绑定选中
        /// </summary>
        /// <param name="checkField">当前字段</param>
        /// <returns>checked</returns>
        public string GetChecked(string currentField)
        {
            string str = string.Empty;

            #region 获取当前账号绑定的字段
            DataTable dt1 = DataFactory.GetInstance().ExecuteTable(
                "select * from wzrb_Fields where UserID=" + UserLoginInfo.UserLoginID +
                    " and UserType='user'");
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                string strFields = dt1.Rows[0]["UserFields"].ToString();
                string strExplain = dt1.Rows[0]["FieldsExplain"].ToString();
                string[] arrFields = strFields.Split(',');
                for (int i = 0; i < arrFields.Length; i++)
                {
                    if (arrFields[i] == currentField)
                    {
                        str = "checked='checked'";
                    }
                }
            }

            #endregion



            return str;
        }

        #endregion
    }
}