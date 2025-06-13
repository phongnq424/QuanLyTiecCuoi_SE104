using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.ViewModel.MonAn;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyTiecCuoi.MVVM.View.MonAn
{
    public partial class ChonMonAn : Page
    {
        private readonly ChonMonAnViewModel _viewModel;

        public ChonMonAn()
        {
            InitializeComponent();
            _viewModel = new ChonMonAnViewModel();
            this.DataContext = _viewModel;
        }

        private void MonAn_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is MONAN monAn)
            {
                _viewModel.ChonMonAn(monAn);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LuuChiTietMenu();
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && (tb.Text == "Tên món ăn" || tb.Text == "Đơn giá"))
            {
                tb.Text = "";
                tb.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    if (tb.Name == "txtSearchName")
                    {
                        tb.Text = "Tên món ăn";
                        tb.Foreground = System.Windows.Media.Brushes.Gray;
                        _viewModel.TuKhoaTen = "";
                    }
                    else if (tb.Name == "txtSearchPrice")
                    {
                        tb.Text = "Đơn giá";
                        tb.Foreground = System.Windows.Media.Brushes.Gray;
                        _viewModel.TuKhoaGia = "";
                    }
                }
                else
                {
                    if (tb.Name == "txtSearchName")
                        _viewModel.TuKhoaTen = tb.Text;
                    else if (tb.Name == "txtSearchPrice")
                        _viewModel.TuKhoaGia = tb.Text;
                }
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
                window.WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
                window.WindowState = (window.WindowState == WindowState.Maximized)
                    ? WindowState.Normal
                    : WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
                window.Close();
        }
    }
}
