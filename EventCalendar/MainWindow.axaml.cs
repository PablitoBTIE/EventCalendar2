using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System.Linq;

namespace EventCalendar
{
    public partial class MainWindow : Window
    {
        private List<Event> allEvents = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnAddEventClick(object sender, RoutedEventArgs e)
        {
            var eventName = EventNameTextBox.Text;
            var dayOfWeek = (DayOfWeekComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            var isImportant = ImportantCheckBox.IsChecked ?? false;

            var newEvent = new Event
            {
                EventDetails = $"{eventName} - {dayOfWeek} - {(isImportant ? "Ważne" : "Nieważne")}",
                IsImportant = isImportant
            };

            allEvents.Add(newEvent);
            EventsListBox.Items = allEvents.ToList(); // Aktualizacja listy wyświetlanej
        }

        private void OnViewDetailsClick(object sender, RoutedEventArgs e)
        {
            if (EventsListBox.SelectedItem is Event selectedEvent)
            {
                var messageBox = new Window
                {
                    Title = "Szczegóły Wydarzenia",
                    Content = new TextBlock { Text = selectedEvent.EventDetails, Margin = new Avalonia.Thickness(10) },
                    Width = 300,
                    Height = 150
                };

                messageBox.ShowDialog(this);
            }
        }

        private void OnFilterChanged(object sender, SelectionChangedEventArgs e)
        {
            var filter = (FilterComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            var filteredEvents = filter switch
            {
                "Ważne" => allEvents.Where(ev => ev.IsImportant).ToList(),
                "Nieważne" => allEvents.Where(ev => !ev.IsImportant).ToList(),
                _ => allEvents
            };

            EventsListBox.Items = filteredEvents; // Filtruj elementy wyświetlane w ListBox
        }
    }

    public class Event
    {
        public string EventDetails { get; set; }
        public bool IsImportant { get; set; }

        public override string ToString() => EventDetails; // Dla wyświetlania w ListBox
    }
}
