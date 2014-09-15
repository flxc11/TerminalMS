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
    public partial class terminaladd : AdminPage
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

        #region 添加终端信息
        /// <summary>
        /// 添加终端信息
        /// </summary>
        private void Save()
        {
            HD.UI.FileUpload upload = new HD.UI.FileUpload();
            HD.Model.Terminal terminal = new HD.Model.Terminal();
            HD.Model.Source source = new HD.Model.Source();

            
            terminal.UpdateModel();
            terminal.Guid = Public.GetGuID;

            //附件上传
            source.TerGuid = terminal.Guid;
            source.CreateTime = DateTime.Now;
            source.SourceUrl = upload.UploadPic();

            

            HD.Data.Terminal bll = new HD.Data.Terminal();
            bll.Add(terminal, source);

            MessageBox.ShowMessage("终端信息添加成功！", "terminaladd.aspx");
        }
        #endregion
    }
}