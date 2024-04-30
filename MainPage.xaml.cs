using CollectionManagementSystem.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace CollectionManagementSystem
{
    public partial class MainPage : ContentPage
    {
        public List<Collection> Collections { get; set; } = new List<Collection>(); // Deklaracja listy kolekcji

        public MainPage()
        {
            InitializeComponent();
            Collections = DataManagement.LoadCollections(); // Załadowanie kolekcji
            CollectionsListView.ItemsSource = Collections; // Ustawienie źródła danych dla listy
        }

        private async void OnAddCollectionClicked(object sender, EventArgs e)
        {
            string newCollectionName = await DisplayPromptAsync("Twoja nowa kolekcja", "Nazwa kolekcji to:", "OK", "Cancel", "Nowa kolekcja"); // Wyświetlenie okna dialogowego do wprowadzenia nazwy kolekcji
            if (!string.IsNullOrWhiteSpace(newCollectionName))
            {
                Collection newCollection = new Collection(newCollectionName); // Utworzenie nowej kolekcji
                Collections.Add(newCollection); // Dodanie nowej kolekcji
                DataManagement.SaveCollections(Collections); // Zapisanie kolekcji
                RefreshCollectionsList(); // Odświeżenie listy kolekcji
            }
        }

        private void OnCollectionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            Collection selectedCollection = (Collection)e.SelectedItem; // Pobranie wybranej kolekcji
            Navigation.PushAsync(new EditCollectionPage(selectedCollection, Collections)); // Przekierowanie do strony edycji kolekcji
        }

        private void OnDeleteCollectionClicked(object sender, EventArgs e)
        {
            if (CollectionsListView.SelectedItem != null)
            {
                Collection selectedCollection = (Collection)CollectionsListView.SelectedItem; // Pobranie wybranej kolekcji do usunięcia
                Collections.Remove(selectedCollection); // Usunięcie wybranej kolekcji
                DataManagement.SaveCollections(Collections); // Zapisanie zmienionej listy kolekcji
                RefreshCollectionsList(); // Odświeżenie widoku listy kolekcji
            }
        }

        private void RefreshCollectionsList()
        {
            CollectionsListView.ItemsSource = null; // Usunięcie źródła danych dla listy
            CollectionsListView.ItemsSource = Collections; // Ustawienie źródła danych dla listy
        }
    }
}
