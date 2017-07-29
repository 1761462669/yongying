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
        public DatabaseHelper dsC2Sql = new DatabaseHelper(ConnectionType.C2Sql);
        DatabaseHelper dsSql = new DatabaseHelper(ConnectionType.Sql);

        private StringBuilder LogStr = new StringBuilder();

        /// <summary>
        /// 获取需要插入生产信息的工单号
        /// </summary>
        public DataSet GetStartWO()
        {
            String sqlWO = @"SELECT vValue WO FROM Live WHERE TagName LIKE '%ZS%_GD' AND CONVERT(VARCHAR,vValue)!='null' AND vValue IS NOT NULL ";
            DataSet ds = new DataSet();
            ds = dsInSql.ExecuteDataSet(sqlWO);
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
        /// <summary>
        /// 获取C2编号标记开始时间
        /// </summary>
        /// <returns></returns>
        public DataSet GetStartTime()
        {
            String sql = @"SELECT TOP 1 * FROM SPC.TIMEFLAG 
            WHERE MACHINE='C2-23051'
            ORDER BY TIMEFLAG DESC

            SELECT TOP 1 * FROM SPC.TIMEFLAG 
            WHERE MACHINE='C2-23052'
            ORDER BY TIMEFLAG DESC

            SELECT TOP 1 * FROM SPC.TIMEFLAG 
            WHERE MACHINE='C2-23059'
            ORDER BY TIMEFLAG DESC";
            DataSet ds = dsSql.ExecuteDataSet(sql, QcAnalysis.ConnectionState.KeepOpen);
            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataSet GetC2Data(DateTime dt, String machine)
        {
            String sql = String.Format(@"SELECT MachineID,GMT_Date_Time,BATCH_BRAND,rowguid FROM DATA 
            WHERE GMT_DATE_TIME>'{0}'  AND MACHINEID ='{1}' ORDER BY GMT_Date_Time", dt, machine);
            DataSet ds = dsC2Sql.ExecuteDataSet(sql, QcAnalysis.ConnectionState.KeepOpen);
            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowGuid"></param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        public String SaveC2Data(DateTime timeFlag, String machine, int rowNum)
        {
            String message = "";
            String sql = String.Format(@"INSERT INTO SPC.ROWNUMFLAG(ROWTIME,MACHINE,ROWNUM) VALUES('{0}','{1}',{2})", timeFlag, machine, rowNum);
            try
            {
                dsSql.ExecuteNonQuery(sql, QcAnalysis.ConnectionState.KeepOpen);
            }
            catch (Exception ex)
            {

                message = ex.Message;
            }
            return message;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeFlag"></param>
        /// <param name="mat"></param>
        /// <param name="machine"></param>
        /// <returns></returns>
        public string SaveC2TimeFlag(DateTime timeFlag, string mat, string machine)
        {
            String message = "";
            String sql = String.Format(@"INSERT INTO SPC.TIMEFLAG(TIMEFLAG,MAT,MACHINE) VALUES('{0}','{1}','{2}')", timeFlag, mat, machine);
            try
            {
                dsSql.ExecuteNonQuery(sql, QcAnalysis.ConnectionState.KeepOpen);
            }
            catch (Exception ex)
            {

                message = ex.Message;
            }
            return message;
        }

        public int GetContinueRowNum(DateTime timeFlag, String machine)
        {
            int rowNum = 0;
            String sqlRowNum = String.Format(@"SELECT ROWNUM FROM SPC.ROWNUMFLAG WHERE ROWTIME='{0}' AND MACHINE='{1}'", timeFlag, machine);
            DataSet ds = dsSql.ExecuteDataSet(sqlRowNum, QcAnalysis.ConnectionState.KeepOpen);
            if (ds.Tables[0].Rows.Count > 0)
            {
                rowNum = Convert.ToInt32(ds.Tables[0].Rows[0]["ROWNUM"]);
            }
            return rowNum;
        }

        public string GetC2RowNum()
        {
            String message = "";
            LogStr.Clear();

            DateTime timeFlag;
            String mat;
            String machine;
            int rowNum = 1;
            String rowGuid = "";
            DataSet dsTimeFlag = this.GetStartTime();

            if (dsTimeFlag.Tables[0].Rows.Count > 0 || dsTimeFlag.Tables[1].Rows.Count > 0 || dsTimeFlag.Tables[2].Rows.Count > 0)
            {
                for (int i = 0; i < dsTimeFlag.Tables.Count; i++)
                {
                    timeFlag = Convert.ToDateTime(dsTimeFlag.Tables[i].Rows[0]["TIMEFLAG"]);
                    mat = dsTimeFlag.Tables[i].Rows[0]["MAT"].ToString();
                    machine = dsTimeFlag.Tables[i].Rows[0]["MACHINE"].ToString();
                    DataSet ds = this.GetC2Data(timeFlag, machine);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["BATCH_BRAND"].ToString().Equals(mat))
                        {
                            rowNum = GetContinueRowNum(timeFlag, machine) + 1;
                        }
                        dsSql.BeginTransaction();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (!dr["BATCH_BRAND"].ToString().Equals(mat))
                            {
                                rowNum = 1;
                            }
                            if (rowNum > 20)
                            {
                                rowNum = 1;
                            }
                            TimeSpan timeSpan = Convert.ToDateTime(dr["GMT_Date_Time"]) - timeFlag;
                            timeFlag = Convert.ToDateTime(dr["GMT_Date_Time"]);
                            if (timeSpan.Hours > 4)
                            {
                                rowNum = 1;
                            }
                            mat = dr["BATCH_BRAND"].ToString();
                            rowGuid = dr["rowguid"].ToString();
                            message += SaveC2Data(timeFlag, machine, rowNum);
                            rowNum++;

                        }
                        message += SaveC2TimeFlag(timeFlag, mat, machine);
                        dsSql.CommitTransaction();
                    }
                }
            }
            if (!String.IsNullOrEmpty(message))
            {
                string tempStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss    ") + message + "\r\n";
                LogStr.Append(tempStr);
                String savePath = "D:\\GetC2RowNum_Log";
                String fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                String fileMsg = LogStr.ToString() + "\r\n";
                LogHelper.SaveMsgToFile(savePath, fileName, fileMsg);
            }
            return message;
        }
    }
}
