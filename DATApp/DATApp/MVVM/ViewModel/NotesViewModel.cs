using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DATApp.MVVM.Model.Classes;
using DATApp.MVVM.Model.Repositories;
using DATApp.Core;

namespace DATApp.MVVM.ViewModel
{
    class NotesViewModel : ViewModelBase
    {
        private readonly INoteRepository noteRepository = new FileNoteRepository("notes.txt");
        private string _name;
        private int _noteNumber;
        private string _noteContent;
        private User _noteClient;
        private Skill _noteSkill;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public int NoteNumber
        {
            get => _noteNumber;
            set { _noteNumber = value; OnPropertyChanged(); }
        }

        public User NoteClient
        {
            get => _noteClient;
            set { _noteClient = value; OnPropertyChanged(); }
        }
        public Skill NoteSkill
        {
            get => _noteSkill;
            set { _noteSkill = value; OnPropertyChanged(); }
        }

        public string NoteContent
        {
            get => _noteContent;
            set { _noteContent = value; OnPropertyChanged(); }
        }

        private Note _selectedNote;
        public Note SelectedNote
        {
            get => _selectedNote;
            set { _selectedNote = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Note> Notes { get; }

        public ICommand AddNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }
        public ICommand SaveNoteCommand { get; }


        public NotesViewModel()
        {
            Notes = new ObservableCollection<Note>(noteRepository.GetAll());

            AddNoteCommand = new RelayCommand(_ => AddNote());
            DeleteNoteCommand = new RelayCommand(_ => DeleteNote(), _ => SelectedNote != null);
            SaveNoteCommand = new RelayCommand(_ => SaveNote(), _ => SelectedNote != null);
        }

        private void AddNote()
        {
            var note = new Note { Name = _name, NoteNumber = _noteNumber, NoteContent = NoteContent, NoteClient = _noteClient, NoteSkill = _noteSkill };
            noteRepository.Add(note);
            Notes.Add(note);
        }

        private void DeleteNote()
        {
            noteRepository.Delete(SelectedNote);
            Notes.Remove(SelectedNote);
            SelectedNote = null;
        }

        private void SaveNote()
        {
            if (SelectedNote != null)
            {
                noteRepository.Update(SelectedNote);
            }
        }
    }
}
