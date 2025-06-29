﻿using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.Model;
using QuanLyTiecCuoi.MVVM.View;
using QuanLyTiecCuoi.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class SanhViewModel : BaseViewModel
    {
        private readonly LoaiSanhService _loaiSanhService;
        private readonly SanhService _sanhService;
        private readonly CaService _caService;
        private readonly DatTiecService _datTiecService;
        public ObservableCollection<LoaiSanh> DanhSachLoaiSanh { get; set; }
        public ObservableCollection<Sanh> DanhSachSanh { get; set; }
        public ObservableCollection<CASANH> DanhSachCaSanh { get; set; }
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

        private LoaiSanh _filterLoaiSanh;
        public LoaiSanh FilterLoaiSanh
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

        private CASANH _selectedCaSanh;
        public CASANH SelectedCaSanh
        {
            get => _selectedCaSanh;
            set 
            { 
                _selectedCaSanh = value; OnPropertyChanged();
                DanhSachSanhView.Refresh();
                OnPropertyChanged(nameof(SoLuongSanhTrong));
                OnPropertyChanged(nameof(ThongTinSoLuongSanh));
            }
        }

        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value; OnPropertyChanged();
                DanhSachSanhView.Refresh();
                OnPropertyChanged(nameof(SoLuongSanhTrong));
                OnPropertyChanged(nameof(ThongTinSoLuongSanh));
            }
        }

        public int SoLuongSanhTrong
        {
            get
            {
                if (!SelectedDate.HasValue || SelectedCaSanh == null)
                    return 0;

                return DanhSachSanh.Count(s =>
                    !_datTiecService.KiemTraSanhDaDat(s.MaSanh, SelectedDate.Value, SelectedCaSanh.MaCa)
                );
            }
        }

        public string ThongTinSoLuongSanh
        {
            get
            {
                if (!SelectedDate.HasValue || SelectedCaSanh == null)
                    return $"Tổng số sảnh: {SoLuongSanh}";
                else
                    return $"Số sảnh trống: {SoLuongSanhTrong}";
            }
        }

        // Thuộc tính chỉ hiển thị (readonly) – binding lên TextBlock
        public decimal? DonGiaBanToiThieu => SelectedLoaiSanh?.DonGiaBanToiThieu;

        public ICommand AddSanhCommand { get; set; }

        public ICommand EditSanhCommand { get; set; }

        public ICommand DeleteSanhCommand { get; set; }


        public SanhViewModel(SanhService sanhService, LoaiSanhService loaiSanhService, CaService caService, DatTiecService datTiecService)
        {
            // Database
            _loaiSanhService = loaiSanhService;
            _sanhService = sanhService;
            _caService = caService;
            _datTiecService = datTiecService;

            DanhSachLoaiSanh = new ObservableCollection<LoaiSanh>(_loaiSanhService.GetAllLoaiSanh());
            DanhSachSanh = new ObservableCollection<Sanh>(_sanhService.GetAllSanh());
            DanhSachCaSanh = new ObservableCollection<CASANH>(_caService.LayDanhSachCa("", "", ""));

            DanhSachSanhView = CollectionViewSource.GetDefaultView(DanhSachSanh);
            DanhSachSanhView.Filter = FilterSanh;

            SelectedDate = DateTime.Today;

            var now = DateTime.Now.TimeOfDay;

            // Tìm ca có giờ hiện tại nằm giữa giờ bắt đầu và giờ kết thúc
            SelectedCaSanh = DanhSachCaSanh.FirstOrDefault(ca =>
            {
                var batDau = ca.GioBatDau.TimeOfDay;
                var ketThuc = ca.GioKetThuc.TimeOfDay;
                return now >= batDau && now <= ketThuc;
            });

            // Nếu không có ca phù hợp, chọn ca đầu tiên
            if (SelectedCaSanh == null && DanhSachCaSanh.Any())
                SelectedCaSanh = DanhSachCaSanh.First();

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
        public void RefreshDanhSachSanh()
        {
            // Gọi từ database
            var danhSachMoi = _sanhService.GetAllSanh();

            DanhSachSanh.Clear();
            foreach (var sanh in danhSachMoi)
            {
                DanhSachSanh.Add(sanh);
            }

            // Gọi lại View
            DanhSachSanhView.Refresh();

            // Cập nhật số lượng
            OnPropertyChanged(nameof(SoLuongSanh));
        }

        // Thêm Sảnh mới
        private void AddSanh()
        {
            var window = new AddOrEditSanhWindow(DanhSachLoaiSanh.ToList(), DanhSachSanh.ToList());
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
            if (SelectedSanh == null) return;

            var window = new AddOrEditSanhWindow(SelectedSanh, DanhSachLoaiSanh.ToList());
            if (window.ShowDialog() == true)
            {
                var newSanh = window.SanhInfo;

                // Kiểm tra nếu số lượng bàn tối đa thay đổi
                if (newSanh.SoLuongBanToiDa != SelectedSanh.SoLuongBanToiDa)
                {
                    // Kiểm tra nếu sảnh đang được đặt trong tương lai
                    if (_sanhService.IsSanhDangDuocSuDung(SelectedSanh.MaSanh))
                    {
                        MessageBox.Show(
                            $"Sảnh '{SelectedSanh.TenSanh}' đang có trong phiếu đặt tiệc sắp tới. Không thể thay đổi số lượng bàn tối đa.",
                            "Không thể thay đổi",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning
                        );
                        return;
                    }
                }

                _sanhService.EditSanh(newSanh);
                RefreshDanhSachSanh();
                SelectedSanh = newSanh;
            }
        }

        // Xóa Sảnh
        private void DeleteSanh()
        {
            if (SelectedSanh == null) return;

            // Xác nhận xóa
            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sảnh '{SelectedSanh.TenSanh}' không?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (_sanhService.IsSanhDangDuocSuDung(SelectedSanh.MaSanh))
                    {
                        MessageBox.Show(
                            $"Sảnh '{SelectedSanh.TenSanh}' đang có trong phiếu đặt tiệc sắp tới. Không thể xóa.",
                            "Không thể xóa",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning
                        );
                        return;
                    }

                    // Xóa ảnh nếu tồn tại
                    if (!string.IsNullOrWhiteSpace(SelectedSanh.HinhAnh))
                    {
                        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                        string projectRoot = Directory.GetParent(baseDir).Parent.Parent.Parent.FullName;
                        string imagePath = System.IO.Path.Combine(projectRoot, "Resources", "Images", "Sanh", SelectedSanh.HinhAnh);

                        if (File.Exists(imagePath))
                        {
                            File.Delete(imagePath);
                        }
                    }

                    // Xóa mềm database
                    _sanhService.DeleteSanh(SelectedSanh);

                    // Refresh
                    RefreshDanhSachSanh();
                    SelectedSanh = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa sảnh: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public bool KiemTraSanhDaDat(int maSanh, DateTime ngay, int maCa, out DATTIEC? datTiec)
        {
            datTiec = _datTiecService.LayPhieuDatTiec(maSanh, ngay, maCa);
            return datTiec != null;
        }

        private bool FilterSanh(object obj)
        {
            if (obj is not Sanh sanh) return false;

            bool matchTen = string.IsNullOrWhiteSpace(FilterTenSanh) ||
                            sanh.TenSanh?.IndexOf(FilterTenSanh, StringComparison.OrdinalIgnoreCase) >= 0;

            bool matchLoai = FilterLoaiSanh == null ||
                 sanh.LoaiSanh?.MaLoaiSanh == FilterLoaiSanh.MaLoaiSanh;

            bool matchSoLuong = true;
            if (int.TryParse(FilterSoLuongBanToiDa, out int soLuong))
            {
                matchSoLuong = sanh.SoLuongBanToiDa >= soLuong;
            }

            bool matchGia = true;
            if (decimal.TryParse(FilterDonGiaBanToiThieu, out decimal gia))
            {
                matchGia = sanh.LoaiSanh?.DonGiaBanToiThieu <= gia;
            }

            // Filter theo ngày + ca
            if (SelectedDate.HasValue && SelectedCaSanh != null)
            {
                var isSanhDaDat = _datTiecService
                    .KiemTraSanhDaDat(sanh.MaSanh, SelectedDate.Value, SelectedCaSanh.MaCa);
                
                if (isSanhDaDat)
                    return false;
            }

            return matchTen && matchLoai && matchSoLuong && matchGia;
        }
    }
}
