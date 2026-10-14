using System;
using System.IO;

namespace VehicleFleetApp
{
    
    public class Truck : Vehicle, IStorable
    {
   
        public double LoadCapacity { get; private set; }

       
        public string FilePath => "truck_log.txt";

      
        public Truck(string model, int power, double loadCapacity) : base(model, power)
        {
            LoadCapacity = loadCapacity;
        }

        
        public override string GetInfo()
        {
            return base.GetInfo() + $", Грузоподъемность: {LoadCapacity} т";
        }

     
        public override double CalculateTax()
        {
            return Power * 25.0;
        }

        
        public void Save(string path)
        {
            string data = $"[Грузовик] {GetInfo()}, Налог: {CalculateTax()} руб.\n";
            File.AppendAllText(path, data);
        }
    }
}