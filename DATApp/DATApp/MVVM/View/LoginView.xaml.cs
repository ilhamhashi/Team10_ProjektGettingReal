using DATApp.MVVM.Model.Repositories;
using System.Windows;
using System.Windows.Controls;


namespace DATApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            ValidateButton = this.validateButton;
        }

        public static Button ValidateButton;

        private void validateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.FortsætButton.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
