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
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
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
        private string _tempImagePath = null;

        private void Image_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*",
                Title = "Chọn ảnh từ máy"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _tempImagePath = openFileDialog.FileName;

                try
                {
                    // Gán ảnh vào điều khiển Image (giả sử bạn có x:Name="imgMonAn" trong XAML)
                    imgMonAn.Source = new BitmapImage(new Uri(_tempImagePath));
                    MessageBox.Show(_tempImagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tải ảnh: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_monAnMoi.TenMon) || _monAnMoi.DonGia <= 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hợp lệ.");
                return;
            }

                try
                {
                    string selectedFileName = System.IO.Path.GetFileName(_tempImagePath);
                    string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                    string projectRoot = Directory.GetParent(baseDir).Parent.Parent.Parent.FullName;
                    string targetFolder = System.IO.Path.Combine(projectRoot, "Resources", "Images", "MonAn");

                    if (!Directory.Exists(targetFolder))
                        Directory.CreateDirectory(targetFolder);

                    string targetPath = System.IO.Path.Combine(targetFolder, selectedFileName);

                    // Copy ảnh vào Resources
                    File.Copy(_tempImagePath, targetPath, true);

                    // Gán tên file cho HinhAnh
                    _monAnMoi.HinhAnh = $"Resources/Images/MonAn/{selectedFileName}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lưu ảnh: {ex.Message}");
                    return; // Dừng lại nếu lỗi
                }
            

            // Lưu vào database
            _monAnService.ThemMonAn(_monAnMoi);
                MessageBox.Show("Thêm món ăn thành công!");

                this.DialogResult = true;
                this.Close();
            
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_monAnMoi.HinhAnh))
            {
                ImageHelper.LoadImageFromRelativePath(imgMonAn, _monAnMoi.HinhAnh);
            }
        }

    }
}
