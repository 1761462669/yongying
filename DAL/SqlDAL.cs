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
                    if (ds.Tables[0].Rows.Count > 19)
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
            String message = "";
            LogStr.Clear();

            DateTime startTime;
            DateTime endTime;
            String wo;
            String machine;
            int orderNo;
            String mat_Ctrl;
            double UCL;
            double LCL;
            DataSet dsC2Data = GetC2DataStatTime();
            CDataResult res = new CDataResult();

            if (dsC2Data.Tables[0].Rows.Count > 0 || dsC2Data.Tables[1].Rows.Count > 0 || dsC2Data.Tables[2].Rows.Count > 0)
            {
                for (int i = 0; i < dsC2Data.Tables.Count; i++)
                {
                    startTime = Convert.ToDateTime(dsC2Data.Tables[i].Rows[0]["CHECKTIME"]);
                    wo = dsC2Data.Tables[i].Rows[0]["WO"].ToString();
                    machine = dsC2Data.Tables[i].Rows[0]["MACHINE"].ToString();
                    orderNo = Convert.ToInt32(dsC2Data.Tables[i].Rows[0]["ORDERNUM"]);
                    DataSet dsEndTime = GetC2DataEndTime(startTime, machine);
                    if (dsEndTime.Tables[0].Rows.Count > 0)
                    {
                        endTime = Convert.ToDateTime(dsEndTime.Tables[0].Rows[0]["ROWTIME"]);
                        DataSet ds = GetC2DataFromMes(startTime, endTime, machine);

                        if (ds.Tables[0].Rows.Count > 1)
                        {
                            IList<CData> list = ConvetToObjList(ds.Tables[0]);
                            //计算平均值
                            res.WG_AVG = list.Average(c => c.Wg);
                            res.CIRC_AVG = list.Average(c => c.CIRC);
                            res.LEN_AVG = list.Average(c => c.Len);
                            res.PD_AVG = list.Average(c => c.Pd);
                            res.TV_AVG = list.Average(c => c.TV);
                            //计算标准偏差
                            IList<double> wgData = mu.InitDoubleData(list, "WG");
                            res.WG_STDV = Convert.ToDecimal(mu.ComputSD(wgData));
                            IList<double> lenData = mu.InitDoubleData(list, "LEN");
                            res.LEN_STDV = Convert.ToDecimal(mu.ComputSD(lenData));
                            IList<double> pdData = mu.InitDoubleData(list, "PD");
                            res.PD_STDV = Convert.ToDecimal(mu.ComputSD(pdData));
                            IList<double> circData = mu.InitDoubleData(list, "CIRC");
                            res.CIRC_STDV = Convert.ToDecimal(mu.ComputSD(circData));
                            IList<double> tvData = mu.InitDoubleData(list, "TV");
                            res.TV_STDV = Convert.ToDecimal(mu.ComputSD(tvData));
                            //其他结果值
                            mat_Ctrl = list[list.Count - 1].mat_Ctrl;
                            res.checkTime = list[list.Count - 1].testdate;
                            res.wo = list[list.Count - 1].Wo;
                            res.machine = machine;
                            if (wo.Equals(res.wo))
                            {
                                res.ordernum = orderNo + 1;
                            }
                            else
                            {
                                res.ordernum = 1;
                            }
                            //取标准 计算CPK
                            DataSet dsStandard = GetStandard(machine, mat_Ctrl);
                            if (dsStandard.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsStandard.Tables[0].Rows)
                                {
                                    UCL = Convert.ToDouble(dr["UCL"]);
                                    LCL = Convert.ToDouble(dr["LCL"]);
                                    switch (dr["TEST"].ToString())
                                    {
                                        case "WG":
                                            res.WG_CPK = Convert.ToDecimal(mu.ComputCPK(wgData, UCL, Convert.ToDouble(res.WG_AVG), LCL));
                                            break;
                                        case "LEN_":
                                            res.LEN_CPK = Convert.ToDecimal(mu.ComputCPK(lenData, UCL, Convert.ToDouble(res.LEN_AVG), LCL));
                                            break;
                                        case "CIRC":
                                            res.CIRC_CPK = Convert.ToDecimal(mu.ComputCPK(circData, UCL, Convert.ToDouble(res.CIRC_AVG), LCL));
                                            break;
                                        case "PD":
                                            res.PD_CPK = Convert.ToDecimal(mu.ComputCPK(pdData, UCL, Convert.ToDouble(res.PD_AVG), LCL));
                                            break;
                                        case "TV":
                                            res.TV_CPK = Convert.ToDecimal(mu.ComputCPK(tvData, UCL, Convert.ToDouble(res.TV_AVG), LCL));
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            res.checkTime = endTime;
                            res.machine = machine;
                            res.wo = wo;
                        }
                        dsSql.BeginTransaction();
                        message = SaveC2StatisticsData(res);
                        dsSql.CommitTransaction();

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

        private DataSet GetStandard(string machine, string mat_Ctrl)
        {
            String sql = String.Format(@"SELECT TIC.TEST,SL.UCL,SL.CL,SL.LCL FROM SPC.TEST_ITEM_CFG TIC
            LEFT JOIN SPC.SPEC_LIM SL ON TIC.TESTID=SL.TESTID
            WHERE TIC.TESTID IN (SELECT TESTID from SPC.TEST_ITEM_CFG WHERE FILTER LIKE '%{0}%')
                       AND SL.PARTID IN (SELECT PARTID FROM SPC.PART WHERE GROUPID=2 AND CTRL='{1}') AND SL.STATUS=1", machine, mat_Ctrl);
            DataSet ds = dsSSQL.ExecuteDataSet(sql);
            return ds;
        }
        private String SaveC2StatisticsData(CDataResult res)
        {
            String message = "";
            String sql = String.Format(@"INSERT INTO [SPC].[C2RESULTDATA]
           ([CHECKTIME]
           ,[WO]
           ,[MACHINE]
           ,[ORDERNUM]
           ,[PD_AVG]
           ,[PD_STDV]
           ,[PD_CPK]
           ,[CIRC_AVG]
           ,[CIRC_STDV]
           ,[CIRC_CPK]
           ,[WG_AVG]
           ,[WG_STDV]
           ,[WG_CPK]
           ,[LEN_AVG]
           ,[LEN_STDV]
           ,[LEN_CPK]
           ,[TV_AVG]
           ,[TV_STDV]
           ,[TV_CPK])
           VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')"
                , res.checkTime, res.wo, res.machine, res.ordernum, res.PD_AVG, res.PD_STDV, res.PD_CPK, res.CIRC_AVG, res.CIRC_STDV
                , res.CIRC_CPK, res.WG_AVG, res.WG_STDV, res.WG_CPK, res.LEN_AVG, res.LEN_STDV, res.LEN_CPK, res.TV_AVG, res.TV_STDV, res.TV_CPK);
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


        private IList<CData> ConvetToObjList(DataTable dataTable)
        {
            IList<CData> list = new List<CData>();
            foreach (DataRow dr in dataTable.Rows)
            {
                CData cd = new CData();
                cd.Pd = Convert.ToDecimal(dr["PD"]);
                cd.Len = Convert.ToDecimal(dr["LEN_"]);
                cd.CIRC = Convert.ToDecimal(dr["CIRC"]);
                cd.Wg = Convert.ToDecimal(dr["WG"]);
                if (String.IsNullOrEmpty(dr["TV"].ToString()))
                {
                    cd.TV = 0;
                }
                else
                {
                    cd.TV = Convert.ToDecimal(dr["TV"]);
                }
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
            WHERE testdate> '{0}' AND testdate<= '{1}' AND EQU_CTRL='{2}'", startTime, endTime, machine);
            DataSet ds = dsSql.ExecuteDataSet(sql, QcAnalysis.ConnectionState.KeepOpen);
            return ds;
        }

        public DataSet GetC2DataEndTime(DateTime timeFlag, string machine)
        {
            String sql = String.Format(@"SELECT TOP 1 DATEADD(HH,8,ROWTIME) ROWTIME FROM SPC.ROWNUMFLAG
            WHERE ROWTIME>DATEADD(HH,-8,'{0}') AND MACHINE='{1}' AND ROWNUM=20
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
