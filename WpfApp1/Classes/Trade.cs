using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Classes
{
    public class Trade
    {
        //  INSERT INTO `tradesassistant`.`trades` (`instrument_name`, `instrument_class`, `instrument_ticker`, `trade_type`, `opening_price`, `trade_volume`, `trade_sum`, `trade_closed`, `closing_price`, `comissions`, `profit`) 
        //VALUES ('Microsoft', 'stock', 'MSFT', 'long', '106', '10', '1060', '0', '110', '2', '38');
        public enum InstrumentType { currency = 1, stock, futures }
        public enum TradeType { Long = 1 , Short }

        public string InstrumentName { get; set; }
        public InstrumentType instrumentType { get; set; }
        public string Ticker { get; set; }
        public TradeType tradeType { get; set; }
        public float OpeningPrice { get; set; }
        public int TradeSize { get; set; }
        public float TradeSum { get; set; }
        public bool TradeClosed { get; set; }
        public float Comission { get; set; }
        public float Taxes { get; set; }
        public float Profit { get; set; }

        //конструктор класса
        public Trade() {
            
        }
    }
}