using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Classes
{
    class ETF : FinInstrument
    {                     //класс ETF - наследник класса финансовый инструмент, содержащий массив - портфель разных акций.

        Stock[] stockPortfolio;

        //КОНСТРУКТОР
        public ETF(string name, int howStocks)
        {
            stockPortfolio = new Stock[howStocks];  //задание границ массива Stock
            Name = name;
            Console.WriteLine("ETF " + name + " создан, помещено {0} акций в портфель.\n", howStocks);
        }

        //ИНДЕКСАТОР ПО НОМЕРУ
        public Stock this[int index]
        {

            get
            {
                return stockPortfolio[index];
            }
            set
            {
                stockPortfolio[index] = value;
            }
        }

        //ИНДЕКСАТОР ПО ТИКЕРУ АКЦИИ - ФАКТИЧЕСКИ ПЕРЕГРУЖЕННЫЙ ИНДЕКСАТОР
        public Stock this[string isticker]
        {
            get
            {
                Stock stock = null;
                foreach (var p in stockPortfolio)   //вот и реализация foreach - для каждого следующего p в массиве stockPortfolio. по в данном перегруженном аксессоре происходит поиск 
                                                    //по элементам массива методом перебора каждого и сравнения значений параметра с полем элемента массива
                {
                    if (p?.Ticker == isticker)      //, где p? - p-й элемент массива акций Stock stockPortfolio. ВОПРОС - а как ticker оставить сокрытым? ОТВЕТ - а с помощью быстрых действий инкапсулируем ticker и создаем свойство Ticker
                    {                               //кстати, далее по тексту ticker везде заменится на свойство Ticker. неплохо, MSFT, но нахрен не нужно - генерирование геттеров и сеттеров в Java намного проще и удобнее
                        stock = p;                  //ссылочному типу stock присвоить значение очередного элемента p
                        break;
                    }
                }
                return stock;                       //и вернуть как результат Stock stock
            }
        }
        //МЕТОД
        override public void printInfo()
        {          //реализация метода printInfoprintInfo имплементированного классом FinInstrument интерфейса FinInfo
            Console.WriteLine("Всего в портфеле ETF фонда {0} находится {1} акций.\n", Name, stockPortfolio.Length);
        }

        public override void showInstrumentInfo()
        {
            Console.WriteLine("Это ETF-фонд - дериватив, который содержит ценные бумаги одного сектора экономики. Покупая его, вы покупаете готовый портфель бумаг под управлением профессионалов.\n");
        }
        public override void showTicker()
        {
            Console.WriteLine("Переопределенный метод showTicker().\n");
        }
        public void showTicker(int i)
        {              //виртуальная функция. можно использовать в классах-наследниках "как есть", либо сделать перегрузку
            Console.WriteLine("А это {0}-я реализация виртуального метода showTicker()", i);
        }
    }
}
