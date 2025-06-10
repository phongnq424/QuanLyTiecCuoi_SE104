using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.ViewModel.DichVu;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyTiecCuoi.MVVM.View.DichVu
{
    public partial class ChonDichVu : Page
    {
        private ChonDichVuViewModel _viewModel;

        public ChonDichVu()
        {
            InitializeComponent();
            _viewModel = new ChonDichVuViewModel();
            this.DataContext = _viewModel;
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && (tb.Text == "Tên dịch vụ" || tb.Text == "Đơn giá"))
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
                        tb.Text = "Tên dịch vụ";
                        tb.Foreground = System.Windows.Media.Brushes.Gray;
                        _viewModel.TuKhoaTimTen = "";
                    }
                    else if (tb.Name == "txtSearchPrice")
                    {
                        tb.Text = "Đơn giá";
                        tb.Foreground = System.Windows.Media.Brushes.Gray;
                        _viewModel.TuKhoaTimGia = "";
                    }
                }
                else
                {
                    if (tb.Name == "txtSearchName")
                        _viewModel.TuKhoaTimTen = tb.Text;
                    else if (tb.Name == "txtSearchPrice")
                        _viewModel.TuKhoaTimGia = tb.Text;
                }
            }
        }

        private void DichVu_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is DICHVU dv)
            {
                _viewModel.ChonDichVu(dv);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Đã chọn {_viewModel.DichVuDaChon.Count} dịch vụ!", "Xác nhận");
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.WindowState = WindowState.Minimized;
            }

        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.WindowState = (window.WindowState == WindowState.Maximized)
                    ? WindowState.Normal
                    : WindowState.Maximized;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }
    }
}
