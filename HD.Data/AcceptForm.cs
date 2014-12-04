using HD.Framework.Aop;
using HD.Framework.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HD.Data
{
    [Aspect]
    public class AcceptForm : ContextBoundObject
    {
        #region 添加受理单
        [Transaction]
        public void Add(Model.AcceptForm acceptform, Model.AD ad, Model.Publish publish)
        {

            //广告插入
            ad.Insert();

            //循环插入发布形式
            string[] pubTypeName = publish.PublishType.Split(',');
            string[] pubTypeNu = publish.PublishQuantity.Split(',');
            for (int i = 0; i < pubTypeName.Length; i++)
			{
                publish.ADGuid = ad.ADPGuid;
                publish.PublishType = pubTypeName[i];
                publish.PublishQuantity = pubTypeNu[i];

                publish.Insert();
			}

            //插入受理单信息
            acceptform.Insert();

        }
        #endregion

        #region 编辑受理单
        [Transaction]
        public void Edit(Model.AcceptForm acceptform, Model.AD ad, Model.Client client, Model.Publish publish)
        {
            //客户信息插入或更新
            Hashtable hs = new Hashtable();
            hs.Add("ClientName", client.ClientName);
            if (client.IsExist(hs))
            {
                client.Update(
                    "ClientName='" + client.ClientName + "',Tel='" + client.Tel + "',Mobile='" + client.Mobile + "',Operator='" + client.Operator + "',AgencyCompany='" + client.AgencyCompany + "'",
                    " and ClientName ='" + client.ClientName + "'");
                HD.Model.Client newClient = HD.Model.Client.Instance.GetModelById(hs);
                acceptform.ClientGuid = newClient.ClientPGuid;
            }
            else
            {
                client.ClientPostTime = DateTime.Now;
                client.ClientPGuid = Public.GetGuID;
                client.Insert();
                acceptform.ClientGuid = client.ClientPGuid;
            }

            //广告插入
            ad.Update();

            //先删除原来的发布形式，再循环插入发布形式
            //string strSql = " and ADGuid='" + publish.ADGuid + "'";
            hs.Clear();
            hs.Add("ADGuid", ad.ADPGuid);
            publish.Delete(hs);
            //string StrSql = "Select * From " + DbConfig.Prefix + "Admin Where AdminName=@AdminName And AdminPass=@AdminPass And IsLock=1";
            //IDataParameter[] Param = new IDataParameter[] { 
            //    DbHelper.MakeParam("@AdminName",AdminName),
            //    DbHelper.MakeParam("@AdminPass",AdminPass)
            //};
            string[] pubTypeName = publish.PublishType.Split(',');
            string[] pubTypeNu = publish.PublishQuantity.Split(',');
            for (int i = 0; i < pubTypeName.Length; i++)
            {
                publish.ADGuid = ad.ADPGuid;
                publish.PublishType = pubTypeName[i];
                publish.PublishQuantity = pubTypeNu[i];

                publish.Insert();
            }

            //更新受理单信息
            acceptform.Update(acceptform);

        }
        #endregion
    }
}
