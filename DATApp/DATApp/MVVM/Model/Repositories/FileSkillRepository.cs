using System.IO;
using DATApp.MVVM.Model.Classes;

namespace DATApp.MVVM.Model.Repositories
{
    public class FileSkillRepository
    {
        private readonly string _skillFilePath;

        public FileSkillRepository(string filePath)
        {
            _skillFilePath = filePath;

            if (!File.Exists(_skillFilePath))
            {
                File.Create(_skillFilePath).Close();
            }
        }

        public void AddSkill(Skill skill)
        {
            skill.Number = Guid.NewGuid().ToString();
            try
            {
                File.AppendAllText(_skillFilePath, skill.ToString() + Environment.NewLine);
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

        public Skill GetSkill(string number)
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
   
