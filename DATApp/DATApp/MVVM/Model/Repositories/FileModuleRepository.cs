
using System.Configuration;
using System.IO;
using System.Xml.Linq;
using DATApp.MVVM.Model.Classes;

namespace DATApp.MVVM.Model.Repositories
{
    public class FileModuleRepository : IModuleRepository
    {
        private readonly string _moduleFilePath;
        public FileModuleRepository(string filePath)
        {
            _moduleFilePath = filePath;
            if (!File.Exists(_moduleFilePath))
            {
                File.Create(_moduleFilePath).Close();
            }
        }

        public void AddModule(Module module)
        {
            try
            {
                File.AppendAllText(_moduleFilePath, module.ToString() + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }

        public void DeleteModule(Module module)
        {
            List<Module> modules = GetAllModules().ToList();
            modules.RemoveAll(m => m.ModuleNumber == module.ModuleNumber);
            RewriteFile(modules);
        }

        public IEnumerable<Module> GetAllModules()
        {
            try
            {
                return File.ReadAllLines(_moduleFilePath)
                           .Where(line => !string.IsNullOrEmpty(line)) // Undgå tomme linjer
                           .Select(Module.FromString)
                           .ToList();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl ved læsning af fil: {ex.Message}");
                return [];
            }
        }

        public Module GetModule(int moduleNumber)
        {
            return GetAllModules().FirstOrDefault(m => m.ModuleNumber == moduleNumber);
        }

        public void UpdateModule(Module module)
        {
            List<Module> modules = GetAllModules().ToList();
            int index = modules.FindIndex(m => m.ModuleNumber == module.ModuleNumber);
            if (index != -1)
            {
                modules[index] = module;
                RewriteFile(modules);
            }
        }

        public List<Module> SearchModule(string searchTerm = null)
        {
            var result = GetAllModules().ToList().AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                result = result.Where(s => s.ModuleNumber.Equals(int.Parse(searchTerm)));
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                result = result.Where(s => s.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                result = result.Where(s => s.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            return result.ToList();
        }

        private void RewriteFile(List<Module> modules)
        {
            try
            {
                File.WriteAllLines(_moduleFilePath, modules.Select(m => m.ToString()));
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }
    }
}
