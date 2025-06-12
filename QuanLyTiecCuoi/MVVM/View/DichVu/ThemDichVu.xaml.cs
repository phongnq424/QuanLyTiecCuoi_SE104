using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Services;
using Microsoft.Win32;
using System;
using System.Data.SqlTypes;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace QuanLyTiecCuoi.MVVM.View.DichVu
{
    public partial class ThemDichVu : Window
    {
        private DICHVU _dichVuMoi;
        private readonly DichVuService _dichVuService;

        public ThemDichVu()
        {
            InitializeComponent();

            _dichVuMoi = new DICHVU();
            DataContext = _dichVuMoi;

            _dichVuService = App.AppHost.Services.GetService(typeof(DichVuService)) as DichVuService;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TxtTenDichVu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtTenDichVu.IsReadOnly = false;
            txtTenDichVu.Background = Brushes.White;
            txtTenDichVu.BorderThickness = new Thickness(1);
            txtTenDichVu.Focus();
            txtTenDichVu.SelectAll();
        }

        private void TxtTenDichVu_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTenDichVu.IsReadOnly = true;
            txtTenDichVu.Background = Brushes.Transparent;
            txtTenDichVu.BorderThickness = new Thickness(0);
        }

        private void TxtDonGia_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtDonGia.IsReadOnly = false;
            txtDonGia.Background = Brushes.White;
            txtDonGia.BorderThickness = new Thickness(1);
            txtDonGia.Focus();
            txtDonGia.SelectAll();
        }

        private void TxtDonGia_LostFocus(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(txtDonGia.Text, out var gia))
            {
                _dichVuMoi.DonGia = gia;
            }
            else
            {
                MessageBox.Show("Đơn giá không hợp lệ. Vui lòng nhập số.",
                                "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtDonGia.Text = "0";
            }

            txtDonGia.IsReadOnly = true;
            txtDonGia.Background = Brushes.Transparent;
            txtDonGia.BorderThickness = new Thickness(0);
        }

        private void TxtMoTa_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtMoTa.IsReadOnly = false;
            txtMoTa.Background = Brushes.White;
            txtMoTa.BorderThickness = new Thickness(1);
            txtMoTa.Focus();
            txtMoTa.SelectAll();
        }

        private void TxtMoTa_LostFocus(object sender, RoutedEventArgs e)
        {
            txtMoTa.IsReadOnly = true;
            txtMoTa.Background = Brushes.Transparent;
            txtMoTa.BorderThickness = new Thickness(0);
        }

        private void imgDichVu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Title = "Chọn ảnh dịch vụ",
                Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp"
            };

            if (dlg.ShowDialog() == true)
            {
                _dichVuMoi.HinhAnh = dlg.FileName;
                imgDichVu.Source = new BitmapImage(new Uri(dlg.FileName));
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_dichVuMoi.TenDichVu) || _dichVuMoi.DonGia <= 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên dịch vụ và đơn giá hợp lệ.",
                                "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _dichVuService.ThemDichVu(_dichVuMoi);
            MessageBox.Show("Thêm dịch vụ thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            this.DialogResult = true;
            this.Close();
        }
    }
}
