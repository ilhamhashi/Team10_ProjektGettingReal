using System.Windows;
using DATApp.MVVM.View;

namespace DATApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BaseMenuBar_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}