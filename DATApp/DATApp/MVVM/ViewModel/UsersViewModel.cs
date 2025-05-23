using DATApp.Core;
using DATApp.MVVM.Model.Classes;
using DATApp.MVVM.Model.Repositories;
using DATApp.MVVM.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;


namespace DATApp.MVVM.ViewModel
{
    public class UsersViewModel : ViewModelBase
    {
        private readonly IUserRepository userRepository = new FileUserRepository("users.txt");
        private ObservableCollection<User> Users { get; }
        public ICollectionView UsersCollectionView { get; }

        private string name;
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        private string email;
        public string Email
        {
            get => email;
            set { email = value; OnPropertyChanged(); }
        }

        private string password;
        public string Password
        {
            get => password;
            set { password = value; OnPropertyChanged(); }
        }

        private bool isAdmin;
        public bool IsAdmin
        {
            get => isAdmin;
            set { isAdmin = value; OnPropertyChanged(); }
        }

        private User selectedUser;
        public User SelectedUser
        {
            get => selectedUser;
            set { selectedUser = value; OnPropertyChanged(); }
        }

        private string _searchTerm = string.Empty;
        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                OnPropertyChanged(nameof(UsersFilter));
                UsersCollectionView.Refresh();
            }
        }
        public ICommand SaveUserCommand { get; }
        public ICommand AddUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand ValidateUserLoginCommand { get; }

        public UsersViewModel()
        {
            Users = new ObservableCollection<User>(userRepository.GetAll());
            UsersCollectionView = CollectionViewSource.GetDefaultView(Users);
            UsersCollectionView.Filter = UsersFilter;

            AddUserCommand = new RelayCommandUser(AddUser, CanAddUser);
            SaveUserCommand = new RelayCommandUser(SaveUser, CanSaveUser);
            ValidateUserLoginCommand = new RelayCommandUser(ValidateUserLogin, CanLoginUser);
            DeleteUserCommand = new RelayCommandUser(DeleteUser, CanDeleteUser);

        }

        private void AddUser()
        {
            var user = new User { Name = name, Email = email, Password = password, IsAdmin = isAdmin };
            if (Users.Where(u => u.Email == user.Email).Any())
            {
                MessageBox.Show($"Kunne ikke tilføjes. Bruger eksisterer i forvejen", "Fejl", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Users.Add(user);
                userRepository.AddUser(user);
                MessageBox.Show($"Bruger '{user.Name}' oprettet!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);

                Name = string.Empty;
                Email = string.Empty;
                Password = string.Empty;
                IsAdmin = false;
            }
        }

        private void SaveUser()
        {
                userRepository.UpdateUser(SelectedUser);
                MessageBox.Show($"Ændringerne er gemt", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
                SelectedUser = null;
        }

        private void DeleteUser()
        {
                userRepository.DeleteUser(SelectedUser);
                Users.Remove(SelectedUser);
                MessageBox.Show($"Brugeren er slettet!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
                SelectedUser = null;
        }

        public void ValidateUserLogin()
        {
            bool loginValue = userRepository.ValidateUserLogin(Email, Password);
            MainWindowViewModel.CurrentUser = userRepository.GetUser(Email);

            if (loginValue && MainWindowViewModel.CurrentUser.IsAdmin)
            {
                MessageBox.Show($"Login valideret. Tryk på fortsæt.", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (loginValue && MainWindowViewModel.CurrentUser.IsAdmin == false)
            {
                MessageBox.Show($"Login valideret. Tryk på fortsæt.", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"E-mail og/eller adgangskode er forkert. Prøv venligst igen", "Ugyldig", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool UsersFilter(object obj)
        {
            if (obj is User user)
            {
                return user.Name.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                    user.Email.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        private bool CanAddUser() => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
        private bool CanOpenAddUser() => true;
        private bool CanSaveUser() => SelectedUser != null;
        private bool CanDeleteUser() => SelectedUser != null;
        private bool CanLoginUser() => !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);

    }
}
