using ClassLibrary1;
using System;

namespace SubsidyCalculation
{
    ///<summary>
    ///Класс расчета субсидии
    ///</summary>
    class SubsidyCalculation : ISubsidyCalculation
    {
        public event EventHandler<string> OnNotify;
        public event EventHandler<Tuple<string, Exception>> OnException;
        /// <summary>
        /// Вычислении субсидии на дом по указанному объёму и указанному тарифу
        /// </summary>
        /// <param name="volumes"></param>
        /// <param name="tariff"></param>
        /// <returns>charge</returns>
        public Charge CalculateSubsidy(Volume volumes, Tariff tariff)
        {
            /// Начало проверок соответствия условиями
            OnNotify?.Invoke(this,"Расчет начат в " + DateTime.Now.ToString("HH:mm:ss dd.MM.yyyy"));

            if (volumes.ServiceId != tariff.ServiceId) NoCorrect("Не совпадают идентификаторы Услуг!");

            if (volumes.HouseId != tariff.HouseId) NoCorrect("Не совпадают идентификаторы Домов!");

            if (volumes.Month.Month < tariff.PeriodBegin.Month || tariff.PeriodEnd.Month < volumes.Month.Month)
                NoCorrect("Период действия тарифа не распространяется на месяц начисления объема!");

            if (tariff.Value <= 0) NoCorrect("Не допускается использование нулевых или отрицательных значений тарифа!");

            if (volumes.Value < 0) NoCorrect("Значение объема не может быть отрицательным!");
            /// Конец проверок соответствия условиями

            /// Нахождение значения начисления
            Charge charge = new Charge();
            try
            {
                charge.ServiceId = volumes.ServiceId;
                charge.HouseId = volumes.HouseId;
                charge.Month = volumes.Month;
                charge.Value = volumes.Value * tariff.Value;
            }
            catch (Exception e)
            {
                NoCorrect("Произошла ошибка при нахождении значения начисления", e);
            }

            OnNotify?.Invoke(this, "Расчет успешно закончен в " + DateTime.Now.ToString("HH:mm:ss dd.MM.yyyy"));
            return charge;
        }
        ///<summary>
        ///Метод, вызываемый при вводе некорректных данных и возникновении ошибки
        ///</summary>
        private void NoCorrect(string message, Exception e = null)
        {
            if (e == null) { e = new Exception(message); }
            OnException?.Invoke(this, Tuple.Create(message, e));
            throw e;
        }
    }
}