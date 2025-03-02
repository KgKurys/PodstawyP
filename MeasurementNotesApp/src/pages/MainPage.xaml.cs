using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using MeasurementNotesApp.Models;
using MeasurementNotesApp.Services;

namespace MeasurementNotesApp.Pages
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Measurement> measurements;
        private MeasurementService measurementService;

        public MainPage()
        {
            InitializeComponent();
            measurementService = new MeasurementService();
            measurements = new ObservableCollection<Measurement>();
            MeasurementTable.ItemsSource = measurements;
        }

        private void OnAddMeasurementClicked(object sender, EventArgs e)
        {
            var newMeasurement = new Measurement
            {
                Resistance = double.Parse(ResistanceEntry.Text),
                Voltage = double.Parse(VoltageEntry.Text),
                Current = double.Parse(CurrentEntry.Text),
                CapacitorCapacity = double.Parse(CapacitorCapacityEntry.Text),
                TotalCapacity = double.Parse(TotalCapacityEntry.Text),
                TotalResistance = double.Parse(TotalResistanceEntry.Text)
            };

            measurements.Add(newMeasurement);
            measurementService.AddMeasurement(newMeasurement);
            ClearEntries();
        }

        private void ClearEntries()
        {
            ResistanceEntry.Text = string.Empty;
            VoltageEntry.Text = string.Empty;
            CurrentEntry.Text = string.Empty;
            CapacitorCapacityEntry.Text = string.Empty;
            TotalCapacityEntry.Text = string.Empty;
            TotalResistanceEntry.Text = string.Empty;
        }

        private void OnExportReportClicked(object sender, EventArgs e)
        {
            var reportService = new ExportService();
            reportService.ExportMeasurements(measurements);
        }
    }
}