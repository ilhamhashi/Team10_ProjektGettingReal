using System;
using System.Collections.Generic;
using System.Linq;

namespace DATApp.MVVM.Model.Classes
{
    public class Skill
    {
        public int SkillNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Goal { get; set; }
        public string ValidationText { get; set; }
        public Level Level { get; set; }
        public Module Module { get; set; }
        public List<EmotionalState> EmotionsMatch { get; set; }

        public override string ToString()
        {
            string emotions = EmotionsMatch != null ? string.Join(";", EmotionsMatch.Select(e => e.ToString())) : "";
            return $"{SkillNumber},{Name},{Description},{Goal},{ValidationText},{Level},{Module?.ModuleNumber},{emotions}";
        }

        public static Skill FromString(string input)
        {
            string[] parts = input.Split(',');

            // Guard clause
            if (parts.Length < 7)
                throw new FormatException("Invalid skill string format");

            var skill = new Skill
            {
                SkillNumber = int.Parse(parts[0]),
                Name = parts[1],
                Description = parts[2],
                Goal = parts[3],
                ValidationText = parts[4],
                Level = Enum.Parse<Level>(parts[5]),
                Module = new Module { ModuleNumber = int.Parse(parts[6]) }
            };

            // Handle optional EmotionsMatch
            if (parts.Length > 7)
            {
                string[] emotionParts = parts[7].Split(';', StringSplitOptions.RemoveEmptyEntries);
                skill.EmotionsMatch = emotionParts.Select(e => Enum.Parse<EmotionalState>(e)).ToList();
            }
            else
            {
                skill.EmotionsMatch = new List<EmotionalState>();
            }

            return skill;
        }
    }
}
