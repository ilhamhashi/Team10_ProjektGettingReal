using DATApp.MVVM.Model.Classes;
using DATApp.MVVM.ViewModel;
using System.ComponentModel;
using System.IO;


namespace DATApp.MVVM.Model.Repositories
{
    internal class FileNoteRepository : INoteRepository
    {
        private readonly string _noteFilePath;
        int counter = 1;

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
            note.Number = counter;
            note.Client = MainWindowViewModel.CurrentUser;
            note.DateTime = DateTime.Now;
            notes.Add(note);
            try
            {
                File.AppendAllText(_noteFilePath, note.ToString() + Environment.NewLine);
                counter++;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }

        public void Delete(Note note)
        {
            List<Note> notes = GetAll().ToList();
            notes.RemoveAll(n => n.Number == note.Number);
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
                    .Where(n => n.Client != null && n.Client.Email == MainWindowViewModel.CurrentUser.Email);
                return userNotes;
            }
        }

        public Note GetNote(int number)
        {
            return GetAll().FirstOrDefault(n => n.Number == number);
        }

        public void Update(Note note)
        {
            List<Note> notes = GetAll().ToList();
            int index = notes.FindIndex(n => n.Number == note.Number);
            if (index != -1)
            {
                notes[index] = note;
                RewriteFile(notes);
            }
        }

        private void RewriteFile(List<Note> notes)
        {
            try
            {
                File.WriteAllLines(_noteFilePath, notes.Select(n => n.ToString()));
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }
    }
}
