using DATApp.Core;
using DATApp.MVVM.Model.Classes;
using DATApp.MVVM.View;
using DATApp.MVVM.View.Controls;
using DATApp.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

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
            FortsætButton = this.fortsætButton;
        }

        public static Button FortsætButton;

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindowViewModel.CurrentUser.Name != null)
            {
                FortsætButton.Visibility = Visibility.Collapsed;
            }
            else
                FortsætButton.Visibility = Visibility.Visible;
        }
    }
}