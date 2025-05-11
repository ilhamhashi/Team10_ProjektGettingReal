using DATApp.MVVM.Model.Classes;

namespace DATApp.MVVM.Model.Classes
{

    public class Note
    {
        public int NoteNumber { get; set; }
        public string NoteContent { get; set; }
        public User NoteClient { get; set; }
        public Skill NoteSkill { get; set; }

        public override string ToString()
        {
            return $"{NoteNumber},{NoteContent},{NoteClient.Email},{NoteSkill.SkillNumber}";
        }

        public static Note FromString(string input)
        {
            string[] parts = input.Split(','); // Opdeler strengen baseret på kommategn
            return new Note
            {
                NoteNumber = int.Parse(parts[0]),
                NoteContent = parts[1],
                //NoteClient.UserName = parts[2],
                //NoteSkill.SkillNumber = int.Parse(parts[3])
            };
        }

    }
}
