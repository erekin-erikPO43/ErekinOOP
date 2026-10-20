using System;
namespace VehicleFleetApp
{
    public abstract class Vehicle
    {
        public string Model { get; private set; }
        public int Power { get; private set; }
        public Vehicle(string model, int power)
        {
            Model = model;
            if (power<=0)
            {
                throw new ArgumentException("Мощность должна быть положительным числом.", nameof(power));
            }
            Power = power;
        }
        public virtual string GetInfo()
        {
            return $"Модель: {Model},  Мощность: {Power} л.с.";
        }
        public abstract double CalculateTax();
    }
}