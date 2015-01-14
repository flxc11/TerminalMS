using System.Collections;
using HD.Config;
using HD.Framework.DataAccess;
using HD.Framework.Helper;
using HD.Framework.Utils;
using HD.Model;
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

namespace WebSite.admin
{
    public partial class adminjson : System.Web.UI.Page
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
                case "Addmanager":
                    Addmanager();
                    break;
                case "GetManagerList":
                    GetManagerList();
                    break;
                case "UpdateManager":
                    UpdateManager();
                    break;
                case "ResetManagerPass":
                    ResetManagerPass();
                    break;
                case "DeleteManager":
                    DeleteManager();
                    break;
                case "GetApplyList":
                    GetApplyList();
                    break;
                case "ApplySearch":
                    this.ApplySearch();
                    break;
                case "ResetPrivilege":
                    this.ResetPrivilege();
                    break;
                default:
                    break;
            }
        }

        #region 添加管理员账号
        private void Addmanager()
        {

        }
        #endregion
        #region 账号列表
        private void GetManagerList()
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
            sqlWhere = UIConfig.Prefix + "Admin where 1=1 ";
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

        #region 更新管理员信息
        private void UpdateManager()
        {
            string Id = Public.FilterSql(Request.Params["Id"]);
            string UserEmail = Request.Params["UserEmail"];
            string TrueName = Request.Params["TrueName"];
            string UserTel = Request.Params["UserTel"];
            string UserUnit = Request.Params["UserUnit"];
            HD.Model.Admin user = new HD.Model.Admin();
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

        #region 管理员密码重置
        private void ResetManagerPass()
        {
            string Id = Public.FilterSql(Request.Params["Id"]);
            string userPass = Request.Params["UserPass"];
            HD.Model.Admin user = new HD.Model.Admin();
            user.Id = Convert.ToInt32(Id);
            user.UserPass = Encrypt.Md5(userPass);
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

        #region 删除管理员信息
        private void DeleteManager()
        {
            string Id = Public.FilterSql(Request.Params["Id"]);
            HD.Model.Admin user = new HD.Model.Admin();
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

        #region 项目申请列表
        /// <summary>
        /// 项目申请列表
        /// </summary>
        private void GetApplyList()
        {
            string pageIndex = Request.Params["Page"];
            string state = Request.Params["State"];
            string sqlWhere = string.Empty;
            string userId = Request.Params["userId"];
            if (string.IsNullOrEmpty(pageIndex) || !Public.IsNumber(pageIndex))
            {
                pageIndex = "1";
            }
            string pageSize = Request.Params["Rows"];
            if (string.IsNullOrEmpty(pageSize) || !Public.IsNumber(pageSize))
            {
                pageIndex = "10";
            }
            sqlWhere = UIConfig.Prefix + "Application where 1=1 ";
            if (!string.IsNullOrEmpty(state) && Public.IsNumber(state))
            {
                sqlWhere += " and AppState=" + state;
            }
            else
            {
                sqlWhere += " and AppState<>4";
            }
            if (!string.IsNullOrEmpty(userId) && Public.IsNumber(userId))
            {
                sqlWhere += " and UserID=" + userId;
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

        #region 搜索列表
        private void ApplySearch()
        {
            string pageIndex = Request.Params["Page"];
            string state = Request.Params["State"];
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
                pageIndex = "10";
            }
            sqlWhere = UIConfig.Prefix + "Application where 1=1 ";
            if (!string.IsNullOrEmpty(state) && Public.IsNumber(state))
            {
                sqlWhere += " and AppState=" + state;
            }
            if (!string.IsNullOrEmpty(_startTime) && !string.IsNullOrEmpty(_endTime))
            {
                sqlWhere += " and CreateTime between '" + _startTime + "' and '" + _endTime + "'";
            }
            if (!string.IsNullOrEmpty(_selectType))
            {
                if (_selectType == "1" || _selectType == "0")
                {
                    sqlWhere += " and IO=" + _selectType;
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

        #region  注册会员权限设置

        private void ResetPrivilege()
        {
            string checkList = Request.Params["CheckInputValue"];
            string userId = Request.Params["Id"];
            string userType = Request.Params["UserType"];
            if (!string.IsNullOrEmpty(userId) && Public.IsNumber(userId))
            {
                if (!string.IsNullOrEmpty(checkList))
                {
                    checkList = "," + checkList + ",";
                }
                Hashtable hs = new Hashtable();
                hs.Add("UserID", userId);
                hs.Add("UserType", userType);

                UserPrivilege userPrivilege = new UserPrivilege();
                if (userPrivilege.IsExist(hs))
                {
                    userPrivilege.Update(
                        "UserPrivilegeList='" + checkList + "'",
                        " and UserID='" + userId + "' and UserType='" + userType + "'");
                    Response.Write("\"returnval\":\"1\",\"returnstr\":\"权限更新成功！\"");
                    Response.End();
                }
                else
                {
                    userPrivilege.UserID = Convert.ToInt32(userId);
                    userPrivilege.UserType = userType;
                    userPrivilege.UserPrivilegeList = checkList;

                    userPrivilege.Insert();

                    Response.Write("\"returnval\":\"1\",\"returnstr\":\"权限设置成功！\"");
                    Response.End();
                }
            }
            else
            {
                Response.Write("\"returnval\":\"0\"");
                Response.End();
            }
        }
        #endregion
    }
}