using System;
using System.Collections.Generic;
using System.Text;

namespace HD.UI
{
    using System.Collections;
    using System.Data;

    using HD.Framework.Helper;
    using HD.Config;
    using HD.Framework.Define;

    public class UserPage : BasePage
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

            Model.User model = new Model.User();

            return model.IsExist(ht);
        }
        #endregion

        #region "登录状态判断"
        /// <summary>
        /// 用户登录地址
        /// </summary>
        private string LoginUrl = "/user/login.aspx";
        /// <summary>
        /// 登录状态判断
        /// </summary>
        private void CheckLogin()
        {
            if (UserLoginInfo == null)
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
        public static UserInfo UserLoginInfo
        {
            get
            {
                return CookieHelper.GetCookie<UserInfo>(UIConfig.UserCookieName);
            }
        }
        #endregion
    }
}
