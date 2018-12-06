﻿using System;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBconnection dBconnection = new DBconnection("localhost", "blabla", "root", "123456");

        public MainWindow()
        {
            InitializeComponent();
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
            //string connectionString = "SERVER=localhost;DATABASE=mobiledb;UID=root;PASSWORD=123456;";
            try
            {
                MySqlConnection connection = new MySqlConnection(dBconnection.makeConnectionString());

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM products", connection);
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
        //окно добавления записи в БД
        private void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RowAddEditWindow win = new RowAddEditWindow("Добавление записи");
            win.Owner = this;
            if (win.ShowDialog() == true)
            {
                MessageBox.Show("Запись добавлена");
            }
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RowAddEditWindow win = new RowAddEditWindow("Редактирование записи");
            win.Owner = this;
            if (win.ShowDialog() == true)
            {
                MessageBox.Show("Запись отредактирована");
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //код, получающий выбранную строку
            DataRowView row = dtGrid.SelectedItem as DataRowView;
            //MessageBox.Show(row.Row.ItemArray[1].ToString());

            string blabla = "";
            for (int i = 0; i < row.Row.ItemArray.Length; i++) {
                blabla = (""+blabla + (row.Row.ItemArray[i].ToString())+"\n");
            }
            MessageBox.Show(blabla);
        }
        
        private void JSON_Click(object sender, RoutedEventArgs e)
        {

        }

        private void XML_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveProfile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadProfile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void NewDB_Click(object sender, RoutedEventArgs e)
        {
            try{
                MySqlConnection connection = new MySqlConnection(dBconnection.makeConnectionString());

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM products", connection);
                connection.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                connection.Close();

                dtGrid.DataContext = dt;
            }
            catch (Exception exc) {
                MessageBox.Show("НЕУДАЧА!\n"+exc.ToString());
            }
        }
    }
}