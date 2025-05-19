using QuanLyTiecCuoi.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyTiecCuoi.MVVM.View.Login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel();
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void toggleShow_Checked(object sender, RoutedEventArgs e)
        {
            tbPasswordVisible.Text = Passwbox.Password;
            tbPasswordVisible.Visibility = Visibility.Visible;
            Passwbox.Visibility = Visibility.Collapsed; 
        }

        private void toggleShow_Unchecked(object sender, RoutedEventArgs e)
        {
            Passwbox.Password = tbPasswordVisible.Text;
            Passwbox.Visibility = Visibility.Visible;
            tbPasswordVisible.Visibility = Visibility.Collapsed;

        }
    }
}
