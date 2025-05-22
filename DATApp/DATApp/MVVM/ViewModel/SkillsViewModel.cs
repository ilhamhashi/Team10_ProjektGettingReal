using DATApp.Core;
using DATApp.MVVM.Model.Classes;
using DATApp.MVVM.Model.Repositories;
using DATApp.MVVM.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace DATApp.MVVM.ViewModel
{
    public class SkillsViewModel : ViewModelBase
    {
        internal static FileSkillRepository skillRepository = new FileSkillRepository("skills.txt");
        public ObservableCollection<Skill> Skills;

        public  ObservableCollection<Model.Classes.Match> Matches;
        public static IEnumerable<string> Emotions { get; } = HomeViewModel.Emotions;
        public IEnumerable<Level> Levels { get; } = HomeViewModel.Levels;
        public ICollectionView ModulesCollectionView { get; }
        public static ICollectionView SkillsCollectionView { get; set; }

        private string _number;
        public string Number
        {
            get => _number;
            set { _number = value; OnPropertyChanged(); }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        private string _purpose;
        public string Purpose
        {
            get => _purpose;
            set { _purpose = value; OnPropertyChanged(); }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        private Module _module;
        public Module Module
        {
            get => _module;
            set { _module = value; OnPropertyChanged(); }
        }

        private Skill _selectedSkill;
        public Skill SelectedSkill
        {
            get => _selectedSkill;
            set { _selectedSkill = value; OnPropertyChanged(); }
        }        

        private string _searchTerm = string.Empty;
        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                OnPropertyChanged(nameof(SkillsFilter));
                SkillsCollectionView.Refresh();
            }
        }

        private string _matchNumber;
        public string MatchNumber
        {
            get { return _matchNumber; }
            set { _matchNumber = value; OnPropertyChanged(); }
        }

        private string _emotion;
        public string Emotion
        {
            get { return _emotion; }
            set { _emotion = value; OnPropertyChanged(); }
        }


        private Level _level;
        public Level Level
        {
            get { return _level; }
            set { _level = value; OnPropertyChanged(); }
        }

        public ICommand OpenAddSkillCommand { get; }
        public ICommand OpenAddNoteCommand { get; }
        public ICommand AddSkillCommand { get; }
        public ICommand SaveSkillCommand { get; }
        public ICommand DeleteSkillCommand { get; }
        public ICommand AddMatchToSkillCommand { get; }


        public SkillsViewModel()
        {
            Skills = new ObservableCollection<Skill>(skillRepository.GetAll());
            Matches = new ObservableCollection<Model.Classes.Match>(HomeViewModel.matchRepository.GetAll());

            SkillsCollectionView = CollectionViewSource.GetDefaultView(Skills);
            SkillsCollectionView.Filter = SkillsFilter;

            ModulesCollectionView = ModulesViewModel.ModulesCollectionView;


            OpenAddSkillCommand = new RelayCommandUser(OpenAddSkill, CanOpenAddSkill);
            OpenAddNoteCommand = new RelayCommandUser(OpenAddNote, CanOpenAddNote);
            AddSkillCommand = new RelayCommandUser(AddSkill, CanAddSkill);
            SaveSkillCommand = new RelayCommandUser(SaveSkill, CanSaveSkill);
            DeleteSkillCommand = new RelayCommandUser(DeleteSkill, CanDeleteSkill);
            AddMatchToSkillCommand = new RelayCommandUser(AddMatchToSkill, CanAddMatchToSkill);
        }

        private void AddSkill()
        {
            var skill = new Skill { Number = _number, Name = _name, Purpose = _purpose, Description = _description, Module = _module};

            Skills.Add(skill);
            skillRepository.AddSkill(skill);
            // Simpel dialogboks som bekræftelse
            MessageBox.Show($"Færdighed '{skill.Name}' oprettet!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);

            Name = string.Empty;
            Purpose = string.Empty;
            Description = string.Empty;
            Module = null;
        }

        private void OpenAddSkill()
        {
            AddSkillView addSkillView = new AddSkillView();
            addSkillView.Show();
        }

        private void OpenAddNote()
        {
            AddNoteView addNoteView = new AddNoteView();
            addNoteView.Show();
        }

        private void SaveSkill()
        {
            skillRepository.UpdateSkill(SelectedSkill);
            MessageBox.Show($"Færdighed '{SelectedSkill.Name}' Rettet!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
            SelectedSkill = null;
        }

        private void DeleteSkill()
        {
            skillRepository.DeleteSkill(SelectedSkill);
            Skills.Remove(SelectedSkill);
            MessageBox.Show($"Færdighed '{Name}' slettet!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
            SelectedSkill = null;
        }

        private bool SkillsFilter(object obj)
        {
            if (obj is Skill skill)
            {
                return skill.Name.Equals(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                    skill.Purpose.Equals(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                    skill.Description.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                    skill.Module.Name.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                    skill.Module.Description.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        private void AddMatchToSkill()
        {
            var match = new Model.Classes.Match { Number = _matchNumber, Skill = _selectedSkill, Emotion = _emotion, Level = _level };

            Matches.Add(match);
            HomeViewModel.matchRepository.AddMatch(match);

            // Simpel dialogboks som bekræftelse
            MessageBox.Show($"Match tilknyttet {match.Skill.Name}!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CanAddMatchToSkill() => SelectedSkill != null;
        private bool CanOpenAddSkill() => true;
        private bool CanOpenAddNote() => MainWindowViewModel.CurrentUser != null;
        private bool CanAddSkill() => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Purpose) && !string.IsNullOrWhiteSpace(Description);
        private bool CanSaveSkill() => SelectedSkill != null;

        private bool CanDeleteSkill() => SelectedSkill != null;
    }
}