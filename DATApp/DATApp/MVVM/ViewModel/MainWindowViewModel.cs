using DATApp.Core;
using DATApp.MVVM.Model.Classes;
using DATApp.MVVM.View;
using DATApp.MVVM.View.Controls;

namespace DATApp.MVVM.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public RelayCommand HomeViewCommand {  get; set; }
        public RelayCommand UsersViewCommand { get; set; }
        public RelayCommand ModulesViewCommand { get; set; }
        public RelayCommand SkillsViewCommand { get; set; }
        public RelayCommand MyAccountViewCommand { get; set; }
        public RelayCommand NotesViewCommand { get; set; }
        public RelayCommand LoginViewCommand { get; set; }
        public RelayCommand LogoutViewCommand { get; set; }

        public RelayCommand BaseViewCommand { get; set; }
        public RelayCommand MenuViewCommand { get; set; }
        public RelayCommand ClientViewCommand { get; set; }
        public RelayCommand CloseMainWindowCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public UsersViewModel UsersVM { get; set; }
        public ModulesViewModel ModulesVM { get; set; }
        public AdminModulesViewModel AdminModulesVM { get; set; }
        public AdminSkillsViewModel AdminSkillsVM { get; set; }
        public SkillsViewModel SkillsVM { get; set; }
        public LoginViewModel LoginVM { get; set; }
        public NotesViewModel NotesVM { get; set; }
        public MyAccountViewModel MyAccountVM { get; set; }
        public LoginView LoginView { get; set; }


        public BaseMenuBar BaseMenuView { get; set; }
        public ClientMenuBar ClientMenuView { get; set; }
        public AdminMenuBar AdminMenuView { get; set; }


        private static User _currentUser;

        public static User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value; 
            }
        }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        private object _currentMenuView;

        public object CurrentMenuView
        {
            get { return _currentMenuView; }
            set
            {
                _currentMenuView = value;
                OnPropertyChanged();
            }
        }


        public MainWindowViewModel()
        {
            HomeVM = new HomeViewModel();
            UsersVM = new UsersViewModel();
            ModulesVM = new ModulesViewModel();
            AdminModulesVM = new AdminModulesViewModel();
            AdminSkillsVM = new AdminSkillsViewModel();
            SkillsVM = new SkillsViewModel();
            LoginVM = new LoginViewModel();
            NotesVM = new NotesViewModel();
            MyAccountVM = new MyAccountViewModel();
            BaseMenuView = new BaseMenuBar();
            ClientMenuView  = new ClientMenuBar();
            AdminMenuView  = new AdminMenuBar();
            CurrentUser = new User();

            CurrentMenuView = BaseMenuView;
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            UsersViewCommand = new RelayCommand(o =>
            {
                CurrentView = UsersVM;
            });

            ModulesViewCommand = new RelayCommand(o =>
            {
                if (CurrentUser == null || CurrentUser.IsAdmin == false)
                    CurrentView = ModulesVM;
                else
                    CurrentView = AdminModulesVM;
            });

            SkillsViewCommand = new RelayCommand(o =>
            {
                if (CurrentUser == null || CurrentUser.IsAdmin == false)
                    CurrentView = SkillsVM;
                else
                    CurrentView = AdminSkillsVM;
            });

            LoginViewCommand = new RelayCommand(o =>
            {
                CurrentView = LoginVM;
            });

            LogoutViewCommand = new RelayCommand(o =>
            {
                CurrentUser = null;
                CurrentView = HomeVM;
                CurrentMenuView = BaseMenuView;
            });


            NotesViewCommand = new RelayCommand(o =>
            {
                CurrentView = NotesVM;
            });


            MenuViewCommand = new RelayCommand(o =>
            {
                if (CurrentUser == null || CurrentUser.Name == null)
                {
                    CurrentMenuView = BaseMenuView;
                }

                else if (CurrentUser.IsAdmin)
                {
                    CurrentMenuView = AdminMenuView;
                    CurrentView = HomeVM;
                }
                else if (CurrentUser.IsAdmin == false)
                {
                    CurrentMenuView = ClientMenuView;
                    CurrentView = HomeVM;
                }
                
            });

            ClientViewCommand = new RelayCommand(o =>
            {
                CurrentMenuView = ClientMenuView;
            });

            BaseViewCommand = new RelayCommand(o =>
            {
                CurrentMenuView = BaseMenuView;
            });

            CloseMainWindowCommand = new RelayCommand(o =>
            {
                System.Windows.Application.Current.Shutdown();
            });

        }
    }
}

            