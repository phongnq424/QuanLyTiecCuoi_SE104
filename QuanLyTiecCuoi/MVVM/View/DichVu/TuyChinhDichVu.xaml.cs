using QuanLyTiecCuoi.MVVM.ViewModel.DichVu;
using QuanLyTiecCuoi.Data.Models;
using System.Windows;
using System.Windows.Controls;

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

            itemsControlDichVu.ItemsSource = _viewModel.DanhSachDichVu;
        }

        private void BtnThemDichVu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Mở form thêm dịch vụ (bạn có thể mở trang mới hoặc hiển thị popup)", "Thêm dịch vụ");
        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is DICHVU dv)
            {
                _viewModel.XoaDichVu(dv);
            }
        }

        private void BtnChinhSua_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is DICHVU dv)
            {
                MessageBox.Show($"Chỉnh sửa dịch vụ: {dv.TenDichVu}");
                // Gợi ý: Mở window mới để chỉnh sửa
            }
        }

        private void BtnChiTiet_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is DICHVU dv)
            {
                MessageBox.Show($"Chi tiết dịch vụ:\nTên: {dv.TenDichVu}\nGiá: {dv.DonGia:N0} VND");
            }
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null && (tb.Text == "Tên dịch vụ" || tb.Text == "Đơn giá"))
                tb.Text = "";
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null && string.IsNullOrWhiteSpace(tb.Text))
            {
                if (tb.Name == "txtSearchName") tb.Text = "Tên dịch vụ";
                if (tb.Name == "txtSearchPrice") tb.Text = "Đơn giá";
            }
        }
    }
}
