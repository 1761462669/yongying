using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CDataResult
    {
        public DateTime cDateTime { get; set; }
        public string EquNo { get; set; }

        public decimal WGAvg { get; set; }
        public decimal CIRCAvg { get; set; }

    }
}
