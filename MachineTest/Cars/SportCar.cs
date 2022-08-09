namespace MachineTest.Cars
{
    /// <summary>
    /// Спортивная машина
    /// </summary>
    internal class SportCar : Car
    {
        /// <summary>
        /// Спортивная машина
        /// </summary>
        /// <param name="type">Тип ТС</param>
        /// <param name="fuelConsumption">Расход топлива</param>
        /// <param name="fuelTankMaxVolume">Объем бака</param>
        /// <param name="maxSpeed">Максимальная скорость</param>
        /// <param name="currentFuel">Текущее значение топлива</param>
        public SportCar(string type, double fuelConsumption, double fuelTankMaxVolume, double maxSpeed, double currentFuel = 0) 
            : base(type, fuelConsumption, fuelTankMaxVolume, maxSpeed, currentFuel)
        {
        }
    }
}
