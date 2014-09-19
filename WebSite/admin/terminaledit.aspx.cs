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

namespace WebSite.admin
{
    public partial class terminaledit : AdminPage
    {
        public string terminalGuid, terminalId, terPage, startTime, endTime, selectType, keyWord = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string Action = Request.Params["Action"];
            switch (Action)
            {
                case "Edit":
                    EditTerminal();
                    break;
            }
            if (!IsPostBack)
            {
                #region 当前页面赋值
                string guid = Request.Params["guid"];
                terPage = Request.Params["page"];
                startTime = Request.Params["StartTime"];
                endTime = Request.Params["EndTime"];
                selectType = Request.Params["SelectType"];
                keyWord = Request.Params["Keyword"];

                if (!string.IsNullOrEmpty(guid))
                {
                    terminalGuid = guid;
                    Hashtable ht = new Hashtable();
                    ht.Add("Guid", guid);
                    HD.Model.Terminal terminal = HD.Model.Terminal.Instance.GetModelById(ht);
                    terminal.SetWebControls(this.Page);

                    terminalId = terminal.Id.ToString();
                    Manufacturer.SelectedValue = terminal.Manufacturer;
                    MachineSize.SelectedValue = terminal.MachineSize;
                    Screen.SelectedValue = terminal.Screen;
                    Area.SelectedValue = terminal.Area;
                    OpenTime.SelectedValue = terminal.OpenTime;
                    System.SelectedValue = terminal.System;
                    OutIn.SelectedValue = terminal.OutIn.ToString();
                    if (!string.IsNullOrEmpty(terminal.SignIn))
                    {
                        SignIn.SelectedValue = terminal.SignIn.ToString();
                    }
                    PostTime.Text = Convert.ToDateTime(terminal.PostTime).ToString("yyyy-MM-dd");

                    //图片附件
                    

                    DataTable dt = DataFactory.GetInstance().
                    ExecuteTable("select * from wzrb_source where TerGuid='" + guid + "'");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        scwOthers.DataSource = dt;
                        scwOthers.DataBind();
                    }
                }
                #endregion
            }
        }

        #region 编辑终端信息
        /// <summary>
        /// 编辑终端信息
        /// </summary>
        private void EditTerminal()
        {
            string terminalGuid = Request.Params["terminalGuid"];
            string terminalId = Request.Params["terminalId"];
            string terPage = Request.Params["terPage"];
            string startTime = Request.Params["startTime"];
            string endTime = Request.Params["endTime"];
            string selectType = Request.Params["selectType"];
            string keyWord = Request.Params["keyWord"];
            HD.UI.FileUpload upload = new HD.UI.FileUpload();
            HD.Model.Terminal terminal = new HD.Model.Terminal();
            HD.Model.Source source = new HD.Model.Source();

            //修改终端信息
            terminal.UpdateModel();
            terminal.Guid = terminalGuid;
            terminal.Id = Convert.ToInt32(terminalId);

            //附件上传
            source.TerGuid = terminalGuid;
            //source.CreateTime = DateTime.Now;
            source.SourceUrl = upload.UploadPic();

            HD.Data.Terminal bll = new HD.Data.Terminal();
            bll.Edit(terminal, source);

            MessageBox.ShowMessage("修改提交成功！", "terminallist.aspx?page=" + terPage + "&StartTime=" + startTime + "&EndTime=" + endTime + "&SelectType=" + selectType + "&Keyword=" + keyWord);
        }
        #endregion
    }
}