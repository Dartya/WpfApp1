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
using WpfApp1.Classes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для RiskProfile.xaml
    /// </summary>
    /// 
    public partial class RiskProfile : Window
    {
        /*//пробую привязать строку
        public static readonly DependencyProperty dpMin_Risk_inOneTrade = DependencyProperty.Register(
            "t", 
            typeof(string), 
            typeof(MainWindow)
            );

        public string sMin_Risk_inOneTrade {
            get { return (string)GetValue(dpMin_Risk_inOneTrade); }
            set { SetValue(dpMin_Risk_inOneTrade, value); }
        }*/

        string RiskProfitRatio { get; set; }

        public RiskProfile()
        {
            InitializeComponent();
        }

        public DBconnection DBconnect { get; internal set; }

        private void enter_button(object sender, RoutedEventArgs e)
        {
            Classes.RiskProfile.min_RiskCapital_inOneTrade = float.Parse(min_Risk_inOneTrade.Text);
            Classes.RiskProfile.max_RiskCapital_inOneTrade = float.Parse(max_Risk_inOneTrade.Text);
            Classes.RiskProfile.max_Persent_ofDeposit = float.Parse(maxPercentOfDeposit.Content.ToString());
            Classes.RiskProfile.min_Profit_inOneTrade = float.Parse(minProfitInOneTrade.Text);
            Classes.RiskProfile.max_Profit_inOneTrade = float.Parse(maxProfitInOneTrade.Text);
            Classes.RiskProfile.RiskProfit_Ratio = RiskProfitRatio;

            this.DialogResult = true;
        }

        private void min_Risk_inOneTrade_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void max_Risk_inOneTrade_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void minProfitInOneTrade_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void maxProfitInOneTrade_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
