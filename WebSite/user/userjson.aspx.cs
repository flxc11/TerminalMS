using HD.Config;
using HD.Framework.DataAccess;
using HD.Framework.Helper;
using HD.Framework.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.user
{
    public partial class userjson : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";

            string Action = Request.Params["action"];

            switch (Action)
            {
                case "GetTerminalList":
                    this.GetTerminalList();
                    break;
                case "Delete":
                    this.Delete();
                    break;
                case "GetUserList":
                    this.GetUserList();
                    break;
                case "UpdateUser":
                    this.UpdateUser();
                    break;
                case "ResetUserPass":
                    this.ResetUserPass();
                    break;
                case "DeleteUser":
                    this.DeleteUser();
                    break;
                case "TerminalSearch":
                    this.TerminalSearch();
                    break;
                case "FileDelete":
                    this.FileDelete();
                    break;
                case "GetFields":
                    GetFields();
                    break;
                case "GetMonthTer":
                    this.GetMonthTer();
                    break;
                default:
                    break;
            }
        }

        #region 终端列表
        /// <summary>
        /// 终端列表
        /// </summary>
        private void GetTerminalList()
        {
            string pageIndex = Request.Params["Page"];
            string _startTime = Request.Params["StartTime"];
            string _endTime = Request.Params["EndTime"];
            string _selectType = Request.Params["SelectType"];
            string _keyword = Request.Params["Keyword"];
            string sqlWhere = string.Empty;
            if (string.IsNullOrEmpty(pageIndex) || !Public.IsNumber(pageIndex))
            {
                pageIndex = "1";
            }
            string pageSize = Request.Params["Rows"];
            if (string.IsNullOrEmpty(pageSize) || !Public.IsNumber(pageSize))
            {
                pageSize = "20";
            }
            sqlWhere = UIConfig.Prefix + "Terminal where 1=1 ";
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
                else if (_selectType == "ClassID")
                {
                    if (_keyword == "A级商业圈")
                    {
                        sqlWhere += " and ClassID=1";
                    }
                    else if (_keyword == "B级商业圈")
                    {
                        sqlWhere += " and ClassID=2";
                    }
                    else if (_keyword == "社区街道")
                    {
                        sqlWhere += " and ClassID=3";
                    }
                    else if (_keyword == "机关单位")
                    {
                        sqlWhere += " and ClassID=4";
                    }
                }
                else
                {
                    sqlWhere += " and " + _selectType + " like '%" + _keyword + "%'";
                }
            }
            int recordCount = 0;
            int pageCount = 0;

            //string strSql = "select  * from " + UIConfig.Prefix + "Application order by createtime desc";

            DataTable dt = DataFactory.GetInstance().ExecutePage("*",
                sqlWhere, "Id", "Id desc", Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), ref recordCount, ref pageCount);
            string easyGrid_Sort = Request.Params["easyGrid_Sort"];

            string str = JsonHelper.EasyGridTable(dt, easyGrid_Sort, recordCount);
            Response.Write(str);
            Response.End();
        }
        #endregion

        #region 删除终端
        private void Delete()
        {
            string guidList = Request.Params["Guid"];
            string rslt = string.Empty;
            if (!string.IsNullOrEmpty(guidList))
            {
                HD.Model.Terminal terminal = new HD.Model.Terminal();
                HD.Model.Source source = new HD.Model.Source();
                string[] guid = guidList.Split(',');
                foreach (var item in guid)
                {
                    terminal.Guid = item;

                    try
                    {
                        HD.Data.Terminal bll = new HD.Data.Terminal();
                        bll.TerDelete(terminal, source);
                        rslt = "1";
                    }
                    catch (Exception e)
                    {
                        rslt = "0";
                    }
                    finally
                    {

                    }
                }
                Response.Write("{\"result\":\"" + rslt + "\"}");
                Response.End();
            }
        }
        #endregion

        #region 注册用户列表
        /// <summary>
        /// 注册用户列表
        /// </summary>
        private void GetUserList()
        {
            string pageIndex = Request.Params["Page"];
            string sqlWhere = string.Empty;
            if (string.IsNullOrEmpty(pageIndex) || !Public.IsNumber(pageIndex))
            {
                pageIndex = "1";
            }
            string pageSize = Request.Params["Rows"];
            if (string.IsNullOrEmpty(pageSize) || !Public.IsNumber(pageSize))
            {
                pageIndex = "10";
            }
            sqlWhere = UIConfig.Prefix + "User where 1=1 ";
            int recordCount = 0;
            int pageCount = 0;
            //string strSql = "select  * from " + UIConfig.Prefix + "Application order by createtime desc";
            DataTable dt = DataFactory.GetInstance().ExecutePage("*",
                sqlWhere, "Id", "Id desc", Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), ref recordCount, ref pageCount);
            string easyGrid_Sort = Request.Params["easyGrid_Sort"];

            string str = JsonHelper.EasyGridTable(dt, easyGrid_Sort, recordCount);
            Response.Write(str);
            Response.End();
        }
        #endregion

        #region 更新注册用户
        /// <summary>
        /// 更新注册用户
        /// </summary>
        private void UpdateUser()
        {
            string Id = Public.FilterSql(Request.Params["Id"]);
            string UserEmail = Request.Params["UserEmail"];
            string TrueName = Request.Params["TrueName"];
            string UserTel = Request.Params["UserTel"];
            string UserUnit = Request.Params["UserUnit"];
            HD.Model.User user = new HD.Model.User();
            user.Id = Convert.ToInt32(Id);
            user.UserEmail = UserEmail;
            user.TrueName = TrueName;
            user.UserTel = UserTel;
            user.UserUnit = UserUnit;
            if (user.Update() == 1)
            {
                Response.Write("{\"returnval\":\"1\"}");
            }
            else
            {
                Response.Write("{\"returnval\":\"0\"}");
            }
            Response.End();
        }
        #endregion

        #region 重置用户密码为888888
        private void ResetUserPass()
        {
            string Id = Public.FilterSql(Request.Params["Id"]);
            string userPass = Encrypt.Md5("888888");
            HD.Model.User user = new HD.Model.User();
            user.Id = Convert.ToInt32(Id);
            user.UserPass = userPass;
            if (user.Update() == 1)
            {
                Response.Write("{\"returnval\":\"1\"}");
            }
            else
            {
                Response.Write("{\"returnval\":\"0\"}");
            }
            Response.End();
        }
        #endregion

        #region 删除注册用户
        /// <summary>
        /// 删除注册用户
        /// </summary>
        private void DeleteUser()
        {
            string Id = Public.FilterSql(Request.Params["Id"]);
            HD.Model.User user = new HD.Model.User();
            user.Id = Convert.ToInt32(Id);
            if (user.Delete(Id) == 1)
            {
                Response.Write("{\"returnval\":\"1\"}");
            }
            else
            {
                Response.Write("{\"returnval\":\"0\"}");
            }
            Response.End();
        }
        #endregion

        #region 搜索列表
        private void TerminalSearch()
        {
            string pageIndex = Request.Params["Page"];
            string _startTime = Request.Params["StartTime"];
            string _endTime = Request.Params["EndTime"];
            string _selectType = Request.Params["SelectType"];
            string _keyword = Request.Params["Keyword"];
            string sqlWhere = string.Empty;
            if (string.IsNullOrEmpty(pageIndex) || !Public.IsNumber(pageIndex))
            {
                pageIndex = "1";
            }
            string pageSize = Request.Params["Rows"];
            if (string.IsNullOrEmpty(pageSize) || !Public.IsNumber(pageSize))
            {
                pageIndex = "20";
            }
            sqlWhere = UIConfig.Prefix + "Terminal where 1=1 ";
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
                else if (_selectType == "ClassID")
                {
                    if (_keyword == "A级商业圈")
                    {
                        sqlWhere += " and ClassID=1";
                    }
                    else if (_keyword == "B级商业圈")
                    {
                        sqlWhere += " and ClassID=2";
                    }
                    else if (_keyword == "社区街道")
                    {
                        sqlWhere += " and ClassID=3";
                    }
                    else if (_keyword == "机关单位")
                    {
                        sqlWhere += " and ClassID=4";
                    }
                }
                else
                {
                    sqlWhere += " and " + _selectType + " like '%" + _keyword + "%'";
                }
            }
            int recordCount = 0;
            int pageCount = 0;

            //string strSql = "select  * from " + UIConfig.Prefix + "Application order by createtime desc";

            DataTable dt = DataFactory.GetInstance().ExecutePage("*",
                sqlWhere, "Id", "Id desc", Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), ref recordCount, ref pageCount);
            string easyGrid_Sort = Request.Params["easyGrid_Sort"];

            string str = JsonHelper.EasyGridTable(dt, easyGrid_Sort, recordCount);
            Response.Write(str);
            Response.End();
        }
        #endregion

        #region 删除 附件信息
        /// <summary>
        /// 删除 附件信息
        /// </summary>
        private void FileDelete()
        {
            string terGuid = Request.Params["terGuid"];
            string sourceType = Request.Params["sourceType"];

            HD.Model.Source source = new HD.Model.Source();
            Hashtable ht = new Hashtable();
            ht.Add("TerGuid", terGuid);
            ht.Add("SourceType", sourceType);

            int num = source.Delete(ht);
            if (num > 0)
            {
                Response.Write("{\"result\":\"1\"}");
            }
            else
            {
                Response.Write("{\"result\":\"0\"}");
            }
            Response.End();
        }
        #endregion

        #region 获取个人字段配置信息
        private void GetFields()
        {
            string userId = Public.FilterSql(Request.Params["UserId"]);
            string userType = Request.Params["UserType"];

            if (string.IsNullOrEmpty(userId) || !Public.IsNumber(userId))
            {
                userId = "1";
            }
            DataTable dt = DataFactory.GetInstance().
                        ExecuteTable("select * from wzrb_Fields where UserId = " + userId + " and UserType = '" + userType + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                Response.Write("{\"result\":\"1\",\"relfields\":\"" + dt.Rows[0]["UserFields"] + "\",\"relexplain\":\"" + dt.Rows[0]["FieldsExplain"] + "\"}");
            }
            else
            {
                Response.Write("{\"returnval\":\"0\"}");
            }
            Response.End();
        }
        #endregion

        #region  根据月份获取当月的安装情况并反馈到首页柱状图中
        private void GetMonthTer()
        {
            string strCnt = string.Empty;
            string yearMonth = Request.Params["yearMonth"];
            if (!string.IsNullOrEmpty(yearMonth))
            {
                string[] dates = yearMonth.Split('-');
                int daylen = DateTime.DaysInMonth(Convert.ToInt32(dates[0]), Convert.ToInt32(dates[1]));
                DataTable dt = HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select SUBSTRING(Convert(varchar(100), PostTime, 23),1,10) as ptime, count(*) as ccount from wzrb_Terminal where SUBSTRING(Convert(varchar(100), PostTime, 23),1,10) like '" + yearMonth + "%' group by SUBSTRING(Convert(varchar(100), PostTime, 23),1,10)");
                for (int i = 1; i <= daylen; i++)
                {
                    string temp = "null,";
                    string newDay = "0" + i.ToString();
                    string newDay1 = newDay.Substring(newDay.Length - 2);
                    string newDate = yearMonth + "-" + newDay1;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (newDate == dt.Rows[j]["ptime"].ToString())
                        {
                            temp = dt.Rows[j]["ccount"] + ",";
                        }
                    }
                    strCnt += temp;
                }
                strCnt = strCnt.Substring(0, strCnt.Length - 1);
                Response.Write("{\"returnval\":\"1\",\"date\":\"" + strCnt + "\"}");
                Response.End();
            }
        }
        #endregion
    }
}