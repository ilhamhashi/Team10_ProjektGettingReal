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

        public ICommand OpenAddNoteCommand { get; }
        public ICommand AddSkillCommand { get; }
        public ICommand SaveSkillCommand { get; }
        public ICommand DeleteSkillCommand { get; }


        public SkillsViewModel()
        {
            Skills = new ObservableCollection<Skill>(skillRepository.GetAll());
            SkillsCollectionView = CollectionViewSource.GetDefaultView(Skills);
            SkillsCollectionView.Filter = SkillsFilter;

            ModulesCollectionView = ModulesViewModel.ModulesCollectionView;

            OpenAddNoteCommand = new RelayCommandUser(OpenAddNote, CanOpenAddNote);
            AddSkillCommand = new RelayCommandUser(AddSkill, CanAddSkill);
            SaveSkillCommand = new RelayCommandUser(SaveSkill, CanSaveSkill);
            DeleteSkillCommand = new RelayCommandUser(DeleteSkill, CanDeleteSkill);
        }


        private void AddSkill()
        {
            var skill = new Skill { Number = _number, Name = _name, Purpose = _purpose, Description = _description, Module = _module };
            if (Skills.Where(s => s.Name == skill.Name).Any())
            {
                MessageBox.Show($"Kunne ikke tilføjes. Færdighed eksisterer i forvejen", "Fejl", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Skills.Add(skill);
                skillRepository.AddSkill(skill);
                // Simpel dialogboks som bekræftelse
                MessageBox.Show($"Færdighed '{skill.Name}' oprettet!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);

                Name = string.Empty;
                Purpose = string.Empty;
                Description = string.Empty;
                Module = null;
            }
        }

        private void OpenAddNote()
        {
            AddNoteView addNoteView = new AddNoteView();
            addNoteView.Show();
        }

        private void SaveSkill()
        {
            if (Skills.Where(s => s.Name == SelectedSkill.Name).Any())
            {
                MessageBox.Show($"Kunne ikke gemmes. Færdighed eksisterer i forvejen", "Fejl", MessageBoxButton.OK, MessageBoxImage.Information);
                SelectedSkill = null;
            }
            else
            {
                skillRepository.UpdateSkill(SelectedSkill);
                MessageBox.Show($"Færdighed '{SelectedSkill.Name}' Rettet!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
                SelectedSkill = null;
            }
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

        private bool CanOpenAddNote() => MainWindowViewModel.CurrentUser != null;
        private bool CanAddSkill() => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Purpose) && !string.IsNullOrWhiteSpace(Description) && Module != null;
        private bool CanSaveSkill() => SelectedSkill != null;
        private bool CanDeleteSkill() => SelectedSkill != null;
    }
}