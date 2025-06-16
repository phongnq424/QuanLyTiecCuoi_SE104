using QuanLyTiecCuoi.MVVM.ViewModel.DichVu;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.Model;
using System.Windows;
using System.Windows.Controls;
using QuanLyTiecCuoi.MVVM.View.MonAn;

namespace QuanLyTiecCuoi.MVVM.View.DichVu
{
    public partial class TuyChinhDichVu : Page
    {
        private readonly TuyChinhDichVuViewModel _viewModel;

        public TuyChinhDichVu()
        {
            InitializeComponent();
            _viewModel = new TuyChinhDichVuViewModel();
            this.DataContext = _viewModel;
        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is DICHVU dv)
            {
                _viewModel?.XoaDichVu(dv);
            }
        }

        private void BtnChiTiet_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is DICHVU dv)
            {
                var window = new ChiTietTC(dv);
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.ShowDialog();
            }
        }

        private void BtnChinhSua_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is DICHVU dv)
            {
                var window = new SuaDichVu(dv);
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.ShowDialog();
            }
        }

        private void BtnThemDichVu_Click(object sender, RoutedEventArgs e)
        {
            var window = new ThemDichVu();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }
        private void txtSearchPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb && _viewModel != null)
            {
                if (decimal.TryParse(tb.Text, out decimal gia))
                {
                    _viewModel.GiaMin = gia;
                    _viewModel.TuKhoaGia = tb.Text;
                }
                else
                {
                    _viewModel.GiaMin = null;
                    _viewModel.TuKhoaGia = "";
                }
            }
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
                        if (_viewModel != null) _viewModel.TuKhoaTen = "";
                    }
                    else if (tb.Name == "txtSearchPrice")
                    {
                        tb.Text = "Đơn giá";
                        tb.Foreground = System.Windows.Media.Brushes.Gray;
                        if (_viewModel != null)
                        {
                            _viewModel.TuKhoaGia = "";
                            _viewModel.GiaMin = null;
                        }
                    }
                }
                else
                {
                    if (_viewModel != null)
                    {
                        if (tb.Name == "txtSearchName")
                        {
                            _viewModel.TuKhoaTen = tb.Text;
                        }
                        else if (tb.Name == "txtSearchPrice")
                        {
                            if (int.TryParse(tb.Text, out int gia))
                            {
                                _viewModel.GiaMin = gia;
                                _viewModel.TuKhoaGia = tb.Text;
                            }
                            else
                            {
                                _viewModel.GiaMin = null;
                                _viewModel.TuKhoaGia = "";
                            }
                        }
                    }
                }
            }
        }
    }
}
