using Microsoft.Win32;
using QuanLyTiecCuoi.MVVM.Model;
using QuanLyTiecCuoi.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class AddOrEditSanhWindow : Window, INotifyPropertyChanged
    {
        public Sanh SanhInfo { get; set; }

        private List<LoaiSanh> _danhSachLoaiSanh;
        public List<LoaiSanh> DanhSachLoaiSanh
        {
            get => _danhSachLoaiSanh;
            set { _danhSachLoaiSanh = value; OnPropertyChanged(); }
        }

        private LoaiSanh _selectedLoaiSanh;
        public LoaiSanh SelectedLoaiSanh
        {
            get => _selectedLoaiSanh;
            set
            {
                _selectedLoaiSanh = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DonGiaBanToiThieuText)); // Cập nhật hiển thị đơn giá
            }
        }

        public string DonGiaBanToiThieuText => SelectedLoaiSanh?.DonGiaBanToiThieu.HasValue == true
                                             ? SelectedLoaiSanh.DonGiaBanToiThieu.Value.ToString("N0")
                                             : "";

        // Constructor khi thêm mới
        public AddOrEditSanhWindow(List<LoaiSanh> danhSachLoaiSanh)
        {
            InitializeComponent();

            DanhSachLoaiSanh = danhSachLoaiSanh;

            // Mặc định chọn loại sảnh đầu tiên (nếu có)
            SelectedLoaiSanh = null;

            SanhInfo = new Sanh();

            DataContext = this;
        }

        // Constructor khi chỉnh sửa
        public AddOrEditSanhWindow(Sanh selectedSanh, List<LoaiSanh> danhSachLoaiSanh)
        {
            InitializeComponent();

            // Danh sách loại sảnh để bind vào ComboBox
            DanhSachLoaiSanh = danhSachLoaiSanh;

            // Tìm loại sảnh tương ứng
            SelectedLoaiSanh = DanhSachLoaiSanh.FirstOrDefault(ls => ls.MaLoaiSanh == selectedSanh.MaLoaiSanh);

            // Clone thông tin sảnh
            SanhInfo = new Sanh
            {
                MaSanh = selectedSanh.MaSanh,
                TenSanh = selectedSanh.TenSanh,
                MaLoaiSanh = selectedSanh.MaLoaiSanh,
                SoLuongBanToiDa = selectedSanh.SoLuongBanToiDa,
                GhiChu = selectedSanh.GhiChu,
                LoaiSanh = SelectedLoaiSanh
            };

            DataContext = this;
        }

        private void btnChonAnh_Click(object sender, RoutedEventArgs e)
        {
            // Tạo một đối tượng OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*"
            };

            // Mở hộp thoại chọn file
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFileName = System.IO.Path.GetFileName(openFileDialog.FileName);
                string targetFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");

                if (!Directory.Exists(targetFolder))
                    Directory.CreateDirectory(targetFolder);

                string targetPath = System.IO.Path.Combine(targetFolder, selectedFileName);

                try
                {
                    // Sao chép file vào thư mục Resources
                    File.Copy(openFileDialog.FileName, targetPath, true); // Ghi đè nếu đã tồn tại

                    // Lưu tên file vào thuộc tính HinhAnh của SanhInfo
                    SanhInfo.HinhAnh = selectedFileName;

                    // Cập nhật TextBlock hoặc TextBox với tên file đã chọn
                    ImageNameTextBlock.Text = selectedFileName;

                    // Ẩn Button chọn hình ảnh
                    ChooseImageButton.Visibility = Visibility.Collapsed;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lưu ảnh: {ex.Message}");
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Gán lại mã loại sảnh từ SelectedLoaiSanh
            if (SelectedLoaiSanh != null)
            {
                SanhInfo.MaLoaiSanh = SelectedLoaiSanh.MaLoaiSanh;
                SanhInfo.LoaiSanh = SelectedLoaiSanh;
            }

            DialogResult = true;
            Close();
        }

        // INotifyPropertyChanged triển khai
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
