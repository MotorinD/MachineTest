using System;

namespace MachineTest.Cars
{
    internal abstract class Car
    {
        /// <summary>
        /// Тип ТС
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Расход топлива
        /// </summary>
        private double FuelConsumption { get; set; }

        /// <summary>
        /// Объем топливного бака
        /// </summary>
        private double FuelTankMaxVolume { get; set; }

        /// <summary>
        /// Текущее значение топлива
        /// </summary>
        private double CurrentFuel { get; set; }

        /// <summary>
        /// Максимальная скорость
        /// </summary>
        private double MaxSpeed { get; set; }

        public Car(string type, double fuelConsumption, double fuelTankMaxVolume, double maxSpeed, double currentFuel = 0)
        {
            if (fuelConsumption <= 0)
                throw new ArgumentOutOfRangeException(
                    "Расход топлива должен быть больше 0", "fuelConsumption");

            if (fuelTankMaxVolume <= 0)
                throw new ArgumentOutOfRangeException(
                    "Объем топливного бака должен быть больше 0", "fuelTankMaxVolume");

            if (maxSpeed <= 0)
                throw new ArgumentOutOfRangeException(
                    "Максимальная скорость должна быть больше 0", "maxSpeed");


            this.Type = type;
            this.FuelConsumption = fuelConsumption;
            this.FuelTankMaxVolume = fuelTankMaxVolume;
            this.CurrentFuel = currentFuel;
            this.MaxSpeed = maxSpeed;
        }

        /// <summary>
        /// Заправить машину
        /// </summary>
        /// <param name="additionalFuel">Топливо для заправки</param>
        public void AddFuel(double additionalFuel)
        {
            var fuelSum = additionalFuel + this.CurrentFuel;

            if (fuelSum > this.FuelTankMaxVolume)
                this.CurrentFuel = this.FuelTankMaxVolume;
            else
                this.CurrentFuel = fuelSum;
        }

        /// <summary>
        /// Запас хода с полным баком
        /// </summary>
        public double GetDriveDistanceOnMaxFuel()
        {
            return this.GetDriveDistanceOnFuelValue(this.FuelTankMaxVolume);
        }

        /// <summary>
        /// Запас хода с текущим уровнем топлива
        /// </summary>
        public double GetDriveDistanceOnCurrentFuel()
        {
            return this.GetDriveDistanceOnFuelValue(this.CurrentFuel);
        }

        /// <summary>
        /// Запас хода с текущим уровнем топлива
        /// </summary>
        protected virtual double GetDriveDistanceOnFuelValue(double fuel)
        {
            var res = fuel / this.FuelConsumption * 100;
            res = Math.Round(res, 2);

            return res;
        }

        /// <summary>
        /// Информация о запасе хода
        /// </summary>
        public virtual string GetInformation()
        {
            var message =
                $"Тип ТС:{this.Type}\n" +
                $"Текущий уровень топлива:{this.CurrentFuel} л.\n" +
                $"Запас хода:{this.GetDriveDistanceOnCurrentFuel()} км.";

            return message;
        }

        /// <summary>
        /// Время за которое будет пройдена указанная дистанция
        /// </summary>
        /// <param name="fuel">Доступное количество топлива</param>
        /// <param name="distance">Дистанция</param>
        /// <param name="time">Время за которое дистанция будет пройдена</param>
        /// <param name="errorMsg">Сообщение в случае если топлива недостаточно для преодоления дистанции</param>
        /// <returns></returns>
        public bool GetDriveTime(double fuel, double distance, ref TimeSpan time, ref string errorMsg)
        {
            var fuelNeed = this.FuelConsumption * distance / 100;
            if (fuelNeed > fuel)
            {
                errorMsg =
                    $"Недостаточно топлива для преодоления заданной дистанции\n" +
                    $"Необходимо:{fuelNeed} л.\n" +
                    $"Текущий уровень топлива:{this.CurrentFuel} л.";

                return false;
            }

            var timeHours = distance / this.MaxSpeed;
            time = TimeSpan.FromHours(timeHours);

            return true;
        }
    }
}
