using Microsoft.Maui.Controls;

namespace MeasurementNotesApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new pages.MainPage());
        }
    }
}