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
using System.IO;

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




        private void Image_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Title = "Chọn ảnh món ăn",
                Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (dlg.ShowDialog() == true)
            {
                string sourcePath = dlg.FileName;
                string projectPath = AppDomain.CurrentDomain.BaseDirectory;
                string imagesFolder = System.IO.Path.Combine(projectPath, "Resources", "Images", "MonAn");

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                string fileName = System.IO.Path.GetFileName(sourcePath);
                string destinationPath = System.IO.Path.Combine(imagesFolder, fileName);

                try
                {
                    // Copy ảnh vào thư mục MonAn (ghi đè nếu trùng)
                    File.Copy(sourcePath, destinationPath, true);

                    // Gán đường dẫn tương đối để lưu vào database
                    string relativePath = $"Resources/Images/MonAn/{fileName}";
                    _monAn.HinhAnh = relativePath;

                    // Hiển thị ảnh
                    imgMonAn.Source = new BitmapImage(new Uri(System.IO.Path.Combine(projectPath, relativePath)));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sao chép ảnh: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }





        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
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
            txtDonGia.SelectAll();
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
