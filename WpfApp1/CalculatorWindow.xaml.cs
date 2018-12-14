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
    /// Логика взаимодействия для CalculatorWindow.xaml
    /// </summary>
    public partial class CalculatorWindow : Window
    {
        double minTakeProfit = 0;
        double maxTakeProfit = 0;
        double minStopLoss = 0;
        double maxStopLoss = 0;
        int volumeIntMin = 0;
        int volumeIntMax = 0;
        decimal Deposit = 0;
        decimal Price = 0;
        decimal priceStopLoss = 0;
        decimal priceTakeProfit = 0;
        string TradeType = "";
        int koef = 1;
        decimal minComission = 2m;
        decimal maxComission = 2m;

        double minRiskCapital = 0;
        double maxRiskCapital = 0;
        decimal minPositionCapital = 0;
        decimal maxPositionCapital = 0;
        double min_Persent_ofDeposit = 0;
        double max_Persent_ofDeposit = 0;

        decimal minPositionSum = 0;
        decimal maxPositionSum = 0;

        private void DecimalValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+([,][0-9]{1,3})");
            e.Handled = regex.IsMatch(e.Text);
        }

        public CalculatorWindow(decimal deposit)
        {
            InitializeComponent();

            minTakeProfit = Classes.RiskProfile.min_Profit_inOneTrade;
            maxTakeProfit = Classes.RiskProfile.max_Profit_inOneTrade;
            minStopLoss = Classes.RiskProfile.min_StopLoss_inOneTrade;
            maxStopLoss = Classes.RiskProfile.max_StopLoss_inOneTrade;
            minRiskCapital = Classes.RiskProfile.min_Persent_ofDeposit;
            maxRiskCapital = Classes.RiskProfile.max_Persent_ofDeposit;
            min_Persent_ofDeposit = Classes.RiskProfile.min_Persent_ofDeposit;
            max_Persent_ofDeposit = Classes.RiskProfile.max_Persent_ofDeposit;

            Deposit = deposit;

            DepositLable.Content = Deposit.ToString();
            minstop.Content = minStopLoss.ToString();
            maxstop.Content = maxStopLoss.ToString();
            mintake.Content = minTakeProfit.ToString();
            maxtake.Content = maxTakeProfit.ToString();
            stoploss.Minimum = minStopLoss;
            stoploss.Maximum = maxStopLoss;
            takeprofit.Minimum = minTakeProfit;
            takeprofit.Maximum = maxTakeProfit;
            countTicks(minStopLoss, maxStopLoss, stoploss);
            countTicks(minTakeProfit, maxTakeProfit, takeprofit);

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Price = decimal.Parse(PriceTextBox.Text);
            }
            catch (Exception exc) {
                MessageBox.Show("Значение должно быть не отрицательным!","Ошибка!");
                PriceTextBox.Text = "0";
            }
            countPriceStopLoss();
            countPriceTakeProfit();
            countMinPosition();
            countMaxPosition();
        }

        //метод вычисления суммы капитала, на которую обеспечивается позиция
        private decimal countPositionCapital(double RiskCapital)
        {
            decimal PositionCapital = Math.Round((Deposit * (decimal)(RiskCapital / 100)), 2);
            return PositionCapital;
        }

        //метод вычисления объема позиции в шт
        private int countPositionVolume(decimal positionCapital)
        {
            int positionVolume;
            if (Price == 0)
                return positionVolume = 0;
            positionVolume = (int)(positionCapital / Price);
            return positionVolume;
        }

        //метод вычисления комиссионных издержек
        public decimal countComissions(decimal Position_Volume)
        {
            decimal comissions = 2m;
            if (Position_Volume > 10000m)
                comissions += Position_Volume * 0.00017m;
            else comissions = 2m;
            return Math.Round(comissions, 2);
        }

        //вычисление минимальной позиции
        private void countMinPosition() {
            minPositionCapital = countPositionCapital(minRiskCapital);
            volumeIntMin = countPositionVolume(minPositionCapital);
            minPositionSum = Price * volumeIntMin;
            minComission = countComissions(minPositionSum);
            minPositionSum = minPositionSum + minComission;

            volumeMin.Content = volumeIntMin.ToString();
            sumPositionMin.Content = minPositionSum.ToString();
            minComissionLabel.Content = minComission.ToString();
        }

        //вычисление максимальной позиции
        private void countMaxPosition()
        {
            maxPositionCapital = countPositionCapital(maxRiskCapital);
            volumeIntMax = countPositionVolume(maxPositionCapital);
            maxPositionSum = Price * volumeIntMax;
            maxComission = countComissions(maxPositionSum);
            maxPositionSum = maxPositionSum + maxComission;

            volumeMax.Content = volumeIntMax.ToString();
            sumPositionMax.Content = maxPositionSum.ToString();
            maxComissionLabel.Content = maxComission.ToString();
        }

        //расчет цены стоп лосс
        private void countPriceStopLoss() {
            if (stoploss == null)
                return;
            else
            {
                priceStopLoss = Math.Round(Price - koef * (Price * ((decimal)stoploss.Value / 100)), 2);
                priceStopLossLabel.Content = priceStopLoss.ToString();
            }
        }
        //расчет цены тейк профит
        private void countPriceTakeProfit()
        {
            if (takeprofit == null)
                return;
            else
            {
                priceTakeProfit = Math.Round(Price + koef * (Price * ((decimal)takeprofit.Value / 100)), 2);
                priceTakeProfitLabel.Content = priceTakeProfit.ToString();
            }
        }
        //изменение состояния группы tradeType
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            TradeType = pressed.Content.ToString();
            if (TradeType == "Short")
                koef = -1;
            else koef = 1;
            countPriceStopLoss();
            countPriceTakeProfit();
        }
        
        private void countTicks(double min, double max, Slider slider) {
            double dif = Math.Round((max - min), 1);
            double tickRatio = dif / 10;
            slider.Ticks.Add(min);
            do
            {
                if ((min + tickRatio) > max)
                    return;
                else
                {
                    slider.Ticks.Add(Math.Round((min + tickRatio), 1));
                    min = min + tickRatio;
                }
            } while (min < max);
            slider.Ticks.Add(max);
            return;
        }

        private void stoploss_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (PriceTextBox.Text != null)
            {
                countPriceStopLoss();
                countRatio();
            }
            else return;
        }

        private void takeprofit_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (PriceTextBox.Text != null)
            {
                countPriceTakeProfit();
                countRatio();
            }
            else return;
        }

        private void countRatio() {
            string ratiostr = "";
            double risk, profit, Ratio;
            risk = stoploss.Value;
            profit = takeprofit.Value;
            Ratio = Math.Round((profit / risk), 2);
            ratiostr = "1 : " + Ratio.ToString();
            ratio.Content = ratiostr;
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
