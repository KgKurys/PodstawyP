using System;

namespace MeasurementNotesApp.Models
{
    public class Measurement
    {
        public double Resistance { get; set; }
        public double Voltage { get; set; }
        public double Current { get; set; }
        public double CapacitorCapacity { get; set; }
        public double TotalCapacity { get; set; }
        public double TotalResistance { get; set; }

        public Measurement(double resistance, double voltage, double current, double capacitorCapacity, double totalCapacity, double totalResistance)
        {
            Resistance = resistance;
            Voltage = voltage;
            Current = current;
            CapacitorCapacity = capacitorCapacity;
            TotalCapacity = totalCapacity;
            TotalResistance = totalResistance;
        }
    }
}