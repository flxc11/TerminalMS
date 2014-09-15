using HD.Config;
using HD.Framework.Define;
using HD.Framework.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HD.UI
{
    public class AdminPage : BasePage
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //登录状态判断
            CheckLogin();
        }
        #region 判断用户是否存在
        public bool IsUserExists(string UserName)
        {
            Hashtable ht = new Hashtable();
            ht.Add("UserName", UserName);

            
            Model.Admin model = new Model.Admin();

            return model.IsExist(ht);
        }
        #endregion

        #region "登录状态判断"
        /// <summary>
        /// 用户登录地址
        /// </summary>
        private string LoginUrl = "/admin/login.aspx";
        /// <summary>
        /// 登录状态判断
        /// </summary>
        private void CheckLogin()
        {
            if (LoginInfo == null)
            {
                Response.Redirect(LoginUrl);
                Response.End();
            }
        }
        #endregion

        #region "用户登录信息"
        /// <summary>
        /// 用户登录信息
        /// </summary>
        public static SystemInfo LoginInfo
        {
            get
            {
                return CookieHelper.GetCookie<SystemInfo>(UIConfig.CookieName);
            }
        }
        #endregion
    }
}
