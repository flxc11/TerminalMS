using HD.Framework.Utils;
using HD.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.client
{
    public partial class clientadd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request.Params["Action"];
            switch (action)
            {
                case "Save":
                    Save();
                    break;
            }
        }

        private void Save()
        {
            HD.Model.Client client = new HD.Model.Client();
            client.UpdateModel();
            client.ClientGuid = Public.GetGuID;
            client.PostTime = DateTime.Now;
            client.Insert();

            MessageBox.ShowMessage("客户添加成功！", "clientadd.aspx");
        }
    }
}