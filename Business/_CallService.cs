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
        public void GetService(String mark)
        {
            if (!mark.Equals(null) && !mark.Equals(""))
            {
                switch (mark)
                {
                    case "spc":
                        CallSPC();
                        break;
                    case "PackProduceAmount":
                        CallMes_LK();
                        break;
                    case "预留":

                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// MES_LK
        /// </summary>
        private static void CallMes_LK()
        {
            MES_LK.MES_LK_Integration_Services mes = new MES_LK.MES_LK_Integration_Services();
            String date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            mes.PackProduceAmount(date);
        }
        /// <summary>
        /// SPC
        /// </summary>
        private static void CallSPC()
        {
            SqlDAL sqlDAL = new SqlDAL();
            DataSet ds = sqlDAL.GetStartWO();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                SPC.SPCDataCollectionTest spc = new SPC.SPCDataCollectionTest();
                String wo = dr["WO"].ToString();
                spc.SPCSTData(wo, "2");            
            }

        }
    }
}
