using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using QuanLyTiecCuoi.Data.Models;

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

            // Load ảnh nếu có
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

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void TxtTenDichVu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtTenDichVu.IsReadOnly = false;
            txtTenDichVu.Focus();
        }

        private void TxtTenDichVu_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTenDichVu.IsReadOnly = true;
        }

        private void TxtDonGia_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtDonGia.IsReadOnly = false;
            txtDonGia.Focus();
        }

        private void TxtDonGia_LostFocus(object sender, RoutedEventArgs e)
        {
            txtDonGia.IsReadOnly = true;

            if (decimal.TryParse(txtDonGia.Text, out decimal gia))
            {
                _dichVu.DonGia = gia;
            }
            else
            {
                MessageBox.Show("Giá không hợp lệ.");
                txtDonGia.Text = _dichVu.DonGia.ToString();
            }
        }

        private void TxtMoTa_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtMoTa.IsReadOnly = false;
            txtMoTa.Focus();
        }

        private void TxtMoTa_LostFocus(object sender, RoutedEventArgs e)
        {
            txtMoTa.IsReadOnly = true;
        }

        private void ImgDichVu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Title = "Chọn ảnh dịch vụ",
                Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (dlg.ShowDialog() == true)
            {
                _dichVu.HinhAnh = dlg.FileName;
                imgDichVu.Source = new BitmapImage(new Uri(dlg.FileName));
            }
        }
    }
}
