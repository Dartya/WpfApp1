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
    /// Логика взаимодействия для DBConnectionSettings.xaml
    /// </summary>
    public partial class DBConnectionSettings : Window
    {
        public DBConnectionSettings()
        {
            InitializeComponent();
        }

        private void enter_button(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void cancel_button(object sender, RoutedEventArgs e)
        {

        }
    }
}
