
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DATApp.MVVM.Model.Repositories;
using DATApp.MVVM.Model.Classes;
using System.Windows.Input;
using System.Windows;

namespace DATApp.MVVM.ViewModel
{    
    public class MainWindowViewModel : INotifyPropertyChanged
    {
          
        private readonly IUserRepository userRepository = new FileUserRepository("users.txt");
        private readonly IModuleRepository moduleRepository = new FileModuleRepository("modules.txt");

        // FELTER TIL BRUGER
        private string name;
        private string email;
        private string password;
        private string userName;
        private Roles role;
        private bool isAdmin;

        // NYT: FELTER TIL SØGNING
        private string searchFeeling;
        public string SearchFeeling
        {
            get => searchFeeling;
            set { searchFeeling = value; OnPropertyChanged(); }
        }

        // PROPERTIES
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => email;
            set { email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => password;
            set { password = value; OnPropertyChanged(); }
        }

        public string Username
        {
            get => userName;
            set { userName = value; OnPropertyChanged(); }
        }

        public Roles Role
        {
            get => role;
            set { role = value; OnPropertyChanged(); }
        }

        public bool IsAdmin
        {
            get => isAdmin;
            set { isAdmin = value; OnPropertyChanged(); }
        }

        public ObservableCollection<User> Users { get; } = new();

        // NYT: Liste over søgte moduler
        public ObservableCollection<Module> SearchedModules { get; } = new();

        // KOMMANDOER
        public ICommand AddUserCommand { get; }
        public ICommand SearchModulesCommand { get; }  // 👈 NY KNAPFUNKTION

        public MainWindowViewModel()
        {
            AddUserCommand = new RelayCommand(AddUser, CanAddUser);
            SearchModulesCommand = new RelayCommand(SearchModules); // 👈 INITIALISER SØG

            // Tilføj STOP modul hvis ikke eksisterer
            if (!moduleRepository.GetAllModules().Any(m => m.ModuleNumber == 1))
            {
                moduleRepository.AddModule(new Module
                {
                    ModuleNumber = 1,
                    Name = "STOP",
                    Description = "Stop op, træk vejret, tænk før du handler",
                    Feelings = new List<string> { "angst", "panik", "stress" }
                });
            }

            // Tilføj Tjek Fakta modul hvis ikke eksisterer
            if (!moduleRepository.GetAllModules().Any(m => m.ModuleNumber == 2))
            {
                moduleRepository.AddModule(new Module
                {
                    ModuleNumber = 2,
                    Name = "Tjek Fakta",
                    Description = "Undersøg om dine tanker passer med virkeligheden",
                    Feelings = new List<string> { "vrede", "sorg", "skam" }
                });
            }
        }

        // NY FUNKTION: Søg moduler via følelse
        private void SearchModules()
        {
            SearchedModules.Clear();
            var found = moduleRepository.FindModulesByFeeling(SearchFeeling);
            foreach (var module in found)
            {
                SearchedModules.Add(module);
            }
        }

        // Tilføj ny bruger
        private void AddUser()
        {
            var user = new User { Name = name, Email = email, Password = password, UserName = userName, Role = role, IsAdmin = isAdmin };
            Users.Add(user);
            userRepository.AddUser(user);

            MessageBox.Show($"Bruger '{user.Name}' tilføjet!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Ryd felter
            Name = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Username = string.Empty;
            Role = Roles.Klient;
            IsAdmin = false;
        }

        private bool CanAddUser() => !string.IsNullOrWhiteSpace(Name);

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string _name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_name));
    }
}