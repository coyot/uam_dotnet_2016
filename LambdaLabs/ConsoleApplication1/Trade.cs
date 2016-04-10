using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Trade
    {
        public string Id { get; set; }
        public int SourceSystemId { get; set; }
        public decimal Mtm { get; set; }
        public string MtmCcy { get; set; }

        public Trade()
        {

        }


        public Trade(string id, int ss, decimal mtm, string ccy)
        {
            Id = id;
            SourceSystemId = ss;
            Mtm = mtm;
            MtmCcy = ccy;
        }

        public override string ToString()
        {
            return string.Format("id: {0}, ss: {1}, {2:0.00} {3}", Id, SourceSystemId, Mtm, MtmCcy);
        }
    }
}
