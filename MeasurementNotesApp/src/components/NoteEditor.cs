using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace MeasurementNotesApp.src.components
{
    public class NoteEditor : ContentView
    {
        private Entry descriptionEntry;
        private Button addButton;
        private StackLayout notesLayout;

        public NoteEditor()
        {
            descriptionEntry = new Entry
            {
                Placeholder = "Wpisz opis..."
            };

            addButton = new Button
            {
                Text = "Dodaj Notatkę"
            };
            addButton.Clicked += OnAddButtonClicked;

            notesLayout = new StackLayout();

            Content = new StackLayout
            {
                Children = { descriptionEntry, addButton, notesLayout }
            };
        }

        private void OnAddButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(descriptionEntry.Text))
            {
                var noteLabel = new Label
                {
                    Text = descriptionEntry.Text,
                    Margin = new Thickness(0, 5)
                };
                notesLayout.Children.Add(noteLabel);
                descriptionEntry.Text = string.Empty;
            }
        }
    }
}