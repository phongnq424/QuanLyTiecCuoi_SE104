using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.View.MainVindow;
using QuanLyTiecCuoi.MVVM.ViewModel;
using QuanLyTiecCuoi.MVVM.ViewModel.DichVu;
using QuanLyTiecCuoi.MVVM.ViewModel.MonAn;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyTiecCuoi.MVVM.View.MonAn
{
    public partial class ChonMonAn : Page
    {
        private readonly ChonMonAnViewModel _viewModel;
        private readonly ThemTiecViewModel _themTiecVM;
        private readonly SuaTiecViewModel _suaTiecVM;

        public ChonMonAn(ThemTiecViewModel themTiecVM)
        {
            InitializeComponent();
            _themTiecVM = themTiecVM;
            _viewModel = new ChonMonAnViewModel(themTiecVM.TiecMoi, themTiecVM.MonAnDaChon);
            this.DataContext = _viewModel;
        }
        public ChonMonAn(SuaTiecViewModel suaTiecVM)
        {
            InitializeComponent();
            _suaTiecVM = suaTiecVM;
            _viewModel = new ChonMonAnViewModel(suaTiecVM.TiecMoi, suaTiecVM.MonAnDaChon);
            this.DataContext = _viewModel;

            TinhTongTien(); // cập nhật ban đầu
        }

        private void MonAn_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is MONAN monAn)
            {
                _viewModel?.ChonMonAn(monAn);
                TinhTongTien();
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
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
        private void lstSelectedFoods_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (lstSelectedFoods.SelectedItem is MONAN selectedMon)
            {
                // Trả lại món về danh sách gốc nếu chưa có
                if (!_viewModel.DanhSachMonAn.Contains(selectedMon))
                    _viewModel.DanhSachMonAn.Add(selectedMon);

                // Xóa khỏi danh sách đã chọn
                _viewModel.MonAnDaChon.Remove(selectedMon);

                // Reset selection để lần sau click lại được
                lstSelectedFoods.SelectedItem = null;
                TinhTongTien();
            }
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

            foreach (var dv in _viewModel.MonAnDaChon)
            {
                int soLuong = dv.SoLuong > 0 ? dv.SoLuong : 1;
                tong += dv.DonGia * soLuong;
            }

            txtTongTien.Text = $"Tổng tiền: {tong:N0} VND";
        }
    }
}
