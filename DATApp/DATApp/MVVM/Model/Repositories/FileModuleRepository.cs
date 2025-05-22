
using System.IO;
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
            module.Number = Guid.NewGuid().ToString();

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
            List<Module> modules = GetAll().ToList();
            modules.RemoveAll(m => m.Number == module.Number);
            RewriteFile(modules);
        }

        public IEnumerable<Module> GetAll()
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

        public Module GetModule(string number)
        {
            return GetAll().FirstOrDefault(m => m.Number == number);
        }

        public void UpdateModule(Module module)
        {
            List<Module> modules = GetAll().ToList();
            int index = modules.FindIndex(m => m.Number == module.Number);
            if (index != -1)
            {
                modules[index] = module;
                RewriteFile(modules);
            }
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
