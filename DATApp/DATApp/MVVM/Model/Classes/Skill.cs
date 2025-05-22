namespace DATApp.MVVM.Model.Classes
{
    public class Skill
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Purpose { get; set; }
        public string Description { get; set; }
        public Module Module { get; set; }

        public override string ToString()
        {
            return $"{Number},{Name},{Purpose},{Description},{Module}";
        }

        public static Skill FromString(string input)
        {
            string[] parts = input.Split(','); // Opdeler strengen baseret på kommategn
            var skill = new Skill 
            {
                Number = parts[0],
                Name = parts[1],
                Purpose = parts[2],
                Description = parts[3],
                Module = new Module { Number = parts[4], Name = parts[5], Description = parts[6] }
            };
            return skill;
        }
    }
}
