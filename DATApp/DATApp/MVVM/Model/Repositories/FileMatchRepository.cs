using DATApp.MVVM.Model.Classes;
using System.IO;

namespace DATApp.MVVM.Model.Repositories
{
    public class FileMatchRepository : IMatchRepository
    {
        private readonly string _matchFilePath;
        int counter = 1;

        public FileMatchRepository(string filePath)
        {
            _matchFilePath = filePath;

            if (!File.Exists(_matchFilePath))
            {
                File.Create(_matchFilePath).Close();
                //Load default Matches if list is not created
                Module module1 = new Classes.Module { Number = FileModuleRepository.counter, Name = "Færdighed i følelsesregulering", Description = "Forstå og håndter intense følelser på en mere balanceret måde." };
                Module module2 = new Module { Number = counter, Name = "Hold-ud færdigheder", Description = "Håndter akutte følelsesmæssige kriser uden at ty til uhensigtsmæssig adfærd." };
                Skill skill1 = new Skill { Number = counter, Name = "Handle Modsat Følelsen", Purpose = "Ændre følelsesmæssige reaktioner", Description = "Alle følelser har en handling der er forbundet med følelsen", Module = module1 };
                Skill skill2 = new Skill { Number = counter, Name = "STOP!", Purpose = "Overleve krisesituationer uden at gøre dem værre.", Description = "At udholde og komme igennem kriser.", Module = module2 };
                var match1 = new Model.Classes.Match { Number = counter, Skill = skill1, Emotion = "Angst", Level = "Gul" };
                var match2 = new Model.Classes.Match { Number = counter, Skill = skill2, Emotion = "Vrede", Level = "Rød" };
                AddMatch(match1);AddMatch(match2);
                
            }
        }

        public void AddMatch(Match match)
        {
            match.Number = counter;
            try
            {
                File.AppendAllText(_matchFilePath, match.ToString() + Environment.NewLine);
                counter++;
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

        public Match GetMatch(int number)
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

