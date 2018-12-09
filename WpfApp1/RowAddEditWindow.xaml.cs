using System;
using System.Collections.Generic;
using System.Data;
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
using System.Text.RegularExpressions;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для RowAddEditWindow.xaml
    /// </summary>
    public partial class RowAddEditWindow : Window
    {
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public string TitleString { get; set; }
        public Trade trade;
        public string SelectedInstrumentClass { get; set; }
        public string SelectedTradeType { get; set; }

        //параметры сделки
        private decimal Opening_price;
        private decimal Closing_price;
        private decimal Position_Volume;
        private decimal Position_Volume_CloseSum;
        private int Trade_size;
        private decimal TAX = 0.13m;
        private decimal Taxes;
        private decimal Comissions = 2m;
        private decimal Profit;
        private int LongShortKoef = 1; //1 - Long, -1 - Short

        //метод вычисления суммы открытой позиции
        public void countPosition_volume() {
            if (opening_price.Text == null || opening_price.Text == "")
                Opening_price = 0;
            else Opening_price = decimal.Parse(opening_price.Text);

            if (trade_size.Text == null || trade_size.Text == "")
                Trade_size = 0;
            else Trade_size = int.Parse(trade_size.Text);

            Position_Volume = Opening_price * Trade_size;
            position_volume.Content = Position_Volume.ToString();
            trade.OpeningPrice = Position_Volume;

            if (checkbox.IsChecked == true) {
                countPosition_Volume_CloseSum();
                countComissions();
                countTaxes();
                countProfit();
            }
        }

        private void Opening_price_TextChanged(object sender, TextChangedEventArgs e)
        {
            countPosition_volume();
        }

        private void Trade_size_TextChanged(object sender, TextChangedEventArgs e)
        {
            countPosition_volume();
        }

        //метод вычисления суммы закрытой позиции
        public void countPosition_Volume_CloseSum() {
            if (closing_price.Text == null || closing_price.Text == "")
                Closing_price = 0;
            else Closing_price = decimal.Parse(closing_price.Text);

            if (trade_size.Text == null || trade_size.Text == "")
                Trade_size = 0;
            else Trade_size = int.Parse(trade_size.Text);

            Position_Volume_CloseSum = (Closing_price * Trade_size) * LongShortKoef;
            closed_position_volume.Content = Position_Volume_CloseSum.ToString();
        }

        private void Closing_price_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (closing_price.IsReadOnly == false)
            {
                countPosition_Volume_CloseSum();
                countComissions();
                countTaxes();
                countProfit();
            }
        }

        //метод вычисления комиссионных издержек
        public void countComissions() {
            countPosition_Volume_CloseSum();

            Comissions = 2m;
            if (Position_Volume > 10000m || Position_Volume_CloseSum > 10000m)
                Comissions += Position_Volume_CloseSum * 0.00017m;
            else Comissions = 2m;
            comissions.Content = Comissions.ToString();
        }

        //метод вычисления налогов
        public void countTaxes() {
            if (Position_Volume_CloseSum - Position_Volume > 0)
                Taxes = (Position_Volume_CloseSum - Position_Volume) * TAX;
            else Taxes = 0;
            taxes.Content = Taxes.ToString();
        }

        //метод вычисления профита
        public void countProfit() {
            Profit = Position_Volume_CloseSum - Position_Volume - Taxes - Comissions;
            FinProfit.Content = Profit.ToString();
        }

        //общий метод высиления всех указанных выше параметров
        public void countAll() {
            countPosition_volume();
            countPosition_Volume_CloseSum();
            countComissions();
            countTaxes();
            countProfit();
        }

        public RowAddEditWindow(string title)
        {
            trade = new Trade();
            Title = title;
            InitializeComponent();
            closing_price.IsReadOnly = true;
            comissions.Content = "0";
            trade.Comission = 0;

            taxes.Content = "0";
            trade.Taxes = 0;

            FinProfit.Content = "0";
            trade.Profit = 0;
        }

        public RowAddEditWindow(string title, DataRowView row)
        {
            trade = new Trade(row);
            Title = title;
            InitializeComponent();
            closing_price.IsReadOnly = true;
            comissions.Content = "0";
            trade.Comission = 0;

            taxes.Content = "0";
            trade.Taxes = 0;

            FinProfit.Content = "0";
            trade.Profit = 0;
        }

        public void enter_button(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Instrument_class_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedInstrumentClass = instrument_class.SelectedValue.ToString();
            int iInstrumentClass = 0;

            switch (SelectedInstrumentClass)
            {
                case "Валюта":
                    iInstrumentClass = 0;
                    break;
                case "Акция":
                    iInstrumentClass = 1;
                    break;
                case "Фьючерс":
                    iInstrumentClass = 2;
                    break;
            }
            trade.InstrumentType = iInstrumentClass;
        }
        private void Trade_type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combobox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)combobox.SelectedItem;
            if (selectedItem != null)
            {
                SelectedTradeType = selectedItem.Content.ToString();

                MessageBox.Show(SelectedTradeType);
            }
            int iSelectedTradeType = 0;
            /*SelectedTradeType = trade_type.SelectedValue.ToString();
            MessageBox.Show(trade_type.SelectedValue.ToString());*/

            switch (SelectedTradeType) {
                case "Long":
                    iSelectedTradeType = 0;
                    LongShortKoef = 1;
                    countAll();
                    break;
                case "Short":
                    iSelectedTradeType = 1;
                    LongShortKoef = -1;
                    countAll();
                    break;
            }

            trade.TradeType = iSelectedTradeType;
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            closing_price.IsReadOnly = false;

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            closing_price.IsReadOnly = true;
            closing_price.Text = "0";

            comissions.Content = "0";
            trade.Comission = 0;

            taxes.Content = "0";
            trade.Taxes = 0;

            FinProfit.Content = "0";
            trade.Profit = 0;
        }

        private void Instrument_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            trade.InstrumentName = instrument_name.Text;
        }

        private void Instrument_ticker_TextChanged(object sender, TextChangedEventArgs e)
        {
            trade.Ticker = instrument_ticker.Text;
        }

    }
}