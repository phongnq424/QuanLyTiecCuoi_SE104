using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.Model;
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
    public class ThemTiecViewModel : BaseViewModel
    {
        private readonly DatTiecService _datTiecService;

        public DATTIEC TiecMoi { get; set; } = new DATTIEC() {
            NgayDaiTiec = DateTime.Today
        };


        public ObservableCollection<CASANH> DanhSachCa { get; set; } = new();
        public ObservableCollection<SANH> DanhSachSanh { get; set; } = new();

        public ObservableCollection<MONAN> MonAnDaChon { get; set; } = new();
        public ObservableCollection<DICHVU> DichVuDaChon { get; set; } = new();

        public ThemTiecViewModel()
        {
            _datTiecService = App.AppHost.Services.GetRequiredService<DatTiecService>();
            _chiTietMenuService = App.AppHost.Services.GetRequiredService<ChiTietMenuService>();
            _chiTietDichVuService = App.AppHost.Services.GetRequiredService<ChiTietDichVuService>();

            LoadDanhSachCa();
            LoadDanhSachSanh();
        }

        public ThemTiecViewModel(Sanh sanh, DateTime ngay, int maCa)
        {
            _datTiecService = App.AppHost.Services.GetRequiredService<DatTiecService>();
            _chiTietMenuService = App.AppHost.Services.GetRequiredService<ChiTietMenuService>();
            _chiTietDichVuService = App.AppHost.Services.GetRequiredService<ChiTietDichVuService>();


            TiecMoi = new DATTIEC
            {
                MaSanh = sanh.MaSanh,
                NgayDaiTiec = ngay,
                MaCa = maCa
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
        private bool KiemTraNgayDai()
        {
            if (TiecMoi?.MaSanh == 0 || TiecMoi.NgayDaiTiec == null)
                return false;

            var soNgayConLai = (TiecMoi.NgayDaiTiec.Date - DateTime.Today).TotalDays;

            if (soNgayConLai < 7)
            {
                MessageBox.Show("Ngày đãi tiệc phải cách ngày hôm nay ít nhất 7 ngày.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private bool KiemTraSDT()
        {
            string sdt = TiecMoi?.SDT.ToString();
            if (string.IsNullOrWhiteSpace(sdt)) return false;
            if (sdt.Length != 10 || !sdt.All(char.IsDigit) || sdt.First() != '0')
            {
                MessageBox.Show("Số điện thoại chưa đúng định dạng.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private readonly DATTIEC _datTiec;
        private readonly ChiTietMenuService _chiTietMenuService;
        private readonly ChiTietDichVuService _chiTietDichVuService;

        public void LuuChiTietMenu()
        {
            if (TiecMoi == null || TiecMoi.MaDatTiec == 0)
                return;

            foreach (var monAn in MonAnDaChon)
            {
                var chiTiet = new CHITIETMENU
                {
                    MaDatTiec = TiecMoi.MaDatTiec,
                    MaMon = monAn.MaMon,
                    SoLuong = (TiecMoi.SoLuongBan + TiecMoi.SoBanDuTru),
                    GhiChu = ""
                };

                _chiTietMenuService.ThemChiTietMenu(chiTiet);
            }
        }
        public void LuuChiTietDichVu()
        {
            if (TiecMoi == null || TiecMoi.MaDatTiec == 0)
                return;

            foreach (var dv in DichVuDaChon)
            {
                var chiTietDV = new CHITIETDVTIEC
                {
                    MaDatTiec = TiecMoi.MaDatTiec,
                    MaDichVu = dv.MaDichVu,
                    SoLuong = 1, // Có thể cho phép chọn số lượng tùy ý
                    DonGia = dv.DonGia,
                };

                _chiTietDichVuService.ThemChiTiet(chiTietDV);
            }
        }

        public event Action DanhSachChanged;
        public bool ThemTiecMoi()
        {
            if (TiecMoi == null) return false;

            try
            {
                if (!KiemTraSoBanHopLe() || !KiemTraNgayDai() || !KiemTraSDT())
                    return false;
                _datTiecService.AddDatTiec(TiecMoi);
                // lưu chi tiết 
                LuuChiTietMenu();
                LuuChiTietDichVu();
                DanhSachChanged?.Invoke();

                decimal tongTienMenu = 0;
                foreach (var mon in MonAnDaChon)
                {
                    tongTienMenu += mon.DonGia;
                }
                decimal tongTienBan = tongTienMenu * TiecMoi.SoLuongBan;
                decimal tongTienDV = 0;
                decimal tienPhat = 0;
                decimal tienDatCoc = TiecMoi.TienDatCoc;
                foreach (var dv in DichVuDaChon)
                {
                    tongTienDV += dv.DonGia;
                }

                var hoaDon = new HOADON
                {
                    MaDatTiec = TiecMoi.MaDatTiec,
                    DonGiaBan = tongTienMenu,
                    TongTienBan = tongTienBan,
                    TongTienDV = tongTienDV,
                    TienPhat = 0,
                    SoLuongBan = TiecMoi.SoLuongBan,
                    TongTienHD = tongTienDV + tongTienBan,
                    TienPhaiThanhToan = tongTienBan + tongTienDV + tienPhat - tienDatCoc,
                };
                // Gọi hàm lưu hóa đơn
                _datTiecService.AddHoaDon(hoaDon); // Cần service này
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
