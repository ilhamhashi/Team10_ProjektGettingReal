using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DATApp.MVVM.Model.Classes;

namespace DATApp.MVVM.Model.Repositories
{
    public class FileEmotionsRepository
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
                           .Where(line => !string.IsNullOrWhiteSpace(line))
                           .Select(line =>
                           {
                               if (Enum.TryParse<EmotionalState>(line, true, out var emotion))
                                   return emotion;
                               else
                                   return (EmotionalState?)null;
                           })
                           .Where(e => e.HasValue)
                           .Select(e => e.Value)
                           .ToList();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl ved læsning af fil: {ex.Message}");
                return new List<EmotionalState>();
            }
        }
    }
}
