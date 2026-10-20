using System;

namespace VehicleFleetApp
{
    public interface IStorable
    {
        string FilePath { get; }
        void Save(string path);
    }
}