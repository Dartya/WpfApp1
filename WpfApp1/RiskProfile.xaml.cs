using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private void DecimalValidationTextBox(object sender, TextCompositionEventArgs e){
            Regex regex = new Regex("[^0-9]+([,][0-9]{1,3})");
            e.Handled = regex.IsMatch(e.Text);
        }

        //риск
        double minRiskValue = 0.1;
        double maxRiskValue = 2;
        double minStopLoss = 0.5;
        double maxStopLoss = 5;
        double minPercent = 0;
        double maxPercent = 0;
        //профит
        double minProfit = 3;
        double maxProfit = 5;

        public RiskProfile()
        {
            InitializeComponent();

            minRiskValue = Classes.RiskProfile.min_RiskCapital_inOneTrade;
            maxRiskValue = Classes.RiskProfile.max_RiskCapital_inOneTrade;
            minStopLoss = Classes.RiskProfile.min_StopLoss_inOneTrade;
            maxStopLoss = Classes.RiskProfile.max_StopLoss_inOneTrade;
            minProfit = Classes.RiskProfile.min_Profit_inOneTrade;
            maxProfit = Classes.RiskProfile.max_Profit_inOneTrade;

            countPercents();

            min_Risk_inOneTrade.Value = minRiskValue;
            max_Risk_inOneTrade.Value = maxRiskValue;
            min_StopLoss.Value = minStopLoss;
            max_StopLoss.Value = maxStopLoss;
            minProfitInOneTrade.Text = minProfit.ToString();
            maxProfitInOneTrade.Text = maxProfit.ToString();
        }

        private void enter_button(object sender, RoutedEventArgs e)
        {
            if (minPercent > maxPercent)
            {
                MessageBox.Show("Значение минимального уровня риска капиталом должно быть МЕНЬШЕ значения максимального уровня риска капиталом!\nВернитесь к настройкам параметров риск-профиля!", "Ошибка!");
                return;
            }
            else {
                 Classes.RiskProfile.min_RiskCapital_inOneTrade = min_Risk_inOneTrade.Value;
                 Classes.RiskProfile.max_RiskCapital_inOneTrade = max_Risk_inOneTrade.Value;
                 Classes.RiskProfile.min_Persent_ofDeposit = max_StopLoss.Value;
                 Classes.RiskProfile.max_Persent_ofDeposit = maxPercent;
                 Classes.RiskProfile.min_Profit_inOneTrade = minPercent;
                 Classes.RiskProfile.max_Profit_inOneTrade = maxPercent;
                 Classes.RiskProfile.min_StopLoss_inOneTrade = min_StopLoss.Value;
                 Classes.RiskProfile.max_StopLoss_inOneTrade = max_StopLoss.Value;
                 Classes.RiskProfile.min_Profit_inOneTrade = double.Parse(minProfitInOneTrade.Text);
                 Classes.RiskProfile.max_Profit_inOneTrade = double.Parse(maxProfitInOneTrade.Text);
            }

            this.DialogResult = true;
        }

        private void minProfitInOneTrade_TextChanged(object sender, TextChangedEventArgs e) {
            try {
                if (double.Parse(minProfitInOneTrade.Text) > maxProfit)
                {
                    MessageBox.Show("Значение минимального уровня take profit должно быть меньше максимального уровня take profit!", "Ошибка!");
                    minProfitInOneTrade.Text = minProfit.ToString();
                    return;
                }
                else
                    minProfit = double.Parse(minProfitInOneTrade.Text);
            }
            catch (Exception exc) {
                MessageBox.Show("Введите корректное значение!", "Ошибка!");
                minProfitInOneTrade.Text = minProfit.ToString();
                return;
            }
        }

        private void maxProfitInOneTrade_TextChanged(object sender, TextChangedEventArgs e) {
            try {
                if (double.Parse(maxProfitInOneTrade.Text) < minProfit)
                {
                    MessageBox.Show("Значение минимального уровня take profit должно быть меньше максимального уровня take profit!", "Ошибка!");
                    maxProfitInOneTrade.Text = maxProfit.ToString();
                    return;
                }
                else
                    maxProfit = double.Parse(maxProfitInOneTrade.Text);
            } catch (Exception exc) {
                MessageBox.Show("Введите корректное значение!", "Ошибка!");
                maxProfitInOneTrade.Text = maxProfit.ToString();
                return;
            }
        }

        private void min_Risk_inOneTrade_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            minRiskValue = min_Risk_inOneTrade.Value;
            if (minRiskValue > maxRiskValue)
            {
                MessageBox.Show("Величина минимального риска капиталом должна быть меньше или равна величине максимального риска капиталом!", "Ошибка!");
                min_Risk_inOneTrade.Value = 0.1;
            }
            else
            {
                if ((minRiskValue / minStopLoss * 100) >= 0)
                {
                    minPercent = minRiskValue / minStopLoss * 100;
                    if (minPercentOfDeposit != null)
                        minPercentOfDeposit.Content = minPercent.ToString();
                    else return;
                }
                else minPercentOfDeposit.Content = "0";
            }
        }

        private void max_Risk_inOneTrade_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            maxRiskValue = max_Risk_inOneTrade.Value;
            if (maxRiskValue < minRiskValue )
            {
                MessageBox.Show("Величина максимального риска капиталом должна быть больше величины минимально риска капиталом!", "Ошибка!");
                max_Risk_inOneTrade.Value = 2;
            }
            else
            {
                if ((maxRiskValue / maxStopLoss * 100) >= 0)
                {
                    maxPercent = maxRiskValue / maxStopLoss * 100;
                    if (maxPercentOfDeposit != null)
                        maxPercentOfDeposit.Content = maxPercent.ToString();
                    else return;
                }
                else maxPercentOfDeposit.Content = "0";
            }
        }

        private void min_StopLoss_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            minStopLoss = min_StopLoss.Value;
            if (maxStopLoss < minStopLoss)
            {
                MessageBox.Show("Величина максимального уровня stop loss должна быть больше величины минимально уровня stop loss!", "Ошибка!");
                min_StopLoss.Value = 0.5;
            }
            else
            {
                if ((minRiskValue / minStopLoss * 100) >= 0)
                {
                    minPercent = minRiskValue / minStopLoss * 100;
                    if (minPercentOfDeposit != null)
                        minPercentOfDeposit.Content = minPercent.ToString();
                    else return;
                }
                else minPercentOfDeposit.Content = "0";
            }
        }

        private void max_StopLoss_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            maxStopLoss = max_StopLoss.Value;
            if (maxStopLoss < minStopLoss)
            {
                MessageBox.Show("Величина максимального уровня stop loss должна быть больше величины минимально уровня stop loss!", "Ошибка!");
                max_StopLoss.Value = 5;
            }
            else
            {
                if ((maxRiskValue / maxStopLoss * 100) >= 0)
                {
                    maxPercent = maxRiskValue / maxStopLoss * 100;
                    if (maxPercentOfDeposit != null)
                        maxPercentOfDeposit.Content = maxPercent.ToString();
                    else return;
                }
                else maxPercentOfDeposit.Content = "0";
            }
        }

        private void countPercents() {
            //min percent
            if ((minRiskValue / minStopLoss * 100) >= 0)
            {
                minPercent = minRiskValue / minStopLoss * 100;
                if (minPercentOfDeposit != null)
                    minPercentOfDeposit.Content = minPercent.ToString();
                else return;
            }
            else minPercentOfDeposit.Content = "0";

            //max percent
            if ((maxRiskValue / maxStopLoss * 100) >= 0)
            {
                maxPercent = maxRiskValue / maxStopLoss * 100;
                if (maxPercentOfDeposit != null)
                    maxPercentOfDeposit.Content = maxPercent.ToString();
                else return;
            }
            else maxPercentOfDeposit.Content = "0";
        }
    }
}
