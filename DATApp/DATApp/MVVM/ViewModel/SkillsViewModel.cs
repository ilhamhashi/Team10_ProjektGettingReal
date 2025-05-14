using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using DATApp.Core;
using DATApp.MVVM.Model.Classes;

namespace DATApp.MVVM.ViewModel
{
    public class SkillsViewModel : INotifyPropertyChanged
    {
        private string _searchTerm;
        private ObservableCollection<SearchResult> _searchResults;
        private SkillSearcher _skillSearcher;
        public string NewSkillName { get; set; }
        public string NewSkillDescription { get; set; }
        public Level NewSkillLevel { get; set; }
        public List<Level> Levels => Enum.GetValues(typeof(Level)).Cast<Level>().ToList();

        public List<EmotionalState> EmotionalStates => Enum.GetValues(typeof(EmotionalState)).Cast<EmotionalState>().ToList();
        public List<EmotionalState> SelectedEmotions { get; set; } = new List<EmotionalState>();



        public SkillsViewModel()
        {
            _searchResults = new ObservableCollection<SearchResult>();
            _skillSearcher = new SkillSearcher();

            // Sample skills for testing
            var skills = new List<Skill>
            {
                new Skill { SkillNumber = 1, Name = "Angst", Description = "Anxiety-related skill" },
                new Skill { SkillNumber = 2, Name = "Vrede", Description = "Anger-related skill" },
                new Skill { SkillNumber = 3, Name = "Stress", Description = "Stress-related skill" },
                new Skill { SkillNumber = 4, Name = "Depression", Description = "Depression-related skill" }
            };
            _skillSearcher.SetSkills(skills);

            // Define commands
            SearchCommand = new RelayCommand(Search);
            AddCommand = new RelayCommand(AddSkill);
        }

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                if (_searchTerm != value)
                {
                    _searchTerm = value;
                    OnPropertyChanged(nameof(SearchTerm)); // Update UI when the search term changes
                }
            }
        }

        public ObservableCollection<SearchResult> SearchResults
        {
            get => _searchResults;
            set
            {
                if (_searchResults != value)
                {
                    _searchResults = value;
                    OnPropertyChanged(nameof(SearchResults));
                }
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand AddCommand { get; }

        private void Search(object parameter)
        {
            var results = _skillSearcher.Search(SearchTerm);

            SearchResults.Clear();
            foreach (var result in results)
            {
                SearchResults.Add(result);
            }
        }

        private void AddSkill(object parameter)
        {
            var newSkill = new Skill
            {
                SkillNumber = _skillSearcher.Search("").Count() + 1,
                Name = NewSkillName,
                Description = NewSkillDescription,
                Level = NewSkillLevel,
                EmotionsMatch = SelectedEmotions.ToList()
            };

            var currentSkills = _skillSearcher.Search("").Select(sr => sr.OriginatingSkill).ToList();
            currentSkills.Add(newSkill);
            _skillSearcher.SetSkills(currentSkills);

            var updatedResults = _skillSearcher.Search(SearchTerm);
            SearchResults.Clear();
            foreach (var result in updatedResults)
            {
                SearchResults.Add(result);
            }

            NewSkillName = "";
            NewSkillDescription = "";
            SelectedEmotions.Clear();
            OnPropertyChanged(nameof(NewSkillName));
            OnPropertyChanged(nameof(NewSkillDescription));
            OnPropertyChanged(nameof(SelectedEmotions));
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}