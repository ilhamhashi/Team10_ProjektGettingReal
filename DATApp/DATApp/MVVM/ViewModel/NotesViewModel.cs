using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DATApp.MVVM.Model.Classes;
using DATApp.MVVM.Model.Repositories;
using DATApp.Core;

namespace DATApp.MVVM.ViewModel
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        private readonly INoteRepository noteRepository;

        private Note _selectedNote;
        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                _selectedNote = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Note> Notes { get; }

        public ICommand AddNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }
        public ICommand SaveNoteCommand { get; }


        public NotesViewModel()
        {
            //Determines who user and if they can see all notes or not.
            //MainWindowViewModel.CurrentUser = new User { Email = "test@example.com", IsAdmin = true };

            noteRepository = new FileNoteRepository("notes.txt");

            Notes = new ObservableCollection<Note>(noteRepository.GetAll());

            AddNoteCommand = new RelayCommand(_ => AddNote());
            DeleteNoteCommand = new RelayCommand(_ => DeleteNote(), _ => SelectedNote != null);
            SaveNoteCommand = new RelayCommand(_ => SaveNote(), _ => SelectedNote != null);
        }

        private void AddNote()
        {
            var newNote = new Note { Name = "Ny note", NoteContent = "Note indhold" };
            noteRepository.Add(newNote);
            Notes.Add(newNote);
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
