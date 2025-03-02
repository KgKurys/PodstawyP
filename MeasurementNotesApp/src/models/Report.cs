using System;
using System.Collections.Generic;

namespace MeasurementNotesApp.Models
{
    public class Report
    {
        public List<Measurement> Measurements { get; set; }
        public string Description { get; set; }

        public Report()
        {
            Measurements = new List<Measurement>();
        }

        public void AddMeasurement(Measurement measurement)
        {
            Measurements.Add(measurement);
        }

        public string GenerateReport()
        {
            var reportContent = "Measurement Report\n";
            reportContent += "===================\n";
            reportContent += $"Description: {Description}\n\n";

            foreach (var measurement in Measurements)
            {
                reportContent += measurement.ToString() + "\n";
            }

            return reportContent;
        }
    }
}