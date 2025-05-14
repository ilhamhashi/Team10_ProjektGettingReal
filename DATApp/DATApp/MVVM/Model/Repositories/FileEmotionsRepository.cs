using System.IO;
using DATApp.MVVM.Model.Classes;

namespace DATApp.MVVM.Model.Repositories
{
    class FileEmotionsRepository
    {
        private readonly string _emotionsFilePath;

        public FileEmotionsRepository(string filePath)
        {
            _emotionsFilePath = filePath;
            if (!File.Exists(_emotionsFilePath))
            {
                File.Create(_emotionsFilePath).Close();
            }
        }

        public IEnumerable<EmotionalState> GetAllEmotions()
        {
            try
            {
                return File.ReadAllLines(_emotionsFilePath)
                           .Where(line => !string.IsNullOrEmpty(line)) // Undgå tomme linjer
                           .Select(Enum.TryParse("Active", out EmotionalState depressed))
                           .ToList();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl ved læsning af fil: {ex.Message}");
                return [];
            }
        }
    }
}
