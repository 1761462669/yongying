using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QcAnalysis;
using System.Data;
using Entity;

namespace DAL
{
    public class SqlDAL
    {
        DatabaseHelper dsInSql = new DatabaseHelper(ConnectionType.InSql);
        public DatabaseHelper dsC2Sql = new DatabaseHelper(ConnectionType.C2Sql);
        DatabaseHelper dsSql = new DatabaseHelper(ConnectionType.Sql);
        DatabaseHelper dsSSQL = new DatabaseHelper(ConnectionType.SSql);
        MathUtils mu = new MathUtils();

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
            DataSet ds = dsC2Sql.ExecuteDataSet(sql);
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
        /// <summary>
        /// C2样本编号
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// C2样本数据计算
        /// </summary>
        /// <returns></returns>
        public string C2DataStatistics()
        {
            String message;
            LogStr.Clear();

            DateTime startTime;
            DateTime endTime;
            String wo;
            String machine;
            int orderNo;
            String mat_Ctrl ;
            String matID ;
            DataSet dsC2Data = GetC2DataStatTime();

            if (dsC2Data.Tables[0].Rows.Count > 0 || dsC2Data.Tables[1].Rows.Count > 0 || dsC2Data.Tables[2].Rows.Count > 0)
            {
                for (int i = 0; i < dsC2Data.Tables.Count; i++)
                {
                    startTime = Convert.ToDateTime(dsC2Data.Tables[i].Rows[0]["CHECKTIME"]);
                    wo = dsC2Data.Tables[i].Rows[0]["WO"].ToString();
                    machine = dsC2Data.Tables[i].Rows[0]["MACHINE"].ToString();
                    orderNo = Convert.ToInt32(dsC2Data.Tables[i].Rows[0]["ORDERNUM"]);
                    DataSet dsEndTime = GetC2DataEndTime(startTime, machine);
                    endTime = Convert.ToDateTime(dsEndTime.Tables[0].Rows[0]["ROWTIME"]);
                    DataSet ds = GetC2DataFromMes(startTime, endTime, machine);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        IList<CData> list = ConvetToObjList(ds.Tables[0]);
                        CDataResult res = new CDataResult();
                       mu.GetStatistics(list, res);
                       mat_Ctrl = list[list.Count - 1].mat_Ctrl;
                       startTime = list[list.Count + 1].testdate;
                       //取标准 计算CPK
                        DataSet dsT = GetTestID(machine);
                        matID = GetMatID(mat_Ctrl);
                        switch (machine)
                        {
                            case "":
                                break;
                            case "":
                                break;
                            case "":
                                break;
                            default:
                                break;
                        }
                        GetCPKAndStandard(list,matID,dsT);

                        res.WG_CPK = Convert.ToDecimal(ComputCPK(wgData));
                        SaveC2StatisticsData(list, res);
                        //
                        //dsSql.BeginTransaction();
                        //foreach (DataRow dr in ds.Tables[0].Rows)
                        //{
                        //    int count = 1;
                        //    decimal data = 0;
                        //    decimal avg;
                        //    decimal stdv;
                        //    decimal cpk;

                        //    if (String.IsNullOrEmpty( dr["PD"].ToString()))
                        //    {
                        //        data += Convert.ToDecimal(dr["PD"]);
                        //    }
                        //    wo = dr["BATCH_BRAND"].ToString();
                        //    rowGuid = dr["rowguid"].ToString();
                        //    message += SaveC2Data(endTime, machine, rowNum);
                        //    rowNum++;

                        //}
                        //message += SaveC2TimeFlag(endTime, wo, machine);
                        //dsSql.CommitTransaction();
                    }

                }
            }
            if (!String.IsNullOrEmpty(message))
            {
                string tempStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss    ") + "C2" + message + "\r\n";
                LogStr.Append(tempStr);
                String savePath = "D:\\GetC2RowNum_Log";
                String fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                String fileMsg = LogStr.ToString() + "\r\n";
                LogHelper.SaveMsgToFile(savePath, fileName, fileMsg);
            }
            return message;
        }

        private void GetCPKAndStandard(IList<CData> list, string matID, DataSet dsT)
        {
            switch ("")
            {
                case "":
                    break;
                default:
            }
        }

        private String GetMatID(string mat_Ctrl)
        {
            String sql = String.Format(@"SELECT PARTID FROM SPC.PART WHERE GROUPID=2 AND CTRL='{0}'", mat_Ctrl);
            String matID = dsSSQL.ExecuteScalar(sql).ToString();
            return matID;
        }

        private DataSet GetTestID(string machine)
        {
            String sql = String.Format(@"SELECT TESTID,TEST from SPC.TEST_ITEM_CFG WHERE FILTER LIKE '%{0}%'", machine);
            DataSet ds = dsSSQL.ExecuteDataSet(sql);
            return ds;
        }

        private void SaveC2StatisticsData(IList<CData> list, CDataResult res)
        {
            throw new NotImplementedException();
        }

        private IList<CData> ConvetToObjList(DataTable dataTable)
        {
            IList<CData> list = new List<CData>();
            CData cd = new CData();
            foreach (DataRow dr in dataTable.Rows)
            {
                cd.Pd = Convert.ToDecimal(dr["PD"]);
                cd.Len = Convert.ToDecimal(dr["LEN_"]);
                cd.CIRC = Convert.ToDecimal(dr["CIRC"]);
                cd.Wg = Convert.ToDecimal(dr["WG"]);
                cd.TV = Convert.ToDecimal(dr["TV"]);
                cd.testdate = Convert.ToDateTime(dr["testdate"]);
                cd.Wo = dr["WO"].ToString();
                cd.mat_Ctrl = dr["MAT_CTRL"].ToString();
                list.Add(cd);
            }
            return list;
        }

        private DataSet GetC2DataFromMes(DateTime startTime, DateTime endTime, string machine)
        {
            String sql = String.Format(@"SELECT  * FROM OfflineDataToSpc
            WHERE testdate>DATEADD(HH, 8, '{0}') AND testdate<=DATEADD(HH, 8, '{1}') AND EQU_CTRL='{2}'", startTime, endTime, machine);
            DataSet ds = dsSql.ExecuteDataSet(sql, QcAnalysis.ConnectionState.KeepOpen);
            return ds;
        }

        public DataSet GetC2DataEndTime(DateTime timeFlag, string machine)
        {
            String sql = String.Format(@"SELECT TOP 1 ROWTIME FROM SPC.ROWNUMFLAG
            WHERE ROWTIME>'{0}' AND MACHINE='{1}' AND ROWNUM=20
            ORDER BY ROWTIME", timeFlag, machine);
            DataSet ds = dsSql.ExecuteDataSet(sql, QcAnalysis.ConnectionState.KeepOpen);
            return ds;
        }

        public DataSet GetC2DataStatTime()
        {
            String sql = @"SELECT TOP 1 CHECKTIME,WO,MACHINE,ORDERNUM FROM SPC.C2RESULTDATA 
            WHERE MACHINE='C2-23051'
            ORDER BY CHECKTIME DESC

            SELECT TOP 1 CHECKTIME,WO,MACHINE,ORDERNUM FROM SPC.C2RESULTDATA 
            WHERE MACHINE='C2-23052'
            ORDER BY CHECKTIME DESC

            SELECT TOP 1 CHECKTIME,WO,MACHINE,ORDERNUM FROM SPC.C2RESULTDATA 
            WHERE MACHINE='C2-23059'
            ORDER BY CHECKTIME DESC";
            DataSet ds = dsSql.ExecuteDataSet(sql, QcAnalysis.ConnectionState.KeepOpen);
            return ds;
        }

    }
}
