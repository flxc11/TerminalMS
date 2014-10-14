using HD.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.user
{
    public partial class mobilehome : UserPage
    {
        public string TerminalCount, str, arrArea, arrCount = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select count(*) as tcount from wzrb_Terminal");
                if (dt != null && dt.Rows.Count > 0)
                {
                    TerminalCount = dt.Rows[0]["tcount"].ToString();
                }

                string currentTime = DateTime.Now.ToString("yyyy-M-dd");
                string beforeWeek = DateTime.Now.AddDays(-7).ToString("yyyy-M-dd");
                string beforeMonth = DateTime.Now.AddMonths(-1).ToString("yyyy-M-dd");
                string beforeTMonth = DateTime.Now.AddMonths(-3).ToString("yyyy-M-dd");

                str += "<div class=\"btn-info\"><a href=\"terminallist.aspx\">已安装终端：<span style='font-size:16px;'>" + TerminalCount + "</span> 台</a></div>";
                str += "<div class=\"btn-info\"><a href=\"terminallist.aspx?StartTime=" + beforeWeek + "&EndTime=" + currentTime + "&SelectType=&Keyword=\">近一个星期安装</a></div>";
                str += "<div class=\"btn-info\"><a href=\"terminallist.aspx?StartTime=" + beforeMonth + "&EndTime=" + currentTime + "&SelectType=&Keyword=\">近一个月安装</a></div>";
                str += "<div class=\"btn-info\"><a href=\"terminallist.aspx?StartTime=" + beforeTMonth + "&EndTime=" + currentTime + "&SelectType=&Keyword=\">近三个月安装</a></div>";

                //区域输出
                DataTable dt1 = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select COUNT(*) as acount, Area from wzrb_Terminal group by Area order by acount desc");
                rptArea.DataSource = dt1;
                rptArea.DataBind();
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        arrArea += "['" + dt1.Rows[i]["Area"] + "(" + dt1.Rows[i]["acount"] + ")" + "', " + dt1.Rows[i]["acount"] + "],";
                    }
                    arrArea = arrArea.Substring(0, arrArea.Length - 1);
                }

                //设备厂商输出
                rptFactory.DataSource = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select COUNT(*) as acount, ManuFacturer from wzrb_Terminal group by ManuFacturer order by acount desc");
                rptFactory.DataBind();

                //每月安装数
                DataTable dt2 = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select SUBSTRING(Convert(varchar(100), PostTime, 23),1,7) as ptime, count(*) as ccount from wzrb_Terminal group by SUBSTRING(Convert(varchar(100), PostTime, 23),1,7)");
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        int tempMonty = 0;
                        string[] arrDate = dt2.Rows[i]["ptime"].ToString().Split('-');
                        if (arrDate[1] != "12")
                        {
                            tempMonty = Convert.ToInt32(arrDate[1]) - 1;
                        }
                        else
                        {
                            tempMonty = Convert.ToInt32(arrDate[1]);
                        }
                        arrCount += "{x:Date.UTC(" + arrDate[0] + "," + tempMonty + "), y:" + dt2.Rows[i]["ccount"] + ", name:'" + arrDate[0] + "年" + arrDate[1] + "月'},";
                    }
                    arrCount = arrCount.Substring(0, arrCount.Length - 1);
                }
            }
        }
    }
}