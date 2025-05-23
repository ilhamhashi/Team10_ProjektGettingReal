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
    public class MatchesViewModel : ViewModelBase
    {
        private readonly FileMatchRepository matchRepository = new FileMatchRepository("matches.txt");
        public ObservableCollection<Model.Classes.Match> Matches;

        public IEnumerable<string> Emotions { get; } = ["Vrede", "Aggression", "Nedtrykthed", "Angst", "Skyld"];
        public IEnumerable<string> Levels { get; } = ["Rød", "Gul", "Grøn"];

        public ICollectionView MatchesSearchCollectionView { get; }
        public ICollectionView MatchesFilterCollectionView { get; }
        public ICollectionView SkillsCollectionView { get; }

        public ICommand SaveMatchCommand { get; }
        public ICommand DeleteMatchCommand { get; }
        public ICommand AddMatchToSkillCommand { get; }

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


        private string _level;
        public string Level
        {
            get { return _level; }
            set { _level = value; OnPropertyChanged(); }
        }

        private Model.Classes.Match _selectedMatch;
        public Model.Classes.Match SelectedMatch
        {
            get { return _selectedMatch; }
            set
            {
                _selectedMatch = value;
                OnPropertyChanged();
            }
        }

        private string _searchTerm = string.Empty;
        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                OnPropertyChanged(nameof(MatchSearchFilter));
                MatchesSearchCollectionView.Refresh();
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

        private string _selectedLevel;
        public string SelectedLevel
        {
            get { return _selectedLevel; }
            set
            {
                _selectedLevel = value;
                OnPropertyChanged(nameof(MatchFilter));
                MatchesFilterCollectionView.Refresh();
            }
        }

        public MatchesViewModel()
        {
            Matches = new ObservableCollection<Model.Classes.Match>(matchRepository.GetAll());
            MatchesSearchCollectionView = CollectionViewSource.GetDefaultView(Matches);
            MatchesSearchCollectionView.Filter = MatchSearchFilter;

            MatchesFilterCollectionView = CollectionViewSource.GetDefaultView(SkillsViewModel.skillRepository.GetAll());
            MatchesFilterCollectionView.Filter = MatchFilter;

            SkillsCollectionView = SkillsViewModel.SkillsCollectionView;

            AddMatchToSkillCommand = new RelayCommandUser(AddMatchToSkill, CanAddMatchToSkill);
            SaveMatchCommand = new RelayCommandUser(SaveMatch, CanSaveMatch);
            DeleteMatchCommand = new RelayCommandUser(DeleteMatch, CanDeleteMatch);
        }
        private void AddMatchToSkill()
        {
            var match = new Model.Classes.Match { Number = _number, Skill = _skill, Emotion = _emotion, Level = _level };
            if (Matches.Where(m => m.Skill.Number == match.Skill.Number && m.Emotion == match.Emotion && m.Level == match.Level).Any())
            {
                MessageBox.Show($"Kunne ikke tilføjes. Match eksisterer i forvejen", "Fejl", MessageBoxButton.OK, MessageBoxImage.Information);
                Skill = null;
                Emotion = null;
                Level = null;
            }
            else
            {
                Matches.Add(match);
                matchRepository.AddMatch(match);
                MessageBox.Show($"Match tilknyttet {match.Skill.Name}!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
                Skill = null;
                Emotion = null;
                Level = null;
            }
        }

        private void SaveMatch()
        {
            matchRepository.UpdateMatch(SelectedMatch);
            MessageBox.Show($"Ændringer gemt!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
            SelectedMatch = null;
        }

        private void DeleteMatch()
        {
            matchRepository.DeleteMatch(SelectedMatch);
            Matches.Remove(SelectedMatch);
            MessageBox.Show($"Match slettet!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
            SelectedMatch = null;
        }

        private bool MatchSearchFilter(object obj)
        {
            if (obj is Model.Classes.Match match)
            {
                return match.Skill.Name.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                    match.Emotion.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                    match.Level.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
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

        private bool CanAddMatchToSkill() => Skill != null && Emotion != null && Level != null;
        private bool CanSaveMatch() => SelectedMatch != null;
        private bool CanDeleteMatch() => SelectedMatch != null;
    }
}