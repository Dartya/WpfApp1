using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Classes
{
    public abstract class FinInstrument : FinInfo
    {   //абстрактный класс для задания 3. мое дополнение - имплементация интерфейса FinInfo и реализация его метода
        //СТРУКТУРА ДАННЫХ
        public enum Currency { RUB = 1, USD, EUR };    //перечисление валют, в которых торгуется финансовый инструмент
        decimal? price, lowest_price, highest_price; //"?" после типа значения указывает на то, что поле содержит значение null. Эквивалентно System.Nullable<T>, где T - тип значения
        string issuer;      //эмитент           //напр. Alphabet inc.
        public Currency nati_curr; //в какой нацвалюте торгуется инструмент

        public enum Country
        {                   //перечисление стран
            Russia = 1,
            USA,
            EU
        };
        Country country;    //страна-эмитент

        public string Ticker { get => ticker; set => ticker = value; } //следим за пальцами: это имя свойства, реализующего методы доступа (аксессоры) get и set/ 
        string ticker;//тикер             //напр. GOOG                 //а это - инкапсулируемое свойством поле

        public string Name { get => name; set => name = value; }
        string name;        //имя инструмента   //напр. Alphabet inc. class C

        string issuer_country { get; set; }     //страна эмитента; автоматическое реализуемое свойство с методами доступа get и set (ПОЛЕ СОЗДАНО ДЛЯ ПРИМЕРА)
        public string Issuer                    //еще одна реализация свойства c методами доступа get и set
        {
            get => issuer;                      //получаем имя эмитента по обращению *имя объекта*.Issuer
            set => issuer = value;              //пишем значение так же: *имя объекта*.Issuer = *значение*
        }

        //КОНСТРУКТОРЫ
        public FinInstrument()
        {    //переопределение конструктора по умолчанию - будут устанавливаться значения перечислений по умолчанию
            nati_curr = Currency.USD;
            country = Country.USA;
        }

        public FinInstrument(Country country, string issuer, string ticker)
        {   //параметрический конструктор

            switch (country)
            {      //в зависимости от инкапсулируемого значение enum Country задается значение enum Currency, входящее в класс FinInstrument
                case Country.Russia:
                    nati_curr = Currency.RUB;
                    break;
                case Country.USA:
                    nati_curr = Currency.USD;
                    break;
                case Country.EU:
                    nati_curr = Currency.EUR;
                    break;
            }
            this.issuer = issuer;       //через this инкапсулируется значение поля именно данного экземпляра объекта, который создается настоящим конструктором
            this.ticker = ticker;       //такой способ можно использовать для самописных методов доступа get и set. здесь же стоит упомянуть, что через super. можно получить доступ к значению поля не объекта, а суперкласса
        }

        //здесь я бы расположил индексатор, если бы имелась какая-либо внятная и адекватная сущность, которую стоило бы реализовать с помощью массива.

        //МЕТОДЫ
        public abstract void printInfo();   //в абстрактном классе метод имплементируемого интерфейса реализуется абстрактно - т.е. о самом методе упоминание есть, но конкретной реализации нет.

        public abstract void showInstrumentInfo(); //абстрактная функция, не имплементированная через интерфейс. требуется последующая реализация в классах-потомках

        public virtual void showTicker()
        {           //виртуальная функция. можно использовать в классах-наследниках "как есть", либо переопределить при написании наследника
            Console.WriteLine(Name + "'s ticker is " + Ticker);
        }
    }
}
