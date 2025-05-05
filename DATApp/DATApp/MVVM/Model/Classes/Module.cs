using System;
using System.Reflection;

namespace DATApp.MVVM.Model.Classes
{
    public class Module
    {
        public int ModuleNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{ModuleNumber},{Name},{Description}";
        }

        public static Module FromString(string input)
        {
            string[] parts = input.Split(','); // Opdeler strengen baseret på kommategn
            return new Module
            {
                ModuleNumber = int.Parse(parts[0]),
                Name = parts[1],
                Description = parts[2],
            };
        }
    }
}
