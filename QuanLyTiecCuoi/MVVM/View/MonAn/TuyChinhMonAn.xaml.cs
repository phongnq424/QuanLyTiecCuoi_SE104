using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.ViewModel.MonAn;

namespace QuanLyTiecCuoi.MVVM.View.MonAn
{
    public partial class TuyChinhMonAn : Page
    {
        private readonly TuyChinhMonAnViewModel _viewModel;

        public TuyChinhMonAn()
        {
            InitializeComponent();
            _viewModel = new TuyChinhMonAnViewModel();
            this.DataContext = _viewModel;
        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is MONAN monAn)
            {
                _viewModel?.XoaMonAn(monAn);
            }
        }

        private void BtnChiTiet_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is MONAN monAn)
            {
                var window = new ChiTietTC(monAn);
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.ShowDialog();
            }
        }

        private void BtnChinhSua_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is MONAN monAn)
            {
                var window = new SuaMonAn(monAn);
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.ShowDialog();
            }
        }

        private void BtnThemMonAn_Click(object sender, RoutedEventArgs e)
        {
            var window = new ThemMonAn();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
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
                        if (_viewModel != null) _viewModel.TuKhoaTen = "";
                    }
                    else if (tb.Name == "txtSearchPrice")
                    {
                        tb.Text = "Đơn giá";
                        tb.Foreground = System.Windows.Media.Brushes.Gray;
                        if (_viewModel != null)
                        {
                            _viewModel.GiaMin = null;
                            _viewModel.TuKhoaGia = "";
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
                }
            }
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
    }
}
