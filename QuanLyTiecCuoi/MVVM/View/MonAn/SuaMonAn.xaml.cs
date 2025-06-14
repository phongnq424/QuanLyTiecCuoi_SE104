using System;
using System.Data.SqlTypes;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using QuanLyTiecCuoi.MVVM.Model;
using QuanLyTiecCuoi.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using QuanLyTiecCuoi.Services;

namespace QuanLyTiecCuoi.MVVM.View.MonAn
{
    public partial class SuaMonAn : Window
    {
        private MONAN _monAn;
       

        public SuaMonAn(MONAN monAn)
        {
            InitializeComponent();

            
            _monAn = monAn;
            this.DataContext = _monAn;

            // Nếu ảnh là URL (link online), cần xử lý đặc biệt
            if (!string.IsNullOrEmpty(monAn.HinhAnh))
            {
                try
                {
                    imgMonAn.Source = new BitmapImage(new System.Uri(monAn.HinhAnh));
                }
                catch
                {
                    MessageBox.Show("Không thể tải ảnh.");
                }
            }
        }
        

        private void TxtHinhAnh_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                var imageUrl = txtHinhAnh.Text?.Trim();
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    imgMonAn.Source = new BitmapImage(new Uri(imageUrl));
                    _monAn.HinhAnh = imageUrl;
                }
            }
            catch
            {
                MessageBox.Show("Không thể tải ảnh từ đường dẫn.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            txtHinhAnh.Visibility = Visibility.Collapsed;
        }

        private void Image_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtHinhAnh.Visibility = Visibility.Visible;
            txtHinhAnh.Focus();
            txtHinhAnh.SelectAll();
        }



        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void TxtTenMon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtTenMon.IsReadOnly = false;
            txtTenMon.Background = Brushes.White;
            txtTenMon.Focus();
        }

        private void TxtTenMon_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTenMon.IsReadOnly = true;
            txtTenMon.Background = Brushes.Transparent;
        }

        private void TxtDonGia_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtDonGia.IsReadOnly = false;
            txtDonGia.Background = Brushes.White;
            txtDonGia.Focus();
        }

        private void TxtDonGia_LostFocus(object sender, RoutedEventArgs e)
        {
            txtDonGia.IsReadOnly = true;
            txtDonGia.Background = Brushes.Transparent;
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var monAnService = App.AppHost.Services.GetService<MonAnService>();

                if (monAnService != null)
                {
                    monAnService.CapNhatMonAn(_monAn); // Cập nhật vào database
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dịch vụ MonAnService", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                this.Close(); // Đóng cửa sổ sau khi cập nhật
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
