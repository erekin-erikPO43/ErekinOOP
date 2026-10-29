using System;
public class Vehicle : IComparable<Vehicle>, ICloneable
{
    public int Id { get; set; }
    public string Model { get; set; }
    public decimal BasePrice { get; set; }
    public int Power { get; set; }
    public int CategoryId { get; set; }
    public Vehicle(int id, string model, decimal basePrice, int power, int categoryId)
    {
        Id = id;
        Model = model;
        BasePrice = basePrice;
        Power = power;
        CategoryId = categoryId;
    }

    public override string ToString() => string.Format("Vehicle: {0} (Power: {1}, Price: {2})", Model, Power, BasePrice);

    public override bool Equals(object obj)
    {
        Vehicle other = obj as Vehicle;
        if (other != null)
        {
            return Model == other.Model && Power == other.Power;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return (Model != null ? Model.GetHashCode() : 0) ^ Power.GetHashCode();
    }

    public virtual decimal CalculateTax(decimal taxRate) => BasePrice * taxRate / 100;

    public int CompareTo(Vehicle other)
    {
        if (other == null) return 1;
        return BasePrice.CompareTo(other.BasePrice);
    }

    public virtual object Clone()
    {
        return new Vehicle(Id, Model, BasePrice, Power, CategoryId);
    }
}
public class CargoVehicle : Vehicle, IExportable
{
    public double MaxLoadTon { get; set; }
    public CargoVehicle(int id, string model, decimal basePrice, int power, int categoryId, double maxLoadTon)
        : base(id, model, basePrice, power, categoryId)
    {
        MaxLoadTon = maxLoadTon;
    }

    public override decimal CalculateTax(decimal taxRate) => base.CalculateTax(taxRate) + (decimal)(MaxLoadTon * 10);

    string IExportable.FilePath => "exports/cargo_" + Id + ".txt";
    string IExportable.GetExportInfo() => string.Format("Cargo: {0}, Load: {1}t", Model, MaxLoadTon);

    public override object Clone()
    {
        return new CargoVehicle(Id, Model, BasePrice, Power, CategoryId, MaxLoadTon);
    }
}
public class PassengerVehicle : Vehicle
{
    public PassengerVehicle(int id, string model, decimal basePrice, int power, int categoryId) : base(id, model, basePrice, power, categoryId) { }
    public override object Clone()
    {
        return new PassengerVehicle(Id, Model, BasePrice, Power, CategoryId);
    }
}