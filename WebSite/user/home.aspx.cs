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
    public partial class home : UserPage
    {
        public string TerminalCount, str = string.Empty;
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
                rptArea.DataSource = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select COUNT(*) as acount, Area from wzrb_Terminal group by Area order by acount desc");
                rptArea.DataBind();

                //区域输出
                rptFactory.DataSource = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select COUNT(*) as acount, ManuFacturer from wzrb_Terminal group by ManuFacturer order by acount desc");
                rptFactory.DataBind();
            }
        }
    }
}