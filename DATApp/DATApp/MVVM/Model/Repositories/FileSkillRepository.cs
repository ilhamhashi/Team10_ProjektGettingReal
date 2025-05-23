using DATApp.MVVM.Model.Classes;
using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection;
using Module = DATApp.MVVM.Model.Classes.Module;

namespace DATApp.MVVM.Model.Repositories
{
    public class FileSkillRepository
    {
        private readonly string _skillFilePath;
        public int counter = 1;

        public FileSkillRepository(string filePath)
        {
            _skillFilePath = filePath;

            if (!File.Exists(_skillFilePath))
            {
                File.Create(_skillFilePath).Close();
                //Load default Skills if list is not created
                Module module1 = new Classes.Module { Number = FileModuleRepository.counter, Name = "Færdighed i følelsesregulering", Description = "Forstå og håndter intense følelser på en mere balanceret måde." };
                Module module2 = new Module { Number = counter, Name = "Hold-ud færdigheder", Description = "Håndter akutte følelsesmæssige kriser uden at ty til uhensigtsmæssig adfærd." };
                Skill skill1 = new Skill { Number = counter, Name = "Handle Modsat Følelsen", Purpose = "Ændre følelsesmæssige reaktioner", Description = "Alle følelser har en handling der er forbundet med følelsen", Module = module1 };
                Skill skill2 = new Skill { Number = counter, Name = "STOP!", Purpose = "Overleve krisesituationer uden at gøre dem værre.", Description = "At udholde og komme igennem kriser.", Module = module2 };
                AddSkill(skill1);AddSkill(skill2);
            }
        }

        public void AddSkill(Skill skill)
        {
            skill.Number = counter;
            try
            {
                File.AppendAllText(_skillFilePath, skill.ToString() + Environment.NewLine);
                counter++;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }

        public void DeleteSkill(Skill skill)
        {
            List<Skill> skills = GetAll().ToList();
            skills.RemoveAll(s => s.Number == skill.Number);
            RewriteFile(skills);
        }

        public IEnumerable<Skill> GetAll()
        {
            try
            {
                return File.ReadAllLines(_skillFilePath)
                           .Where(line => !string.IsNullOrEmpty(line)) // Avoid empty lines
                           .Select(Skill.FromString)
                           .ToList();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return new List<Skill>(); // Return an empty list if there is an error
            }
        }

        public Skill GetSkill(int number)
        {
            return GetAll().FirstOrDefault(s => s.Number == number);
        }



        public void UpdateSkill(Skill skill)
        {
            List<Skill> skills = GetAll().ToList();
            int index = skills.FindIndex(s => s.Number == skill.Number);
            if (index != -1)
            {
                skills[index] = skill;
                RewriteFile(skills);
            }
        }

        private void RewriteFile(List<Skill> skills)
        {
            try
            {
                File.WriteAllLines(_skillFilePath, skills.Select(s => s.ToString()));
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }

        }
    }
}
   
