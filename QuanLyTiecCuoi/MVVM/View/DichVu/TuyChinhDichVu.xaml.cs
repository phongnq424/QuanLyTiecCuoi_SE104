using QuanLyTiecCuoi.MVVM.ViewModel.DichVu;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.Model; // nếu có DichVuModel
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
                _viewModel.XoaDichVu(dv);
            }
        }

        private void BtnChiTiet_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is DICHVU dv)
            {
                var window = new ChiTietTC(); // nếu có window riêng
                window.ShowDialog();
            }
        }

        private void BtnChinhSua_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is DICHVU dv)
            {
                var window = new SuaDichVu(dv); // truyền dữ liệu nếu cần
                window.ShowDialog();
            }
        }

        private void BtnThemDichVu_Click(object sender, RoutedEventArgs e)
        {
            var window = new ThemDichVu(); 
            window.ShowDialog();
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && (tb.Text == "Tên dịch vụ" || tb.Text == "Đơn giá"))
            {
                tb.Text = "";
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && string.IsNullOrWhiteSpace(tb.Text))
            {
                if (tb.Name == "txtSearchName") tb.Text = "Tên dịch vụ";
                if (tb.Name == "txtSearchPrice") tb.Text = "Đơn giá";
            }
        }
    }
}
