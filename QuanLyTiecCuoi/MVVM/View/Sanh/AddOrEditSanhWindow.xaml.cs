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

        // Biến lưu đường dẫn tạm của ảnh được chọn
        private string _tempImagePath = null;

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
                // Lưu đường dẫn tạm của ảnh được chọn
                _tempImagePath = openFileDialog.FileName;

                // Chỉ hiển thị tên ảnh
                string selectedFileName = System.IO.Path.GetFileName(_tempImagePath);
                ImageNameTextBlock.Text = selectedFileName;

                // Ẩn nút chọn ảnh 
                ChooseImageButton.Visibility = Visibility.Collapsed;
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

            // Chỉ copy ảnh nếu người dùng nhấn Lưu
            if (!string.IsNullOrEmpty(_tempImagePath))
            {
                try
                {
                    string selectedFileName = System.IO.Path.GetFileName(_tempImagePath);
                    string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                    string projectRoot = Directory.GetParent(baseDir).Parent.Parent.Parent.FullName;
                    string targetFolder = System.IO.Path.Combine(projectRoot, "Resources", "Images", "Sanh");

                    if (!Directory.Exists(targetFolder))
                        Directory.CreateDirectory(targetFolder);

                    string targetPath = System.IO.Path.Combine(targetFolder, selectedFileName);

                    // Copy ảnh vào Resources
                    File.Copy(_tempImagePath, targetPath, true);

                    // Gán tên file cho HinhAnh
                    SanhInfo.HinhAnh = selectedFileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lưu ảnh: {ex.Message}");
                    return; // Dừng lại nếu lỗi
                }
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
