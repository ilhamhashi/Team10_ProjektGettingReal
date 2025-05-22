using DATApp.MVVM.Model.Classes;
using System.IO;

namespace DATApp.MVVM.Model.Repositories
{
    public class FileMatchRepository : IMatchRepository
    {
        private readonly string _matchFilePath;

        public FileMatchRepository(string filePath)
        {
            _matchFilePath = filePath;

            if (!File.Exists(_matchFilePath))
            {
                File.Create(_matchFilePath).Close();
            }
        }

        public void AddMatch(Match match)
        {
            match.Number = Guid.NewGuid().ToString();
            try
            {
                File.AppendAllText(_matchFilePath, match.ToString() + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }

        public void DeleteMatch(Match match)
        {
            List<Match> matches = GetAll().ToList();
            matches.RemoveAll(m => m.Number == match.Number);
            RewriteFile(matches);
        }

        public IEnumerable<Match> GetAll()
        {
            try
            {
                return File.ReadAllLines(_matchFilePath)
                           .Where(line => !string.IsNullOrEmpty(line)) // Avoid empty lines
                           .Select(Match.FromString)
                           .ToList();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return new List<Match>(); // Return an empty list if there is an error
            }
        }

        public Match GetMatch(string number)
        {
            return GetAll().FirstOrDefault(m => m.Number == number);
        }



        public void UpdateMatch(Match match)
        {
            List<Match> matches = GetAll().ToList();
            int index = matches.FindIndex(m => m.Number == match.Number);
            if (index != -1)
            {
                matches[index] = match;
                RewriteFile(matches);
            }
        }

        private void RewriteFile(List<Match> matches)
        {
            try
            {
                File.WriteAllLines(_matchFilePath, matches.Select(m => m.ToString()));
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }

        }
    }
}

