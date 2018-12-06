using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Classes
{
    class Currency : FinInstrument
    {//класс валюта - наследник класса финансовый инструмент
        override public void printInfo()
        {  //реализация метода printInfoprintInfo имплементированного классом FinInstrument интерфейса FinInfo
            Console.WriteLine("Информация о валюте {0}: эмитент {1}, тикер {2}.\n", Name, Issuer, Ticker);
        }
        public override void showInstrumentInfo()
        {
            Console.WriteLine("Это валюта - универсальное инструмент обмена.\n");
            throw new NotImplementedException();
        }
    }
}
