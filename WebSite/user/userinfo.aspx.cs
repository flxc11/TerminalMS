using HD.Framework.Utils;
using HD.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.user
{
    public partial class userinfo : UserPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HD.Model.User user = HD.Model.User.Instance.GetModelById(UserLoginInfo.UserLoginID);
                user.SetWebControls(this.Page);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HD.Model.User user = new HD.Model.User();
            user.UpdateModel();
            user.Id = Convert.ToInt32(UserLoginInfo.UserLoginID);
            if (!string.IsNullOrEmpty(UserPass1.Text))
            {
                user.UserPass = Encrypt.Md5(UserPass1.Text);
            }

            user.Update();
            MessageBox.ShowMessage("修改成功！", "userinfo.aspx");
        }
    }
}