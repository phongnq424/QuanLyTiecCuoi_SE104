using QuanLyTiecCuoi.MVVM.Model;
using System;
using System.Collections.Generic;
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

namespace QuanLyTiecCuoi.MVVM.View
{
    public partial class AddOrEditLoaiSanhWindow : Window
    {
        private List<LoaiSanh> _danhSachLoaiSanh;
        private bool _isEditMode = false;
        public LoaiSanh LoaiSanhInfo { get; set; }

        // Constructor khi thêm mới
        public AddOrEditLoaiSanhWindow(List<LoaiSanh> danhSachLoaiSanh)
        {
            InitializeComponent();
            LoaiSanhInfo = new LoaiSanh();
            _danhSachLoaiSanh = danhSachLoaiSanh;
            _isEditMode = false;
            DataContext = this;
        }

        // Constructor khi chỉnh sửa
        public AddOrEditLoaiSanhWindow(LoaiSanh selectedLoaiSanh)
        {
            InitializeComponent();
            // Clone để không ảnh hưởng trực tiếp đến dữ liệu gốc nếu hủy
            LoaiSanhInfo = new LoaiSanh
            {
                MaLoaiSanh = selectedLoaiSanh.MaLoaiSanh,
                TenLoaiSanh = selectedLoaiSanh.TenLoaiSanh,
                DonGiaBanToiThieu = selectedLoaiSanh.DonGiaBanToiThieu
            };
            _isEditMode = true;
            DataContext = this;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra trùng tên loại sảnh
            if (!_isEditMode && _danhSachLoaiSanh != null && _danhSachLoaiSanh.Any(s => s.TenLoaiSanh.Equals(LoaiSanhInfo.TenLoaiSanh, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Tên loại sảnh này đã tồn tại. Vui lòng nhập tên khác.", "Trùng tên", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
            Close();
        }

        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private static bool IsTextNumeric(string text)
        {
            return text.All(char.IsDigit);
        }
    }
}
