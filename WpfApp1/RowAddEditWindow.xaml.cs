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
    /// Логика взаимодействия для RowAddEditWindow.xaml
    /// </summary>
    public partial class RowAddEditWindow : Window
    {
        public string TitleString { get; set; }

        public RowAddEditWindow(string title)
        {
            Title = title;
            InitializeComponent();
        }

        public void enter_button(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}