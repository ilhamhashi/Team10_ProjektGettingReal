
using System.IO;
using DATApp.MVVM.Model.Classes;

namespace DATApp.MVVM.Model.Repositories
{
    public class FileModuleRepository : IModuleRepository
    {
        private readonly string _moduleFilePath;

        // Konstruktor for at initialisere filstien og oprette filen, hvis den ikke findes
        public FileModuleRepository(string filePath)
        {
            _moduleFilePath = filePath;
            if (!File.Exists(_moduleFilePath))
            {
                // Hvis filen ikke findes, oprettes den
                File.Create(_moduleFilePath).Close();
            }
        }

        // Tilføj et modul til filen
        public void AddModule(Module module)
        {
            try
            {
                // Tilføj modulet til filen
                File.AppendAllText(_moduleFilePath, module.ToString() + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }

        // Slet et modul ud fra moduleNumber
        public void DeleteModule(int moduleNumber)
        {
            List<Module> modules = GetAllModules().ToList();
            modules.RemoveAll(s => s.ModuleNumber == moduleNumber);
            RewriteFile(modules);
        }

        // Hent alle moduler fra filen
        public IEnumerable<Module> GetAllModules()
        {
            try
            {
                // Læs alle linjer fra filen, konverter dem til moduler og returner som en liste
                return File.ReadAllLines(_moduleFilePath)
                           .Where(line => !string.IsNullOrEmpty(line)) // Undgå tomme linjer
                           .Select(Module.FromString) // Konverter hver linje til et modul
                           .ToList();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl ved læsning af fil: {ex.Message}");
                return new List<Module>(); // Returnerer en tom liste, hvis der opstår fejl
            }
        }

        // Hent et enkelt modul baseret på moduleNumber
        public Module GetModule(int moduleNumber)
        {
            return GetAllModules().FirstOrDefault(m => m.ModuleNumber == moduleNumber);
        }

        // Opdater et eksisterende modul
        public void UpdateModule(Module module)
        {
            List<Module> modules = GetAllModules().ToList();
            int index = modules.FindIndex(m => m.ModuleNumber == module.ModuleNumber);
            if (index != -1)
            {
                modules[index] = module;
                RewriteFile(modules); // Skriv de opdaterede moduler tilbage til filen
            }
        }

        // Hjælpefunktion til at skrive opdaterede moduler tilbage til filen
        private void RewriteFile(List<Module> modules)
        {
            try
            {
                // Skriv alle modulerne tilbage til filen
                File.WriteAllLines(_moduleFilePath, modules.Select(m => m.ToString()));
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }

        // Ny funktion til at finde moduler baseret på følelser
        public IEnumerable<Module> FindModulesByFeeling(string feeling)
        {
            var modules = GetAllModules(); // Hent alle moduler én gang

            // Filtrer moduler, hvor følelserne indeholder den ønskede følelse
            return modules
                .Where(m => m.Feelings
                    .Any(f => f.Contains(feeling, StringComparison.OrdinalIgnoreCase))) 
                .ToList();
        }
    }
}
