

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
        Module Module { get; set; }
        List<EmotionalState> EmotionsMatch { get; set; }

        public override string ToString()
        {
            return $"{SkillNumber},{Name},{Description},{Goal},{ValidationText},{Level},{Module.ModuleNumber},{EmotionsMatch}";
        }

        public static Skill FromString(string input)
        {
            string[] parts = input.Split(','); // Opdeler strengen baseret på kommategn
            return new Skill
            {
                SkillNumber = int.Parse(parts[0]),
                Name = parts[1],
                Description = parts[2],
                Goal = parts[3],
                ValidationText = parts[4],
                Level = Enum.Parse<Level>(parts[5]),            
                //Module.ModuleNumber = int.Parse(parts[6])
                //EmotionsMatch??

            };
        }

        /* public Skill(string name, string description, string goal, string validationText, Level level, List<Module> modules, List<EmotionalState> emotionsMatch)
        {
            _name = name;
            _description = description;
            _goal = goal;
            _validationText = validationText;
            _level = level;
            _modules = modules;
            _emotionsMatch = emotionsMatch;
        }*/
    }
}

