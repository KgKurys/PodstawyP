using System;
using Microsoft.Maui.Controls;

namespace MeasurementNotesApp.src.components
{
    public class Header : ContentView
    {
        public Header()
        {
            var titleLabel = new Label
            {
                Text = "Measurement Notes",
                FontSize = 24,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            var navigationStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Children =
                {
                    new Button
                    {
                        Text = "Home",
                        Command = new Command(() => NavigateToHome())
                    },
                    new Button
                    {
                        Text = "Reports",
                        Command = new Command(() => NavigateToReports())
                    }
                }
            };

            var layout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { titleLabel, navigationStack }
            };

            Content = layout;
        }

        private void NavigateToHome()
        {
            // Logic to navigate to the home page
        }

        private void NavigateToReports()
        {
            // Logic to navigate to the reports page
        }
    }
}