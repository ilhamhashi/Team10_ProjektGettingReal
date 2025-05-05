
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
        private string name;
        private string email;
        private string password;
        private string userName;
        private Roles role;
        private bool isAdmin;

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

        public ObservableCollection<User> Users { get; } = [];

        public ICommand AddUserCommand { get; }

        public MainWindowViewModel()
        {
            //List<User> usersFromFile = userRepository.GetAllUsers().ToList();
            //foreach (var user in usersFromFile)
            //{
            //    new User { Name = user.Name,  };
            //    Users.Add(user);
            //}
            AddUserCommand = new RelayCommand(AddUser, CanAddUser);
        }

        private void AddUser()
        {
            var user = new User { Name = name, Email = email, Password = password, UserName = userName, Role = role, IsAdmin = isAdmin };
            Users.Add(user);
            userRepository.AddUser(user);

            // Simpel dialogboks som bekræftelse
            MessageBox.Show($"Bruger '{user.Name}' added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

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
