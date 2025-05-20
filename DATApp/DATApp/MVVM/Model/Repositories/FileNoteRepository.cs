using DATApp.Core;
using DATApp.MVVM.Model.Classes;
using DATApp.MVVM.ViewModel;
using System.IO;
using System.Windows.Shapes;

namespace DATApp.MVVM.Model.Repositories
{
    internal class FileNoteRepository : INoteRepository
    {
        private readonly string _noteFilePath;

        public FileNoteRepository(string filePath)
        {
            _noteFilePath = filePath;

            if (!File.Exists(_noteFilePath))
            {
                File.Create(_noteFilePath).Close();
            }
        }

        public void Add(Note note)
        {
            var notes = GetAll().ToList();
            if (notes.Count == 0)
            {
                note.NoteNumber  = 1;
            }
            else
            {
                note.NoteNumber = notes.Max(n => n.NoteNumber) + 1;
            }
            note.NoteClient = MainWindowViewModel.CurrentUser;
            notes.Add(note);

            try
            {
                File.AppendAllText(_noteFilePath, note.ToString() + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }

        public void Delete(Note note)
        {
            List<Note> notes = GetAll().ToList();
            notes.RemoveAll(n => n.NoteNumber == note.NoteNumber);
            File.WriteAllLines(_noteFilePath, notes.Select(n => n.ToString()));
        }

        public IEnumerable<Note> GetAll()
        {
            List<Note> notes = File.ReadAllLines(_noteFilePath).Where(line => !string.IsNullOrEmpty(line)).Select(Note.FromString).ToList();

            if (MainWindowViewModel.CurrentUser == null)
            {
                return [];
            }

            else if (MainWindowViewModel.CurrentUser.IsAdmin)
            {
                return notes;
            }
            else
            {
                var userNotes = notes
                    .Where(n => n.NoteClient != null && n.NoteClient.Email == MainWindowViewModel.CurrentUser.Email);
                return userNotes;
            }
        }

        public Note GetByID(int number)
        {
            return GetAll().FirstOrDefault(n => n.NoteNumber == number);
        }

        public void Update(Note note)
        {
            var existingNote = GetAll().ToList().FirstOrDefault(n => n.NoteNumber == note.NoteNumber);
            if (existingNote != null)
            {
                existingNote.Name = note.Name;
                existingNote.NoteContent = note.NoteContent;
                existingNote.NoteNumber = note.NoteNumber;
               
                try
                {
                    File.WriteAllLines(_noteFilePath, GetAll().Select(n => n.ToString()));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fejl ved opdatering af fil: {ex.Message}");
                }
            }
        }
    }
}
