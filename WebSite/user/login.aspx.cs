using System.Data;
using HD.Config;
using HD.Framework.DataAccess;
using HD.Framework.Define;
using HD.Framework.Helper;
using HD.Framework.Utils;
using HD.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.user
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Action = Request.Params["Action"];
                switch (Action)
                {
                    case "Login":
                        CheckLogin();
                        break;
                    case "LoginOut":
                        LoginOut();
                        break;
                    default:
                        LoginOut();
                        break;
                }
            }
        }

        private void CheckLogin()
        {
            string userName = Request.Params["UserName"];
            string userPass = Public.FilterSql(Request.Params["password"]);
            Hashtable ht = new Hashtable();
            ht.Add("UserName", userName);
            ht.Add("UserPass", Encrypt.Md5(userPass));

            HD.Model.User model = HD.Model.User.Instance.GetModelById(ht);
            if (!string.IsNullOrEmpty(model.Id.ToString()))
            {
                UserInfo info = new UserInfo();
                info.UserLoginID = model.Id.ToString();
                info.UserLoginName = model.UserName;

                //获取该账号登入时，默认要跳到哪一个页面
                string defaultUrl = string.Empty;
                using (DataTable dt = DataFactory.GetInstance().ExecuteTable("select * from wzrb_UserPrivilege where UserType='user' and UserID=" + model.Id))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string privilege = dt.Rows[0]["UserPrivilegeList"].ToString();
                        privilege = privilege.Substring(1, privilege.Length - 2);
                        string[] arrayStrings = privilege.Split(',');
                        using (DataTable dt1 = DataFactory.GetInstance().ExecuteTable("select * from wzrb_Module where UserGroup='user' and ID=" + arrayStrings[0]))
                        {
                            if (dt1 != null && dt1.Rows.Count > 0)
                            {
                                defaultUrl += dt1.Rows[0]["ModuleUrl"];
                            }
                        }
                    }
                }

                Response.Cookies["uloginid"].Value = model.Id.ToString();
                //创建登录状态
                CookieHelper.WriteCookie(UIConfig.UserCookieName, info, UIConfig.Expires);

                IsPhoneAttribute isphone = new IsPhoneAttribute();
                if (!isphone.OnActionExecuting())
                {
                    Response.Redirect("mobilehome.aspx");
                }
                else
                {
                    Response.Redirect(defaultUrl);
                }
                
            }
            else
            {
                MessageBox.ShowMessage("登录帐号或者密码不正确", "login.aspx");
            }
        }

        private void LoginOut()
        {
            CookieHelper.WriteCookie(UIConfig.UserCookieName, "", -1);
            Session.Clear();
            Session.Abandon();
        }
    }
}