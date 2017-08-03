using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using QcAnalysis;
using System.Data;
namespace DAL
{
    public class MathUtils
    {
        public  void GetStatistics(IList<CData> list, CDataResult res)
        {
            //计算平均值
            res.WG_AVG = list.Average(c => c.Wg);
            res.CIRC_AVG = list.Average(c => c.CIRC);
            res.LEN_AVG = list.Average(c => c.Len);
            res.PD_AVG = list.Average(c => c.Pd);
            res.TV_AVG = list.Average(c => c.TV);
            //计算标准偏差
            IList<double> wgData = InitDoubleData(list, "WG");
            res.WG_STDV = Convert.ToDecimal(ComputSD(wgData));
            IList<double> lenData = InitDoubleData(list, "LEN");
            res.LEN_STDV = Convert.ToDecimal(ComputSD(lenData));
            IList<double> pdData = InitDoubleData(list, "PD");
            res.PD_STDV = Convert.ToDecimal(ComputSD(wgData));
            IList<double> circData = InitDoubleData(list, "CIRC");
            res.CIRC_STDV = Convert.ToDecimal(ComputSD(lenData));
            IList<double> tvData = InitDoubleData(list, "TV");
            res.TV_STDV = Convert.ToDecimal(ComputSD(lenData));
        }

        private static IList<double> InitDoubleData(IList<CData> list, string type)
        {
            IList<double> dList = new List<double>();
            switch (type)
            {
                case "WG":
                    foreach (var item in list)
                    {
                        dList.Add(Convert.ToDouble(item.Wg));
                    }
                    break;
                case "LEN":
                    foreach (var item in list)
                    {
                        dList.Add(Convert.ToDouble(item.Len));
                    }
                    break;
                case "PD":
                    foreach (var item in list)
                    {
                        dList.Add(Convert.ToDouble(item.Pd));
                    }
                    break;
                case "CIRC":
                    foreach (var item in list)
                    {
                        dList.Add(Convert.ToDouble(item.CIRC));
                    }
                    break;
                case "TV":
                    foreach (var item in list)
                    {
                        dList.Add(Convert.ToDouble(item.TV));
                    }
                    break;
            }
            return dList;
        }
        /// 统计一组数据的标偏
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public  double ComputSD(IList<double> data)
        {
            double xSum = 0.0;
            double xAvg = 0.0;
            double sSum = 0.0;
            double tmpStDev = 0.0;
            int arrNum = data.Count;
            for (int i = 0; i < arrNum; i++)
            {
                xSum += data[i];
            }
            xAvg = xSum / arrNum;
            for (int j = 0; j < arrNum; j++)
            {
                sSum += ((data[j] - xAvg) * (data[j] - xAvg));
            }
            tmpStDev = Convert.ToSingle(Math.Sqrt((sSum / (arrNum - 1))).ToString());
            return tmpStDev;
        }
        public  double ComputCPK(IList<double> list, double max_std, double AvgValue, double min_std)
        {
            double SdValue = 0.0;
            IList<double> cList = new List<double>();
            IList<double> sdList = new List<double>();
            for (int i = 0; i < list.Count; i++)
            {
                cList.Add(Convert.ToDouble(list[i]));
                if (cList.Count == 5)
                {
                    //根据cllist计算标偏
                    sdList.Add(ComputSD(cList));
                    cList = new List<double>();
                }
            }
            SdValue = sdList.Average() / 0.94;
            return Math.Min(((Convert.ToDouble(max_std) - Convert.ToDouble(AvgValue)) / (3 * Convert.ToDouble(SdValue))), ((Convert.ToDouble(AvgValue) - Convert.ToDouble(min_std)) / (3 * Convert.ToDouble(SdValue))));
        }
        public  double ComputCPK(double max_std, double AvgValue, double SdValue, double min_std)
        {
            return Math.Min(((Convert.ToDouble(max_std) - Convert.ToDouble(AvgValue)) / (3 * Convert.ToDouble(SdValue))), ((Convert.ToDouble(AvgValue) - Convert.ToDouble(min_std)) / (3 * Convert.ToDouble(SdValue))));
        }

    }
}
