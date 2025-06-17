using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore;

namespace QuanLyTiecCuoi.Repository
{
    public class DatTiecRepository
    {
        private readonly WeddingDbContext _context;

        public DatTiecRepository(WeddingDbContext context)
        {
            _context = context;
        }

        public List<DATTIEC> GetAllDatTiec()
        {
            return _context.DatTiecs
                .Include(dt => dt.CaSanh)
                .Include(dt => dt.Sanh)
                .Where(dt => dt.NgayDaiTiec > DateTime.Now)
                .OrderBy(dt => dt.NgayDaiTiec)
                .ToList();
        }

        public DATTIEC? GetDatTiecById(int id)
        {
            return _context.DatTiecs.FirstOrDefault(d => d.MaDatTiec == id);
        }
        public List<DATTIEC> GetDatTiecByCoDau(string name)
        {
            return _context.DatTiecs.Where(d => d.TenCoDau == name).ToList();
        }
        public List<DATTIEC> GetDatTiecByChuRe(string name)
        {
            return _context.DatTiecs.Where(d => d.TenChuRe == name).ToList();
        }

        public List<DATTIEC> GetDatTiecByPhone(string SDT)
        {
            return _context.DatTiecs.Where(d => d.SDT == SDT).ToList();
        }

        public DATTIEC? GetDATTIEC(int maSanh, DateTime ngay, int maCa)
        {
            return _context.DatTiecs
                .Include(dt => dt.Sanh)
                .Include(dt => dt.CaSanh)
                .FirstOrDefault(dt => dt.MaSanh == maSanh && dt.NgayDaiTiec.Date == ngay.Date && dt.MaCa == maCa);
        }

        public void AddDatTiec(DATTIEC datTiec)
        {
            _context.DatTiecs.Add(datTiec);
            _context.SaveChanges();
        }

        public void UpdateDatTiec(DATTIEC datTiec)
        {
            if (datTiec == null)
                throw new ArgumentNullException(nameof(datTiec));

            var existingEntity = _context.DatTiecs.Find(datTiec.MaDatTiec);
            if (existingEntity == null)
                throw new InvalidOperationException($"Không tìm thấy tiệc với mã {datTiec.MaDatTiec}");

            // Cập nhật các trường cần thiết
            _context.Entry(existingEntity).CurrentValues.SetValues(datTiec);

            _context.SaveChanges();
        }

        public void DeleteDatTiec(int id)
        {
            var datTiec = _context.DatTiecs.FirstOrDefault(d => d.MaDatTiec == id);
            if (datTiec != null)
            {
                _context.DatTiecs.Remove(datTiec);
                _context.SaveChanges();
            }
        }
        public List<CASANH> GetAllCaSanhs()
        {
            return _context.CaSanhs
                .Where(ca => !ca.isDelelte) // Lọc ra các ca chưa bị xóa
                .ToList();
        }
        public List<SANH> GetAllSanhs()
        {
            return _context.Sanhs.ToList();
        }
        public bool KiemTraSanhDaDat(int maSanh, DateTime ngay, int maCa)
        {
            return _context.DatTiecs.Any(dt =>
                dt.MaSanh == maSanh &&
                dt.NgayDaiTiec.Date == ngay.Date &&
                dt.MaCa == maCa);
        }
        public List<SANH> GetSanhsTrong(DateTime ngayDaiTiec, int maCa)
        {
            // Lấy danh sách mã sảnh đã được đặt vào ngày và ca đó
            var sanhsDaDat = _context.DatTiecs
                .Where(dt => dt.NgayDaiTiec.Date == ngayDaiTiec.Date && dt.MaCa == maCa)
                .Select(dt => dt.MaSanh)
                .ToList();

            // Trả về các sảnh KHÔNG nằm trong danh sách đã đặt
            var sanhsTrong = _context.Sanhs
                .Where(s => (!sanhsDaDat.Contains(s.MaSanh) && s.isDelelte == false))
                .ToList();

            return sanhsTrong;
        }

        public LOAISANH GetLoaiSanhById(int maLoaiSanh)
        {
            return _context.LoaiSanhs.FirstOrDefault(ls => ls.MaLoaiSanh == maLoaiSanh);
        }
        public HOADON? GetHoaDonTheoMaDatTiec(int maDatTiec)
        {
            return _context.HoaDons.FirstOrDefault(hd => hd.MaDatTiec == maDatTiec);
        }
        public void AddHoaDon(DATTIEC datTiec)
        {
            var chiTietMenu = _context.ChiTietMenus.Where(ctm => ctm.MaDatTiec == datTiec.MaDatTiec).ToList();
            var chiTietDichVu = _context.ChiTietDVTiecs.Where(ctdv => ctdv.MaDatTiec == datTiec.MaDatTiec).ToList();

            decimal donGiaBan = 0;
            foreach (var ct in chiTietMenu)
            {
                var monAn = _context.MonAns.Where(monAn => monAn.MaMon == ct.MaMon).FirstOrDefault();
                donGiaBan += monAn.DonGia;
            }
            decimal tongTienBan = donGiaBan * datTiec.SoLuongBan;
            decimal tongTienDV = 0;
            foreach (var ctdv in chiTietDichVu)
            {
                var dichVu = _context.DichVus.Where(dichVu => dichVu.MaDichVu == ctdv.MaDichVu).FirstOrDefault();
                tongTienDV += dichVu.DonGia;
            }
            decimal tongTien = tongTienBan + tongTienDV;
            decimal tiLeDC = _context.ThamSos.FirstOrDefault().PhanTramDatCoc;
            datTiec.TienDatCoc = tongTien * tiLeDC; // Giả sử tiền đặt cọc là 30% tổng tiền


            var hoaDon = new HOADON
            {
                MaDatTiec = datTiec.MaDatTiec,
                DonGiaBan = donGiaBan,
                TongTienBan = tongTienBan,
                TongTienDV = tongTienDV,
                TienPhat = 0,
                TongTienHD = tongTienDV + tongTienBan,
                SoLuongBan = datTiec.SoLuongBan,
                TienPhaiThanhToan = tongTienBan + tongTienDV - datTiec.TienDatCoc,
            };
            _context.HoaDons.Add(hoaDon);
            _context.SaveChanges();
        }
        public void UpdateHoaDon(int maDatTiec)
        {
            var datTiec = _context.DatTiecs.FirstOrDefault(d => d.MaDatTiec == maDatTiec);
            if (datTiec == null) return;

            var hoaDon = _context.HoaDons.FirstOrDefault(h => h.MaDatTiec == maDatTiec);
            if (hoaDon == null) return;

            var chiTietMenu = _context.ChiTietMenus.Where(ct => ct.MaDatTiec == maDatTiec).ToList();
            var chiTietDV = _context.ChiTietDVTiecs.Where(ct => ct.MaDatTiec == maDatTiec).ToList();

            decimal donGiaBan = 0;
            foreach (var ct in chiTietMenu)
            {
                var monAn = _context.MonAns.FirstOrDefault(m => m.MaMon == ct.MaMon);
                if (monAn != null)
                    donGiaBan += monAn.DonGia;
            }

            decimal tongTienBan = donGiaBan * datTiec.SoLuongBan;

            decimal tongTienDV = 0;
            foreach (var ct in chiTietDV)
            {
                var dv = _context.DichVus.FirstOrDefault(d => d.MaDichVu == ct.MaDichVu);
                if (dv != null)
                    tongTienDV += dv.DonGia;
            }

            decimal tongTien = tongTienBan + tongTienDV;
            decimal tiLeDC = _context.ThamSos.FirstOrDefault()?.PhanTramDatCoc ?? 0;

            datTiec.TienDatCoc = tongTien * tiLeDC;

            // Cập nhật hóa đơn
            hoaDon.DonGiaBan = donGiaBan;
            hoaDon.TongTienBan = tongTienBan;
            hoaDon.TongTienDV = tongTienDV;
            hoaDon.TongTienHD = tongTien;
            hoaDon.SoLuongBan = datTiec.SoLuongBan;
            hoaDon.TienPhaiThanhToan = tongTien - datTiec.TienDatCoc;

            _context.SaveChanges();
        }

        public SANH? GetSanhById(int maSanh)
        {
            return _context.Sanhs.FirstOrDefault(s => s.MaSanh == maSanh);
        }
    }
}