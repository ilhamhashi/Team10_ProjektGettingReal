using System.Collections.ObjectModel;
using DATApp.MVVM.Model.Classes;
using DATApp.MVVM.Model.Repositories;
using System.Windows.Input;
using System.Windows;
using DATApp.Core;
using DATApp.MVVM.View;

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

        public ObservableCollection<User> Users { get; }

        public ICommand OpenAddUserCommand { get; }
        public ICommand SaveUserCommand { get; }
        public ICommand AddUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand ValidateUserLoginCommand { get; }

        public UsersViewModel()
        {
            Users = new ObservableCollection<User>(userRepository.GetAllUsers());
            AddUserCommand = new RelayCommandUser(AddUser, CanAddUser);
            SaveUserCommand = new RelayCommandUser(SaveUser, CanSaveUser);
            OpenAddUserCommand = new RelayCommandUser(OpenAddUser, CanOpenAddUser);
            ValidateUserLoginCommand = new RelayCommandUser(ValidateUserLogin, CanLoginUser);
            DeleteUserCommand = new RelayCommandUser(DeleteUser,CanDeleteUser);
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

        private void OpenAddUser()
        {
            AddUserView addUserView = new AddUserView();
            addUserView.Show();
        }

        private void SaveUser()
        {
            userRepository.UpdateUser(SelectedUser);
            MessageBox.Show($"Bruger '{Name}' Rettet!", "Redigeret", MessageBoxButton.OK, MessageBoxImage.Information);
            SelectedUser = null;
        }

        private void DeleteUser()
        {
                userRepository.DeleteUser(SelectedUser);
                Users.Remove(SelectedUser);
                MessageBox.Show($"Bruger '{Name}' slettet!", "Fjernet", MessageBoxButton.OK, MessageBoxImage.Information);
                SelectedUser = null;
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

        private bool CanAddUser() => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
        private bool CanOpenAddUser() => true;
        private bool CanSaveUser() => SelectedUser != null;
        private bool CanDeleteUser() => SelectedUser != null;
        private bool CanLoginUser() => !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);

    }
}
