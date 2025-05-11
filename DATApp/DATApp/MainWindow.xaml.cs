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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LogInd logInd = new LogInd();
            logInd.Show();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Brugere brugere = new Brugere();
            brugere.Show();
        }
    }
}