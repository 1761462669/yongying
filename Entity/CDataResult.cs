using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CDataResult
    {
        public DateTime checkTime { get; set; }
        public string machine { get; set; }
        public string wo { get; set; }
        public int ordernum { get; set; }
        public decimal PD_AVG { get; set; }
        public decimal PD_STDV { get; set; }
        public decimal PD_CPK { get; set; }
        public decimal CIRC_AVG { get; set; }
        public decimal CIRC_STDV { get; set; }
        public decimal CIRC_CPK { get; set; }
        public decimal WG_AVG { get; set; }
        public decimal WG_STDV { get; set; }
        public decimal WG_CPK { get; set; }
        public decimal LEN_AVG { get; set; }
        public decimal LEN_STDV { get; set; }
        public decimal LEN_CPK { get; set; }
        public decimal TV_AVG { get; set; }
        public decimal TV_STDV { get; set; }
        public decimal TV_CPK { get; set; }

    }
}
