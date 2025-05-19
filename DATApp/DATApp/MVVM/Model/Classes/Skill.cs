using System;
using System.Collections.Generic;
using System.Linq;

namespace DATApp.MVVM.Model.Classes
{
    public class Skill
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public string Goal { get; set; }
        //public string ValidationText { get; set; }
        public Level Level { get; set; }
        public Module Module { get; set; }
        public string EmotionMatch { get; set; }

        public override string ToString()
        {
            return $"{Number},{Name},{Description},{Level},{Module},{EmotionMatch}";
        }

        public static Skill FromString(string input)
        {

            string[] parts = input.Split(','); // Opdeler strengen baseret på kommategn
            var skill = new Skill 
            {
                Number = int.Parse(parts[0]),
                Name = parts[1],
                Description = parts[2],
                Level = Enum.Parse<Level>(parts[3]),
                Module = new Module { Number = int.Parse(parts[4]), Name = parts[5], Description = parts[6] },
                EmotionMatch = parts[7]
            };
            return skill;

            //Module = parts[4],
            //EmotionMatches = parts[5]

            /*

            string[] parts = input.Split(',');

            if (parts.Length < 7)
                throw new FormatException("Invalid skill string format");

            var skill = new Skill
            {
                Number = int.Parse(parts[0]),
                Name = parts[1],
                Description = parts[2],
                Goal = parts[3],
                ValidationText = parts[4],
                Level = Enum.Parse<Level>(parts[5]),
                Module = new Module { ModuleNumber = int.Parse(parts[6]) }
            };

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

            */

        }
    }
}
