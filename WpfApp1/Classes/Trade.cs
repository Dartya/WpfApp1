using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfApp1.Classes
{
    public class Trade
    {
        //  INSERT INTO `tradesassistant`.`trades` (`instrument_name`, `instrument_class`, `instrument_ticker`, `trade_type`, `opening_price`, `trade_volume`, `trade_sum`, `trade_closed`, `closing_price`, `comissions`, `profit`) VALUES ('Microsoft', 'stock', 'MSFT', 'long', '106', '10', '1060', '0', '110', '2', '38');
        /*public enum InstrumentType { currency = 1, stock, futures }
        public enum TradeType { Long = 1 , Short }*/

        //свойства БД
        public string Schema { get; set; }
        public string Table { get; set; }

        //свойства трейда
        public int TradeId { get; set; }
        public string InstrumentName { get; set; }
        public int InstrumentType { get; set; }
        public string Ticker { get; set; }
        public int TradeType { get; set; }
        public float OpeningPrice { get; set; }
        public int TradeSize { get; set; }
        public float TradeSum { get; set; }
        public bool TradeClosed { get; set; }
        public float Comission { get; set; }
        public float Taxes { get; set; }
        public float Profit { get; set; }

        //строка DataGrid
        DataRowView row;

        //конструктор без параметра
        public Trade(){
        }

        //конструктор с параметром выбранной строки
        public Trade(DataRowView row) {
            this.row = row;
            TradeId = Int32.Parse(row.Row.ItemArray[0].ToString());
            InstrumentName = row.Row.ItemArray[1].ToString();
            InstrumentType = Int32.Parse(row.Row.ItemArray[2].ToString());
            Ticker = row.Row.ItemArray[3].ToString();
            TradeType = Int32.Parse(row.Row.ItemArray[4].ToString());
            OpeningPrice = float.Parse(row.Row.ItemArray[5].ToString());
            TradeSize = Int32.Parse(row.Row.ItemArray[6].ToString());
            TradeSum = float.Parse(row.Row.ItemArray[7].ToString());

            int iTradeClosed = Int32.Parse(row.Row.ItemArray[8].ToString());
            if (iTradeClosed == 0)
                TradeClosed = false;
            else TradeClosed = true;

            Comission = float.Parse(row.Row.ItemArray[9].ToString());
            Taxes = float.Parse(row.Row.ItemArray[10].ToString());
            Profit = float.Parse(row.Row.ItemArray[11].ToString());
        }

        public string AddQuery() {
            int iTradeClosed = 0;
            if (TradeClosed == false) iTradeClosed = 0; else iTradeClosed = 1;
            string query = "INSERT INTO `"+Schema+"`.`"+Table+"` " +
                "(`instrument_name`, " +
                "`instrument_class`, " +
                "`instrument_ticker`, " +
                "`trade_type`, " +
                "`opening_price`, " +
                "`trade_volume`, " +
                "`trade_sum`, " +
                "`trade_closed`, " +
                "`closing_price`, " +
                "`comissions`, " +
                "`profit`) " +
                "VALUES ('"+
                InstrumentName+ "', '"
                +InstrumentType.ToString() +"', '"
                +Ticker+ "', '"
                +TradeType.ToString() + "', '"
                + OpeningPrice.ToString() + "', '"
                + TradeSize.ToString() + "', '"
                + TradeSum.ToString() + "', '"
                + iTradeClosed.ToString() + "', '" 
                + Comission.ToString() + "', '" 
                + Taxes.ToString() + "', '" 
                + Profit.ToString() + "');";

            return query;
        }

        public string EditQuery() {
            string query = "";

            return query;
        }

        public string DeleteQuery() {
            string query = "";

            return query;
        }
    }
}