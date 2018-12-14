using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для NewDBWindow.xaml
    /// </summary>
    public partial class NewDBWindow : Window
    {
        public NewDBWindow()
        {
            InitializeComponent();
        }

        public string Schema { get; set; }
        public string Table { get; set; }

        public string CreateDB() {
            Schema = DBName.Text;
            string query = "CREATE SCHEMA `" + Schema + "` CHARACTER SET utf8 COLLATE utf8_bin;";
            return query;
        }
        //создание таблицы типов инструментов
        public string CreateInstrumentTypeTable() {
            Schema = DBName.Text;
            string query = "CREATE TABLE `"+Schema+ "`.`instrument_type` (`id` INT NOT NULL, " +
                "`instrumenttype` VARCHAR(45) NOT NULL, " +
                "PRIMARY KEY(`id`)); ";
            return query;
        }
        //запросы на внесение данных в таблицу типов инструментов
        public string CreateRowInstrumentType1() {
            Schema = DBName.Text;
            string query = "INSERT INTO `" + Schema + "`.`instrument_type` (`id`, `instrumenttype`) VALUES ('0', 'Валюта');";
            return query;
        }

        public string CreateRowInstrumentType2()
        {
            Schema = DBName.Text;
            string query = "INSERT INTO `" + Schema + "`.`instrument_type` (`id`, `instrumenttype`) VALUES ('1', 'Акция');";
            return query;
        }

        public string CreateRowInstrumentType3()
        {
            Schema = DBName.Text;
            string query = "INSERT INTO `" + Schema + "`.`instrument_type` (`id`, `instrumenttype`) VALUES ('2', 'Фьючерс');";
            return query;
        }

        //создание таблицы типов сделок
        public string CreateTradeTypeTable() {
            Schema = DBName.Text;
            string query = "CREATE TABLE `" + Schema + "`.`trade_types` (`id` INT NOT NULL, " +
                "`tradetype` VARCHAR(5) NOT NULL, " +
                "PRIMARY KEY(`id`));";
                return query;
        }

        //внесение данных типов сделок

        public string CreateTradeTypeRow1() {
            Schema = DBName.Text;
            string query = "INSERT INTO `" + Schema + "`.`trade_types` (`id`, `tradetype`) VALUES ('0','Long');";
            return query;
        }

        public string CreateTradeTypeRow2()
        {
            Schema = DBName.Text;
            string query = "INSERT INTO `" + Schema + "`.`trade_types` (`id`, `tradetype`) VALUES ('1','Short');";
            return query;
        }

        public string CreateTable() {
            Schema = DBName.Text;
            Table = TableName.Text;
            string query = "CREATE TABLE `"+Schema+"`.`"+Table+"` (`id` INT NOT NULL AUTO_INCREMENT," +
                "`instrument_name` VARCHAR(45) NOT NULL, " +
                "`instrument_class` INT NOT NULL, " +
                "`instrument_ticker` VARCHAR(45) NOT NULL, " +
                "`trade_type` INT NOT NULL, " +
                "`opening_price` DECIMAL(10, 2) NOT NULL, " +
                "`trade_volume` INT NOT NULL, " +
                "`trade_sum` DECIMAL(10, 2) NOT NULL, " +
                "`trade_closed` TINYINT(1) NULL, " +
                "`closing_price` DECIMAL(10, 2) NULL, " +
                "`taxes` DECIMAL(10, 2) NULL, " +
                "`comissions` DECIMAL(10, 2) NULL, " +
                "`profit` DECIMAL(10, 2) NULL, " +
                "PRIMARY KEY(`id`), " +
                "INDEX `instr_class_id_idx` (`instrument_class` ASC), " +
                "INDEX `trade_type_id_idx` (`trade_type` ASC), " +
                "CONSTRAINT `instr_class_id` " +
                "FOREIGN KEY(`instrument_class`) " +
                "REFERENCES `tradesassistant`.`instrument_type` (`id`) " +
                "ON DELETE NO ACTION " +
                "ON UPDATE NO ACTION, " +
                "CONSTRAINT `trade_type_id` " +
                "FOREIGN KEY(`trade_type`) " +
                "REFERENCES `tradesassistant`.`trade_types` (`id`) " +
                "ON DELETE NO ACTION " +
                "ON UPDATE NO ACTION);";
            return query;
        }

        public void enter_button(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}