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

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class LoaiSanhViewModel : BaseViewModel
    {
        private readonly LoaiSanhService _service;
        public ObservableCollection<LoaiSanh> DanhSachLoaiSanh { get; set; }

        private LoaiSanh _selectedLoaiSanh;
        public LoaiSanh SelectedLoaiSanh
        {
            get => _selectedLoaiSanh;
            set { _selectedLoaiSanh = value; OnPropertyChanged(); }
        }

        public ICommand AddLoaiSanhCommand { get; }
        public ICommand EditLoaiSanhCommand { get; }
        public ICommand DeleteLoaiSanhCommand { get; }

        public LoaiSanhViewModel()
        {
            // Database
            _service = new LoaiSanhService();
            DanhSachLoaiSanh = new ObservableCollection<LoaiSanh>(_service.GetAll());

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

        // Thêm mới Loại
        private void AddLoaiSanh()
        {
            var window = new AddOrEditLoaiSanhWindow();
            if (window.ShowDialog() == true)
            {
                var newLoai = window.LoaiSanhInfo;
                newLoai.MaLoaiSanh = DanhSachLoaiSanh.Any() ? DanhSachLoaiSanh.Max(x => x.MaLoaiSanh) + 1 : 1;

                _service.Add(newLoai);

                DanhSachLoaiSanh = _service.GetAll();
                OnPropertyChanged(nameof(DanhSachLoaiSanh));

                SelectedLoaiSanh = newLoai;
            }
        }

        // Chỉnh sửa Loại Sảnh
        private void EditLoaiSanh()
        {
            if (SelectedLoaiSanh == null) return;

            var window = new AddOrEditLoaiSanhWindow(SelectedLoaiSanh);
            if (window.ShowDialog() == true)
            {
                var editedLoai = window.LoaiSanhInfo;

                // Cập nhật dữ liệu
                SelectedLoaiSanh.TenLoaiSanh = editedLoai.TenLoaiSanh;
                SelectedLoaiSanh.DonGiaBanToiThieu = editedLoai.DonGiaBanToiThieu;

                _service.Edit(SelectedLoaiSanh);

                OnPropertyChanged(nameof(SelectedLoaiSanh));
            }
        }

        // Xóa Loại Sảnh
        private void DeleteLoaiSanh()
        {
            if (SelectedLoaiSanh == null) return;

            _service.Delete(SelectedLoaiSanh);

            // Cập nhật lại danh sách loại sảnh
            DanhSachLoaiSanh = _service.GetAll();
            OnPropertyChanged(nameof(DanhSachLoaiSanh));

            SelectedLoaiSanh = null;
        }
    }
}
