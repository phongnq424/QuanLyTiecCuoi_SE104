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
        private readonly ChiTietMenuService _chiTietService;
        private readonly ChiTietDichVuService _chiTietServiceDV;
        public DATTIEC TiecMoi { get; set; }

        public ObservableCollection<CASANH> DanhSachCa { get; set; } = new();
        public ObservableCollection<SANH> DanhSachSanh { get; set; } = new();

        public ObservableCollection<MONAN> MonAnDaChon { get; set; } = new();
        public ObservableCollection<DICHVU> DichVuDaChon { get; set; } = new();

        public SuaTiecViewModel(DATTIEC tiecCanSua)
        {
            _datTiecService = App.AppHost.Services.GetRequiredService<DatTiecService>();
            _chiTietService = App.AppHost.Services.GetRequiredService<ChiTietMenuService>();
            _chiTietServiceDV = App.AppHost.Services.GetRequiredService<ChiTietDichVuService>();
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
            LoadDanhSachMonAn();
            LoadDanhSachDichVu();
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
                var danhSachSanh = _datTiecService.GetSanhsTrong(TiecMoi.NgayDaiTiec, TiecMoi.MaCa).ToList();

                // Thêm sảnh đã được chọn (nếu không có trong danh sách sảnh trống)
                var sanhCu = _datTiecService.GetSanhById(TiecMoi.MaSanh);
                if (sanhCu != null && !danhSachSanh.Any(s => s.MaSanh == sanhCu.MaSanh))
                {
                    danhSachSanh.Add(sanhCu);
                }

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
            var loaiSanh = _datTiecService.GetLoaiSanhById(sanh.MaLoaiSanh);
            if (loaiSanh != null)
            {
                decimal tongTienBan = MonAnDaChon.Sum(mon => mon.DonGia);
                if (tongTienBan < loaiSanh.DonGiaBanToiThieu)
                {
                    MessageBox.Show($"Tổng tiền bàn ({tongTienBan:N0}đ) phải >= đơn giá tối thiểu ({loaiSanh.DonGiaBanToiThieu:N0} đ).", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
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

        public void LoadDanhSachMonAn()
        {
            try
            {
                var chiTietMonAn = _chiTietService.LayDanhSachMonAnTheoDatTiec(TiecMoi.MaDatTiec);
                MonAnDaChon.Clear();
                foreach (var mon in chiTietMonAn)
                    MonAnDaChon.Add(mon);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi LoadDanhSachMonAn: " + ex.Message);
            }
        }
        public void LoadDanhSachDichVu()
        {
            try
            {
                var danhSach = _chiTietServiceDV.LayDanhSachDichVuTheoDatTiec(TiecMoi.MaDatTiec);
                DichVuDaChon.Clear();
                foreach (var dv in danhSach)
                    DichVuDaChon.Add(dv);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi LoadDanhSachDichVu: " + ex.Message);
            }
        }


        public event Action DanhSachChanged;
        private readonly HoaDonService _hoaDonService;

        public void LuuChiTietMenu()
        {
            if (TiecMoi == null || TiecMoi.MaDatTiec == 0)
                return;

            if (MonAnDaChon == null || MonAnDaChon.Count == 0)
                return;

            if (_chiTietService == null)
            {
                Console.WriteLine("ChiTietMenuService chưa được khởi tạo.");
                return;
            }

            try
            {
                var danhSachMoi = MonAnDaChon
                    .Where(mon => mon != null)
                    .Select(monAn => new CHITIETMENU
                    {
                        MaDatTiec = TiecMoi.MaDatTiec,
                        MaMon = monAn.MaMon,
                        SoLuong = TiecMoi.SoLuongBan + TiecMoi.SoBanDuTru,
                        GhiChu = ""
                    }).ToList();

                _chiTietService.CapNhatChiTietMenu(TiecMoi.MaDatTiec, danhSachMoi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lưu ChiTietMenu: " + ex.Message);
            }
        }

        public void LuuChiTietDichVu()
        {
            if (TiecMoi == null || TiecMoi.MaDatTiec == 0)
                return;

            if (DichVuDaChon == null || DichVuDaChon.Count == 0)
                return;

            if (_chiTietServiceDV == null)
            {
                Console.WriteLine("ChiTietDichVuService chưa được khởi tạo.");
                return;
            }

            try
            {
                var danhSachMoi = DichVuDaChon
                    .Where(dv => dv != null)
                    .Select(dv => new CHITIETDVTIEC
                    {
                        MaDatTiec = TiecMoi.MaDatTiec,
                        MaDichVu = dv.MaDichVu,
                        SoLuong = 1, // hoặc cho người dùng chỉnh sửa nếu cần
                        DonGia = dv.DonGia
                    }).ToList();

                _chiTietServiceDV.CapNhatChiTietDichVu(TiecMoi.MaDatTiec, danhSachMoi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lưu ChiTietDichVu: " + ex.Message);
            }
        }




        public bool CapNhatTiec()
        {
            if (TiecMoi == null) return false;
            if (!KiemTraSoBanHopLe() || !KiemTraNgayDai() || !KiemTraSDT())
                return false;
            // Lấy các tiệc khác trùng sảnh - ca - ngày
            var tiecTrung = _datTiecService
                .GetAllDatTiec() // bạn cần có 1 hàm trả về toàn bộ danh sách DATTIEC
                .FirstOrDefault(dt =>
                    dt.MaSanh == TiecMoi.MaSanh &&
                    dt.NgayDaiTiec.Date == TiecMoi.NgayDaiTiec.Date &&
                    dt.MaCa == TiecMoi.MaCa &&
                    dt.MaDatTiec != TiecMoi.MaDatTiec); // loại trừ chính tiệc hiện tại

            if (tiecTrung != null)
            {
                MessageBox.Show("Sảnh đã được đặt vào ngày và ca này. Vui lòng chọn sảnh khác.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            try
            {
                _datTiecService.UpdateDatTiec(TiecMoi);
                DanhSachChanged?.Invoke();
                LuuChiTietMenu();
                LuuChiTietDichVu();
                _datTiecService.UpdateHoaDon(TiecMoi);
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
            get => DanhSachSanh.FirstOrDefault(c => c.MaSanh == TiecMoi.MaSanh);
        }
    }
}