using System;

namespace MachineTest.Cars
{
    /// <summary>
    /// Грузовой автомобиль
    /// </summary>
    internal class TruckCar : Car
    {
        /// <summary>
        /// Снижение запаса хода за каждые 200кг груза в процентах
        /// </summary>
        private static readonly double _reduceDriveDistancePer200Cargo = 4;

        /// <summary>
        /// Итоговое уменьшение запаса хода из-за груза в процентах
        /// </summary>
        private double ReduceDriveDistancePercent
        {
            get
            {
                var res = this.Cargo / 200 * _reduceDriveDistancePer200Cargo;
                if (res > 100)
                    res = 100;

                return Math.Round(res, 2);
            }
        }

        /// <summary>
        /// Максимальная масса груза
        /// </summary>
        public int MaxAllowCargo { get; private set; }

        /// <summary>
        /// Масса груза
        /// </summary>
        public int Cargo { get; private set; }

        /// <summary>
        /// Грузовой автомобиль
        /// </summary>
        /// <param name="type">Тип ТС</param>
        /// <param name="fuelConsumption">Расход топлива</param>
        /// <param name="fuelTankMaxVolume">Объем бака</param>
        /// <param name="maxSpeed">Максимальная скорость</param>
        /// <param name="maxAllowCargo">Максимальная масса груза</param>
        /// <param name="currentFuel">Текущее значение топлива</param>
        /// <param name="cargo">Масса груза</param>
        /// <exception cref="ArgumentOutOfRangeException">Некорректное значение входного параметра</exception>

        public TruckCar(string type, double fuelConsumption, double fuelTankMaxVolume, double maxSpeed, int maxAllowCargo, double currentFuel = 0, int cargo = 0)
            : base(type, fuelConsumption, fuelTankMaxVolume, maxSpeed, currentFuel)
        {
            if (!this.CheckCargoRange(cargo, maxAllowCargo))
                throw new ArgumentOutOfRangeException($"Недопустимое значение груза," +
                    $"должно быть не меньше 0 и не больше maxAllowCargo:{maxAllowCargo}");

            this.Cargo = cargo;
            this.MaxAllowCargo = maxAllowCargo;
        }

        /// <summary>
        /// Изменить массу груза
        /// </summary>
        /// <param name="cargo"></param>
        public void TryChangeCargo(int cargo)
        {
            if (!this.CheckCargoRange(cargo, this.MaxAllowCargo))
                return;

            this.Cargo = cargo;
        }

        protected override double GetDriveDistanceOnFuelValue(double fuel)
        {
            var distance = base.GetDriveDistanceOnFuelValue(fuel);
            var res = distance * (1 - this.ReduceDriveDistancePercent / 100);

            if (res < 0)
                res = 0;

            res = Math.Round(res, 2);
            return res; ;
        }

        public override string GetInformation()
        {
            var info = base.GetInformation();

            info += $"\nМасса груза:{this.Cargo} кг\n" +
                $"Запас хода снижен на {this.ReduceDriveDistancePercent}%";

            return info;
        }

        private bool CheckCargoRange(int cargo, int maxCargo)
        {
            var res = cargo >= 0 && cargo <= maxCargo;
            return res;
        }
    }
}
