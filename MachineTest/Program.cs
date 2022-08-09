using MachineTest.Cars;
using System;
using System.Collections.Generic;

namespace MachineTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var carList = new List<Car>
            {
                new PassengerCar("Жигули",9,70,80,5,88,4),
                new SportCar("SportX",25,100,200,100),
                new TruckCar("Kamaz1", 35, 400, 70, 800, 350, 600),
                new TruckCar("Kamaz2", 35, 400, 70, 800, 350),
                new TruckCar("Kamaz3", 35, 400, 70, 800)
            };

            foreach (var car in carList)
                Console.WriteLine(car.GetInformation());

            Console.ReadLine();
        }
    }
}
