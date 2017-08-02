using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    /// <summary>
    /// 获取定时服务调用对象
    /// </summary>
    public class _CallService
    {
        public static SqlDAL sqlDAL = new SqlDAL();
        String message = "";

        public String GetService(String mark)
        {
            String message = "";
            if (!mark.Equals(null) && !mark.Equals(""))
            {
                switch (mark)
                {
                    case "spc":
                        message = CallSPC();
                        break;
                    case "PackProduceAmount":
                        message = CallMes_LK();
                        break;
                    case "预留":

                        break;
                    default:
                        break;
                }
            }
            return message;
        }
        /// <summary>
        /// MES_LK
        /// </summary>
        private static String CallMes_LK()
        {
            MES_LK.MES_LK_Integration_Services mes = new MES_LK.MES_LK_Integration_Services();
            String date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            String message = mes.PackProduceAmount(date);
            return message;
        }
        /// <summary>
        /// SPC
        /// </summary>
        private static String CallSPC()
        {
            String message = "";
            //sqlDAL.GetStartWOInfo();
            DataSet ds = sqlDAL.GetStartWO();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                SPC.SPCDataCollectionTest spc = new SPC.SPCDataCollectionTest();
                String wo = dr["WO"].ToString();
                message = Convert.ToString(spc.SPCSTData(wo, "2"));
            }
            return message;
        }

        public string GetC2RowNum()
        {

            string msg = sqlDAL.GetC2RowNum();


            return msg;
        }


        public string C2DataStatistics()
        {
            message = sqlDAL.C2DataStatistics();
            return message;
        }
    }
}
