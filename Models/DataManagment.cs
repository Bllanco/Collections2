using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CollectionManagementSystem.Models;

namespace CollectionManagementSystem
{
    public static class DataManagement
    {
        private const string FileName = "collections.txt";

        public static List<Collection> LoadCollections()
        {
            List<Collection> collections = new List<Collection>();

            try
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), FileName);
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        // Parsowanie danych z pliku i tworzenie obiektów Collection
                        Collection collection = new Collection(line);
                        collections.Add(collection);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas wczytywania kolekcji: {ex.Message}");
            }

            return collections;
        }

        public static void SaveCollections(List<Collection> collections)
        {
            try
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), FileName);
                List<string> lines = collections.Select(collection => collection.Name).ToList();
                File.WriteAllLines(filePath, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas zapisywania kolekcji: {ex.Message}");
            }
        }
    }
}
