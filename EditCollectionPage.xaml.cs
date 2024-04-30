using CollectionManagementSystem.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionManagementSystem
{
    public partial class EditCollectionPage : ContentPage
    {
        private Collection collection; // Aktualnie edytowana kolekcja
        private List<Collection> collections; // Lista wszystkich kolekcji

        public EditCollectionPage(Collection selectedCollection, List<Collection> allCollections)
        {
            InitializeComponent();
            collection = selectedCollection; // Przypisanie aktualnie edytowanej kolekcji
            collections = allCollections; // Przypisanie listy wszystkich kolekcji
            ItemsListView.ItemsSource = collection.Items; // Ustawienie �r�d�a danych dla listy
        }

        // Obs�uga dodawania nowego elementu do kolekcji
        private void OnAddItemClicked(object sender, EventArgs e)
        {
            collection.Items.Add("New Item"); // Dodanie nowego elementu do kolekcji
            SaveAndRefresh(); // Zapisanie zmian i od�wie�enie widoku
        }

        private List<string> selectedItems = new List<string>(); // Lista zaznaczonych element�w

        // Obs�uga usuwania zaznaczonych element�w
        private void OnItemDeleted(object sender, EventArgs e)
        {
            selectedItems.ToList().ForEach(item =>
            {
                collection.Items.Remove(item); // Usuni�cie zaznaczonego elementu
                selectedItems.Remove(item); // Usuni�cie zaznaczonego elementu z listy zaznaczonych
            });
            SaveAndRefresh(); // Zapisanie zmian i od�wie�enie widoku
        }

        // Obs�uga edycji zaznaczonego elementu
        private async void OnItemEdited(object sender, EventArgs e)
        {
            if (ItemsListView.SelectedItem != null)
            {
                int index = collection.Items.IndexOf((string)ItemsListView.SelectedItem); // Indeks zaznaczonego elementu
                string newName = await DisplayPromptAsync("Edit Item", "Enter new item name:", "OK", "Cancel", "Edited Item"); // Okno dialogowe do edycji elementu
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    collection.Items[index] = newName; // Aktualizacja nazwy elementu
                    SaveAndRefresh(); // Zapisanie zmian i od�wie�enie widoku
                }
            }
        }

        // Obs�uga zaznaczania element�w
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                string item = (string)e.SelectedItem; // Zaznaczony element
                if (!selectedItems.Contains(item)) selectedItems.Add(item); // Dodanie do listy zaznaczonych
                else selectedItems.Remove(item); // Usuni�cie z listy zaznaczonych
            }
        }

        // Zapisanie zmian i od�wie�enie widoku
        private void SaveAndRefresh()
        {
            DataManagement.SaveCollections(collections); // Zapisanie kolekcji
            ItemsListView.ItemsSource = null; // Usuni�cie �r�d�a danych dla listy
            ItemsListView.ItemsSource = collection.Items; // Ustawienie �r�d�a danych dla listy
        }
    }
}
