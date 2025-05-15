using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using QuanLyTiecCuoi.MVVM.Model;

namespace QuanLyTiecCuoi.MVVM.View.DichVu
{
    public partial class ThemDichVu : Window
    {
        // Đây là model sẽ trả về cho caller khi ShowDialog() == true
        private readonly DichVuModel _dichVu;
        public DichVuModel NewDichVu => _dichVu;
        public ThemDichVu()
        {
            InitializeComponent();
            // Khởi tạo model và bind về UI
            _dichVu = new DichVuModel();
            DataContext = _dichVu;
        }

        
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        // Đóng form
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Chọn ảnh mới cho dịch vụ
        private void imgDichVu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Title = "Chọn ảnh dịch vụ",
                Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp"
            };

            if (dlg.ShowDialog() == true)
            {
                _dichVu.ImagePath = dlg.FileName;

                // Nếu cần, cập nhật lại Image control thủ công
                imgDichVu.Source = new BitmapImage(new Uri(dlg.FileName));
            }
        }

        // click cho phép edit Tên món
        private void TxtTenDichVu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtTenDichVu.IsReadOnly = false;
            txtTenDichVu.Background = Brushes.White;
            txtTenDichVu.BorderThickness = new Thickness(1);
            txtTenDichVu.Focus();
            txtTenDichVu.SelectAll();
        }

        // Khi mất focus, khóa lại và giữ giá trị
        private void TxtTenDichVu_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTenDichVu.IsReadOnly = true;
            txtTenDichVu.Background = Brushes.Transparent;
            txtTenDichVu.BorderThickness = new Thickness(0);
        }

        // click cho phép edit Đơn giá
        private void TxtDonGia_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtDonGia.IsReadOnly = false;
            txtDonGia.Background = Brushes.White;
            txtDonGia.BorderThickness = new Thickness(1);
            txtDonGia.Focus();
            txtDonGia.SelectAll();
        }

        // Khi mất focus, parse và gán lại model hoặc rollback
        private void TxtDonGia_LostFocus(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtDonGia.Text, out var gia))
            {
                _dichVu.DonGia = (SqlMoney)gia;
            }
            else
            {
                MessageBox.Show("Đơn giá không hợp lệ. Vui lòng nhập số.",
                                "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                // rollback
                txtDonGia.Text = _dichVu.DonGia.ToString();
            }

            txtDonGia.IsReadOnly = true;
            txtDonGia.Background = Brushes.Transparent;
            txtDonGia.BorderThickness = new Thickness(0);
        }
        // click cho phép edit Đơn giá
        private void TxtMoTa_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtMoTa.IsReadOnly = false;
            txtMoTa.Background = Brushes.White;
            txtMoTa.BorderThickness = new Thickness(1);
            txtMoTa.Focus();
            txtMoTa.SelectAll();
        }

        // Khi mất focus, parse và gán lại model hoặc rollback
        private void TxtMoTa_LostFocus(object sender, RoutedEventArgs e)
        {
            txtMoTa.IsReadOnly = true;
            txtMoTa.Background = Brushes.Transparent;
            txtMoTa.BorderThickness = new Thickness(0);
        }

        // Bấm nút Thêm: hỏi xác nhận, nếu Yes thì đóng dialog với kết quả true
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn có chắc muốn thêm dịch vụ này?",
                "Xác nhận thêm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Đánh dấu DialogResult = true để caller biết là OK
                this.DialogResult = true;
            }
        }
    }
}
