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
                SkillNumber = _skillSearcher.Search("").Count() + 1, // Generate new skill number
                Name = "New Skill",
                Description = "This is a new skill added by the user.",
                Level = Level.None,
                EmotionsMatch = new List<EmotionalState> { EmotionalState.Happy }
            };

            var currentSkills = _skillSearcher.Search("").Select(sr => sr.OriginatingSkill).ToList();
            currentSkills.Add(newSkill);
            _skillSearcher.SetSkills(currentSkills);

            // Update the search results after adding the new skill
            var updatedResults = _skillSearcher.Search(SearchTerm);
            SearchResults.Clear();
            foreach (var result in updatedResults)
            {
                SearchResults.Add(result);
            }
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}