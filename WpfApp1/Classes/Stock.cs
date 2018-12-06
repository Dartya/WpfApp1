using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Classes
{
    class Stock : FinInstrument
    {           //класс акция - наследник класса финансовый инструмент
        public Stock(string issuer, string ticker)
        {//конструктор класса Stock
            Ticker = ticker;
            Issuer = issuer;
        }
        public Stock(string name, string issuer, string ticker)
        {//конструктор класса Stock
            Name = name;
            Ticker = ticker;
            Issuer = issuer;
        }

        override public void printInfo()
        {          //реализация метода printInfoprintInfo имплементированного классом FinInstrument интерфейса FinInfo
            Console.WriteLine("Информация о ценной бумаге компании {0}: эмитент {1}, тикер {2}, торгуется в {3}\n", Name, Issuer, Ticker, this.nati_curr);
        }

        override public void showInstrumentInfo()
        { //обязательное переопределение реализации абстрактного метода (функции)
            Console.WriteLine("Акция - это ценная бумага, позволяющая наращивать капитализацию компании, а инвестору - получать ежеквартальный дивидендный доход и рассчитывать на доход от последующей ее продажи.\n");
        }

        /*override public void showTicker(){          //не обязательное переопределение виртуальной функции 
            Console.WriteLine("Тикер акции - "+Ticker+". По тикеру легко можно найти акцию на информационных порталах.\n");
        }*/
    }
}
