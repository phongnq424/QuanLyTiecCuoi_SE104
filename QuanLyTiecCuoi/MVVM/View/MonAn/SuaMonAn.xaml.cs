using System;
using System.Data.SqlTypes;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using QuanLyTiecCuoi.MVVM.Model;
using QuanLyTiecCuoi.Data.Models;

namespace QuanLyTiecCuoi.MVVM.View.MonAn
{
    public partial class SuaMonAn : Window
    {
        private MONAN _monAn;
       

        public SuaMonAn(MONAN monAn)
        {
            InitializeComponent();

            this.DataContext = _monAn;
            _monAn = monAn;

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
        }

        private void TxtDonGia_LostFocus(object sender, RoutedEventArgs e)
        {
            txtDonGia.IsReadOnly = true;

            // Convert lại về số nếu người dùng gõ chuỗi
            if (decimal.TryParse(txtDonGia.Text, out decimal gia))
            {
                _monAn.DonGia = gia;
            }
            else
            {
                MessageBox.Show("Giá không hợp lệ.");
                txtDonGia.Text = _monAn.DonGia.ToString();
            }
        }

        private void ImgMonAn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Tùy chọn: Mở cửa sổ đổi ảnh?
            MessageBox.Show("Tính năng chọn ảnh sẽ được thêm sau.");
        }
    }
}
