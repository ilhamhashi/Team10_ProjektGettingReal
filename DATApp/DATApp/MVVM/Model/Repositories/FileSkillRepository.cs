using System.ComponentModel;
using System.DirectoryServices;
using System.IO;
using System.Printing;
using DATApp.MVVM.Model.Classes;
using SearchResult = DATApp.MVVM.Model.Classes.SearchResult;

namespace DATApp.MVVM.Model.Repositories
{
    public class FileSkillRepository : ISkillRepository
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
            try
            {
                File.AppendAllText(_skillFilePath, skill.ToString() + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }

        public void DeleteSkill(int skillNumber)
        {
            List<Skill> skills = GetAllSkills().ToList();
            skills.RemoveAll(s => s.SkillNumber == skillNumber);
            RewriteFile(skills);
        }

        public IEnumerable<Skill> GetAllSkills()
        {
            try
            {
                return File.ReadAllLines(_skillFilePath)
                           .Where(line => !string.IsNullOrEmpty(line)) // Undgå tomme linjer
                           .Select(Skill.FromString)
                           .ToList();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl ved læsning af fil: {ex.Message}");
                return [];
            }
        }

        public Skill GetSkill(int skillNumber)
        {
            return GetAllSkills().FirstOrDefault(s => s.SkillNumber == skillNumber);
        }

        public void UpdateSkill(Skill skill)
        {
            List<Skill> skills = GetAllSkills().ToList();
            int index = skills.FindIndex(s => s.SkillNumber == skill.SkillNumber);
            if (index != -1)
            {
                skills[index] = skill;
                RewriteFile(skills);
            }

        }

        //Anna start
        public class SkillSearcher

        {
            private List<Skill> Skills { get; set; }

            public SkillSearcher()
            {
                Skills = new List<Skill>();

            }

            public void SetSkills(IEnumerable<Skill> skills)
            {
                Skills = skills.ToList();
            }

            public IEnumerable<SearchResult> Search(string searchTerm)
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return Enumerable.Empty<SearchResult>();

                var results = Skills

                .Where(p =>
                (!string.IsNullOrEmpty(p.Description) && p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(p.Name) && p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)))
                .Select(p => new SearchResult
                {
                    Category = "Skill",
                    Description = p.Name,
                    ID = p.SkillNumber,
                    OriginatingSkill = p,
                    SkillInformation = GetSkillInfo(p.SkillNumber)
                })
                    .ToList();
                return results;

            }

            private static Dictionary<string, string> GetSkillInfo(int id)
            {
                return new Dictionary<string, string>

                {

                 { "controller", "skill" },
                 { "action", "details"},
                 { "ID", id.ToString() }

                 };

            }

        }

        //Anna færdig her


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
   
