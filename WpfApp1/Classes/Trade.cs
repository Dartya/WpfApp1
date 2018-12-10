using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.Classes
{
    public class Trade
    {
        //  INSERT INTO `tradesassistant`.`trades` (`instrument_name`, `instrument_class`, `instrument_ticker`, `trade_type`, `opening_price`, `trade_volume`, `trade_sum`, `trade_closed`, `closing_price`, `comissions`, `profit`) VALUES ('Microsoft', 'stock', 'MSFT', 'long', '106', '10', '1060', '0', '110', '2', '38');
        /*public enum InstrumentType { currency = 1, stock, futures }
        public enum TradeType { Long = 1 , Short }*/

        //свойства БД
        private string Schema;
        private string Table;

        //свойства трейда
        public int TradeId { get; set; }
        public string InstrumentName { get; set; }
        public int InstrumentType { get; set; }
        public string Ticker { get; set; }
        public int TradeType { get; set; }
        public decimal OpeningPrice { get; set; }
        public int TradeSize { get; set; }
        public decimal TradeSum { get; set; }
        public bool TradeClosed { get; set; }
        public decimal ClosingPrice { get; set; }
        public decimal Comission { get; set; }
        public decimal Taxes { get; set; }
        public decimal Profit { get; set; }

        //округление параметров
        private void roundParams() {
            OpeningPrice = decimal.Round(OpeningPrice, 2);
            TradeSum = decimal.Round(TradeSum, 2);
            ClosingPrice = decimal.Round(ClosingPrice, 2);
            Comission = decimal.Round(Comission, 2);
            Taxes = decimal.Round(Taxes, 2);
            Profit = decimal.Round(Profit, 2);
        }

        //строка DataGrid
        DataRowView row;

        //конструктор без параметра
        public Trade() {
            Schema = "tradesassistant";
            Table = "trade";
        }

        //конструктор с параметром выбранной строки
        public Trade(DataRowView row) {
            Schema = "tradesassistant";
            Table = "trade";

            this.row = row;
            TradeId = int.Parse(row.Row.ItemArray[0].ToString());
            InstrumentName = row.Row.ItemArray[1].ToString();
            InstrumentType = int.Parse(row.Row.ItemArray[2].ToString());
            Ticker = row.Row.ItemArray[3].ToString();
            TradeType = int.Parse(row.Row.ItemArray[4].ToString());
            OpeningPrice = decimal.Parse(row.Row.ItemArray[5].ToString());
            TradeSize = int.Parse(row.Row.ItemArray[6].ToString());
            TradeSum = decimal.Parse(row.Row.ItemArray[7].ToString());

            TradeClosed = (bool)row.Row.ItemArray[8];
            if (TradeClosed == true) {
                TradeClosed = true;
                ClosingPrice = decimal.Parse(row.Row.ItemArray[9].ToString());
                Comission = decimal.Parse(row.Row.ItemArray[10].ToString());
                Taxes = decimal.Parse(row.Row.ItemArray[11].ToString());
                Profit = decimal.Parse(row.Row.ItemArray[12].ToString());
            }
        }

        //установка значений имени БД и таблицы
        public void setDataBaseParams(string schema, string table) {
            Schema = schema;
            Table = table;
        }

        //метод формирования строки удаления записи
        public string AddQuery() {
            roundParams();
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
                "`taxes`, " +
                "`profit`) " +
                "VALUES ('"+
                InstrumentName+ "', '"
                +InstrumentType.ToString() +"', '"
                +Ticker+ "', '"
                +TradeType.ToString() + "', '"
                + OpeningPrice.ToString().Replace(",", ".") + "', '"
                + TradeSize.ToString() + "', '"
                + TradeSum.ToString().Replace(",", ".") + "', '"
                + iTradeClosed.ToString() + "', '"
                + ClosingPrice.ToString().Replace(",", ".") + "', '"
                + Comission.ToString().Replace(",", ".") + "', '" 
                + Taxes.ToString().Replace(",", ".") + "', '" 
                + Profit.ToString().Replace(",", ".") + "');";

            return query;
        }

        public string EditQuery() {
            roundParams();
            int iTradeClosed = 0;
            if (TradeClosed == false) iTradeClosed = 0; else iTradeClosed = 1;

            //UPDATE `tradesassistant`.`trades` SET `trade_type`='0', `trade_sum`='2120.00' WHERE `id`='6';
            string query = "UPDATE `" + Schema + "`.`" + Table + "` SET " +
                "`instrument_name`='" + InstrumentName + "', " +
                "`instrument_class`='" + InstrumentType.ToString() + "', " +
                "`instrument_ticker`='" + Ticker + "', " +
                "`trade_type`='" + TradeType.ToString() + "', " +
                "`opening_price`='" + OpeningPrice.ToString().Replace(",", ".") + "', " +
                "`trade_volume`='" + TradeSize.ToString() + "', " +
                "`trade_sum`='" + TradeSum.ToString().Replace(",", ".") + "', " +
                "`trade_closed`='" + iTradeClosed.ToString() + "', " +
                "`closing_price`='" + ClosingPrice.ToString().Replace(",", ".") + "', " +
                "`comissions`='" + Comission.ToString().Replace(",", ".") + "', " +
                "`taxes`='" + Taxes.ToString().Replace(",", ".") + "', " +
                "`profit`='" + Profit.ToString().Replace(",", ".") + "', " +
                "WHERE `id`='" + TradeId + "`";

            return query;
        }

        public string DeleteQuery() {
            string query = "";

            return query;
        }
    }
}