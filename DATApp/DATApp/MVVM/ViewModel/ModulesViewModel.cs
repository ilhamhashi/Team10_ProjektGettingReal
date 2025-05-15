using DATApp.MVVM.Model.Classes;
using DATApp.MVVM.Model.Repositories;
using DATApp.Core;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DATApp.MVVM.View;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;

namespace DATApp.MVVM.ViewModel
{
    class ModulesViewModel : ViewModelBase
    {
        private readonly FileModuleRepository moduleRepository = new FileModuleRepository("modules.txt");
        private int moduleNumber;
        private string name;
        private string description;
        private Module selectedModule;
        private string searchTerm = string.Empty;
        private readonly ObservableCollection<Module> Modules;
        public ICollectionView ModulesCollectionView { get; }

        public string SearchTerm
        {
            get { return searchTerm; }
            set
            {
                searchTerm = value;
                OnPropertyChanged(nameof(ModulesFilter));
                ModulesCollectionView.Refresh();
            }
        }

        public int ModuleNumber
        {
            get => moduleNumber;
            set { moduleNumber = value; OnPropertyChanged(); }
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

        public Module SelectedModule
        {
            get => selectedModule;
            set { selectedModule = value; OnPropertyChanged(); }
        }

        public ICommand OpenAddModuleCommand { get; }
        public ICommand SaveModuleCommand { get; }
        public ICommand AddModuleCommand { get; }
        public ICommand DeleteModuleCommand { get; }
        public ICommand SearchModuleCommand { get; }

        public ModulesViewModel()
        {
            Modules = new ObservableCollection<Module>(moduleRepository.GetAllModules());
            ModulesCollectionView = CollectionViewSource.GetDefaultView(Modules);
            ModulesCollectionView.Filter = ModulesFilter;

            AddModuleCommand = new RelayCommandUser(AddModule, CanAddModule);
            SaveModuleCommand = new RelayCommandUser(SaveModule, CanSaveModule);
            OpenAddModuleCommand = new RelayCommandUser(OpenAddModule, CanOpenAddModule);
            DeleteModuleCommand = new RelayCommandUser(DeleteModule, CanDeleteModule);
        }

        private void AddModule()
        {
            int modulescount = Modules.Count();
            var module = new Module { ModuleNumber = moduleNumber, Name = name, Description = description};
            
            Modules.Add(module);
            moduleRepository.AddModule(module);

            // Simpel dialogboks som bekræftelse
            MessageBox.Show($"Modul '{module.Name}' oprettet!", "Tilføjet", MessageBoxButton.OK, MessageBoxImage.Information);

            Name = string.Empty;
            Description = string.Empty;            
        }

        private void OpenAddModule()
        {
            AddModuleView addModuleView = new AddModuleView();
            addModuleView.Show();
        }

        private void SaveModule()
        {
            moduleRepository.UpdateModule(SelectedModule);
            MessageBox.Show($"Module '{SelectedModule.Name}' Rettet!", "Redigeret", MessageBoxButton.OK, MessageBoxImage.Information);
            SelectedModule = null;
        }

        private void DeleteModule()
        {
            moduleRepository.DeleteModule(SelectedModule);
            Modules.Remove(SelectedModule);
            MessageBox.Show($"Bruger '{Name}' slettet!", "Fjernet", MessageBoxButton.OK, MessageBoxImage.Information);
            SelectedModule = null;
        }

        private bool ModulesFilter(object obj)
        {
            if (obj is Module module)
            {
                return module.Name.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                    module.Description.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        private bool CanAddModule() => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Description);
        private bool CanOpenAddModule() => true;
        private bool CanSaveModule() => SelectedModule != null;
        private bool CanDeleteModule() => SelectedModule != null;
        private bool CanSearchModule() => true;

    }
}
