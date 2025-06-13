using QuanLyTiecCuoi.Data.Models;
using System.Windows;
using System.Windows.Input;

namespace QuanLyTiecCuoi.MVVM.View.MonAn
{
    public partial class ChiTietTC : Window
    {
        public ChiTietTC(MONAN monAn)
        {
            InitializeComponent();
            this.DataContext = monAn;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
