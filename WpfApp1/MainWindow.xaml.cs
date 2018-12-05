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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string string3;

        public MainWindow()
        {
            InitializeComponent();
            label1.Content = "blablabla";
            DBconnection dBconnection = new DBconnection("localhost", "mobiledb", "root", "123456");
            //string connectionString = "SERVER=localhost;DATABASE=mobiledb;UID=root;PASSWORD=123456;";

            MySqlConnection connection = new MySqlConnection(dBconnection.connectionString);

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM products", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();

            dtGrid.DataContext = dt;
        }

        private void newFile_action(object sender, RoutedEventArgs e) {
            button3.Content = "blabla1";
        }

        private void cancel_action(object sender, RoutedEventArgs e)
        {
            //richText1.Document.Blocks.Clear();
            button3.Content = "Button 1";
        }

        private void hren_action(object sender, RoutedEventArgs e)
        {
            button3.Content = "хрень";
        }

        private void bren_action(object sender, RoutedEventArgs e)
        {
            button3.Content = "брень";
        }

        private void openFile_action(object sender, RoutedEventArgs e)
        {
            button3.Content = "откупориваю";
        }
        
        private void DBSettings_Click(object sender, RoutedEventArgs e)
        {
            DBConnectionSettings dBConnectionSettings = new DBConnectionSettings();
            dBConnectionSettings.Owner = this;
            dBConnectionSettings.Show();
        }

        private void RiskSettings_Click(object sender, RoutedEventArgs e)
        {
            RiskProfile riskProfile = new RiskProfile();
            riskProfile.Owner = this;
            riskProfile.Show();
        }

        private void PositionCalculator_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Manual_Click(object sender, RoutedEventArgs e)
        {

        }

        private void About_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            string3 = "Число символов: ";
            label1.Content = string3;
            //label1.Content = string3;
        }
        
        /*private void richtextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = new TextRange(richText1.Document.ContentStart, richText1.Document.ContentEnd).Text;
            string3 = "Число символов: " + (text.Length - 2).ToString();
            label1.Content = string3;
        }*/
    }
}