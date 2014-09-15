using HD.Framework.DataAccess;
using HD.Framework.Utils;
using HD.UI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.user
{
    public partial class Export : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string _startTime = Request.Params["StartTime"];
                string _endTime = Request.Params["EndTime"];
                string _selectType = Request.Params["SelectType"];
                string _keyword = Request.Params["Keyword"];
                string sqlWhere = string.Empty;
                sqlWhere =
                    " wzrb_Terminal where 1=1 ";
                if (!string.IsNullOrEmpty(_startTime) && !string.IsNullOrEmpty(_endTime))
                {
                    sqlWhere += " and PostTime between '" + _startTime + "' and '" + _endTime + "'";
                }
                if (!string.IsNullOrEmpty(_selectType))
                {
                    if (_selectType == "OutIn")
                    {
                        if (_keyword == "室外")
                        {
                            sqlWhere += " and OutIn=1";
                        }
                        else
                        {
                            sqlWhere += " and OutIn=0";
                        }

                    }
                    else if (_selectType == "SignIn")
                    {
                        if (_keyword == "未签收")
                        {
                            sqlWhere += " and SignIn=0";
                        }
                        else if (_keyword == "已签收")
                        {
                            sqlWhere += " and SignIn=1";
                        }
                    }
                    else
                    {
                        sqlWhere += " and " + _selectType + " like '%" + _keyword + "%'";
                    }
                }
                DataTable dt = DataFactory.GetInstance().ExecuteTable("select * from " + sqlWhere + " order by PostTime desc");

                HSSFWorkbook workbook = new HSSFWorkbook();
                MemoryStream ms = new MemoryStream();
                ISheet sheet = workbook.CreateSheet("Sheet1");
                sheet.CreateRow(0).CreateCell(0).SetCellValue("设备厂商");    //第一行需要生成
                sheet.GetRow(0).CreateCell(1).SetCellValue("尺寸");
                sheet.GetRow(0).CreateCell(2).SetCellValue("屏幕");
                sheet.GetRow(0).CreateCell(3).SetCellValue("室内外");
                sheet.GetRow(0).CreateCell(4).SetCellValue("时间");
                sheet.GetRow(0).CreateCell(5).SetCellValue("放置地点");
                sheet.GetRow(0).CreateCell(6).SetCellValue("签收");
                sheet.GetRow(0).CreateCell(7).SetCellValue("开机时长");
                sheet.GetRow(0).CreateCell(8).SetCellValue("区域");
                sheet.GetRow(0).CreateCell(9).SetCellValue("编号");
                sheet.GetRow(0).CreateCell(10).SetCellValue("转移记录");
                sheet.GetRow(0).CreateCell(11).SetCellValue("使用状况");
                sheet.GetRow(0).CreateCell(12).SetCellValue("系统");
                sheet.GetRow(0).CreateCell(13).SetCellValue("详细地址");
                sheet.GetRow(0).CreateCell(14).SetCellValue("联系人和电话");
                sheet.GetRow(0).CreateCell(15).SetCellValue("备注");
                sheet.GetRow(0).CreateCell(16).SetCellValue("赞助商");

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sheet.CreateRow(i + 1).CreateCell(0).SetCellValue(dt.Rows[i]["Manufacturer"].ToString());
                        sheet.GetRow(i + 1).CreateCell(1).SetCellValue(dt.Rows[i]["MachineSize"].ToString());
                        sheet.GetRow(i + 1).CreateCell(2).SetCellValue(dt.Rows[i]["Screen"].ToString());
                        sheet.GetRow(i + 1).CreateCell(3).SetCellValue(dt.Rows[i]["OutIn"].ToString() == "1" ? "室外" : "室内");
                        sheet.GetRow(i + 1).CreateCell(4).SetCellValue(Convert.ToDateTime(dt.Rows[i]["PostTime"].ToString()).ToString("yyyy-MM-dd"));
                        sheet.GetRow(i + 1).CreateCell(5).SetCellValue(dt.Rows[i]["Location"].ToString());
                        sheet.GetRow(i + 1).CreateCell(6).SetCellValue(SignInState(dt.Rows[i]["SignIn"].ToString()));
                        sheet.GetRow(i + 1).CreateCell(7).SetCellValue(dt.Rows[i]["OpenTime"].ToString());
                        sheet.GetRow(i + 1).CreateCell(8).SetCellValue(dt.Rows[i]["Area"].ToString());
                        sheet.GetRow(i + 1).CreateCell(9).SetCellValue(dt.Rows[i]["Numb"].ToString());
                        sheet.GetRow(i + 1).CreateCell(10).SetCellValue(dt.Rows[i]["Recores"].ToString());
                        sheet.GetRow(i + 1).CreateCell(11).SetCellValue(dt.Rows[i]["Stituation"].ToString());
                        sheet.GetRow(i + 1).CreateCell(12).SetCellValue(dt.Rows[i]["System"].ToString());
                        sheet.GetRow(i + 1).CreateCell(13).SetCellValue(dt.Rows[i]["Address"].ToString());
                        sheet.GetRow(i + 1).CreateCell(14).SetCellValue(dt.Rows[i]["ContentTel"].ToString());
                        sheet.GetRow(i + 1).CreateCell(15).SetCellValue(dt.Rows[i]["Remark"].ToString());
                        sheet.GetRow(i + 1).CreateCell(16).SetCellValue(dt.Rows[i]["Sponsor"].ToString());
                    }
                }
                workbook.Write(ms);
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename=Export.xls"));
                Response.BinaryWrite(ms.ToArray());
                workbook = null;
                ms.Close();
                ms.Dispose();
            }
        }
        #region 签收状态
        private string SignInState(string SignIn)
        {
            switch (SignIn)
            {
                case "0":
                    return "未签收";
                    break;
                case "1":
                    return "已签收";
                    break;
                default:
                    return " ";
                    break;
            }
        }
        #endregion
    }
}