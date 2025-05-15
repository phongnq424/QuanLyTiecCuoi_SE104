using System;
using System.Data.SqlTypes;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using QuanLyTiecCuoi.MVVM.Model; 

namespace QuanLyTiecCuoi.MVVM.View.MonAn
{
    public partial class ThemMonAn : Window
    {
        // Đây là model sẽ trả về cho caller khi ShowDialog() == true
        private readonly MonAnModel _monAn;

        public MonAnModel NewMonAn => _monAn;

        public ThemMonAn()
        {
            InitializeComponent();

            // Khởi tạo model và bind về UI
            _monAn = new MonAnModel();
            DataContext = _monAn;
        }

        // Cho phép kéo window khi WindowStyle=None
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

        // Chọn ảnh mới cho món ăn
        private void ImgMonAn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Title = "Chọn ảnh món ăn",
                Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp"
            };

            if (dlg.ShowDialog() == true)
            {
                _monAn.ImagePath = dlg.FileName;

                // Nếu cần, cập nhật lại Image control thủ công
                imgMonAn.Source = new BitmapImage(new Uri(dlg.FileName));
            }
        }

        // click cho phép edit Tên món
        private void TxtTenMon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtTenMon.IsReadOnly = false;
            txtTenMon.Background = Brushes.White;
            txtTenMon.BorderThickness = new Thickness(1);
            txtTenMon.Focus();
            txtTenMon.SelectAll();
        }

        // Khi mất focus, khóa lại và giữ giá trị
        private void TxtTenMon_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTenMon.IsReadOnly = true;
            txtTenMon.Background = Brushes.Transparent;
            txtTenMon.BorderThickness = new Thickness(0);
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
                _monAn.DonGia = (SqlMoney)gia;
            }
            else
            {
                MessageBox.Show("Đơn giá không hợp lệ. Vui lòng nhập số.",
                                "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                // rollback
                txtDonGia.Text = _monAn.DonGia.ToString();
            }

            txtDonGia.IsReadOnly = true;
            txtDonGia.Background = Brushes.Transparent;
            txtDonGia.BorderThickness = new Thickness(0);
        }

        // Bấm nút Thêm: hỏi xác nhận, nếu Yes thì đóng dialog với kết quả true
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn có chắc muốn thêm món ăn này?",
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
