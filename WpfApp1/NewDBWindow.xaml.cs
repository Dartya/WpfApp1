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
            string query = "CREATE SCHEMA " + Schema + " CHARACTER SET utf8 COLLATE utf8_bin;";
            return query;
        }

        public string CreateTable() {
            Schema = DBName.Text;
            Table = TableName.Text;
            string query = "CREATE TABLE "+Schema+"."+Table+" (id INT NOT NULL AUTO_INCREMENT, " +
                "instrument_name VARCHAR(45) NOT NULL, " +
                "instrument_class INT NOT NULL, " +
                "instrument_ticker VARCHAR(45) NOT NULL, " +
                "trade_type INT NOT NULL, " +
                "opening_price DECIMAL(10,2) NOT NULL, " +
                "trade_volume INT NOT NULL, " +
                "trade_sum DECIMAL(10,2) NOT NULL, " +
                "trade_closed TINYINT(1), " +
                "closing_price DECIMAL(10,2) NULL, " +
                "taxes DECIMAL(10,2) NULL," +
                "comissions DECIMAL(10,2) NULL, " +
                "profit DECIMAL(10,2) NULL, " +
                "PRIMARY KEY(id)); ";
            return query;
        }

        public void enter_button(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
