using HD.Framework.Aop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.Data
{
    [Aspect]
    public class Terminal : ContextBoundObject
    {
        #region 添加终端信息
        [Transaction]
        public void Add(Model.Terminal terminal, Model.Source source)
        {
            terminal.Insert();
            

            //循环插入附件
            string[] picName = source.SourceUrl.Replace("|$|", "&").Split(new char[] { '&' });
            for (int i = 0; i < picName.Length - 1; i++)
            {
                string[] picUrl = picName[i].Replace("|#|", "|").Split(new char[] { '|' });
                source.SourceUrl = picUrl[1];
                source.SourceType = picUrl[0];
                source.Insert();
            }
        }
        #endregion

        #region 编辑终端信息
        [Transaction]
        /// <summary>
        /// 编辑终端信息
        /// </summary>
        public void Edit(Model.Terminal terminal, Model.Source source1)
        {
            terminal.Update();
            

            //循环更新附件
            string[] picName = source1.SourceUrl.Replace("|$|", "&").Split(new char[] { '&' });
            for (int i = 0; i < picName.Length - 1; i++)
            {
                string[] picUrl = picName[i].Replace("|#|", "|").Split(new char[] { '|' });
                source1.SourceUrl = picUrl[1];
                source1.SourceType = picUrl[0];
                Hashtable ht = new Hashtable();
                ht.Add("TerGuid", source1.TerGuid);
                ht.Add("SourceType", source1.SourceType);
                if (source1.IsExist(ht))
                {
                    source1.Update(
                        "SourceUrl='" + source1.SourceUrl + "'",
                        " and TerGuid='" + source1.TerGuid + "' and SourceType='" + source1.SourceType + "'");
                }
                else
                {
                    source1.CreateTime = DateTime.Now;
                    source1.Insert();
                }

            }

            
        }
        #endregion

        #region 删除终端信息
        [Transaction]
        public void TerDelete(Model.Terminal terminal, Model.Source source)
        {
            Hashtable ht = new Hashtable();
            ht.Add("Guid", terminal.Guid);
            terminal.Delete(ht);

            ht.Clear();
            ht.Add("TerGuid", terminal.Guid);
            source.Delete(ht);
        }
        #endregion
    }
}
