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
        public ObservableCollection<decimal> TienDatCoc { get; set; } = new ObservableCollection<decimal>();

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
        private DateTime _ngayDaiTiec = DateTime.Today;
        public DateTime NgayDaiTiec
        {
            get => _ngayDaiTiec;
            set
            {
                if (_ngayDaiTiec != value)
                {
                    _ngayDaiTiec = value;
                    TiecMoi.NgayDaiTiec = value;
                    OnPropertyChanged();
                    LoadDanhSachSanh(); // Gọi lại khi thay đổi
                }
            }
        }

        private int _maCa;
        public int MaCa
        {
            get => _maCa;
            set
            {
                if (_maCa != value)
                {
                    _maCa = value;
                    TiecMoi.MaCa = value;
                    OnPropertyChanged();
                    LoadDanhSachSanh(); // Gọi lại khi thay đổi
                }
            }
        }
        public void LoadDanhSachSanh()
        {
            try
            {
                var danhSachSanh = _datTiecService.GetSanhsTrong(TiecMoi.NgayDaiTiec, TiecMoi.MaCa);
                Console.WriteLine($"Số lượng sảnh: {danhSachSanh.Count()}"); // thêm dòng này

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
                decimal tongTienBan = MonAnDaChon.Sum(mon => mon.DonGia) * TiecMoi.SoLuongBan;
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
                    SoLuong = 1,
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
                    SoLuong = 1, 
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
                if (_datTiecService.KiemTraSanhDaDat(TiecMoi.MaSanh, TiecMoi.NgayDaiTiec, TiecMoi.MaCa))
                {
                    MessageBox.Show("Sảnh đã được đặt vào ngày và ca này. Vui lòng chọn sảnh khác.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
                _datTiecService.AddDatTiec(TiecMoi);
                // lưu chi tiết 
                LuuChiTietMenu();
                LuuChiTietDichVu();
                _datTiecService.AddHoaDon(TiecMoi);
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
