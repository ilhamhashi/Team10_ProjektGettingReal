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
    public class ModulesViewModel : ViewModelBase
    {
        private readonly FileModuleRepository moduleRepository = new FileModuleRepository("modules.txt");
        public ObservableCollection<Module> Modules;

        private string number;
        public string Number
        {
            get => number;
            set { number = value; OnPropertyChanged(); }
        }

        private string name;
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        private string description;
        public string Description
        {
            get => description;
            set { description = value; OnPropertyChanged(); }
        }

        private Module selectedModule;
        public Module SelectedModule
        {
            get => selectedModule;
            set { selectedModule = value; OnPropertyChanged(); }
        }

        private string searchTerm = string.Empty;
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

        public ICommand OpenAddModuleCommand { get; }
        public ICommand SaveModuleCommand { get; }
        public ICommand AddModuleCommand { get; }
        public ICommand DeleteModuleCommand { get; }
        public static ICollectionView ModulesCollectionView { get; set; }

        public ModulesViewModel()
        {
            Modules = new ObservableCollection<Module>(moduleRepository.GetAll());
            ModulesCollectionView = CollectionViewSource.GetDefaultView(Modules);

            AddModuleCommand = new RelayCommandUser(AddModule, CanAddModule);
            SaveModuleCommand = new RelayCommandUser(SaveModule, CanSaveModule);
            OpenAddModuleCommand = new RelayCommandUser(OpenAddModule, CanOpenAddModule);
            DeleteModuleCommand = new RelayCommandUser(DeleteModule, CanDeleteModule);
        }

        private void AddModule()
        {
            var module = new Module { Number = number, Name = name, Description = description};
            
            Modules.Add(module);
            moduleRepository.AddModule(module);

            // Dialogboks som bekræftelse
            MessageBox.Show($"Modul '{module.Name}' oprettet!", "Tilføjet", MessageBoxButton.OK, MessageBoxImage.Information);
            // Tekstfelter ryddes for indhold
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
            // Dialogboks som bekræftelse
            MessageBox.Show($"Modul '{SelectedModule.Name}' Rettet!", "Redigeret", MessageBoxButton.OK, MessageBoxImage.Information);
            // Tekstfelter ryddes for indhold
            SelectedModule = null;
        }

        private void DeleteModule()
        {
            moduleRepository.DeleteModule(SelectedModule);
            Modules.Remove(SelectedModule);
            // Dialogboks som bekræftelse
            MessageBox.Show($"Modul '{Name}' slettet!", "Fjernet", MessageBoxButton.OK, MessageBoxImage.Information);
            // Tekstfelter ryddes for indhold
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

        private bool CanAddModule() => !string.IsNullOrWhiteSpace(Name);
        private bool CanOpenAddModule() => true;
        private bool CanSaveModule() => SelectedModule != null;
        private bool CanDeleteModule() => SelectedModule != null;
    }
}
