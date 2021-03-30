using ClassLibrary1;
using System;

namespace SubsidyCalculation
{
    class Program
    {
        static void Main(string[] args)
        {
            Volume[] volume = new Volume[5];
            ///Данные по использованному объему(подходящие)
            volume[0] = new Volume()
            {
                ServiceId = 1,
                HouseId = 1,
                Month = new DateTime(2021, 2, 27),
                Value = 10
            };
            ///Данные по использованному объему с некорректным идентификатором услуги
            volume[1] = new Volume()
            {
                ServiceId = 2,
                HouseId = 1,
                Month = new DateTime(2021, 2, 27),
                Value = 11
            };
            ///Данные по использованному объему с некорректным идентификатором дома
            volume[2] = new Volume()
            {
                ServiceId = 1,
                HouseId = 1,
                Month = new DateTime(2021, 2, 27),
                Value = 12
            };
            ///Данные по использованному объему с некорректным месяцем
            volume[3] = new Volume()
            {
                ServiceId = 1,
                HouseId = 1,
                Month = new DateTime(2021, 3, 27),
                Value = 13
            };
            ///Данные по использованному объему с некорректным значением
            volume[4] = new Volume()
            {
                ServiceId = 1,
                HouseId = 1,
                Month = new DateTime(2021, 2, 27),
                Value = -14
            };

            Tariff[] tariff = new Tariff[2];
            ///Данные подходящего тарифа
            tariff[0] = new Tariff()
            {
                ServiceId = 1,
                HouseId = 1,
                PeriodBegin = new DateTime(2021, 2, 1),
                PeriodEnd = new DateTime(2021, 2, 28),
                Value = 100
            };
            ///Данные тарифа со значением 0
            tariff[1] = new Tariff()
            {
                ServiceId = 1,
                HouseId = 1,
                PeriodBegin = new DateTime(2021, 2, 1),
                PeriodEnd = new DateTime(2021, 2, 28),
                Value = 0
            };


            ///Расчет
            SubsidyCalculation Subsidy = new SubsidyCalculation();
            Subsidy.OnNotify += dispOnNotify;
            Subsidy.OnException += dispOnException;

            Charge charge;
            charge = Subsidy.CalculateSubsidy(volume[0], tariff[0]);
            Console.WriteLine(charge.Value);
        }
        public static void dispOnNotify(object sender, string message)
        {
            Console.WriteLine(message);
        }
        public static void dispOnException(object sender, Tuple<string, Exception> message)
        {
            Console.WriteLine(message);
        }
    }
}
