namespace DATApp.MVVM.Model.Classes
{
    public class Match
    {
        public string Number { get; set; }
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
                Number = parts[0],
                Skill = new Skill
                {
                    Number = parts[1],
                    Name = parts[2],
                    Purpose = parts[3],
                    Description = parts[4],
                    Module = new Module { Number = parts[5], Name = parts[6], Description = parts[7] }
                },
                Emotion = parts[8],
                Level = parts[9]
            };
            return match;
        }
    }
}
