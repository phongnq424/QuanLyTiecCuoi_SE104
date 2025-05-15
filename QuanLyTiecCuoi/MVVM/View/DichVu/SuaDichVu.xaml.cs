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
    /// <summary>
    /// Interaction logic for SuaDichVu.xaml
    /// </summary>
    public partial class SuaDichVu : Window
    {
        private readonly DichVuModel _dichVu;
        public SuaDichVu(DichVuModel dichVu)
        {
            InitializeComponent();
            // Lưu lại model và bind
            _dichVu = dichVu ?? throw new ArgumentNullException(nameof(dichVu));
            DataContext = _dichVu;
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
        private void ImgDichVu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Title = "Chọn ảnh dịch vụ",
                Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|All files (*.*)|*.*"
            };

            if (dlg.ShowDialog() == true)
            {
                _dichVu.ImagePath = dlg.FileName;

                // Nếu MonAn implement INotifyPropertyChanged thì binding tự cập nhật,
                // ngược lại ta đặt lại Source thủ công:
                imgDichVu.Source = new BitmapImage(new Uri(dlg.FileName));
            }
        }

        // Double-click để cho phép sửa tên dịch vụ
        private void TxtTenDichVu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtTenDichVu.IsReadOnly = false;
            txtTenDichVu.Background = Brushes.White;
            txtTenDichVu.BorderThickness = new Thickness(1);
            txtTenDichVu.Focus();
            txtTenDichVu.SelectAll();
        }

        // Khi mất focus -> khóa lại, binding giá trị trở lại model
        private void TxtTenDichVu_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTenDichVu.IsReadOnly = true;
            txtTenDichVu.Background = Brushes.Transparent;
            txtTenDichVu.BorderThickness = new Thickness(0);
            
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
                _dichVu.DonGia = (SqlMoney)gia;
            }
            else
            {
                txtDonGia.Text = _dichVu.DonGia.ToString();
            }

            txtDonGia.IsReadOnly = true;
            txtDonGia.Background = Brushes.Transparent;
            txtDonGia.BorderThickness = new Thickness(0);
        }
        private void TxtMoTa_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtMoTa.IsReadOnly = false;
            txtMoTa.Background = Brushes.White;
            txtMoTa.BorderThickness = new Thickness(1);
            txtMoTa.Focus();
            txtMoTa.SelectAll();
        }

        // Khi mất focus -> khóa lại, binding giá trị trở lại model
        private void TxtMoTa_LostFocus(object sender, RoutedEventArgs e)
        {
            txtMoTa.IsReadOnly = true;
            txtMoTa.Background = Brushes.Transparent;
            txtMoTa.BorderThickness = new Thickness(0);

        }
    }
}
