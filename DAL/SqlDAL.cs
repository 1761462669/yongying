using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QcAnalysis;
using System.Data;

namespace DAL
{
    public class SqlDAL
    {
        DatabaseHelper dsInSql = new DatabaseHelper(ConnectionType.InSql);
        /// <summary>
        /// 获取需要插入生产信息的工单号
        /// </summary>
        public DataSet GetStartWO()
        {
            String sqlWO = @"SELECT vValue WO FROM Live WHERE TagName LIKE '%ZS%_GD' AND CONVERT(VARCHAR,vValue)!='null' AND vValue IS NOT NULL ";
            DataSet ds = new DataSet();
            ds=dsInSql.ExecuteDataSet(sqlWO);
            return ds;
        }
        /// <summary>
        /// 获取需要插入生产信息的工单号并重新插入生产信息
        /// </summary>
        public void GetStartWOInfo()
        {
            String sqlWO = @"SELECT DateTime,TagName CTRL,vValue INFO FROM Live WHERE TagName LIKE 'SPC_ZSJK%' AND CONVERT(VARCHAR,vValue)!='null' AND vValue IS NOT NULL
                                    ORDER BY TagName ";
            DataSet ds = dsInSql.ExecuteDataSet(sqlWO);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                String tag = dr["CTRL"].ToString();
                String value = dr["INFO"].ToString();
                String insql = string.Format(@"INSERT INSQL.Runtime.dbo.StringHistory (DateTime, TagName,Value, QualityDetail, wwVersion)
                        VALUES(getdate(), '{0}','{1}', 192, 'REALTIME')", tag, value);
                dsInSql.ExecuteNonQuery(insql);
            }
        }
    }
}
