namespace DATApp.MVVM.Model.Classes
{

    public class Note
    {
        public int Number { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public User Client { get; set; }
        public Skill Skill { get; set; }

        public override string ToString()
        {
            return $"{Number},{Content},{DateTime},{Client}, {Skill}";
        }

        public static Note FromString(string input)
        {
            var parts = input.Split(',');
            var note = new Note
            {
                Number = int.Parse(parts[0]),
                Content = parts[1],
                DateTime = DateTime.Parse(parts[2]),
                Client = new User { Name = parts[3], Email = parts[4], Password = parts[5], IsAdmin = bool.Parse(parts[6]) },
                Skill = new Skill
                {
                    Number = int.Parse(parts[7]),
                    Name = parts[8],
                    Purpose = parts[9],
                    Description = parts[10],
                    Module = new Module { Number = int.Parse(parts[11]), Name = parts[12], Description = parts[13] }
                }
            };

            return note;
        }

    }
}
