using System.Windows;
using DATApp.MVVM.ViewModel;

namespace DATApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(); // Knyt ViewModel til View
        }
    }
}
