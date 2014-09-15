//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Text;
using HD.Framework.Utils;
using HD.Framework.Helper;

namespace HD.Framework.DataAccess
{
    /// <summary>
    /// 数据表相关语句
    /// </summary>
    public class SqlTables
    {
        #region "获取所有数据表"
        /// <summary>
        /// 获取所有数据表
        /// </summary>
        /// <returns></returns>
        public static string GetTableList()
        {
            string TableList = string.Empty;
            DataBaseType dbType = DataFactory.GetInstance().DataBaseType;
            switch (dbType)
            {
                case DataBaseType.Oracle:
                    break;
                case DataBaseType.SqlServer:
                    TableList = GetSqlServerTable();
                    break;
                case DataBaseType.MySql:
                    break;
                case DataBaseType.Access:
                    break;
                case DataBaseType.Sqlite:
                    break;
            }
            return TableList;
        }
        #region "SqlServer所有表"
        /// <summary>
        /// SqlServer所有表
        /// </summary>
        /// <returns></returns>
        private static string GetSqlServerTable()
        {
            string TableInfo = string.Empty;
            if (DbPager.IsSql2000)
            {
                TableInfo = @"SELECT Field = CASE WHEN a.colorder = 1 THEN d.name 
                            ELSE '' END , 
                            Remark = CASE WHEN a.colorder = 1 THEN ISNULL(f.value, '') 
                                           ELSE '' 
                                      END 
                             FROM 
                                syscolumns a 
                                INNER JOIN sysobjects d ON a.id = d.id 
                                                           AND d.xtype = 'U' 
                                                           AND d.name <> 'sys.extended_properties' 
                                                           AND d.name <> 'dtproperties' 
                                LEFT JOIN sysproperties f ON a.id = f.id 
                                                             AND f.smallid = 0 
                             WHERE 
                                ( CASE WHEN a.colorder = 1 THEN d.name 
                                       ELSE '' 
                                  END ) <> '' 
                             ORDER BY 
                                Field";
            }
            else
            {
                TableInfo = @"SELECT ID = D.ID ,
                            Field = CASE WHEN A.COLORDER = 1 THEN D.NAME
                            ELSE ''
                            END ,
                            Remark = CASE WHEN A.COLORDER = 1 THEN ISNULL(F.VALUE, '')
                            ELSE ''
                            END ,
                            ParentID = 0 ,
                            colorder = 0
                            FROM SYSCOLUMNS A
                            LEFT JOIN SYSTYPES B ON A.XUSERTYPE = B.XUSERTYPE
                            INNER JOIN SYSOBJECTS D ON A.ID = D.ID
                            AND D.XTYPE = 'U'
                            AND D.NAME <> 'DTPROPERTIES'
                            LEFT JOIN sys.extended_properties F ON D.ID = F.major_id
                            WHERE a.COLORDER = 1
                            AND F.minor_id = 0";
            }
            return TableInfo;
        }
        #endregion
        #endregion
        #region "获取相关表主键"
        /// <summary>
        /// 获取表的主键
        /// </summary>
        /// <param name="TableName">表的名称</param>
        /// <returns></returns>
        public static string GetPrimaryKey(string TableName)
        {
            string PrimaryKey = string.Empty;
            DataBaseType dbType = DataFactory.GetInstance().DataBaseType;
            switch (dbType)
            {
                case DataBaseType.Oracle:
                    break;
                case DataBaseType.SqlServer:
                    PrimaryKey = SqlServerPrimaryKey(TableName);
                    break;
                case DataBaseType.MySql:
                    break;
                case DataBaseType.Access:
                    break;
                case DataBaseType.Sqlite:
                    break;
            }
            return PrimaryKey;
        }
        #region "SqlServer获取表的主键"
        /// <summary>
        /// SqlServer获取表的主键
        /// </summary>
        /// <param name="TableName">表的名称</param>
        /// <returns></returns>
        private static string SqlServerPrimaryKey(string TableName)
        {
            string PrimaryKey = string.Empty;
            PrimaryKey = @"SELECT  a.name
                         FROM    SYSCOLUMNS A
                         INNER JOIN SYSOBJECTS D ON A.ID = D.ID
                         AND D.XTYPE = 'U'
                         AND D.NAME <> 'DTPROPERTIES'
                         WHERE   d.name = '{0}' AND EXISTS ( SELECT 1  FROM SYSOBJECTS WHERE XTYPE = 'PK' AND PARENT_OBJ = A.ID AND NAME IN ( SELECT NAME FROM SYSINDEXES WHERE INDID IN ( SELECT INDID FROM SYSINDEXKEYS WHERE  ID = A.ID AND COLID = A.COLID ) ) )";
            PrimaryKey = string.Format(PrimaryKey, TableName);
            return PrimaryKey;
        }
        #endregion
        #endregion
        #region "获取所有的字段"
        /// <summary>
        /// 获取所有的字段
        /// </summary>
        /// <param name="TableName">数据表名称</param>
        /// <returns></returns>
        public static string GetTableColumn(string TableName)
        {
            string ColumnList = string.Empty;
            DataBaseType dbType = DataFactory.GetInstance().DataBaseType;
            switch (dbType)
            {
                case DataBaseType.Oracle:
                    break;
                case DataBaseType.SqlServer:
                    ColumnList = SqlServerColumn(TableName);
                    break;
                case DataBaseType.MySql:
                    break;
                case DataBaseType.Access:
                    break;
                case DataBaseType.Sqlite:
                    break;
            }
            return ColumnList;
        }
        #region "SqlServer表结构"
        /// <summary>
        /// SqlServer表结构
        /// <param name="TableName">数据表名称</param>
        /// </summary>
        /// <returns></returns>
        private static string SqlServerColumn(string TableName)
        {
            string ColumnList = string.Empty;
            if (DbPager.IsSql2000)
            {
                ColumnList = @"SELECT 
                            [number]   = a.colorder,
                            [column]   = a.name,
                            [datatype] = b.name,
                            [length]   = COLUMNPROPERTY(a.id,a.name,'PRECISION'),
                            [identity] = case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end,
                            [key]      = case when exists(SELECT 1 FROM sysobjects where xtype='PK' and parent_obj=a.id and name in (
                            SELECT name FROM sysindexes WHERE indid in(
                            SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid)))then '√' else '' end,
                            [isnullable]= case when a.isnullable=1 then '√'else '' end,
                            [default]  = isnull(e.text,''),
                            [remark]   = isnull(g.[value],'')
                            FROM syscolumns a left 
                            join systypes b 
                            on a.xusertype=b.xusertype
                            inner join sysobjects d 
                            on a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties'
                            left join syscomments e 
                            on a.cdefault=e.id
                            left join sysproperties g on a.id=g.id and a.colid=g.smallid  
                            left join sysproperties f on d.id=f.id and f.smallid=0
                            where  d.name='{0}' order by a.id,a.colorder";
            }
            else
            {
               ColumnList = @"SELECT
                            [number]=a.colorder,
                            [column] =a.name,
		                    [datatype]=b.name,
		                    [length]=COLUMNPROPERTY(a.id,a.name,'PRECISION'),
		                    [identity]=case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end,
                            [key]=case when exists(SELECT 1 FROM sysobjects where xtype='PK' and parent_obj=a.id and name in                             (
                            SELECT name FROM sysindexes WHERE indid in(
                            SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid
                            ))) then '√' else '' end,
                            [isnullable]=case when a.isnullable=1 then '√'else '' end,
                            [default]=isnull(e.text,''),
                            [remark]=isnull(g.[value],'')
                            FROM syscolumns a
                            left join systypes b on a.xusertype=b.xusertype
                            inner join sysobjects d on a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties'
                            left join syscomments e on a.cdefault=e.id
                            left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id 
                            left join sys.extended_properties f on d.id=f.major_id and f.minor_id=0
                            where d.name='{0}' order by a.id,a.colorder";
            }
            ColumnList = string.Format(ColumnList, TableName);
            return ColumnList;
        }
        #endregion
        #endregion
    }
}