using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.MVVM.Model;
using QuanLyTiecCuoi.MVVM.View;
using QuanLyTiecCuoi.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class SanhViewModel : BaseViewModel
    {
        private readonly LoaiSanhService _loaiSanhService;
        private readonly SanhService _sanhService;
        public ObservableCollection<LoaiSanh> DanhSachLoaiSanh { get; set; }
        public ObservableCollection<Sanh> DanhSachSanh { get; set; }
        public ICollectionView DanhSachSanhView { get; set; }

        private Sanh _selectedSanh;
        public Sanh SelectedSanh
        {
            get => _selectedSanh;
            set { _selectedSanh = value; OnPropertyChanged(); }
        }

        private string _filterTenSanh;
        public string FilterTenSanh
        {
            get => _filterTenSanh;
            set { _filterTenSanh = value; OnPropertyChanged(nameof(FilterTenSanh)); DanhSachSanhView.Refresh(); }
        }

        private string _filterLoaiSanh;
        public string FilterLoaiSanh
        {
            get => _filterLoaiSanh;
            set { _filterLoaiSanh = value; OnPropertyChanged(nameof(FilterLoaiSanh)); DanhSachSanhView.Refresh(); }
        }

        private string _filterSoLuongBanToiDa;
        public string FilterSoLuongBanToiDa
        {
            get => _filterSoLuongBanToiDa;
            set { _filterSoLuongBanToiDa = value; OnPropertyChanged(nameof(FilterSoLuongBanToiDa)); DanhSachSanhView.Refresh(); }
        }

        private string _filterDonGiaBanToiThieu;
        public string FilterDonGiaBanToiThieu
        {
            get => _filterDonGiaBanToiThieu;
            set { _filterDonGiaBanToiThieu = value; OnPropertyChanged(nameof(FilterDonGiaBanToiThieu)); DanhSachSanhView.Refresh(); }
        }

        private LoaiSanh _selectedLoaiSanh;
        public LoaiSanh SelectedLoaiSanh
        {
            get => _selectedLoaiSanh;
            set
            {
                if (_selectedLoaiSanh != value)
                {
                    _selectedLoaiSanh = value;
                    OnPropertyChanged();

                    // Tự động cập nhật giá bàn tối thiểu hiển thị khi chọn loại sảnh
                    OnPropertyChanged(nameof(DonGiaBanToiThieu));
                }
            }
        }

        // Thuộc tính chỉ hiển thị (readonly) – binding lên TextBlock
        public double? DonGiaBanToiThieu => SelectedLoaiSanh?.DonGiaBanToiThieu;

        public ICommand AddSanhCommand { get; set; }

        public ICommand EditSanhCommand { get; set; }

        public ICommand DeleteSanhCommand { get; set; }


        public SanhViewModel(SanhService sanhService, LoaiSanhService loaiSanhService)
        {
            // Database
            _loaiSanhService = loaiSanhService;
            _sanhService = sanhService;

            DanhSachLoaiSanh = new ObservableCollection<LoaiSanh>(_loaiSanhService.GetAllLoaiSanh());
            DanhSachSanh = new ObservableCollection<Sanh>(_sanhService.GetAllSanh());

            DanhSachSanhView = CollectionViewSource.GetDefaultView(DanhSachSanh);
            DanhSachSanhView.Filter = FilterSanh;

            // Khởi tạo lệnh 
            AddSanhCommand = new RelayCommand<object>(
                canExecute: _ => true,
                execute: _ => AddSanh()
            );

            EditSanhCommand = new RelayCommand<object>(
                canExecute: _ => SelectedSanh != null,
                execute: _ => EditSanh()
            );

            DeleteSanhCommand = new RelayCommand<object>(
                canExecute: _ => SelectedSanh != null,
                execute: _ => DeleteSanh()
            );
        }

        // Đếm số Sảnh
        public int SoLuongSanh => DanhSachSanh.Count;

        // Refresh DS Sảnh
        private void RefreshDanhSachSanh()
        {
            DanhSachSanh.Clear();
            foreach (var sanh in _sanhService.GetAllSanh())
                DanhSachSanh.Add(sanh);

            OnPropertyChanged(nameof(SoLuongSanh));
        }

        // Thêm Sảnh mới
        private void AddSanh()
        {
            var window = new AddOrEditSanhWindow(DanhSachLoaiSanh.ToList());
            if (window.ShowDialog() == true)
            {
                var newSanh = window.SanhInfo;
                _sanhService.AddSanh(newSanh);
                RefreshDanhSachSanh();
                SelectedSanh = newSanh;
            }
        }

        // Chỉnh sửa Sảnh
        private void EditSanh()
        {
            var window = new AddOrEditSanhWindow(SelectedSanh, DanhSachLoaiSanh.ToList());
            if (window.ShowDialog() == true)
            {
                var newSanh = window.SanhInfo;
                _sanhService.EditSanh(newSanh);
                RefreshDanhSachSanh();
                SelectedSanh = newSanh;
            }
        }

        // Xóa Sảnh
        private void DeleteSanh()
        {
            if (SelectedSanh == null) return;
            _sanhService.DeleteSanh(SelectedSanh);
            RefreshDanhSachSanh();
            SelectedSanh = null;
        }

        private bool FilterSanh(object obj)
        {
            if (obj is not Sanh sanh) return false;

            bool matchTen = string.IsNullOrWhiteSpace(FilterTenSanh) ||
                            sanh.TenSanh?.IndexOf(FilterTenSanh, StringComparison.OrdinalIgnoreCase) >= 0;

            bool matchLoai = string.IsNullOrWhiteSpace(FilterLoaiSanh) ||
                             sanh.LoaiSanh?.TenLoaiSanh?.IndexOf(FilterLoaiSanh, StringComparison.OrdinalIgnoreCase) >= 0;

            bool matchSoLuong = true;
            if (int.TryParse(FilterSoLuongBanToiDa, out int soLuong))
            {
                matchSoLuong = sanh.SoLuongBanToiDa >= soLuong;
            }

            bool matchGia = true;
            if (double.TryParse(FilterDonGiaBanToiThieu, out double gia))
            {
                matchGia = sanh.LoaiSanh?.DonGiaBanToiThieu <= gia;
            }

            return matchTen && matchLoai && matchSoLuong && matchGia;
        }
    }
}
