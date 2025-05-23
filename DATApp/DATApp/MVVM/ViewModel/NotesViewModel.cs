using DATApp.Core;
using DATApp.MVVM.Model.Classes;
using DATApp.MVVM.Model.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace DATApp.MVVM.ViewModel
{
    public class NotesViewModel : ViewModelBase
    {
        private readonly INoteRepository noteRepository = new FileNoteRepository("notes.txt");
        public ObservableCollection<Note> Notes;

        private string _number;
        public string Number
        {
            get => _number;
            set { _number = value; OnPropertyChanged(); }
        }

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get => _dateTime;
            set { _dateTime = value; OnPropertyChanged(); }
        }

        private string _content;
        public string Content
        {
            get => _content;
            set { _content = value; OnPropertyChanged(); }
        }

        private Skill _skill;
        public Skill Skill
        {
            get => _skill;
            set { _skill = value; OnPropertyChanged(); }
        }

        private Note _selectedNote;
        public Note SelectedNote
        {
            get => _selectedNote;
            set { _selectedNote = value; OnPropertyChanged(); }
        }

        public ICommand AddNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }
        public ICommand SaveNoteCommand { get; }
        public ICollectionView NotesCollectionView { get; set; }
        public ICollectionView SkillsCollectionView { get; }

        private bool CanAddNote() => !string.IsNullOrWhiteSpace(Content) && Skill != null;
        private bool CanSaveNote() => SelectedNote != null && MainWindowViewModel.CurrentUser.Email == SelectedNote.Client.Email;
        private bool CanDeleteNote() => SelectedNote != null && MainWindowViewModel.CurrentUser.Email == SelectedNote.Client.Email;

        public NotesViewModel()
        {
            Notes = new ObservableCollection<Note>(noteRepository.GetAll());
            NotesCollectionView = CollectionViewSource.GetDefaultView(Notes);

            SkillsCollectionView = SkillsViewModel.SkillsCollectionView;

            AddNoteCommand = new RelayCommand(_ => AddNote(), _ => CanAddNote());
            DeleteNoteCommand = new RelayCommand(_ => DeleteNote(), _ => CanDeleteNote());
            SaveNoteCommand = new RelayCommand(_ => SaveNote(), _ => CanSaveNote());
        }

        private void AddNote()
        {
            var note = new Note { Number = _number, Content = _content, DateTime = _dateTime, Client = MainWindowViewModel.CurrentUser, Skill = _skill };
            noteRepository.Add(note);
            Notes.Add(note);
            MessageBox.Show($"Note oprettet!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);

            Content = string.Empty;
            Skill = null;
        }

        private void DeleteNote()
        {
            noteRepository.Delete(SelectedNote);
            Notes.Remove(SelectedNote);
            SelectedNote = null;
        }

        private void SaveNote()
        {
            noteRepository.Update(SelectedNote);
            // Dialogboks som bekræftelse
            MessageBox.Show($"Ændringer Gemt!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
            // Tekstfelter ryddes for indhold
            SelectedNote = null;
        }
    }
}
