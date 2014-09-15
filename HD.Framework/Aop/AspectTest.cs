using HD.Framework.DataAccess;
using HD.Framework.Utils;
//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Framework.Aop
{
    [Aspect]
    public class AspectTest : ContextBoundObject
    {
        [Transaction]
        public void Test()
        {
            IDataBase DbHelper = DataFactory.GetInstance();
            for (int i = 0; i < 100; i++)
            {
                string AdminID = Public.GetGuID;
                string RoleID = Rand.Number(6);
                string AdminName = "潘向福";
                string strSql = "INSERT INTO CNVP_Admin (AdminID,RoleID,AdminName) VALUES (@AdminID,@RoleID,@AdminName)";
                SqlParam[] Param = new SqlParam[] { 
                    DbHelper.MakeParam("@AdminID",AdminID),
                    DbHelper.MakeParam("@AdminName",AdminName),
                    DbHelper.MakeParam("@RoleID",RoleID)
                };
                DbHelper.ExecuteNonQuery(strSql, Param);                
            }
            Test2();
        }
        [Transaction]
        public void Test2()
        {
            IDataBase DbHelper = DataFactory.GetInstance();
            for (int i = 0; i < 100; i++)
            {
                string AdminID = Public.GetGuID;
                string RoleID = Rand.Number(6);
                string AdminName = "捷点科技";
                string strSql = string.Format("INSERT INTO CNVP_Admin (AdminID,RoleID,AdminName) VALUES ('{0}','{1}','{2}')", AdminID, RoleID, AdminName);
                DbHelper.ExecuteNonQuery(strSql);
                //if (i >= 2)
                //{
                //    throw new Exception("SQL 错误");
                //}
            }
        }
    }
}

