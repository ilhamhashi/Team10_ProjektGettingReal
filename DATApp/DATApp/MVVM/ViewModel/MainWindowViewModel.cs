
using DATApp.Core;
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
        public RelayCommand DatMatchViewCommand { get; set; }
        public RelayCommand MyAccountViewCommand { get; set; }
        public RelayCommand NotesViewCommand { get; set; }
        public RelayCommand LoginViewCommand { get; set; }
        public RelayCommand BaseMenuViewCommand { get; set; }
        public RelayCommand ClientMenuViewCommand { get; set; }
        public RelayCommand AdminMenuViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public UsersViewModel UsersVM { get; set; }
        public ModulesViewModel ModulesVM { get; set; }
        public SkillsViewModel SkillsVM { get; set; }
        public DatMatchViewModel DatMatchVM { get; set; }
        public NotesViewModel NotesVM { get; set; }
        public MyAccountViewModel MyAccountVM { get; set; }
        public LoginView LoginView { get; set; }
        public BaseMenuBar BaseMenuView { get; set; }
        public ClientMenuBar ClientMenuView { get; set; }
        public AdminMenuBar AdminMenuView { get; set; }

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

        private object _currentMenu;

        public object CurrentMenu
        {
            get { return _currentMenu; }
            set
            {
                _currentMenu= value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            HomeVM = new HomeViewModel();
            UsersVM = new UsersViewModel();
            ModulesVM =  new ModulesViewModel();
            SkillsVM = new SkillsViewModel();
            DatMatchVM = new DatMatchViewModel();
            NotesVM = new NotesViewModel();
            MyAccountVM = new MyAccountViewModel();
            LoginView = new LoginView();
            BaseMenuView = new BaseMenuBar();
            ClientMenuView = new ClientMenuBar();
            AdminMenuView = new AdminMenuBar();


            CurrentView = HomeVM;
            CurrentMenu = BaseMenuView;


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
                CurrentView = ModulesVM;
            });

            SkillsViewCommand = new RelayCommand(o =>
            {
                CurrentView = SkillsVM;
            });

            DatMatchViewCommand = new RelayCommand(o =>
            {
                CurrentView = DatMatchVM;
            });

            NotesViewCommand = new RelayCommand(o =>
            {
                CurrentView = NotesVM;
            });

            MyAccountViewCommand = new RelayCommand(o =>
            {
                CurrentView = MyAccountVM;
            });

            LoginViewCommand = new RelayCommand(o =>
            {
                LoginView.Show();
            });

            BaseMenuViewCommand = new RelayCommand(o =>
            {
                CurrentMenu = BaseMenuView;
            });

            ClientMenuViewCommand = new RelayCommand(o =>
            {
                CurrentMenu = ClientMenuView;
            });

            AdminMenuViewCommand = new RelayCommand(o =>
            {
                CurrentMenu = AdminMenuView;
            });

        }
    }
}

            