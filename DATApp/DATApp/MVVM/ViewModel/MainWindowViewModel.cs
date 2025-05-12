
using DATApp.Core;

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

        public HomeViewModel HomeVM { get; set; }
        public UsersViewModel UsersVM { get; set; }
        public ModulesViewModel ModulesVM { get; set; }
        public SkillsViewModel SkillsVM { get; set; }
        public DatMatchViewModel DatMatchVM { get; set; }
        public NotesViewModel NotesVM { get; set; }
        public MyAccountViewModel MyAccountVM { get; set; }

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


//< Button Grid.Column = "1" Content = "Log ind" HorizontalAlignment = "Left" Margin = "564,0,0,0" VerticalAlignment = "Center" Click = "Button_Click" Height = "29" Width = "72" />

//            < Button Name = "CloseButton" Content = "Luk" Click = "CloseButton_Click" Grid.Column = "1" Margin = "647,23,15,23" />
            