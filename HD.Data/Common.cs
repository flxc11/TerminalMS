using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HD.Data
{
    public class Common
    {
        public DataTable TClass()
        {
            return HD.Framework.DataAccess.DataFactory.GetInstance().ExecuteTable("select * from wzrb_Class order by ID");
        }
    }
}
