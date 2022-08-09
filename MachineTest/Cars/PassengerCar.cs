using System;

namespace MachineTest.Cars
{
    /// <summary>
    /// Легковой автомобиль
    /// </summary>
    internal class PassengerCar : Car
    {
        /// <summary>
        /// Уменьшение запаса хода за одного пассажира в процентах
        /// </summary>
        private static readonly double _reduceDriveDistancePerPassenger = 6;

        /// <summary>
        /// Итоговое уменьшение запаса хода из-за пассажиров в процентах
        /// </summary>
        private double ReduceDriveDistancePercent
        {
            get
            {
                var res = _reduceDriveDistancePerPassenger * this.PassengerCount;
                if (res > 100)
                    res = 100;

                return Math.Round(res, 2);
            }
        }

        /// <summary>
        /// Максимальное кол-во пассажиров
        /// </summary>
        public int MaxPassengerCount { get; private set; }

        /// <summary>
        /// Кол-во пассажиров
        /// </summary>
        public int PassengerCount { get; private set; }

        /// <summary>
        /// Легковой автомобиль
        /// </summary>
        /// <param name="type">Тип ТС</param>
        /// <param name="fuelConsumption">Расход топлива</param>
        /// <param name="fuelTankMaxVolume">Объем бака</param>
        /// <param name="maxSpeed">Максимальная скорость</param>
        /// <param name="maxPassengerCount">Максимальное кол-во пассажиров</param>
        /// <param name="currentFuel">Текущее значение топлива</param>
        /// <param name="passengerCount">Кол-во пассажиров</param>
        /// <exception cref="ArgumentOutOfRangeException">Некорректное значение входного параметра</exception>

        public PassengerCar(string type, double fuelConsumption, double fuelTankMaxVolume, double maxSpeed, int maxPassengerCount, double currentFuel = 0, int passengerCount = 0)
            : base(type, fuelConsumption, fuelTankMaxVolume, maxSpeed, currentFuel)
        {
            if (!this.CheckPassengerRange(passengerCount, maxPassengerCount))
                throw new ArgumentOutOfRangeException($"Недопустимое значение пассажиров," +
                    $"должно быть не меньше 0 и не больше maxPassengerCount:{maxPassengerCount}");

            this.PassengerCount = passengerCount;
            this.MaxPassengerCount = passengerCount;
        }

        /// <summary>
        /// Изменить кол-во пассажиров
        /// </summary>
        public void TryChangePassengerCount(int count)
        {
            if (!this.CheckPassengerRange(count, this.MaxPassengerCount))
                return;

            this.PassengerCount = count;
        }

        protected override double GetDriveDistanceOnFuelValue(double fuel)
        {
            var distance = base.GetDriveDistanceOnFuelValue(fuel);
            var res = distance * (1 - this.ReduceDriveDistancePercent / 100);

            if (res < 0)
                res = 0;

            res = Math.Round(res, 2);
            return res;
        }

        public override string GetInformation()
        {
            var info = base.GetInformation();

            info += $"\nКоличество пассажиров:{this.PassengerCount}\n" +
                $"Запас хода снижен на {this.ReduceDriveDistancePercent}%";

            return info;
        }

        private bool CheckPassengerRange(int passengerCount, int maxPassengerCount)
        {
            var res = passengerCount >= 0 && passengerCount <= maxPassengerCount;
            return res;
        }
    }
}
