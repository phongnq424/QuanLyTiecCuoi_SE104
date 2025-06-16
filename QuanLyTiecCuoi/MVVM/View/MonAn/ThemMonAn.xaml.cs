using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Services;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace QuanLyTiecCuoi.MVVM.View.MonAn
{
    public partial class ThemMonAn : Window
    {
        private MONAN _monAnMoi;
        private readonly MonAnService _monAnService;

        public ThemMonAn()
        {
            InitializeComponent();

            _monAnMoi = new MONAN();
            DataContext = _monAnMoi;

            _monAnService = App.AppHost.Services.GetService(typeof(MonAnService)) as MonAnService;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TxtTenMon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtTenMon.IsReadOnly = false;
            txtTenMon.Focus();
        }

        private void TxtTenMon_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTenMon.IsReadOnly = true;
        }

        private void TxtDonGia_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtDonGia.IsReadOnly = false;
            txtDonGia.Focus();
            txtDonGia.SelectAll();
        }

        private void TxtDonGia_LostFocus(object sender, RoutedEventArgs e)
        {
            txtDonGia.IsReadOnly = true;

            if (!decimal.TryParse(txtDonGia.Text, out decimal donGia))
            {
                MessageBox.Show("Đơn giá không hợp lệ.");
                txtDonGia.Text = "0";
            }
            else
            {
                _monAnMoi.DonGia = donGia;
            }
        }

        private void Image_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtHinhAnh.Visibility = Visibility.Visible;
            txtHinhAnh.Focus();
            txtHinhAnh.SelectAll();
        }
        private void TxtHinhAnh_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                var imageUrl = txtHinhAnh.Text?.Trim();
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    imgMonAn.Source = new BitmapImage(new Uri(imageUrl));
                    _monAnMoi.HinhAnh = imageUrl;
                }
            }
            catch
            {
                MessageBox.Show("Không thể tải ảnh từ đường dẫn.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            txtHinhAnh.Visibility = Visibility.Collapsed;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_monAnMoi.TenMon) || _monAnMoi.DonGia <= 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hợp lệ.");
                return;
            }

            // Lưu vào database
            _monAnService.ThemMonAn(_monAnMoi);
            MessageBox.Show("Thêm món ăn thành công!");

            this.DialogResult = true;
            this.Close();
        }
    }
}
