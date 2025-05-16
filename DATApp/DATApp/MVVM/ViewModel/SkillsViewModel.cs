using DATApp.Core;
using DATApp.MVVM.Model.Classes;
using DATApp.MVVM.Model.Repositories;
using DATApp.MVVM.View;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;

namespace DATApp.MVVM.ViewModel
{
    class SkillsViewModel : ViewModelBase
    {
        private readonly FileSkillRepository skillRepository = new FileSkillRepository("skills.txt");
        private int number;
        private string name;
        private string description;
        private Module module;
        private List<string> emotionMatches;
        private Level level;
        private Skill selectedSkill;
        private string searchTerm = string.Empty;
        private readonly ObservableCollection<Skill> Skills;
        public ICollectionView SkillsCollectionView { get; }

        public int Number
        {
            get => number;
            set { number = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get => description;
            set { description = value; OnPropertyChanged(); }
        }

        public Module Module
        {
            get => module;
            set { module = value; OnPropertyChanged(); }
        }

        public Level Level
        {
            get { return level; }
            set { level = value; OnPropertyChanged(); }
        }

        public List<string> EmotionMatches
        {
            get { return emotionMatches; }
            set { emotionMatches = value; OnPropertyChanged(); }
        }

        public Skill SelectedSkill
        {
            get => selectedSkill;
            set { selectedSkill = value; OnPropertyChanged(); }
        }

        public string SearchTerm
        {
            get { return searchTerm; }
            set
            {
                searchTerm = value;
                OnPropertyChanged(nameof(SkillsFilter));
                SkillsCollectionView.Refresh();
            }
        }

        public ICommand OpenAddSkillCommand { get; }
        public ICommand AddSkillCommand { get; }
        public ICommand SaveSkillCommand { get; }
        public ICommand DeleteSkillCommand { get; }

        public SkillsViewModel()
        {
            Skills = new ObservableCollection<Skill>(skillRepository.GetAllSkills());
            SkillsCollectionView = CollectionViewSource.GetDefaultView(Skills);
            SkillsCollectionView.Filter = SkillsFilter;

            OpenAddSkillCommand = new RelayCommandUser(OpenAddSkill, CanOpenAddSkill);
            AddSkillCommand = new RelayCommandUser(AddSkill, CanAddSkill);
            SaveSkillCommand = new RelayCommandUser(SaveSkill, CanSaveSkill);
            DeleteSkillCommand = new RelayCommandUser(DeleteSkill, CanDeleteSkill);

        }


        private void AddSkill()
        {
            var skill = new Skill { Number = number, Name = name, Description = description, Level = level};

            Skills.Add(skill);
            skillRepository.AddSkill(skill);

            // Simpel dialogboks som bekræftelse
            MessageBox.Show($"Færdighed '{skill.Name}' oprettet!", "Tilføjet", MessageBoxButton.OK, MessageBoxImage.Information);

            Name = string.Empty;
            Description = string.Empty;
            Module = null;
            Description = string.Empty;
            Level = 0;
            EmotionMatches = null;
        }

        private void OpenAddSkill()
        {
            AddSkillView addSkillView = new AddSkillView();
            addSkillView.Show();
        }

        private void SaveSkill()
        {
            skillRepository.UpdateSkill(SelectedSkill);
            MessageBox.Show($"Færdighed '{SelectedSkill.Name}' Rettet!", "Redigeret", MessageBoxButton.OK, MessageBoxImage.Information);
            SelectedSkill = null;
        }

        private void DeleteSkill()
        {
            skillRepository.DeleteSkill(SelectedSkill);
            Skills.Remove(SelectedSkill);
            MessageBox.Show($"Færdighed '{Name}' slettet!", "Fjernet", MessageBoxButton.OK, MessageBoxImage.Information);
            SelectedSkill = null;
        }

        private bool SkillsFilter(object obj)
        {
            if (obj is Skill skill)
            {
                return skill.Name.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                    skill.Description.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        private bool CanAddSkill() => !string.IsNullOrWhiteSpace(Name);
        private bool CanOpenAddSkill() => true;
        private bool CanSaveSkill() => SelectedSkill != null;
        private bool CanDeleteSkill() => SelectedSkill != null;
        private bool CanSearchSkill() => true;




        /*
        private string searchTerm;
        private ObservableCollection<SearchResult> _searchResults;
        private SkillSearcher _skillSearcher;
        public string NewSkillName { get; set; }
        public string NewSkillDescription { get; set; }
        public Level NewSkillLevel { get; set; }
        public List<Level> Levels => Enum.GetValues(typeof(Level)).Cast<Level>().ToList();

        public List<EmotionalState> EmotionalStates => Enum.GetValues(typeof(EmotionalState)).Cast<EmotionalState>().ToList();
        public List<EmotionalState> SelectedEmotions { get; set; } = new List<EmotionalState>();

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
            */

       
    }
}