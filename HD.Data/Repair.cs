using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HD.Framework.Aop;

namespace HD.Data
{
    [Aspect]
    public class Repair
    {
        #region 添加报修回复

        [Transaction]
        public void Add(Model.Repair repair, Model.Reply reply)
        {
            string guid = repair.Guid;
            repair.Update();

            Hashtable hs = new Hashtable();
            hs.Add("RepairGuid", guid);
            if (reply.IsExist(hs))
            {
                reply.Update(
                    "ReplyContent='" + reply.ReplyContent + "',ReplyName='" + reply.ReplyName + "',ReplyRepairTime='" + reply.ReplyRepairTime + "'",
                    " and RepairGuid='" + guid + "'");
            }
            else
            {
                reply.Insert();
            }
        }
        #endregion
    }
}
