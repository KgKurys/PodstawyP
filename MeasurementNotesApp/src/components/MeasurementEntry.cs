using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MeasurementNotesApp.src.components
{
    public class MeasurementEntry : INotifyPropertyChanged
    {
        private double _resistance;
        private double _voltage;
        private double _current;
        private double _capacitorCapacity;
        private double _totalCapacity;
        private double _totalResistance;
        private string _description;

        public double Resistance
        {
            get => _resistance;
            set
            {
                _resistance = value;
                OnPropertyChanged();
            }
        }

        public double Voltage
        {
            get => _voltage;
            set
            {
                _voltage = value;
                OnPropertyChanged();
            }
        }

        public double Current
        {
            get => _current;
            set
            {
                _current = value;
                OnPropertyChanged();
            }
        }

        public double CapacitorCapacity
        {
            get => _capacitorCapacity;
            set
            {
                _capacitorCapacity = value;
                OnPropertyChanged();
            }
        }

        public double TotalCapacity
        {
            get => _totalCapacity;
            set
            {
                _totalCapacity = value;
                OnPropertyChanged();
            }
        }

        public double TotalResistance
        {
            get => _totalResistance;
            set
            {
                _totalResistance = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}