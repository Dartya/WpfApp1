using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Classes
{

    class Trade
    {
        public string InstrumentName { get; set; }
        public enum InstrumentType { currency, stock, futures }
        public string Tecker { get; set; }
        public enum TradeType { Long, Short }
        public float OpeningPrice { get; set; }
        public int TradeSize { get; set; }
        public float PositionSum { get; set; }
        public bool IsClosed { get; set; }
        public float Comission { get; set; }
        public float Profit { get; set; }

        //конструктор класса
        public Trade() {
            
        }

        //
    }
}