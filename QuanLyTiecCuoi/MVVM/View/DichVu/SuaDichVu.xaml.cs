using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using QuanLyTiecCuoi.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using QuanLyTiecCuoi.Services;

namespace QuanLyTiecCuoi.MVVM.View.DichVu
{
    public partial class SuaDichVu : Window
    {
        private DICHVU _dichVu;

        public SuaDichVu(DICHVU dichVu)
        {
            InitializeComponent();

            _dichVu = dichVu ?? throw new ArgumentNullException(nameof(dichVu));
            this.DataContext = _dichVu;

            if (!string.IsNullOrEmpty(_dichVu.HinhAnh))
            {
                try
                {
                    imgDichVu.Source = new BitmapImage(new Uri(_dichVu.HinhAnh));
                }
                catch
                {
                    MessageBox.Show("Không thể tải ảnh.");
                }
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ImgDichVu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtImagePath.Visibility = Visibility.Visible;
            txtImagePath.Focus();
            txtImagePath.SelectAll();
        }

        private void TxtImagePath_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                var url = txtImagePath.Text?.Trim();
                if (!string.IsNullOrEmpty(url))
                {
                    imgDichVu.Source = new BitmapImage(new Uri(url));
                    _dichVu.HinhAnh = url;
                }
            }
            catch
            {
                MessageBox.Show("Không thể tải ảnh từ đường dẫn.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            txtImagePath.Visibility = Visibility.Collapsed;
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
            txtDonGia.IsReadOnly = true;
            txtDonGia.Background = Brushes.Transparent;
            txtDonGia.BorderThickness = new Thickness(0);

            if (decimal.TryParse(txtDonGia.Text, out decimal gia))
            {
                _dichVu.DonGia = gia;
            }
            else
            {
                MessageBox.Show("Giá không hợp lệ. Vui lòng nhập số.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtDonGia.Text = _dichVu.DonGia.ToString();
            }
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

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dichVuService = App.AppHost.Services.GetService<DichVuService>();

                if (dichVuService != null)
                {
                    dichVuService.CapNhatDichVu(_dichVu);
                    MessageBox.Show("Cập nhật dịch vụ thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dịch vụ DichVuService.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
