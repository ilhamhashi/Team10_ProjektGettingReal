
using DATApp.Core;
using DATApp.MVVM.View;

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

        public HomeViewModel HomeVM { get; set; }
        public UsersViewModel UsersVM { get; set; }
        public ModulesViewModel ModulesVM { get; set; }
        public SkillsViewModel SkillsVM { get; set; }
        public DatMatchViewModel DatMatchVM { get; set; }
        public NotesViewModel NotesVM { get; set; }
        public MyAccountViewModel MyAccountVM { get; set; }
        public LoginViewModel LoginVM { get; set; }

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

        public MainWindowViewModel()
        {
            HomeVM = new HomeViewModel();
            UsersVM = new UsersViewModel();
            ModulesVM =  new ModulesViewModel();
            SkillsVM = new SkillsViewModel();
            DatMatchVM = new DatMatchViewModel();
            NotesVM = new NotesViewModel();
            MyAccountVM = new MyAccountViewModel();
            LoginVM = new LoginViewModel();


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

        }
    }
}

            