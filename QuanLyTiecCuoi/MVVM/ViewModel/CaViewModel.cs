using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.View.TuyChinh;
using QuanLyTiecCuoi.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class CaViewModel : BaseViewModel
    {
        private readonly CaService _caService;

        public ObservableCollection<CASANH> DanhSachSanh { get; set; } = new();

        private CASANH _selectedSanh;
        public CASANH SelectedSanh
        {
            get => _selectedSanh;
            set => SetProperty(ref _selectedSanh, value);
        }

        private string _filterTenSanh;
        public string FilterTenSanh
        {
            get => _filterTenSanh;
            set
            {
                if (SetProperty(ref _filterTenSanh, value))
                    LoadCa();
            }
        }

        private string _filterGioBatDau;
        public string FilterGioBatDau
        {
            get => _filterGioBatDau;
            set
            {
                if (SetProperty(ref _filterGioBatDau, value))
                    LoadCa();
            }
        }

        private string _filterGioKetThuc;
        public string FilterGioKetThuc
        {
            get => _filterGioKetThuc;
            set
            {
                if (SetProperty(ref _filterGioKetThuc, value))
                    LoadCa();
            }
        }

        public ICommand AddCaCommand { get; }
        public ICommand UpdateCaCommand { get; }
        public ICommand DeleteCaCommand { get; }

        public CaViewModel(CaService caService)
        {
            _caService = caService;

            AddCaCommand = new RelayCommand<object>(_ => true, _ => AddCa());
            UpdateCaCommand = new RelayCommand<object>(_ => SelectedSanh != null, _ => UpdateCa());
            DeleteCaCommand = new RelayCommand<object>(_ => SelectedSanh != null, _ => DeleteCa());

            LoadCa();
        }

        private void LoadCa()
        {
            var list = _caService.LayDanhSachCa(FilterTenSanh, FilterGioBatDau, FilterGioKetThuc);
            DanhSachSanh.Clear();
            foreach (var ca in list)
                DanhSachSanh.Add(ca);
        }

        private void AddCa()
        {
            var win = new AddOrEditCaWindow();
            if (win.ShowDialog() == true)
            {
                var caMoi = ((AddOrEditCaViewModel)win.DataContext).CaInfo;
                _caService.ThemCa(caMoi);
                LoadCa();
            }
        }

        private void UpdateCa()
        {
            var win = new AddOrEditCaWindow(SelectedSanh); // truyền ca cần sửa
            if (win.ShowDialog() == true)
            {
                var caMoi = ((AddOrEditCaViewModel)win.DataContext).CaInfo;
                _caService.CapNhatCa(caMoi);
                LoadCa();
            }
        }


        private void DeleteCa()
        {
            if (MessageBox.Show("Xác nhận xóa ca?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _caService.XoaCa(SelectedSanh.MaCa);
                LoadCa();
            }
        }
    }
}
