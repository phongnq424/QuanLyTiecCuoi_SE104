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
    public class SuaTiecViewModel : BaseViewModel
    {
        private readonly DatTiecService _datTiecService;

        public DATTIEC TiecMoi { get; set; }

        public ObservableCollection<CASANH> DanhSachCa { get; set; } = new();
        public ObservableCollection<SANH> DanhSachSanh { get; set; } = new();

        public List<string> MonAnDaChon { get; set; } = new();
        public List<string> DichVuDaChon { get; set; } = new();

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
            // Giả lập danh sách món ăn đã chọn (sau này có thể mở form chọn món ăn)
            MonAnDaChon = new List<string> { "Gỏi cuốn", "Tôm chiên", "Chè sen" };
        }

        public void ChonDichVu()
        {
            // Giả lập danh sách dịch vụ đã chọn
            DichVuDaChon = new List<string> { "Ban nhạc", "Chụp ảnh", "MC" };
        }
        public event Action DanhSachChanged;
        public bool CapNhatTiec()
        {
            if (TiecMoi == null) return false;

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
    }
}
