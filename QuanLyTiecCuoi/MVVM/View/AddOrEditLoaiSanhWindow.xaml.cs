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
        public LoaiSanh LoaiSanhInfo { get; set; }

        // Constructor khi thêm mới
        public AddOrEditLoaiSanhWindow()
        {
            InitializeComponent();
            LoaiSanhInfo = new LoaiSanh();
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
            DataContext = this;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
