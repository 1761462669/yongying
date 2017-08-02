using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
namespace TimingService.Ultils
{
   public class MathUtils
    {

       public static void GetAvg(IList<CData> list, CDataResult res)
       {
           res.WGAvg = list.Average(c => c.Wg);
           res.CIRCAvg = list.Average(c => c.CIRC);
       }
    }
}
