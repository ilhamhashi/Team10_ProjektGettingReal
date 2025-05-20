using DATApp.MVVM.Model.Classes;

namespace DATApp.MVVM.Model.Classes
{

    public class Note
    {
        public int NoteNumber { get; set; }
        public string Name { get; set; }
        public string NoteContent { get; set; }
        public User NoteClient { get; set; }
        public Skill NoteSkill { get; set; }
        public bool IsAdmin { get; set; }

        public override string ToString()
        {
            return $"{NoteNumber},{NoteContent},{Name},{NoteClient?.Email}";
        }

        public static Note FromString(string input)
        {
            var parts = input.Split(',');

            if (parts.Length < 3)
                return null;

            var note = new Note
            {
                NoteNumber = int.Parse(parts[0]),
                NoteContent = parts[1],
                Name = parts[2]
            };

            if (parts.Length >= 4)
            {
                note.NoteClient = new User { Email = parts[3] };
            }

            return note;
        }

    }
}
