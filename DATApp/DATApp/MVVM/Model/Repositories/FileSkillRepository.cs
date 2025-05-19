using System.IO;
using System.Runtime.CompilerServices;
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
            if (GetAllSkills().ToList().Count == 0)
            {
                skill.Number = 1;
            }
            else
            {
                skill.Number = GetAllSkills().Max(s => s.Number) + 1;
            }
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
            List<Skill> skills = GetAllSkills().ToList();
            skills.RemoveAll(s => s.Number == skill.Number);
            RewriteFile(skills);
        }

        public IEnumerable<Skill> GetAllSkills()
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

        public Skill GetSkill(int skillNumber)
        {
            return GetAllSkills().FirstOrDefault(s => s.Number == skillNumber);
        }



        public void UpdateSkill(Skill skill)
        {
            List<Skill> skills = GetAllSkills().ToList();
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
   
