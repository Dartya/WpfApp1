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
        public static float min_RiskCapital_inOneTrade { get; set; }    //мин риск в сделке, %
        public static float max_RiskCapital_inOneTrade { get; set; }    //макс риск в сделке, %
        public static float max_Persent_ofDeposit { get; set; }         //макс % от депозита в сделке
        public static float min_Profit_inOneTrade { get; set; }         //мин профит в сделке, %
        public static float max_Profit_inOneTrade { get; set; }         //макс профит в трейде, %
        public static string RiskProfit_Ratio { get; set; }             //соотношение риска к профиту, рекомендуется 1:3


    }
}
