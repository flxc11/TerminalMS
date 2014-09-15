//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using HD.Framework.Helper;
using HD.Framework.Utils;

namespace HD.Framework.DataAccess
{
    /// <summary>
    /// 数据库操作公共方法
    /// </summary>
    public class DbUtils<T> : Singleton<T> where T : new()
    {
        #region "实体赋值"
        /// <summary>
        /// 根据实体类赋值
        /// </summary>
        public void UpdateModel()
        {
            UpdateModel(this, string.Empty);
        }
        /// <summary>
        /// 根据实体类赋值
        /// </summary>
        /// <param name="model">实体对象</param>
        public void UpdateModel(object model)
        {
            UpdateModel(model, string.Empty);
        }
        /// <summary>
        /// 根据实体类赋值
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="preName">前缀名进行过滤</param>
        public void UpdateModel(object model, string preName)
        {
            NameValueCollection _nvc = getFormCollection;
            if (_nvc.Count > 0)
            {
                Type type = model.GetType();
                foreach (PropertyInfo p in type.GetProperties())
                {
                    if (!p.CanWrite) continue;
                    //反射实体属性名称
                    string modelName = p.Name;
                    object val = _nvc[modelName];
                    if (!string.IsNullOrEmpty(preName))
                    {
                        if (!modelName.StartsWith(preName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            modelName = string.Format("{0}{1}", preName, modelName);
                            val = _nvc[modelName];
                            //移除前缀
                            modelName = modelName.Remove(0, preName.Length);
                        }
                    }
                    if (val == null) continue;//如果为null跳出本次循环
                    p.SetValue(model, Public.GetDefaultValue(val, p.PropertyType), null);
                }
            }
        }
        /// <summary>
        /// 获取实体表单值
        /// </summary>
        private NameValueCollection getFormCollection
        {
            get
            {
                NameValueCollection nvc = HttpContext.Current.Request.Params;
                return nvc;
            }
        }
        #endregion
        #region "控件赋值"
        /// <summary>
        /// 设置页面控件的值
        /// </summary>
        /// <param name="page"></param>
        public void SetWebControls(Control page)
        {
            SetWebControls(page, HashTableHelper.GetModelToHashtable(this));
        }
        /// <summary>
        /// 设置页面控件的值
        /// </summary>
        /// <param name="page"></param>
        /// <param name="ht"></param>
        public void SetWebControls(Control page, Hashtable ht)
        {
            if (ht.Count != 0)
            {
                int size = ht.Keys.Count;
                foreach (string key in ht.Keys)
                {
                    object val = ht[key];
                    if (val != null)
                    {
                        Control control = page.FindControl(key);
                        if (control == null) continue;
                        if (control is HtmlInputText)
                        {
                            HtmlInputText txt = (HtmlInputText)control;
                            txt.Value = val.ToString().Trim();
                        }
                        if (control is TextBox)
                        {
                            TextBox txt = (TextBox)control;
                            txt.Text = val.ToString().Trim();
                        }
                        if (control is HtmlSelect)
                        {
                            HtmlSelect txt = (HtmlSelect)control;
                            txt.Value = val.ToString().Trim();
                        }
                        if (control is HtmlInputHidden)
                        {
                            HtmlInputHidden txt = (HtmlInputHidden)control;
                            txt.Value = val.ToString().Trim();
                        }
                        if (control is HtmlInputPassword)
                        {
                            HtmlInputPassword txt = (HtmlInputPassword)control;
                            txt.Value = val.ToString().Trim();
                        }
                        if (control is Label)
                        {
                            Label txt = (Label)control;
                            txt.Text = val.ToString().Trim();
                        }
                        if (control is HtmlInputCheckBox)
                        {
                            HtmlInputCheckBox chk = (HtmlInputCheckBox)control;
                            //chk.Checked = CommonHelper.GetInt(val) == 1 ? true : false;
                        }
                        if (control is HtmlTextArea)
                        {
                            HtmlTextArea area = (HtmlTextArea)control;
                            area.Value = val.ToString().Trim();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 获取服务器控件值
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public Hashtable GetWebControls(Control page)
        {
            Hashtable ht = new Hashtable();
            int size = HttpContext.Current.Request.Params.Count;
            for (int i = 0; i < size; i++)
            {
                string id = HttpContext.Current.Request.Params.GetKey(i);
                Control control = page.FindControl(id);
                if (control == null) continue;
                control = page.FindControl(id);
                if (control is HtmlInputText)
                {
                    HtmlInputText txt = (HtmlInputText)control;
                    ht[txt.ID] = txt.Value.Trim();
                }
                if (control is HtmlSelect)
                {
                    HtmlSelect txt = (HtmlSelect)control;
                    ht[txt.ID] = txt.Value.Trim();
                }
                if (control is HtmlInputHidden)
                {
                    HtmlInputHidden txt = (HtmlInputHidden)control;
                    ht[txt.ID] = txt.Value.Trim();
                }
                if (control is HtmlInputPassword)
                {
                    HtmlInputPassword txt = (HtmlInputPassword)control;
                    ht[txt.ID] = txt.Value.Trim();
                }
                if (control is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox chk = (HtmlInputCheckBox)control;
                    ht[chk.ID] = chk.Checked ? 1 : 0;
                }
                if (control is HtmlTextArea)
                {
                    HtmlTextArea area = (HtmlTextArea)control;
                    ht[area.ID] = area.Value.Trim();
                }
            }
            return ht;
        }
        #endregion
        #region "是否存在"
        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <param name="pkValue">主键的值</param>
        /// <returns></returns>
        public bool IsExist(string pkValue)
        {
            bool flg = false;
            string strSql = DbCommon.RecordSql(this, false);
            SqlParam[] sqlParams = new SqlParam[] { 
                DbCommon.MakeParam("@ID", pkValue)
            };
            object value = DataFactory.GetInstance().ExecuteScalar(strSql, sqlParams);
            if (TypeConverter.ObjectToInt(value, 0) > 0)
                flg = true;
            return flg;
        }
        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <param name="ht">参数</param>
        /// <returns></returns>
        public bool IsExist(Hashtable ht)
        {
            bool flg = false;
            string strSql = DbCommon.RecordSql(this, ht);
            SqlParam[] sqlParams = DbCommon.GetParameter(ht);
            object value = DataFactory.GetInstance().ExecuteScalar(strSql, sqlParams);
            if (TypeConverter.ObjectToInt(value, 0) > 0)
                flg = true;
            return flg;
        }
        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <param name="where">条件语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <returns></returns>
        public bool IsExist(string where, SqlParam[] sqlParams)
        {
            bool flg = false;
            string strSql = DbCommon.RecordSql(this, where);
            object value = DataFactory.GetInstance().ExecuteScalar(strSql, sqlParams);
            if (TypeConverter.ObjectToInt(value, 0) > 0)
                flg = true;
            return flg;
        }
        #endregion
        #region "记录条数"
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <returns></returns>
        public int GetRecord()
        {
            string strSql = DbCommon.RecordSql(this);
            return TypeConverter.ObjectToInt(DataFactory.GetInstance().ExecuteScalar(strSql));
        }
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="ht">参数</param>
        /// <returns></returns>
        public int GetRecord(Hashtable ht)
        {
            string strSql = DbCommon.RecordSql(this, ht);
            SqlParam[] sqlParams = DbCommon.GetParameter(ht);
            return TypeConverter.ObjectToInt(DataFactory.GetInstance().ExecuteScalar(strSql, sqlParams));
        }
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="where">条件语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <returns></returns>
        public int GetRecord(string where, SqlParam[] sqlParams)
        {
            string strSql = DbCommon.RecordSql(this, where);
            return TypeConverter.ObjectToInt(DataFactory.GetInstance().ExecuteScalar(strSql, sqlParams));
        }
        #endregion
        #region "最大数字"
        /// <summary>
        /// 最大数字
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public int GetMax(string fieldName)
        {
            string strSql = DbCommon.MaxIDSql(this, fieldName);
            return TypeConverter.ObjectToInt(DataFactory.GetInstance().ExecuteScalar(strSql), 1);
        }
        /// <summary>
        /// 最大数字
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="where">条件语句</param>
        /// <returns></returns>
        public int GetMax(string fieldName, string where)
        {
            string strSql = DbCommon.MaxIDSql(this, fieldName, where);
            return TypeConverter.ObjectToInt(DataFactory.GetInstance().ExecuteScalar(strSql), 1);
        }
        /// <summary>
        /// 最大数字
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="where">条件语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <returns></returns>
        public int GetMax(string fieldName, string where, SqlParam[] sqlParams)
        {
            string strSql = DbCommon.MaxIDSql(this, fieldName, where);
            return TypeConverter.ObjectToInt(DataFactory.GetInstance().ExecuteScalar(strSql, sqlParams), 1);
        }
        #endregion
        #region "获取对象"
        /// <summary>
        /// 根据主键获取对象
        /// </summary>
        /// <param name="pkValue">主键的值</param>
        /// <returns></returns>
        public T GetModelById(string pkValue)
        {
            string strSql = DbCommon.SingleSql(this);
            SqlParam[] sqlParams = new SqlParam[] { DbCommon.MakeParam("@ID", pkValue) };
            DataTable dt = DataFactory.GetInstance().ExecuteTable(strSql, sqlParams);
            if (dt != null && dt.Rows.Count > 0)
            {
                return DbReader.ReaderToModel<T>(dt.Rows[0]);
            }
            return new T();
        }
        /// <summary>
        /// 根据参数获取对象
        /// </summary>
        /// <param name="ht">参数</param>
        /// <returns></returns>
        public T GetModelById(Hashtable ht)
        {
            string strSql = DbCommon.SingleSql(this, ht);
            SqlParam[] sqlParams = DbCommon.GetParameter(ht);
            DataTable Dt = DataFactory.GetInstance().ExecuteTable(strSql, sqlParams);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                return DbReader.ReaderToModel<T>(Dt.Rows[0]);
            }
            return new T();
        }
        /// <summary>
        /// 根据条件获取对象
        /// </summary>
        /// <param name="where">条件语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <returns></returns>
        public T GetModelById(string where, SqlParam[] sqlParams)
        {
            string strSql = DbCommon.SingleSql(this, where);
            DataTable Dt = DataFactory.GetInstance().ExecuteTable(strSql, sqlParams);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                return DbReader.ReaderToModel<T>(Dt.Rows[0]);
            }
            return new T();
        }
        #endregion
        #region "所有记录"
        /// <summary>
        /// 所有记录
        /// </summary>
        /// <param name="orderFields">排序字段</param>
        /// <returns></returns>
        public IList GetAllList(string orderFields)
        {
            return GetAllList("*", "", orderFields);
        }
        /// <summary>
        /// 所有记录
        /// </summary>
        /// <param name="where">条件语句</param>
        /// <param name="orderFields">排序字段</param>
        /// <returns></returns>
        public IList GetAllList(string where,string orderFields)
        {
            return GetAllList("*", where, orderFields);
        }
        /// <summary>
        /// 所有记录
        /// </summary>
        /// <param name="fields">显示字段</param>
        /// <param name="where">条件语句</param>
        /// <param name="orderFields">排序字段</param>
        /// <returns></returns>
        public IList GetAllList(string fields, string where, string orderFields)
        {
            return GetAllList(fields, where, (SqlParam[])null, orderFields);
        }
        /// <summary>
        /// 所有记录
        /// </summary>
        /// <param name="fields">显示字段</param>
        /// <param name="where">条件语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <param name="orderFields">排序字段</param>
        /// <returns></returns>
        public IList GetAllList(string fields, string where, SqlParam[] sqlParams, string orderFields)
        {
            string strSql = DbCommon.MultipleSql(this, fields, where, orderFields);
            return DataFactory.GetInstance().ExecuteTable<T>(strSql, sqlParams);
        }
        #endregion
        #region "多条记录"
        /// <summary>
        /// 多条记录
        /// </summary>
        /// <param name="number">记录条数</param>
        /// <param name="fields">显示字段</param>
        /// <param name="ht">参数列表</param>
        /// <param name="orderFields">排序字段</param>
        /// <returns></returns>
        public IList GetList(int number, string fields, Hashtable ht, string orderFields)
        {
            StringBuilder sb = new StringBuilder();
            if (ht != null && ht.Count > 0)
            {
                foreach (string key in ht.Keys)
                {
                    sb.AppendFormat(" And {0}=@{0}", key);
                }
            }
            SqlParam[] sqlParams = DbCommon.GetParameter(ht);
            return GetList(number, fields, sb.ToString(), sqlParams, orderFields);
        }
        /// <summary>
        /// 多条记录
        /// </summary>
        /// <param name="number">记录条数</param>
        /// <param name="fields">显示字段</param>
        /// <param name="where">条件语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <param name="orderFields">排序字段</param>
        /// <returns></returns>
        public IList GetList(int number,string fields, string where, SqlParam[] sqlParams, string orderFields)
        {
            string strSql = DbCommon.MultipleSql(this, number, fields, where, orderFields);
            return DataFactory.GetInstance().ExecuteTable<T>(strSql, sqlParams);
        }
        #endregion
        #region "插入数据"
        /// <summary>
        /// 通过实体类插入数据
        /// </summary>
        /// <returns></returns>
        public int Insert()
        {
            return Insert(this);
        }
        /// <summary>
        /// 通过实体类插入数据
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public int Insert(object model)
        {
            string strSql = DbCommon.InsertSql(model);
            SqlParam[] sqlParams = DbCommon.GetParameter(model);
            return DataFactory.GetInstance().ExecuteNonQuery(strSql, sqlParams);
        }
        /// <summary>
        /// 通过哈希表插入数据
        /// </summary>
        /// <param name="ht">参数</param>
        /// <returns></returns>
        public int Insert(Hashtable ht)
        {
            string strSql = DbCommon.InsertSql(this, ht);
            SqlParam[] sqlParams = DbCommon.GetParameter(ht);
            return DataFactory.GetInstance().ExecuteNonQuery(strSql, sqlParams);
        }
        #endregion
        #region "更新数据"
        /// <summary>
        /// 根据主键更新数据
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            return Update(this);
        }
        /// <summary>
        /// 根据主键更新数据
        /// </summary>
        /// <returns></returns>
        public int Update(string pkValue)
        {
            Type type = this.GetType();
            KeyAttribute Key = Public.GetAttribute(type);
            object Value = Convert.ChangeType(pkValue, type.GetProperty(Key.PkName).PropertyType);
            type.GetProperty(Key.PkName).SetValue(this, Value, null);
            return Update(this);
        }
        /// <summary>
        /// 根据主键更新数据
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public int Update(object model)
        {
            string strSql = DbCommon.UpdateSql(model).ToString();
            SqlParam[] sqlParams = DbCommon.GetParameter(model);
            return DataFactory.GetInstance().ExecuteNonQuery(strSql, sqlParams);
        }
        /// <summary>
        /// 根据条件更新数据
        /// </summary>
        /// <param name="where">条件语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <returns></returns>
        public int Update(string where, SqlParam[] sqlParams)
        {
            return Update(this, where, sqlParams);
        }
        /// <summary>
        /// 根据条件更新数据
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="where">条件语句</param>
        /// <param name="Params">对象参数</param>
        /// <returns></returns>
        public int Update(object model, string where, SqlParam[] Params)
        {
            string strSql = DbCommon.UpdateSql(model, where);
            SqlParam[] sqlParams = DbCommon.GetParameter(model, Params);
            return DataFactory.GetInstance().ExecuteNonQuery(strSql, sqlParams);
        }
        /// <summary>
        /// 根据条件更新数据
        /// </summary>
        /// <param name="fields">更新字段</param>
        /// <param name="where">条件语句</param>
        /// <returns></returns>
        public int Update(string fields, string where)
        {
            return Update(fields, where, (SqlParam[])null);
        }
        /// <summary>
        /// 根据条件更新数据
        /// </summary>
        /// <param name="fields">更新字段</param>
        /// <param name="where">条件语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <returns></returns>
        public int Update(string fields, string where, SqlParam[] sqlParams)
        {
            string strSql = DbCommon.UpdateSql(this, fields, where);
            return DataFactory.GetInstance().ExecuteNonQuery(strSql, sqlParams);
        }
        #endregion
        #region "删除数据"
        /// <summary>
        /// 根据主键删除数据
        /// </summary>
        /// <param name="pkValue">主键的值</param>
        /// <returns></returns>
        public int Delete(string pkValue)
        {
            string strSql = DbCommon.DeleteSql(this);
            SqlParam[] sqlParams = new SqlParam[] { 
                DbCommon.MakeParam("@ID",pkValue)
            };
            return DataFactory.GetInstance().ExecuteNonQuery(strSql, sqlParams);
        }
        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="ht">参数</param>
        /// <returns></returns>
        public int Delete(Hashtable ht)
        {
            string strSql = DbCommon.DeleteSql(this, ht);
            SqlParam[] sqlParams = DbCommon.GetParameter(ht);
            return DataFactory.GetInstance().ExecuteNonQuery(strSql, sqlParams);
        }
        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="where">条件语句</param>
        /// <param name="sqlParams">对象参数</param>
        /// <returns></returns>
        public int Delete(string where, SqlParam[] sqlParams)
        {
            string strSql = DbCommon.DeleteSql(this, where);
            return DataFactory.GetInstance().ExecuteNonQuery(strSql, sqlParams);
        }
        #endregion
    }
}