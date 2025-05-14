using System.Collections.ObjectModel;
using DATApp.MVVM.Model.Classes;
using DATApp.MVVM.Model.Repositories;
using System.Windows.Input;
using System.Windows;
using DATApp.Core;
using DATApp.MVVM.View;
using DATApp.MVVM.ViewModel;

namespace DATApp.MVVM.ViewModel
{
    class UsersViewModel : ViewModelBase
    {
        private readonly IUserRepository userRepository = new FileUserRepository("users.txt");
        private string name;
        private string email;
        private string password;
        private bool isAdmin;
        private User selectedUser;

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
        public bool IsAdmin
        {
            get => isAdmin;
            set { isAdmin = value; OnPropertyChanged(); }
        }

        public User SelectedUser
        {
            get => selectedUser;
            set { selectedUser = value; OnPropertyChanged(); }
        }

        public ObservableCollection<User> Users { get; } = [];

        public ICommand AddUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand ValidateUserLoginCommand { get; }

        public UsersViewModel()
        {
            List<User> usersFromFile = userRepository.GetAllUsers().ToList();
            foreach (var user in usersFromFile)
            {
                new User { Name = user.Name, Email = user.Email, Password = user.Password, IsAdmin = user.IsAdmin };
                Users.Add(user);
            }
            AddUserCommand = new RelayCommandUser(AddUser, CanAddUser);
            ValidateUserLoginCommand = new RelayCommandUser(ValidateUserLogin, CanLoginUser);
            //DeleteUserCommand = new RelayCommandUser(DeleteUser);
        }

        private void AddUser()
        {
            var user = new User { Name = name, Email = email, Password = password, IsAdmin = isAdmin };
            Users.Add(user);
            userRepository.AddUser(user);

            // Simpel dialogboks som bekræftelse
            MessageBox.Show($"Bruger '{user.Name}' oprettet!", "Tilføjet", MessageBoxButton.OK, MessageBoxImage.Information);

            Name = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            IsAdmin = false;
        }

        private void DeleteUser()
        {
            var user = new User { Name = name, Email = email, Password = password, IsAdmin = isAdmin };
            Users.Add(user);
            userRepository.AddUser(user);

            // Simpel dialogboks som bekræftelse
            MessageBox.Show($"Bruger '{user.Name}' slettet!", "Fjernet", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void ValidateUserLogin()
        {
            bool loginValye = userRepository.ValidateUserLogin(Email, Password);
            if (loginValye && IsAdmin)
            {
                MessageBox.Show($"Login Succesful. You are signed in as Admin", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (loginValye && IsAdmin == false)
            {
                MessageBox.Show($"Login Succesful. You are signed in as Client", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"Login invalid. Try again", "Failed", MessageBoxButton.OK, MessageBoxImage.Information);
            }


        }

        private bool CanAddUser() => !string.IsNullOrWhiteSpace(Name);
        private bool CanLoginUser() => !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);

    }
}
