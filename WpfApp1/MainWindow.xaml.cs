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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;
using WpfApp1.Classes;
using System.Text.RegularExpressions;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBconnection dBconnection; //= new DBconnection("localhost", "tradesassistant", "root", "123456");

        private void DecimalValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+([,][0-9]{1,3})");
            e.Handled = regex.IsMatch(e.Text);
        }
        public MainWindow()
        {
            InitializeComponent();
            dBconnection = new DBconnection("localhost", "tradesassistant", "root", "123456");
            refreshData();
        }
        //окно настроек соединения с БД  
        private void DBSettings_Click(object sender, RoutedEventArgs e)
        {
            DBConnectionSettings dBConnectionSettings = new DBConnectionSettings();
            dBConnectionSettings.Owner = this;
            //dBConnectionSettings.ShowDialog();
            if (dBConnectionSettings.ShowDialog() == true) {
                dBconnection.setParams(dBConnectionSettings.dBCon);
                MessageBox.Show(dBconnection.makeConnectionString());
            }
        }
        //окно настроек риск-профиля
        private void RiskSettings_Click(object sender, RoutedEventArgs e)
        {
            RiskProfile riskProfile = new RiskProfile();
            riskProfile.Owner = this;
            riskProfile.ShowDialog();   //модальное окно
        }

        private void PositionCalculator_Click(object sender, RoutedEventArgs e)
        {
            CalculatorWindow win = new CalculatorWindow(decimal.Parse(DepositTexxtBox.Text));
            win.Owner = this;
            win.Show();
        }
        //окно справочной информации о функциях программы
        private void Manual_Click(object sender, RoutedEventArgs e)
        {

        }
        //окно "О программе"
        private void About_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefreshMenuItem_Click(object sender, RoutedEventArgs e)
        {
            refreshData();
        }
        //окно добавления записи в БД
        private void AddMenuItem_Click(object sender, RoutedEventArgs e) {
            if (dtGrid.SelectedItem == null)
            {
                RowAddEditWindow win = new RowAddEditWindow("Добавление записи");
                win.Owner = this;
                if (win.ShowDialog() == true)
                {
                    string query;
                    win.trade.setDataBaseParams(dBconnection.DB, dBconnection.Table);
                    query = win.trade.AddQuery();
                    try
                    {
                        MySqlConnection connection = new MySqlConnection(dBconnection.makeConnectionString());

                        MySqlCommand cmdAddDB = new MySqlCommand(query, connection);
                        //создание БД
                        connection.Open();
                        cmdAddDB.ExecuteNonQuery();
                        connection.Close();

                        MessageBox.Show("Запись добавлена");
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("НЕУДАЧА!\n" + exc.ToString());
                    }
                    refreshData();
                }
            }
            else if (dtGrid.SelectedItem != null) {
                DataRowView row = dtGrid.SelectedItem as DataRowView;

                Trade trade = new Trade(row);

                string query;
                trade.setDataBaseParams(dBconnection.DB, dBconnection.Table);
                query = trade.AddQuery();
                try
                {
                    MySqlConnection connection = new MySqlConnection(dBconnection.makeConnectionString());

                    MySqlCommand cmdAddDB = new MySqlCommand(query, connection);
                    //создание БД
                    connection.Open();
                    cmdAddDB.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Запись добавлена");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("НЕУДАЧА!\n" + exc.ToString());
                }
                refreshData();
            }
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = dtGrid.SelectedItem as DataRowView;
            if (row == null) {
                MessageBox.Show("Ошибка!\nНе выбрана запись! Выберите запись в таблице и повторите попытку.", "Ошибка");
                return;
            }
            RowAddEditWindow win = new RowAddEditWindow("Редактирование записи", row);
            win.Owner = this;

            if (win.ShowDialog() == true)
            {
                string query;
                win.trade.setDataBaseParams(dBconnection.DB, dBconnection.Table);
                query = win.trade.EditQuery();
                try
                {
                    MySqlConnection connection = new MySqlConnection(dBconnection.makeConnectionString());

                    MySqlCommand cmdAddDB = new MySqlCommand(query, connection);
                    //создание БД
                    connection.Open();
                    cmdAddDB.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Запись отредактирована", "ОК");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("НЕУДАЧА!\n" + exc.ToString());
                }
                refreshData();
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e) { //метод удаляет выбранную запись по ее id
            //код, получающий выбранную строку
            DataRowView row = dtGrid.SelectedItem as DataRowView;
            if (row == null)
            {
                MessageBox.Show("Ошибка!\nНе выбрана запись! Выберите запись в таблице и повторите попытку.", "Ошибка");
                return;
            }
            try
            {
                MySqlConnection connection = new MySqlConnection(dBconnection.makeConnectionString());

                Trade trade = new Trade(row);
                MySqlCommand cmd = new MySqlCommand(trade.DeleteQuery(), connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            refreshData();
        }

        //создание новой БД
        private void NewDB_Click(object sender, RoutedEventArgs e)
        {
            NewDBWindow win = new NewDBWindow();
            win.Owner = this;
            if (win.ShowDialog() == true) {
                try{
                    dBconnection.DB = win.Schema;   //костыль, работает, не трогай!
                    dBconnection.Table = win.Table; //костыль, работает, не трогай!
                    MySqlConnection connection = new MySqlConnection(dBconnection.makeConnectionString());

                    //создание бд
                    MySqlCommand cmdCreateDB = new MySqlCommand(win.CreateDB(), connection);
                    //таблица типов финансовых инструментов
                    MySqlCommand cmdCreateInstrumentTypeTable = new MySqlCommand(win.CreateInstrumentTypeTable(), connection);
                    MySqlCommand cmdCreateITRow1 = new MySqlCommand(win.CreateRowInstrumentType1(), connection);
                    MySqlCommand cmdCreateITRow2 = new MySqlCommand(win.CreateRowInstrumentType2(), connection);
                    MySqlCommand cmdCreateITRow3 = new MySqlCommand(win.CreateRowInstrumentType3(), connection);

                    //таблица типов сделок
                    MySqlCommand cmdCreateTradeTypeTable = new MySqlCommand(win.CreateTradeTypeTable(), connection);
                    MySqlCommand cmdCreateTTrow1 = new MySqlCommand(win.CreateTradeTypeRow1(), connection);
                    MySqlCommand cmdCreateTTrow2 = new MySqlCommand(win.CreateTradeTypeRow2(), connection);

                    //таблица записей
                    MySqlCommand cmdCreateTable = new MySqlCommand(win.CreateTable(), connection);
                    //создание БД
                    connection.Open();
                    cmdCreateDB.ExecuteNonQuery();
                    connection.Close();

                    //создание таблицы типов финансовых инструментов
                    connection.Open();
                    cmdCreateInstrumentTypeTable.ExecuteNonQuery();
                    connection.Close();
                    //добавление записей в таблицу выше
                    connection.Open();
                    cmdCreateITRow1.ExecuteNonQuery();
                    connection.Close();
                    connection.Open();
                    cmdCreateITRow2.ExecuteNonQuery();
                    connection.Close();
                    connection.Open();
                    cmdCreateITRow3.ExecuteNonQuery();
                    connection.Close();

                    //создание таблицы типов сделок
                    connection.Open();
                    cmdCreateTradeTypeTable.ExecuteNonQuery();
                    connection.Close();
                    //заполнение таблицы выше
                    connection.Open();
                    cmdCreateTTrow1.ExecuteNonQuery();
                    connection.Close();
                    connection.Open();
                    cmdCreateTTrow2.ExecuteNonQuery();
                    connection.Close();

                    //создание основной таблицы 
                    connection.Open();
                    cmdCreateTable.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("База данных "+ win.Schema +" и таблица "+ win.Table + " успешно созданы.", "Успех[");
                    dBconnection.setParams(win.Schema, win.Table); //обновление подключения
                }
            catch (Exception exc)
                {
                    MessageBox.Show("НЕУДАЧА!\n" + exc.ToString(), "Ошибка!");
                }
                refreshData();
            }
        }

        //метод обновления данных
        private void refreshData() {
            //string connectionString = "SERVER=localhost;DATABASE=mobiledb;UID=root;PASSWORD=123456;";
            try
            {
                MySqlConnection connection = new MySqlConnection(dBconnection.makeConnectionString());
                string command = "SELECT `"+dBconnection.DB+"`.`"+ dBconnection.Table+ "`.`id` AS `id`, " +
                    "`tradesassistant`.`trades`.`instrument_name` AS `Имя инструмента`, " +
                    "`tradesassistant`.`instrument_type`.`instrumenttype` AS `Класс инструмента`, " +
                    "`tradesassistant`.`trades`.`instrument_ticker` AS `Тикер инструмента`," +
                    " `tradesassistant`.`trade_types`.`tradetype` AS `Тип сделки`, " +
                    "`tradesassistant`.`trades`.`opening_price` AS `Цена открытия`, " +
                    "`tradesassistant`.`trades`.`trade_volume` AS `Объем позиции`, " +
                    "`tradesassistant`.`trades`.`trade_sum` AS `Сумма позиции`,	" +
                    "`tradesassistant`.`trades`.`trade_closed` AS `Сделка закрыта`,	" +
                    "`tradesassistant`.`trades`.`closing_price` AS `Цена закрытия`, " +
                    "`tradesassistant`.`trades`.`taxes` AS `Налог`, " +
                    "`tradesassistant`.`trades`.`comissions` AS `Комиссия`, " +
                    "`tradesassistant`.`trades`.`profit` AS `Финансовый результат` " +
                    "FROM `tradesassistant`.`trades` " +
                    "JOIN `tradesassistant`.`instrument_type` ON `tradesassistant`.`trades`.`instrument_class` = `tradesassistant`.`instrument_type`.`id` " +
                    "JOIN `tradesassistant`.`trade_types` ON `tradesassistant`.`trades`.`trade_type` = `tradesassistant`.`trade_types`.`id` " +
                    "ORDER BY `tradesassistant`.`trades`.`id`;";
                MySqlCommand cmd = new MySqlCommand(command, connection);
                //MessageBox.Show(cmd.CommandText);
                connection.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                connection.Close();

                dtGrid.DataContext = dt;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
    }
}