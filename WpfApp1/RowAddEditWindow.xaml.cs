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
        private void DecimalValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+([,][0-9]{1,3})");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void IntValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        
        //глобальные переменные
        public string TitleString { get; set; }
        public Trade trade;

        //параметры сделки
        private int TradeId;
        private int iInstrumentType = 0;
        private decimal Opening_price;
        private decimal Closing_price;
        private decimal Position_Volume;
        private decimal Position_Volume_CloseSum;
        private int Trade_size;
        private decimal TAX = 0.13m;
        private decimal Taxes;
        private decimal Comissions = 2m;
        private decimal Profit;
        private int iSelectedTradeType = 0; //0 - Long, 1 - Short

        //конструктор экрана добавления записи
        public RowAddEditWindow(string title)
        {
            trade = new Trade();
            Title = title;
            InitializeComponent();
            closing_price.IsReadOnly = true;

            setParamsNull();
        }
        //конструктор экрана редактирования записи
        public RowAddEditWindow(string title, DataRowView row)
        {
            trade = new Trade(row);
            Title = title;
            InitializeComponent();

            getTradeData();
        }

        //метод обновления данных объекта trade
        private void updateTradeParams() {
            trade.InstrumentName = instrument_name.Text;    //имя инструмента
            trade.InstrumentType = iInstrumentType;         //класс инструмента
            trade.Ticker = instrument_ticker.Text;          //тикер инструмента
            trade.TradeType = iSelectedTradeType;           //тип сделки
            trade.OpeningPrice = Opening_price;             //цена открытия
            trade.TradeSize = Trade_size;                   //объем позиции
            trade.TradeSum = Position_Volume;               //сумма открытия сделки
            trade.TradeClosed = (bool)checkbox.IsChecked;   //флаг закрытия сделки
            trade.ClosingPrice = Closing_price;             //цена закрытия
            trade.Comission = Comissions;                   //комисии
            trade.Taxes = Taxes;                            //налоги
            trade.Profit = Profit;                          //финансовый результат
        }

        //метод получения данных из трейда
        private void getTradeData() {

            instrument_name.Text = trade.InstrumentName;    //имя инструмента
            /*switch (trade.InstrumentType) {                 //комбобокс класс инструмента
                case 0:
                    instrument_class.SelectedItem = instrument_class.FindName("Валюта");
                    break;
                case 1:
                    instrument_class.SelectedItem = instrument_class.FindName("Акция");
                    break;
                case 2:
                    instrument_class.SelectedItem = instrument_class.FindName("Фьючерс");
                    break;
            }*/

            instrument_ticker.Text = trade.Ticker;          //тикер инструмента
            instrument_name.Text = trade.InstrumentName;    //имя инструмента

            /*switch (trade.TradeType) {                      //комбобокс тип сделки
                case 0:
                    trade_type.SelectedItem = trade_type.FindName("Long");
                    break;
                case 1:
                    trade_type.SelectedItem = trade_type.FindName("Short");
                    break;
            }*/

            opening_price.Text = trade.OpeningPrice.ToString().Replace(".", ",");   //цена открытия
            trade_size.Text = trade.TradeSize.ToString();   //объем позиции
            position_volume.Content = trade.TradeSum.ToString().Replace(".", ",");  //сумма открытия сделки
            checkbox.IsChecked = trade.TradeClosed;         //флаг закрытия сделки
            closing_price.Text = trade.ClosingPrice.ToString().Replace(".", ",");   //цена закрытия сделки
            comissions.Content = trade.Comission.ToString().Replace(".", ",");      //комиссии
            taxes.Content = trade.Taxes.ToString().Replace(".", ",");               //налоги
            FinProfit.Content = trade.Profit.ToString().Replace(".", ",");          //профит
        }

        //метод вычисления суммы открытой позиции
        public void countPosition_volume() {
            if (opening_price.Text == null || opening_price.Text == "")
                Opening_price = 0;
            else {
                try
                {
                    Opening_price = decimal.Parse(opening_price.Text);
                }
                catch (Exception exc) { //на случай, если будет введена точка вместо запятой:
                    opening_price.Text = opening_price.Text.Remove(opening_price.Text.Length -1, 1) + ",";
                    opening_price.CaretIndex = opening_price.Text.Length;
                }
            }

            if (trade_size.Text == null || trade_size.Text == "")
                Trade_size = 0;
            else {
                try
                {
                    Trade_size = int.Parse(trade_size.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("НЕВЕРНЫЙ ФОРМАТ ВВОДА!\n" + exc.ToString() + "\n используете цифры!");
                    opening_price.Text = "";
                }
            }

            Position_Volume = Opening_price * Trade_size;
            position_volume.Content = Position_Volume.ToString();
            trade.OpeningPrice = Position_Volume;

            if (checkbox.IsChecked == true) {
                countPosition_Volume_CloseSum();
                countComissions();
                countTaxes();
                countProfit();
            }
            updateTradeParams(); //МЕТОД ОБНОВЛЕНИЯ ПАРАМЕТРОВ
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
            else {
                try
                {
                    Closing_price = decimal.Parse(closing_price.Text);
                }
                catch (Exception exc)
                { //на случай, если будет введена точка вместо запятой:
                    closing_price.Text = closing_price.Text.Remove(closing_price.Text.Length - 1, 1) + ",";
                    closing_price.CaretIndex = closing_price.Text.Length;
                }
            }

            if (trade_size.Text == null || trade_size.Text == "")
                Trade_size = 0;
            else
            {
                try
                {
                    Trade_size = int.Parse(trade_size.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("НЕВЕРНЫЙ ФОРМАТ ВВОДА!\n" + exc.ToString() + "\n используете цифры!");
                    opening_price.Text = "";
                }
            }

            Position_Volume_CloseSum = (Closing_price * Trade_size);
            closed_position_volume.Content = Position_Volume_CloseSum.ToString();
            updateTradeParams(); //МЕТОД ОБНОВЛЕНИЯ ПАРАМЕТРОВ
        }

        private void Closing_price_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (closing_price.IsReadOnly == false)
            {
                countPosition_Volume_CloseSum();
                countComissions();
                countTaxes();
                countProfit();
                updateTradeParams(); //МЕТОД ОБНОВЛЕНИЯ ПАРАМЕТРОВ
            }
        }

        //метод вычисления комиссионных издержек
        public void countComissions() {
            countPosition_Volume_CloseSum();
            if (checkbox.IsChecked == true) {
                Comissions = 2m;
                if (Position_Volume > 10000m || Position_Volume_CloseSum > 10000m)
                    Comissions += Position_Volume_CloseSum * 0.00017m;
                else Comissions = 2m;
                comissions.Content = Comissions.ToString();
            } else Comissions = 0;
            updateTradeParams(); //МЕТОД ОБНОВЛЕНИЯ ПАРАМЕТРОВ
        }

        //метод вычисления налогов
        public void countTaxes() {
            if (iSelectedTradeType == 0) {
                if (Position_Volume_CloseSum - Position_Volume > 0)
                    Taxes = (Position_Volume_CloseSum - Position_Volume) * TAX;
                else Taxes = 0;
            }
            else if (iSelectedTradeType == 1) {
                if (Position_Volume - Position_Volume_CloseSum > 0)
                    Taxes = (Position_Volume - Position_Volume_CloseSum) * TAX;
                else Taxes = 0;
            }
            taxes.Content = Taxes.ToString();
            updateTradeParams(); //МЕТОД ОБНОВЛЕНИЯ ПАРАМЕТРОВ
        }

        //метод вычисления профита
        public void countProfit() {
            if (checkbox.IsChecked == true) {
                if (iSelectedTradeType == 0)
                {
                    Profit = Position_Volume_CloseSum - Position_Volume - Taxes - Comissions;
                    FinProfit.Content = Profit.ToString();
                }
                else if (iSelectedTradeType == 1)
                {
                    Profit = Position_Volume - Position_Volume_CloseSum - Taxes - Comissions;
                    FinProfit.Content = Profit.ToString();
                }
            }
            updateTradeParams(); //МЕТОД ОБНОВЛЕНИЯ ПАРАМЕТРОВ
        }

        //общий метод высиления всех указанных выше параметров
        public void countAll() {
            countPosition_volume();
            countPosition_Volume_CloseSum();
            countComissions();
            countTaxes();
            countProfit();
            updateTradeParams(); //МЕТОД ОБНОВЛЕНИЯ ПАРАМЕТРОВ
        }

        public void enter_button(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Instrument_class_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            string SelectedInstrumentClass;
            SelectedInstrumentClass = instrument_class.SelectedValue.ToString();
            
            switch (SelectedInstrumentClass)
            {
                case "System.Windows.Controls.ComboBoxItem: Валюта":
                    iInstrumentType = 0;
                    break;
                case "System.Windows.Controls.ComboBoxItem: Акция":
                    iInstrumentType = 1;
                    break;
                case "System.Windows.Controls.ComboBoxItem: Фьючерс":
                    iInstrumentType = 2;
                    break;
            }
            trade.InstrumentType = iInstrumentType;
        }
        private void Trade_type_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            string SelectedTradeType;
            SelectedTradeType = trade_type.SelectedItem.ToString(); //КОСТЫЛЬ, когда будет время - исправить!
            //MessageBox.Show(SelectedTradeType);

            switch (SelectedTradeType) {
                case "System.Windows.Controls.ComboBoxItem: Long":  //да, костыль! времени нет
                    iSelectedTradeType = 0;
                    countAll();
                    break;
                case "System.Windows.Controls.ComboBoxItem: Short": //да, костыль! времени нет
                    iSelectedTradeType = 1;
                    countAll();
                    break;
            }
            trade.TradeType = iSelectedTradeType;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) {
            closing_price.IsReadOnly = false;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e) {
            closing_price.IsReadOnly = true;
            setParamsNull();
        }

        private void Instrument_name_TextChanged(object sender, TextChangedEventArgs e) {
            trade.InstrumentName = instrument_name.Text;
        }

        private void Instrument_ticker_TextChanged(object sender, TextChangedEventArgs e) {
            trade.Ticker = instrument_ticker.Text;
        }

        //метод обнуления параметров
        private void setParamsNull() {
            closing_price.Text = "0";

            comissions.Content = "0";
            trade.Comission = 0;

            taxes.Content = "0";
            trade.Taxes = 0;

            FinProfit.Content = "0";
            trade.Profit = 0;

            closed_position_volume.Content = "0";
            Position_Volume_CloseSum = 0;
            updateTradeParams(); //МЕТОД ОБНОВЛЕНИЯ ПАРАМЕТРОВ
        }
    }
}