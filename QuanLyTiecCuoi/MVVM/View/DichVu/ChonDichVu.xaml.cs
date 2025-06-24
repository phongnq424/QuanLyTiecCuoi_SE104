using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.ViewModel;
using QuanLyTiecCuoi.MVVM.ViewModel.DichVu;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace QuanLyTiecCuoi.MVVM.View.DichVu
{
    public partial class ChonDichVu : Page
    {
        private readonly ChonDichVuViewModel _viewModel;

        private readonly ThemTiecViewModel _themTiecVM;
        private readonly SuaTiecViewModel _suaTiecVM;

        public ChonDichVu(ThemTiecViewModel themTiecVM)
        {
            InitializeComponent();
            _themTiecVM = themTiecVM;
            _viewModel = new ChonDichVuViewModel(themTiecVM.TiecMoi, themTiecVM.DichVuDaChon);
            this.DataContext = _viewModel;

            TinhTongTien(); // cập nhật ban đầu
        }
        public ChonDichVu(SuaTiecViewModel suaTiecVM)
        {
            InitializeComponent();
            _suaTiecVM = suaTiecVM;
            _viewModel = new ChonDichVuViewModel(suaTiecVM.TiecMoi, suaTiecVM.DichVuDaChon);
            this.DataContext = _viewModel;

            TinhTongTien(); // cập nhật ban đầu
        }


        private void DichVu_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is DICHVU dichVu)
            {
                _viewModel.ChonDichVu(dichVu);
                TinhTongTien();
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (_themTiecVM != null)
                _themTiecVM.CapNhatTongTien(); // cập nhật ngay khi đóng
            else
                _suaTiecVM.CapNhatTongTien(); // cập nhật ngay khi đóng

            if (this.NavigationService != null && this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }
        private void lstSelectedServices_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (lstSelectedFoods.SelectedItem is DICHVU selectedDV)
            {
                // Trả lại món về danh sách gốc nếu chưa có
                if (!_viewModel.DanhSachDichVu.Contains(selectedDV))
                    _viewModel.DanhSachDichVu.Add(selectedDV);

                // Xóa khỏi danh sách đã chọn
                _viewModel.DichVuDaChon.Remove(selectedDV);

                // Reset selection để lần sau click lại được
                lstSelectedFoods.SelectedItem = null;
            }
            TinhTongTien();
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
                        tb.Text = "Tên món ăn";
                        tb.Foreground = System.Windows.Media.Brushes.Gray;
                        if (_viewModel != null)
                        {
                            _viewModel.TuKhoaTen = "";
                        }
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
                    if (tb.Name == "txtSearchName")
                    {
                        if (_viewModel != null)
                            _viewModel.TuKhoaTen = tb.Text;
                    }
                    else if (tb.Name == "txtSearchPrice")
                    {
                        if (_viewModel != null)
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

        private void txtSearchPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb && _viewModel != null)
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

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService != null && this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }
        private void SoLuong_TextChanged(object sender, TextChangedEventArgs e)
        {
            TinhTongTien();
        }

        private void TinhTongTien()
        {
            decimal tong = 0;

            foreach (var dv in _viewModel.DichVuDaChon)
            {
                int soLuong = dv.SoLuong > 0 ? dv.SoLuong : 1;
                tong += dv.DonGia * soLuong;
            }

            txtTongTien.Text = $"Tổng tiền: {tong:N0} VND";
        }

    }
}