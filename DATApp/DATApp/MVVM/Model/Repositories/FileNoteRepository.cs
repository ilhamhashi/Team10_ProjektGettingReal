using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATApp.MVVM.Model.Classes;
using System.IO;
using DATApp.Core;

namespace DATApp.MVVM.Model.Repositories
{
    internal class FileNoteRepository : INoteRepository
    {
        private readonly List<Note> _notes = new List<Note>();
        private int _nextId = 0;

        private readonly string _noteFilePath;
        private readonly bool _isLoggedIn;

        public FileNoteRepository(string filePath)
        {
            _noteFilePath = filePath;

            if (!File.Exists(_noteFilePath))
            {
                File.Create(_noteFilePath).Close();
            }

            var lines = File.ReadAllLines(_noteFilePath);
            foreach (var line in lines)
            {
                var note = Note.FromString(line);
                if (note != null)
                {
                    note.Id = _nextId++;
                    _notes.Add(note);
                }
            }
        }

        public void Add(Note note)
        {
            note.Id = _nextId++;
            note.NoteClient = Session.CurrentUser;
            _notes.Add(note);

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
            _notes.RemoveAll(n => n.Id == note.Id);
        }

        public IEnumerable<Note> GetAll()
        {
            if (Session.CurrentUser == null)
            {
                Console.WriteLine("No user logged in.");
                return Enumerable.Empty<Note>();
            }

            if (Session.CurrentUser.IsAdmin)
            {
                Console.WriteLine("Admin user: loading all notes.");
                return _notes;
            }

            var userNotes = _notes
                .Where(n => n.NoteClient != null && n.NoteClient.Email == Session.CurrentUser.Email)
                .ToList();

            Console.WriteLine($"User {Session.CurrentUser.Email}: loading {userNotes.Count} notes.");
            return userNotes;
        }

        public Note GetByID(int id)
        {
            return _notes.FirstOrDefault(n => n.Id == id);
        }

        public void Update(Note note)
        {
            var existingNote = _notes.FirstOrDefault(n => n.Id == note.Id);
            if (existingNote != null)
            {
                existingNote.Name = note.Name;
                existingNote.NoteContent = note.NoteContent;
                existingNote.NoteNumber = note.NoteNumber;
               
                try
                {
                    File.WriteAllLines(_noteFilePath, _notes.Select(n => n.ToString()));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fejl ved opdatering af fil: {ex.Message}");
                }
            }
        }
    }
}
