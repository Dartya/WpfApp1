using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApp1.Classes;

namespace WpfApp1Tests
{
    [TestClass]
    public class TradeTests  //все юнит тесты собираются в класс, помеченный атрибутом [TestClass]
    {

        [TestMethod]
        public void setDataBaseParams1() //юнит тест - это этот самый метод, помеченный атрибутом [TestMethod]
        {
            //arrange - настройка
            //вводим здесь параметры для тестируемого метода
            //и предполагаемый результат работы
            string schema = "tradeAssist";
            string table = "trades";

            //act - выполнить действия
            //здесь создаем экземпляр тестируемого класса и вызываем метод с параметрами
            Trade trade = new Trade();  //создаем объект тестируемого класса Trade
            
            //assert
            //вызываем Assert.AreEqual(предполагаемый результат, результат работы тестируемого метода)
            //можно вызывать несколько методов сравнения
            trade.setDataBaseParams(schema, table); //вызываем тестируемый метод
            Assert.AreEqual(trade.Schema, schema);
            Assert.AreEqual(trade.Table, table);
        }

        [TestMethod]
        public void AddQuery1() //юнит тест - это этот самый метод, помеченный атрибутом [TestMethod]
        {
            //arrange - настройка
            //вводим здесь параметры для тестируемого метода
            //и предполагаемый результат работы
            string testQuery;   //строка, которая будет принимать результат тестируемого метода

            string schema = "tradeAssist";
            string table = "trades";
            int TradeId = 1;
            string InstrumentName = "Microsoft";
            string InstrumentType = "Акция";
            string Ticker = "MSFT";
            string TradeType = "Short";
            decimal OpeningPrice = 100;
            int TradeSize = 10;
            decimal TradeSum = 1000;
            
            //дополнительные параметры сделки
            bool TradeClosed = false;
            decimal ClosingPrice = 0;
            decimal Comission = 0;
            decimal Taxes = 0;
            decimal Profit = 0;

            //дополнительный метод
            //округление параметров
            //в части arrange можно определять свои методы и приводить их в исполнение
            void roundParams()
            {
                OpeningPrice = decimal.Round(OpeningPrice, 2);
                TradeSum = decimal.Round(TradeSum, 2);
                ClosingPrice = decimal.Round(ClosingPrice, 2);
                Comission = decimal.Round(Comission, 2);
                Taxes = decimal.Round(Taxes, 2);
                Profit = decimal.Round(Profit, 2);
            }

            roundParams();
            //вызов определенного выше метода. т.к. в данном тесте в эталонной строке сравнения данные конкатенируются напрямую, 
            //а не через вызов поля через .ToString(), то этот метод выполняется, но практического смысла не имеет.
            //смысл его вызова заключается только в одном - удостовериться, что он вызывается, и выполняется без ошибок

            //эталонная строка сравнения
            string query = "INSERT INTO `tradeAssist`.`trades` " +
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
                "VALUES ('" +
                "Microsoft" + "', '"
                + "1" + "', '"
                + "MSFT" + "', '"
                + "1"+ "', '"
                + "100" + "', '"
                + "10" + "', '"
                + "1000" + "', '"
                + "0" + "', '"
                + "0" + "', '"
                + "0" + "', '"
                + "0" + "', '"
                + "0" + "');";
            
            //act - выполнить действия
            //здесь создаем экземпляр тестируемого класса и вызываем метод с параметрами
            Trade trade = new Trade();  //создаем объект типа Trade
            trade.setDataBaseParams(schema, table); //задаем параметры БД

            //задаем значения свойств созданного объекта
            trade.TradeId = TradeId;
            trade.InstrumentName = InstrumentName;
            trade.InstrumentType = InstrumentType;
            trade.Ticker = Ticker;
            trade.TradeType = TradeType;
            trade.OpeningPrice = OpeningPrice;
            trade.TradeSize = TradeSize;
            trade.TradeSum = TradeSum;

            testQuery = trade.AddQuery(); //выполняем тестируемый метод

            //assert
            //вызываем Assert.AreEqual(предполагаемый результат, результат работы тестируемого метода)
            Assert.AreEqual(testQuery, query);
        }

        [TestMethod]
        public void EditQuery1() //юнит тест - это этот самый метод, помеченный атрибутом [TestMethod]
        {
            //arrange - настройка
            //вводим здесь параметры для тестируемого метода
            //и предполагаемый результат работы
            string testQuery;   //строка, которая будет принимать результат тестируемого метода

            string schema = "tradeAssist";
            string table = "trades";
            int TradeId = 1;
            string InstrumentName = "Microsoft";
            string InstrumentType = "Акция";
            string Ticker = "MSFT";
            string TradeType = "Short";
            decimal OpeningPrice = 100;
            int TradeSize = 10;
            decimal TradeSum = 1000;

            //дополнительные параметры сделки
            bool TradeClosed = true;
            decimal ClosingPrice = 110;
            decimal Comission = 2;
            decimal Taxes = 13;
            decimal Profit = 85;
            
            //эталонная строка сравнения
            string query = "UPDATE `tradeAssist`.`trades` SET " +
                "`instrument_name`='Microsoft', " +
                "`instrument_class`='1', " +
                "`instrument_ticker`='MSFT', " +
                "`trade_type`='1', " +
                "`opening_price`='100', " +
                "`trade_volume`='10', " +
                "`trade_sum`='1000', " +
                "`trade_closed`='1', " +
                "`closing_price`='110', " +
                "`comissions`='2', " +
                "`taxes`='13', " +
                "`profit`='85' " +
                "WHERE `id`='1';";

            //act - выполнить действия
            //здесь создаем экземпляр тестируемого класса и вызываем метод с параметрами
            Trade trade = new Trade();  //создаем объект типа Trade
            trade.setDataBaseParams(schema, table); //задаем параметры БД

            //задаем значения свойств созданного объекта
            trade.TradeId = TradeId;
            trade.InstrumentName = InstrumentName;
            trade.InstrumentType = InstrumentType;
            trade.Ticker = Ticker;
            trade.TradeType = TradeType;
            trade.OpeningPrice = OpeningPrice;
            trade.TradeSize = TradeSize;
            trade.TradeSum = TradeSum;
            trade.TradeClosed = TradeClosed;
            trade.ClosingPrice = ClosingPrice;
            trade.Comission = Comission;
            trade.Taxes = Taxes;
            trade.Profit = Profit;

            testQuery = trade.EditQuery(); //выполняем тестируемый метод

            //assert
            //вызываем Assert.AreEqual(предполагаемый результат, результат работы тестируемого метода)
            Assert.AreEqual(testQuery, query);
        }
    }
}