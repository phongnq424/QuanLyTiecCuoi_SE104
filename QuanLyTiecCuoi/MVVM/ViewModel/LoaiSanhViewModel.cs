using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.MVVM.Model;
using QuanLyTiecCuoi.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuanLyTiecCuoi.MVVM.View;
using System.ComponentModel;
using System.Windows.Data;

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class LoaiSanhViewModel : BaseViewModel
    {
        private readonly LoaiSanhService _loaiSanhService;
        public ObservableCollection<LoaiSanh> DanhSachLoaiSanh { get; set; }

        private LoaiSanh _selectedLoaiSanh;
        public LoaiSanh SelectedLoaiSanh
        {
            get => _selectedLoaiSanh;
            set { _selectedLoaiSanh = value; OnPropertyChanged(); }
        }

        private string _filterTenLoaiSanh;
        public string FilterTenLoaiSanh
        {
            get => _filterTenLoaiSanh;
            set
            {
                _filterTenLoaiSanh = value;
                OnPropertyChanged();
                DanhSachLoaiSanhView.Refresh();
            }
        }

        private string _filterDonGiaBanToiThieu;
        public string FilterDonGiaBanToiThieu
        {
            get => _filterDonGiaBanToiThieu;
            set
            {
                _filterDonGiaBanToiThieu = value;
                OnPropertyChanged();
                DanhSachLoaiSanhView.Refresh();
            }
        }

        public ICollectionView DanhSachLoaiSanhView { get; set; }
        public ICommand AddLoaiSanhCommand { get; }
        public ICommand EditLoaiSanhCommand { get; }
        public ICommand DeleteLoaiSanhCommand { get; }

        public LoaiSanhViewModel(LoaiSanhService loaiSanhService)
        {
            // Database
            _loaiSanhService = loaiSanhService;

            DanhSachLoaiSanh = new ObservableCollection<LoaiSanh>(_loaiSanhService.GetAllLoaiSanh());

            DanhSachLoaiSanhView = CollectionViewSource.GetDefaultView(DanhSachLoaiSanh);
            DanhSachLoaiSanhView.Filter = FilterLoaiSanh;

            // Khởi tạo lệnh 
            AddLoaiSanhCommand = new RelayCommand<object>(
                canExecute: _ => true,
                execute: _ => AddLoaiSanh()
            );

            EditLoaiSanhCommand = new RelayCommand<object>(
                canExecute: _ => SelectedLoaiSanh != null,
                execute: _ => EditLoaiSanh()
            );

            DeleteLoaiSanhCommand = new RelayCommand<object>(
                canExecute: _ => SelectedLoaiSanh != null,
                execute: _ => DeleteLoaiSanh()
            );
        }

        // Đếm số Loại Sảnh
        public int SoLuongLoaiSanh => DanhSachLoaiSanh.Count;

        // Refresh Loại Sảnh
        private void RefreshDanhSachLoaiSanh()
        {
            DanhSachLoaiSanh.Clear();
            foreach (var loaiSanh in _loaiSanhService.GetAllLoaiSanh())
                DanhSachLoaiSanh.Add(loaiSanh);

            OnPropertyChanged(nameof(SoLuongLoaiSanh));
        }

        // Thêm mới Loại
        private void AddLoaiSanh()
        {
            var window = new AddOrEditLoaiSanhWindow();
            if (window.ShowDialog() == true)
            {
                var newLoaiSanh = window.LoaiSanhInfo;
                _loaiSanhService.AddLoaiSanh(newLoaiSanh);
                RefreshDanhSachLoaiSanh();
                SelectedLoaiSanh = newLoaiSanh;
            }
        }

        // Chỉnh sửa Loại Sảnh
        private void EditLoaiSanh()
        {
            var window = new AddOrEditLoaiSanhWindow(SelectedLoaiSanh);
            if (window.ShowDialog() == true)
            {
                var newLoaiSanh = window.LoaiSanhInfo;
                _loaiSanhService.EditLoaiSanh(newLoaiSanh);
                RefreshDanhSachLoaiSanh();
                SelectedLoaiSanh = newLoaiSanh;
            }
        }

        // Xóa Loại Sảnh
        private void DeleteLoaiSanh()
        {
            if (SelectedLoaiSanh == null) return;
            _loaiSanhService.DeleteLoaiSanh(SelectedLoaiSanh);
            RefreshDanhSachLoaiSanh();
            SelectedLoaiSanh = null;
        }

        private bool FilterLoaiSanh(object obj)
        {
            if (obj is LoaiSanh loaiSanh)
            {
                bool matchTen = string.IsNullOrWhiteSpace(FilterTenLoaiSanh) ||
                    (loaiSanh.TenLoaiSanh?.IndexOf(FilterTenLoaiSanh, StringComparison.OrdinalIgnoreCase) >= 0);

                bool matchGia = true;
                if (!string.IsNullOrWhiteSpace(FilterDonGiaBanToiThieu))
                {
                    if (decimal.TryParse(FilterDonGiaBanToiThieu, out decimal giaFilter))
                    {
                        matchGia = loaiSanh.DonGiaBanToiThieu.HasValue && loaiSanh.DonGiaBanToiThieu.Value <= giaFilter;
                    }
                    else
                    {
                        matchGia = loaiSanh.DonGiaBanToiThieu.HasValue &&
                          loaiSanh.DonGiaBanToiThieu.Value.ToString().Contains(FilterDonGiaBanToiThieu);
                    }
                }

                return matchTen && matchGia;
            }
            return false;
        }
    }
}
