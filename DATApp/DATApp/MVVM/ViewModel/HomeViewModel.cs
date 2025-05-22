using DATApp.Core;
using DATApp.MVVM.Model.Classes;
using DATApp.MVVM.Model.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Linq;

namespace DATApp.MVVM.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        internal static FileMatchRepository matchRepository = new FileMatchRepository("matches.txt");            

        public ObservableCollection<Model.Classes.Match> Matches;
        public ObservableCollection<Skill> Skills;
        public static IEnumerable<string> Emotions { get; } = ["Vrede", "Aggression", "Nedtrykthed", "Angst", "Skyld"];
        public static IEnumerable<Level> Levels { get; } = Enum.GetValues(typeof(Level)).Cast<Level>();

        public ICollectionView MatchesCollectionView { get; }
        public ICollectionView MatchesFilterCollectionView { get; }
        public ICollectionView SkillsCollectionView { get; }
        public ICommand SaveMatchCommand { get; }
        public ICommand DeleteMatchCommand { get; }


        private string _number;
        public string Number
        {
            get => _number;
            set { _number = value; OnPropertyChanged(); }
        }

        private Skill _skill;
        public Skill Skill
        {
            get => _skill;
            set { _skill = value; OnPropertyChanged(); }
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

        private Match _selectedMatch;
        public Match SelectedMatch
        {
            get { return _selectedMatch; }
            set
            {
                _selectedMatch = value;
                OnPropertyChanged();
            }
        }

        private Skill _selectedSkill;
        public Skill SelectedSkill
        {
            get => _selectedSkill;
            set { _selectedSkill = value; OnPropertyChanged(); }
        }

        private string _selectedEmotion;
        public string SelectedEmotion
        {
            get { return _selectedEmotion; }
            set
            {
                _selectedEmotion = value;
                OnPropertyChanged(nameof(MatchFilter));
                MatchesFilterCollectionView.Refresh();
            }
        }

        private Level _selectedLevel;
        public Level SelectedLevel
        {
            get { return _selectedLevel; }
            set
            {
                _selectedLevel = value;
                OnPropertyChanged(nameof(MatchFilter));
                MatchesFilterCollectionView.Refresh();
            }
        }

        public HomeViewModel()
        {
            Matches = new ObservableCollection<Model.Classes.Match>(matchRepository.GetAll());
            MatchesCollectionView = CollectionViewSource.GetDefaultView(Matches);
            MatchesFilterCollectionView = CollectionViewSource.GetDefaultView(SkillsViewModel.skillRepository.GetAll());
            MatchesFilterCollectionView.Filter = MatchFilter;

            SaveMatchCommand = new RelayCommandUser(SaveMatch, CanSaveMatch);
            DeleteMatchCommand = new RelayCommandUser(DeleteMatch, CanDeleteMatch);

        }

        private void SaveMatch()
        {
            matchRepository.UpdateMatch(SelectedMatch);
            MessageBox.Show($"Ændringer gemt!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
            SelectedSkill = null;
        }

        private void DeleteMatch()
        {
            matchRepository.DeleteMatch(SelectedMatch);
            Matches.Remove(SelectedMatch);
            MessageBox.Show($"Match slettet!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
            SelectedSkill = null;
        }

        private bool MatchFilter(object obj)
        {
            if (obj is Skill skill)
            {
                var matches = Matches.ToList().FindAll(m => m.Level.Equals(SelectedLevel) && m.Emotion.Equals(SelectedEmotion));
                return matches.Where(m => m.Skill.Number == skill.Number).Any();
            }
            return false;
        }
        private bool CanSaveMatch() => SelectedMatch != null;
        private bool CanDeleteMatch() => SelectedMatch != null;
    }
}