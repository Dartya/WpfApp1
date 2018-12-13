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
    /// Логика взаимодействия для CalculatorWindow.xaml
    /// </summary>
    public partial class CalculatorWindow : Window
    {
        double minTakeProfit = 0;
        double maxTakeProfit = 0;
        double minStopLoss = 0;
        double maxStopLoss = 0;
        double priceStopLossDouble = 0;
        double priceTakeProfitDouble = 0;
        int volumeInt = 0;
        double sumPositionDouble = 0;

        public CalculatorWindow()
        {
            InitializeComponent();

            minTakeProfit = Classes.RiskProfile.min_Profit_inOneTrade;
            maxTakeProfit = Classes.RiskProfile.max_Profit_inOneTrade;
            minStopLoss = Classes.RiskProfile.min_StopLoss_inOneTrade;
            maxStopLoss = Classes.RiskProfile.max_StopLoss_inOneTrade;
            priceStopLossDouble = 0;
            priceTakeProfitDouble = 0;
            volumeInt = 0;
            sumPositionDouble = 0;
        }
    }
}
