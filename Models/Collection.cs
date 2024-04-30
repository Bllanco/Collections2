using System.Collections.Generic;

namespace CollectionManagementSystem.Models
{
    public class Collection
    {
        public string Name { get; set; }
        public List<string> Items { get; set; }

        public Collection(string name)
        {
            Name = name;
            Items = new List<string>();
        }
    }
}
