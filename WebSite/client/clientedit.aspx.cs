using HD.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.client
{
    public partial class clientedit : AdminPage
    {
        public string clientGuid, terPage, startTime, endTime, selectType, keyWord = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string Action = Request.Params["Action"];
            switch (Action)
            {
                case "Edit":
                    EditClient();
                    break;
            }

            if (!IsPostBack)
            {
                #region 当前页面赋值

                clientGuid = Request.Params["ClientGuid"];
                terPage = Request.Params["page"];
                startTime = Request.Params["StartTime"];
                endTime = Request.Params["EndTime"];
                selectType = Request.Params["SelectType"];
                keyWord = Request.Params["Keyword"];
                if (!string.IsNullOrEmpty(clientGuid))
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("ClientPGuid", clientGuid);
                    HD.Model.Client client = HD.Model.Client.Instance.GetModelById(ht);
                    client.SetWebControls(this.Page);
                }
                #endregion
            }
        }

        #region 编辑客户信息
        private void EditClient()
        {
            string clientGuid = Request.Params["clientPGuid"];
            string terPage = Request.Params["terPage"];
            string startTime = Request.Params["startTime"];
            string endTime = Request.Params["endTime"];
            string selectType = Request.Params["selectType"];
            string keyWord = Request.Params["keyWord"];

            if (!string.IsNullOrEmpty(clientGuid))
            {
                HD.Model.Client client = new HD.Model.Client();
                client.UpdateModel();
                client.ClientPGuid = clientGuid;

                client.Update();
            }

            MessageBox.ShowMessage("修改提交成功！", "clientlist.aspx?page=" + terPage + "&StartTime=" + startTime + "&EndTime=" + endTime + "&SelectType=" + selectType + "&Keyword=" + keyWord);
        }
        #endregion
    }
}