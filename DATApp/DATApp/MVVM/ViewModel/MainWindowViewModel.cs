using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows;
using System.Xml.Linq;
using DATApp.Core;

namespace DATApp.MVVM.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public RelayCommand HomeViewCommand {  get; set; }
        public RelayCommand UsersViewCommand { get; set; }
        public HomeViewModel HomeVM { get; set; }
        public UsersViewModel UsersVM { get; set; }

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
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            UsersViewCommand = new RelayCommand(o =>
            {
                CurrentView = UsersVM;
            });
        }
    }
}


//< Button Grid.Column = "1" Content = "Log ind" HorizontalAlignment = "Left" Margin = "564,0,0,0" VerticalAlignment = "Center" Click = "Button_Click" Height = "29" Width = "72" />

//            < Button Name = "CloseButton" Content = "Luk" Click = "CloseButton_Click" Grid.Column = "1" Margin = "647,23,15,23" />
            