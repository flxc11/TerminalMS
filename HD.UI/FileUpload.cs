using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;

namespace HD.UI
{
    public class FileUpload
    {
        public string UploadPic()
        {
            string url = HttpContext.Current.Request.Url.LocalPath; 
            string TempPath = "/UploadFile/" + DateTime.Now.ToString("yyyyMMdd");
            string str = string.Empty;
            if (HttpContext.Current.Request.Files.Count != 0)
            {
                for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    var filepath = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    HttpPostedFile file = HttpContext.Current.Request.Files[i];
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();
                    string up_name = HttpContext.Current.Request.Files.Keys[i];
                    //if (!TalentFile.CheckFileExt("GIF|JPG|PNG|BMP", fileExtension.Replace(".", "")) && !string.IsNullOrEmpty(file.FileName))
                    //{
                    //    HttpContext.Current.Response.Write("不允许上传" + fileExtension.Replace(".", "") + "类型的文件！");
                    //    HttpContext.Current.Response.End();
                    //}
                    if (file.ContentLength > 0 && (!string.IsNullOrEmpty(file.FileName)))
                    {
                        if (file.ContentLength > Convert.ToInt32(HD.Config.UIConfig.FileMaxLength))
                        {
                            //HttpContext.Current.Response.Write("<script type='text/javascript'>window.alert('上传失败，每个上传文件不能超过5M');</script>");
                            //HttpContext.Current.Response.Redirect(url);
                            MessageBox.ShowMessage("上传失败，每个上传文件不能超过5M", url);
                        }
                        else
                        {
                            if (!Directory.Exists(HttpContext.Current.Server.MapPath(TempPath)))
                            {
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(TempPath));
                            }
                            file.SaveAs(System.Web.HttpContext.Current.Server.MapPath(TempPath + "/" + filepath + fileExtension));
                            str += up_name + "|#|" + TempPath + "/" + filepath + fileExtension + "|$|";
                        }
                        
                    }

                    Thread.Sleep(100);
                }
            }
            return str;
        }

        #region 根据上传类型获取附件地址
        /// <summary>
        /// 根据上传类型获取附件地址
        /// </summary>
        /// <param name="imgType">证件类型</param>
        /// <param name="guid">当前申请单guid</param>
        /// <returns></returns>
        public string GetImgUrl(string imgType, string guid)
        {
            string str = string.Empty;
            Model.Source source = new Model.Source();

            IList list = source.
                GetAllList(" and SourceType='" + imgType + "' and TerGuid='" + guid + "'", "Id");
            if (list != null)
            {
                foreach (Model.Source item in list)
                {
                    str = item.SourceUrl;
                }
            }


            return str;
        }
        #endregion
    }
}
