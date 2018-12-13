using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Classes
{
    [Serializable]
    public static class RiskProfile
    {
        //константы 
        public const double MINRISK = 0.1;      //минимальное значение риска капиталом
        public const double MAXRISK = 2;         //максимальное значение риска капиталом
        public const double MINSTOPLOSS = 0.5;  //минимальное значение стопа
        public const double MASXTOPLOSS = 5;     //максимальное значение стопа

        //поля
        public static double min_RiskCapital_inOneTrade { get; set; } = 0.1; //мин риск в сделке, %
        public static double max_RiskCapital_inOneTrade { get; set; } = 2;   //макс риск в сделке, %
        public static double min_StopLoss_inOneTrade { get; set; } = 0.5;    //минимальный стоп-лосс, %
        public static double max_StopLoss_inOneTrade { get; set; } = 5;      //максимальный стоп-лосс, %
        public static double min_Persent_ofDeposit { get; set; } = 0;        //мин % от депозита в сделке
        public static double max_Persent_ofDeposit { get; set; } = 0;        //макс % от депозита в сделке
        public static double min_Profit_inOneTrade { get; set; } = 3;        //мин профит в сделке, %
        public static double max_Profit_inOneTrade { get; set; } = 5;        //макс профит в трейде, %

    }
}
