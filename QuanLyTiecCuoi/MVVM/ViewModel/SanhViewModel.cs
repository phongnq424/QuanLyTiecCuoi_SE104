using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.MVVM.Model;
using QuanLyTiecCuoi.MVVM.View;
using QuanLyTiecCuoi.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class SanhViewModel : BaseViewModel
    {
        private readonly LoaiSanhService _loaiSanhService;
        private readonly SanhService _sanhService;
        public ObservableCollection<LoaiSanh> DanhSachLoaiSanh { get; set; }
        public ObservableCollection<Sanh> DanhSachSanh { get; set; }

        private Sanh _selectedSanh;
        public Sanh SelectedSanh
        {
            get => _selectedSanh;
            set { _selectedSanh = value; OnPropertyChanged(); }
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
        public double? DonGiaBanToiThieu
        {
            get => SelectedLoaiSanh?.DonGiaBanToiThieu;
        }

        public ICommand AddSanhCommand { get; set; }

        public ICommand EditSanhCommand { get; set; }

        public ICommand DeleteSanhCommand { get; set; }


        public SanhViewModel()
        {
            // Database
            _loaiSanhService = new LoaiSanhService();
            DanhSachLoaiSanh = new ObservableCollection<LoaiSanh>(_loaiSanhService.GetAll());

            _sanhService = new SanhService();
            DanhSachSanh = new ObservableCollection<Sanh>(_sanhService.GetAll());

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
        public int SoLuongSanh
        {
            get { return DanhSachSanh.Count; }
        }

        // Thêm Sảnh mới
        private void AddSanh()
        {
            var window = new AddOrEditSanhWindow(DanhSachLoaiSanh.ToList());
            if (window.ShowDialog() == true)
            {
                var newSanh = window.SanhInfo;

                _sanhService.AddSanh(newSanh);

                DanhSachSanh = _sanhService.GetAll();
                OnPropertyChanged(nameof(DanhSachSanh));

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

                DanhSachSanh = _sanhService.GetAll();
                OnPropertyChanged(nameof(DanhSachSanh));

                SelectedSanh = newSanh;
            }
        }

        // Xóa Sảnh
        private void DeleteSanh()
        {
            if (SelectedSanh == null) return;

            _sanhService.DeleteSanh(SelectedSanh);

            DanhSachSanh = _sanhService.GetAll();
            OnPropertyChanged(nameof(DanhSachSanh));

            SelectedSanh = null;
        }

    }
}
