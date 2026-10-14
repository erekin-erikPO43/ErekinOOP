using System;

namespace VehicleFleetApp
{
    public class Car: Vehicle
    {
        public bool IsElectric { get; private set; }
        public Car(string model, int power, bool isElectric) : base(model, power)
        {
            IsElectric = isElectric;
        }
        public override string GetInfo()
        {
            string typeInfo = IsElectric ? "Электромобиль" : "ДВС";
            return base.GetInfo()+$", Тип: {typeInfo}";
        }
        public override double CalculateTax()
        {
            double rate = IsElectric ? 5.0 : 12.0;
            return Power * rate;
        }
    }
}