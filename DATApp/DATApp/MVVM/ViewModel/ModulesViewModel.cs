using DATApp.MVVM.Model.Classes;
using DATApp.MVVM.Model.Repositories;
using DATApp.Core;
using System.Collections.ObjectModel;
using System.Windows.Input;
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

        public ICommand SaveModuleCommand { get; }
        public ICommand AddModuleCommand { get; }
        public ICommand DeleteModuleCommand { get; }
        public static ICollectionView ModulesCollectionView { get; set; }

        public ModulesViewModel()
        {
            Modules = new ObservableCollection<Module>(moduleRepository.GetAll());
            ModulesCollectionView = CollectionViewSource.GetDefaultView(Modules);
            ModulesCollectionView.Filter = ModulesFilter;

            AddModuleCommand = new RelayCommandUser(AddModule, CanAddModule);
            SaveModuleCommand = new RelayCommandUser(SaveModule, CanSaveModule);
            DeleteModuleCommand = new RelayCommandUser(DeleteModule, CanDeleteModule);
        }

        private void AddModule()
        {
            var module = new Module { Number = number, Name = name, Description = description};
            if (Modules.Where(m => m.Name == module.Name).Any())
            {
                MessageBox.Show($"Kunne ikke tilføjes. Modulet eksisterer i forvejen", "Fejl", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Modules.Add(module);
                moduleRepository.AddModule(module);
                MessageBox.Show($"Modul '{module.Name}' oprettet!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);

                // Tekstfelter ryddes for indhold
                Name = string.Empty;
                Description = string.Empty;
            }     
        }

        private void SaveModule()
        {
            if (Modules.Where(m => m.Name == SelectedModule.Name).Any())
            {
                MessageBox.Show($"Kunne ikke gemmes. Modul eksisterer i forvejen", "Fejl", MessageBoxButton.OK, MessageBoxImage.Information);
                SelectedModule = null;
            }
            else
            {
                moduleRepository.UpdateModule(SelectedModule);
                MessageBox.Show($"Ændringer gemt!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
                // Tekstfelter ryddes for indhold
                SelectedModule = null;
            }
        }

        private void DeleteModule()
        {
            moduleRepository.DeleteModule(SelectedModule);
            Modules.Remove(SelectedModule);
            // Dialogboks som bekræftelse
            MessageBox.Show($"Modul slettet!", "Udfør", MessageBoxButton.OK, MessageBoxImage.Information);
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
        private bool CanSaveModule() => SelectedModule != null;
        private bool CanDeleteModule() => SelectedModule != null;
    }
}
