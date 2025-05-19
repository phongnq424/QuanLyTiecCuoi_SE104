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
    public partial class SuaMonAn : Window
    {
        private readonly MonAnModel _monAn;

        public SuaMonAn(MonAnModel monAn)
        {
            InitializeComponent();

            // Lưu lại model và bind
            _monAn = monAn ?? throw new ArgumentNullException(nameof(monAn));
            DataContext = _monAn;
        }

        // Đóng cửa sổ
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Cho phép kéo window khi click giữ chuột
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        // Khi user click vào ảnh -> mở FileDialog chọn ảnh mới
        private void ImgMonAn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Title = "Chọn ảnh món ăn",
                Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|All files (*.*)|*.*"
            };

            if (dlg.ShowDialog() == true)
            {
                _monAn.ImagePath = dlg.FileName;

                // Nếu MonAn implement INotifyPropertyChanged thì binding tự cập nhật,
                // ngược lại ta đặt lại Source thủ công:
                imgMonAn.Source = new BitmapImage(new Uri(dlg.FileName));
            }
        }

        // Double-click để cho phép sửa tên món
        private void TxtTenMon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtTenMon.IsReadOnly = false;
            txtTenMon.Background = Brushes.White;
            txtTenMon.BorderThickness = new Thickness(1);
            txtTenMon.Focus();
            txtTenMon.SelectAll();
        }

        // Khi mất focus -> khóa lại, binding giá trị trở lại model
        private void TxtTenMon_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTenMon.IsReadOnly = true;
            txtTenMon.Background = Brushes.Transparent;
            txtTenMon.BorderThickness = new Thickness(0);
            // giá trị Text đã được TwoWay bind về _monAn.TenMon
        }

        // Double-click để cho phép sửa đơn giá
        private void TxtDonGia_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtDonGia.IsReadOnly = false;
            txtDonGia.Background = Brushes.White;
            txtDonGia.BorderThickness = new Thickness(1);
            txtDonGia.Focus();
            txtDonGia.SelectAll();
        }

        // Khi mất focus -> parse, gán về model rồi khóa lại
        private void TxtDonGia_LostFocus(object sender, RoutedEventArgs e)
        {
            // cố gắng parse số, nếu sai thì rollback về giá gốc
            if (double.TryParse(txtDonGia.Text, out var gia))
            {
                _monAn.DonGia = (SqlMoney)gia;
            }
            else
            {
                txtDonGia.Text = _monAn.DonGia.ToString();
            }

            txtDonGia.IsReadOnly = true;
            txtDonGia.Background = Brushes.Transparent;
            txtDonGia.BorderThickness = new Thickness(0);
        }
    }
}
