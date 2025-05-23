namespace DATApp.MVVM.Model.Classes
{
    public class Match
    {
        public int Number { get; set; }
        public Skill Skill { get; set; }
        public string Emotion { get; set; }
        public string Level { get; set; }

        public override string ToString()
        {
            return $"{Number},{Skill},{Emotion},{Level}";
        }

        public static Match FromString(string input)
        {
            string[] parts = input.Split(','); // Opdeler strengen baseret på kommategn
            var match = new Match
            {
                Number = int.Parse(parts[0]),
                Skill = new Skill
                {
                    Number = int.Parse(parts[1]),
                    Name = parts[2],
                    Purpose = parts[3],
                    Description = parts[4],
                    Module = new Module { Number = int.Parse(parts[5]), Name = parts[6], Description = parts[7] }
                },
                Emotion = parts[8],
                Level = parts[9]
            };
            return match;
        }
    }
}
