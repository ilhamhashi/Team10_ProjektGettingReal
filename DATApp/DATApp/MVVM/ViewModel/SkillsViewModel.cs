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
        private readonly FileModuleRepository moduleRepository = new FileModuleRepository("modules.txt");
        private int number;
        private string name;
        private string description;
        private Module module;
        private string emotionMatch;
        private Level level;
        private Skill selectedSkill;
        private string searchTerm = string.Empty;
        private readonly ObservableCollection<Skill> Skills;
        public ICollectionView SkillsCollectionView { get; }
        public List<string> Emotions { get; } = new List<string> { "Vrede", "Aggression", "Nedtrykthed", "Angst", "Skyld" };
        private readonly ObservableCollection<Module> Modules;
        public ICollectionView ModulesCollectionView { get; }
        public IEnumerable<Level> Levels { get
            {
                return Enum.GetValues(typeof(Level)).Cast<Level>();
            }}

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

        public string EmotionMatch
        {
            get { return emotionMatch; }
            set { emotionMatch = value; OnPropertyChanged(); }
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

            Modules = new ObservableCollection<Module>(moduleRepository.GetAllModules());
            ModulesCollectionView = CollectionViewSource.GetDefaultView(Modules);


            OpenAddSkillCommand = new RelayCommandUser(OpenAddSkill, CanOpenAddSkill);
            AddSkillCommand = new RelayCommandUser(AddSkill, CanAddSkill);
            SaveSkillCommand = new RelayCommandUser(SaveSkill, CanSaveSkill);
            DeleteSkillCommand = new RelayCommandUser(DeleteSkill, CanDeleteSkill);

        }

        private void AddSkill()
        {
            var skill = new Skill { Number = number, Name = name, Description = description, Module = module, Level = level, EmotionMatch = emotionMatch};

            Skills.Add(skill);
            skillRepository.AddSkill(skill);

            // Simpel dialogboks som bekræftelse
            MessageBox.Show($"Færdighed '{skill.Name}' oprettet!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);

            Name = string.Empty;
            Description = string.Empty;
            Module = null;
            Description = string.Empty;
            Level = 0;
            EmotionMatch = string.Empty;
        }

        private void OpenAddSkill()
        {
            AddSkillView addSkillView = new AddSkillView();
            addSkillView.Show();
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
                return skill.Name.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                    skill.Description.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                    skill.EmotionMatch.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                    skill.Module.Name.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                    skill.Module.Description.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                    skill.Level.ToString().Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        private bool CanAddSkill() => !string.IsNullOrWhiteSpace(Name);
        private bool CanOpenAddSkill() => true;
        private bool CanSaveSkill() => SelectedSkill != null;
        private bool CanDeleteSkill() => SelectedSkill != null;       
    }
}