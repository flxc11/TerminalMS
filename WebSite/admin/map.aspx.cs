using HD.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.admin
{
    public partial class map : AdminPage
    {
        public string TerminalCount, str, arrArea, arrCount = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //区域输出
                DataTable dt1 = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select COUNT(*) as acount, Area from wzrb_Terminal where Status=1 group by Area order by acount desc");
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
                rptFactory.DataSource = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select COUNT(*) as acount, ManuFacturer from wzrb_Terminal where Status=1 group by ManuFacturer order by acount desc");
                rptFactory.DataBind();

                //每月安装数
                DataTable dt2 = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select SUBSTRING(Convert(varchar(100), PostTime, 23),1,7) as ptime, count(*) as ccount from wzrb_Terminal where Status=1 group by SUBSTRING(Convert(varchar(100), PostTime, 23),1,7)");
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

                //按类别输出
                rptClass.DataSource = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select COUNT(*) as acount, ClassID, wzrb_Class.ClassName from wzrb_Terminal left join wzrb_Class on wzrb_Terminal.ClassID=wzrb_Class.ID where Status=1 group by ClassName, ClassID order by acount desc");
                rptClass.DataBind();
            }
        }
    }
}