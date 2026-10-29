using System;
public class VehicleEventArgs : EventArgs
{
    public string Message { get; }
    public decimal OperationPrice { get; }
    public VehicleEventArgs(string message, decimal operationPrice)
    {
        Message = message;
        OperationPrice = operationPrice;
    }
}
public class FleetManager
{
    public event EventHandler<VehicleEventArgs> StatusChanged;
    public void PerformOperation(string vehicleName, decimal price)
    {
        EventHandler<VehicleEventArgs> handler = StatusChanged;
        if (handler != null)
        {
            handler(this, new VehicleEventArgs("Операция с " + vehicleName, price));
        }
    }
}