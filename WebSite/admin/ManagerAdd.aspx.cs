using HD.Framework.Utils;
using HD.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.admin
{
    public partial class ManagerAdd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HD.Model.Admin admin = new HD.Model.Admin();
            admin.UpdateModel();
            HD.UI.AdminPage adminpage = new AdminPage();
            if (adminpage.IsUserExists(admin.UserName))
            {
                MessageBox.ShowMessage("此用户名已存在", "ManagerAdd.aspx");
            }
            else
            {
                admin.UserPass = Encrypt.Md5(admin.UserPass);
                admin.CreateTime = DateTime.Now;
                admin.Insert();
                MessageBox.ShowMessage("用户添加成功", "ManagerList.aspx");
            }
        }
    }
}