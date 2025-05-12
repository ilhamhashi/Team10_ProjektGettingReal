using System.Windows;
using DATApp.MVVM.ViewModel;

namespace DATApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for Note.xaml
    /// </summary>
    public partial class Note : Window
    {
        public Note()
        {
            InitializeComponent();
            DataContext = new NotesViewModel();
        }
    }
}
