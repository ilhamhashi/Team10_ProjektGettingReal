using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATApp.MVVM.Model.Classes;

namespace DATApp.MVVM.Model.Repositories
{
    internal class FileNoteRepository : INoteRepository
    {
        private readonly List<Note> _notes = new List<Note>();
        private int _nextId = 0;

        public void Add(Note note)
        {
            note.Id = _nextId++;
            _notes.Add(note);
        }

        public void Delete(Note note)
        {
            _notes.RemoveAll(n => n.Id == note.Id);
        }

        public IEnumerable<Note> GetAll()
        {
            return _notes;
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
            }
        }
    }
}
