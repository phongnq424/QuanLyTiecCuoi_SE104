using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Services;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using QuanLyTiecCuoi.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class SuaTiecViewModel : BaseViewModel
    {
        private readonly DatTiecService _datTiecService;

        public DATTIEC TiecMoi { get; set; }

        public ObservableCollection<CASANH> DanhSachCa { get; set; } = new();
        public ObservableCollection<SANH> DanhSachSanh { get; set; } = new();

        public ObservableCollection<MONAN> MonAnDaChon { get; set; } = new();
        public ObservableCollection<DICHVU> DichVuDaChon { get; set; } = new();

        public SuaTiecViewModel(DATTIEC tiecCanSua)
        {
            _datTiecService = App.AppHost.Services.GetRequiredService<DatTiecService>();

            // Tạo bản sao dữ liệu tiệc cưới để chỉnh sửa
            TiecMoi = new DATTIEC
            {
                MaDatTiec = tiecCanSua.MaDatTiec,
                TenCoDau = tiecCanSua.TenCoDau,
                TenChuRe = tiecCanSua.TenChuRe,
                SDT = tiecCanSua.SDT,
                TienDatCoc = tiecCanSua.TienDatCoc,
                SoLuongBan = tiecCanSua.SoLuongBan,
                SoBanDuTru = tiecCanSua.SoBanDuTru,
                NgayDaiTiec = tiecCanSua.NgayDaiTiec,
                MaCa = tiecCanSua.MaCa,
                MaSanh = tiecCanSua.MaSanh
                // Thêm các thuộc tính khác nếu có
            };

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
        private Boolean KiemTraSoBanHopLe()
        {
            if (TiecMoi?.MaSanh == 0 || TiecMoi?.SoLuongBan <= 0)
                return false;

            var sanh = DanhSachSanh.FirstOrDefault(s => s.MaSanh == TiecMoi.MaSanh);
            if (sanh != null && (TiecMoi.SoLuongBan > sanh.SoLuongBanToiDa || (TiecMoi.SoLuongBan + TiecMoi.SoBanDuTru) > sanh.SoLuongBanToiDa))
            {
                MessageBox.Show($"Số bàn vượt quá sức chứa của sảnh ({sanh.SoLuongBanToiDa} bàn).", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
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
        public event Action DanhSachChanged;
        public bool CapNhatTiec()
        {
            if (TiecMoi == null) return false;
            if (!KiemTraSoBanHopLe())
                return false;
            try
            {
                _datTiecService.UpdateDatTiec(TiecMoi);
                DanhSachChanged?.Invoke(); 
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật tiệc: " + ex.Message);
                return false;
            }
        }
        public CASANH? CaDuocChon
        {
            get => DanhSachCa.FirstOrDefault(c => c.MaCa == TiecMoi.MaCa);
        }

        public SANH? SanhDuocChon
        {
            get => DanhSachSanh.FirstOrDefault(s => s.MaSanh == TiecMoi.MaSanh);
        }
    }
}
