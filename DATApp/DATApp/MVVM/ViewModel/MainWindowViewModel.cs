
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
       

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string _name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_name));
    }
}
