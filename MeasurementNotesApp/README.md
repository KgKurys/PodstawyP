# Measurement Notes App

## Overview
The Measurement Notes App is designed to help users record and manage various electrical measurements, including resistance, voltage, current, capacitor capacity, total capacity, and total resistance. Users can input their measurements, add descriptions, and export their data to a text file for reporting purposes.

## Features
- Input fields for various measurement types.
- Dynamic addition of new measurement entries.
- Tabular display of entered measurements.
- Text area for writing descriptions related to measurements.
- Export functionality to save measurements and reports as a text file.

## Project Structure
```
MeasurementNotesApp
├── src
│   ├── components
│   │   ├── Header.cs
│   │   ├── MeasurementEntry.cs
│   │   ├── MeasurementTable.cs
│   │   └── NoteEditor.cs
│   ├── models
│   │   ├── Measurement.cs
│   │   └── Report.cs
│   ├── pages
│   │   ├── MainPage.xaml
│   │   ├── MainPage.xaml.cs
│   │   ├── ReportPage.xaml
│   │   └── ReportPage.xaml.cs
│   ├── services
│   │   ├── ExportService.cs
│   │   └── MeasurementService.cs
│   └── App.xaml.cs
├── Resources
│   └── Styles
│       └── AppStyles.xaml
├── MeasurementNotesApp.csproj
└── README.md
```

## Setup Instructions
1. Clone the repository to your local machine.
2. Open the project in your preferred IDE.
3. Restore the project dependencies.
4. Build and run the application.

## Usage Guidelines
- Navigate to the main page to enter measurements.
- Fill in the required fields for each measurement type.
- Add descriptions in the provided text area.
- Click on the export button to save your measurements and notes to a text file.

## Contributing
Contributions are welcome! Please feel free to submit a pull request or open an issue for any enhancements or bug fixes.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.