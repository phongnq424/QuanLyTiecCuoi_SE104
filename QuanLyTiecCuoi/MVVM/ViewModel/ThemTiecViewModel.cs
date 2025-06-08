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

        public List<string> MonAnDaChon { get; set; } = new();
        public List<string> DichVuDaChon { get; set; } = new();

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

        public void ChonMonAn()
        {
            MonAnDaChon = new List<string> { "Gà quay", "Lẩu hải sản", "Bò sốt tiêu" };
        }

        public void ChonDichVu()
        {
            DichVuDaChon = new List<string> { "Ca sĩ", "Xe hoa", "MC chuyên nghiệp" };
        }

        public bool ThemTiecMoi()
        {
            if (TiecMoi == null) return false;

            try
            {
                _datTiecService.AddDatTiec(TiecMoi);
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
