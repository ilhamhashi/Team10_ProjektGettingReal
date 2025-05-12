using DATApp.MVVM.Model.Classes;

namespace DATApp.MVVM.Model.Classes
{

    public class Note
    {
        public int NoteNumber { get; set; }
        public string NoteContent { get; set; }
        public User NoteClient { get; set; }
        public Skill NoteSkill { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{NoteNumber},{NoteContent},{Name}";
        }

        public static Note FromString(string input)
        {
            var parts = input.Split(',');
            if (parts.Length < 3) return null;

            return new Note
            {
                NoteNumber = int.Parse(parts[0]),
                NoteContent = parts[1],
                Name = parts[2]
            };
        }

    }
}
