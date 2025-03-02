using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace MeasurementNotesApp.src.components
{
    public partial class MeasurementTable : ComponentBase
    {
        private List<Measurement> measurements = new List<Measurement>();
        private Measurement newMeasurement = new Measurement();

        private void AddMeasurement()
        {
            if (newMeasurement.IsValid())
            {
                measurements.Add(newMeasurement);
                newMeasurement = new Measurement(); // Reset for new entry
            }
        }

        private void ExportMeasurements()
        {
            // Logic to export measurements to a text file
        }
    }
}