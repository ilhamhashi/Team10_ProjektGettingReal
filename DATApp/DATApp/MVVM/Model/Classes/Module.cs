using System;
using System.Reflection;

namespace DATApp.MVVM.Model.Classes
{
    public class Module
    {
        public int ModuleNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Feelings { get; set; } = new(); // Ny: Liste af følelser

        public override string ToString()
        {
            return $"{ModuleNumber},{Name},{Description},{string.Join(";", Feelings)}";
        }

        public static Module FromString(string input)
        {
            string[] parts = input.Split(','); // Opdeler strengen baseret på kommategn
            return new Module
            {
                ModuleNumber = int.Parse(parts[0]),
                Name = parts[1],
                Description = parts[2],
                Feelings = parts.Length > 3 ? parts[3].Split(';').ToList() : new List<string>()
            };
        }
    }
}
