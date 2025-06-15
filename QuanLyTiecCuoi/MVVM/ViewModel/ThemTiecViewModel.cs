using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Services;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using QuanLyTiecCuoi.Core;
using Microsoft.Extensions.DependencyInjection;

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class ThemTiecViewModel : BaseViewModel
    {
        private readonly DatTiecService _datTiecService;

        public DATTIEC TiecMoi { get; set; } = new DATTIEC();

        public ObservableCollection<CASANH> DanhSachCa { get; set; } = new();
        public ObservableCollection<SANH> DanhSachSanh { get; set; } = new();

        public ObservableCollection<MONAN> MonAnDaChon { get; set; } = new();
        public ObservableCollection<DICHVU> DichVuDaChon { get; set; } = new();

        public ThemTiecViewModel()
        {
            _datTiecService = App.AppHost.Services.GetRequiredService<DatTiecService>();

            LoadDanhSachCa();
            LoadDanhSachSanh();
        }

        public void LoadDanhSachCa()
        {
            try
            {
                var danhSachCa = _datTiecService.GetAllCaSanhs();
                DanhSachCa.Clear();
                foreach (var ca in danhSachCa)
                    DanhSachCa.Add(ca);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi LoadDanhSachCa: " + ex.Message);
            }
        }

        public void LoadDanhSachSanh()
        {
            try
            {
                var danhSachSanh = _datTiecService.GetAllSanhs();
                DanhSachSanh.Clear();
                foreach (var sanh in danhSachSanh)
                    DanhSachSanh.Add(sanh);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi LoadDanhSachSanh: " + ex.Message);
            }
        }

        public void ChonMonAn(IEnumerable<MONAN> danhSachMonAn)
        {
            MonAnDaChon.Clear();
            foreach (var mon in danhSachMonAn)
            {
                MonAnDaChon.Add(mon);
            }
            OnPropertyChanged(nameof(MonAnDaChon));
        }

        public void ChonDichVu(IEnumerable<DICHVU> danhSachDichVu)
        {
            DichVuDaChon.Clear();
            foreach (var dv in danhSachDichVu)
            {
                DichVuDaChon.Add(dv);
            }
            OnPropertyChanged(nameof(DichVuDaChon)); // thêm dòng này
        }
        public event Action DanhSachChanged;
        public bool ThemTiecMoi()
        {
            if (TiecMoi == null) return false;

            try
            {
                _datTiecService.AddDatTiec(TiecMoi);
                DanhSachChanged?.Invoke();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm tiệc mới: " + ex.Message);
                return false;
            }
        }
    }
}
